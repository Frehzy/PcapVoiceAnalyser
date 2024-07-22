namespace VoiceDecoder.Models;

public class OptionsModel
{
    public string Host { get; set; } = "localhost";

    public int Port { get; set; } = 5432;

    public string Database { get; set; } = "voicesDb";

    public string Username { get; set; } = "postgres";

    public string Password { get; set; } = string.Empty;

    public int Rate { get; set; } = 8000;
}