using Microsoft.EntityFrameworkCore;
using NAudio.Wave;
using System.ComponentModel;
using VoiceDecoder.Entities;
using VoiceDecoder.Entities.Intermediate;
using VoiceDecoder.Models;
using VoiceDecoder.Services;

namespace VoiceDecoder;

public partial class Form1 : Form, INotifyPropertyChanged
{
    private readonly List<Channel> _channels = [];
    private WaveOut? _waveOut;

    private string _selectedFilePath = string.Empty;
    private bool _canSave = false;

    public string SelectedFilePath
    {
        get => _selectedFilePath;
        set
        {
            _selectedFilePath = value;
            OnPropertyChanged(nameof(SelectedFilePath));
        }
    }

    public bool CanSave
    {
        get => _canSave;
        set
        {
            _canSave = value;
            OnPropertyChanged(nameof(CanSave));
        }
    }

    public BindingList<VoiceTrack> Voices { get; } = [];

    public bool CanAnalyse => !string.IsNullOrEmpty(SelectedFilePath);

    public event PropertyChangedEventHandler? PropertyChanged;

    public Form1()
    {
        InitializeComponent();

        PcapPathTxb.DataBindings.Add(new Binding(nameof(TextBox.Text), this, nameof(SelectedFilePath), true, DataSourceUpdateMode.OnPropertyChanged));

        AnalyseButton.DataBindings.Add(new Binding(nameof(Button.Enabled), this, nameof(CanAnalyse), true, DataSourceUpdateMode.OnPropertyChanged));

        Voices.ListChanged += (_, _) => CanSave = Voices.Count > 0;
        SaveToDatabaseButton.DataBindings.Add(new Binding(nameof(Button.Enabled), this, nameof(CanSave), true, DataSourceUpdateMode.OnPropertyChanged));

        VoicesDgv.DataSource = Voices;
        var playColumn = new DataGridViewButtonColumn
        {
            HeaderText = "Воспроизвести",
            Text = "*тык*",
            UseColumnTextForButtonValue = true,
            Name = "PlayButton",
            DataPropertyName = nameof(VoiceTrack.Id)
        };
        VoicesDgv.Columns.Add(playColumn);
    }

    private void OpenFileButton_Click(object sender, EventArgs e)
    {
        using var ofd = new OpenFileDialog()
        {
            CheckFileExists = true,
            CheckPathExists = true,
            Title = "Выберите PCAP файл",
            Filter = "PCAP files (*.pcap)|*.pcap",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            Multiselect = false
        };
        if (ofd.ShowDialog() == DialogResult.OK)
            SelectedFilePath = ofd.FileName;
    }

    private void AnalyseButton_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(SelectedFilePath))
            return;

        var channels = PacketService.Analyse(SelectedFilePath);

        var voicesPath = Path.Combine(Environment.CurrentDirectory, DateTime.Now.ToFileTime().ToString());
        if (Directory.Exists(voicesPath) is false)
            Directory.CreateDirectory(voicesPath);

        foreach (var item in channels)
        {
            var voice = item.GetDecodeVoice();
            var fileName = item.CallId.ToString().Replace("-", "");
            var filePath = Path.Combine(voicesPath, $"{fileName}.wav");
            var voiceTrack = new VoiceTrack(item.CallId, filePath);
            Voices.Add(voiceTrack);

            using var waveWriter = new WaveFileWriter(filePath, new WaveFormat(8000, 16, 1));
            waveWriter.Write(voice, 0, voice.Length);
            voiceTrack.Duration = waveWriter.TotalTime;
        }

        _channels.Clear();
        _channels.AddRange(channels);
    }

    private void VoicesDgv_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.ColumnIndex == VoicesDgv.Columns["PlayButton"].Index)
        {
            DataGridViewRow row = VoicesDgv.Rows[e.RowIndex];
            var trackId = Guid.Parse(row.Cells[nameof(VoiceTrack.Id)].Value.ToString());
            var voiceTrack = Voices.First(x => x.Id.Equals(trackId));

            if (_waveOut?.PlaybackState == PlaybackState.Playing)
                StopAudio();
            else
                PlayAudio(voiceTrack.FilePath);
        }

        void PlayAudio(string filePath)
        {
            if (_waveOut is not null)
            {
                _waveOut.Stop();
                _waveOut.Dispose();
            }

            _waveOut = new WaveOut();
            var reader = new WaveFileReader(filePath);
            _waveOut.Init(reader);
            _waveOut.Play();

            _waveOut.PlaybackStopped += (sender, args) =>
            {
                _waveOut.Dispose();
                reader.Dispose();
            };
        }

        void StopAudio()
        {
            if (_waveOut is not null)
            {
                _waveOut.Stop();
                _waveOut.Dispose();
                _waveOut = default;
            }
        }
    }

    private void SaveToDatabaseButton_Click(object sender, EventArgs e)
    {
        var connectionString = "Host=localhost;Port=5432;Database=voicesDb;Username=postgres;Password=96877439;";
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        optionsBuilder.UseNpgsql(connectionString);
        var context = new ApplicationContext(optionsBuilder.Options);

        var contacts = new Dictionary<string, ContactEntity>();
        var contactVoices = new List<ContactVoiceEntity>();
        foreach (var item in Voices)
        {
            var channel = _channels.First(x => x.CallId.Equals(item.Id));

            var voiceEntity = new VoiceEntity()
            {
                CallId = item.Id,
                VoicePath = item.FilePath
            };
            context.Voices.Add(voiceEntity);

            foreach (var sip in channel.Sip)
            {
                GetOrAdd(sip.From, out var contactEntityFrom);
                GetOrAdd(sip.To, out var contactEntityTo);

                contactVoices.Add(new ContactVoiceEntity
                {
                    Contact = contactEntityFrom,
                    ContactId = contactEntityFrom.Identificator,
                    Voice = voiceEntity,
                    VoiceId = voiceEntity.Identificator
                });
                contactVoices.Add(new ContactVoiceEntity
                {
                    Contact = contactEntityTo,
                    ContactId = contactEntityTo.Identificator,
                    Voice = voiceEntity,
                    VoiceId = voiceEntity.Identificator
                });

                void GetOrAdd(Contact contact, out ContactEntity existContact)
                {
                    existContact = contacts.Values.FirstOrDefault(x => x.Phone.Equals(contact.PhoneNumber));
                    if (existContact is null)
                    {
                        var newContact = new ContactEntity()
                        {
                            Ip = contact.Ip,
                            Phone = contact.PhoneNumber
                        };
                        contacts.Add(newContact.Phone, newContact);
                        existContact = newContact;
                    }
                }
            }
        }

        context.Contacts.AddRange(contacts.Values);
        context.ContactVoiceEntities.AddRange(contactVoices);

        context.SaveChanges();
    }

    private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}