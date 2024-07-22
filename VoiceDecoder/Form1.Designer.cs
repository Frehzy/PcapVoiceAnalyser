namespace VoiceDecoder
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            PcapPathTxb = new TextBox();
            OpenFileButton = new Button();
            tableLayoutPanel3 = new TableLayoutPanel();
            VoicesDgv = new DataGridView();
            tableLayoutPanel4 = new TableLayoutPanel();
            AnalyseButton = new Button();
            SaveToDatabaseButton = new Button();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)VoicesDgv).BeginInit();
            tableLayoutPanel4.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(800, 450);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.Controls.Add(PcapPathTxb, 0, 0);
            tableLayoutPanel2.Controls.Add(OpenFileButton, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Margin = new Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(800, 29);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // PcapPathTxb
            // 
            PcapPathTxb.Dock = DockStyle.Fill;
            PcapPathTxb.Location = new Point(3, 3);
            PcapPathTxb.Name = "PcapPathTxb";
            PcapPathTxb.ReadOnly = true;
            PcapPathTxb.Size = new Size(634, 23);
            PcapPathTxb.TabIndex = 0;
            // 
            // OpenFileButton
            // 
            OpenFileButton.Dock = DockStyle.Fill;
            OpenFileButton.Location = new Point(643, 3);
            OpenFileButton.Name = "OpenFileButton";
            OpenFileButton.Size = new Size(154, 23);
            OpenFileButton.TabIndex = 1;
            OpenFileButton.Text = "Выбрать PCAP файл";
            OpenFileButton.UseVisualStyleBackColor = true;
            OpenFileButton.Click += OpenFileButton_Click;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(VoicesDgv, 0, 0);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel4, 1, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 32);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(794, 415);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // VoicesDgv
            // 
            VoicesDgv.AllowUserToAddRows = false;
            VoicesDgv.AllowUserToDeleteRows = false;
            VoicesDgv.AllowUserToResizeRows = false;
            VoicesDgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            VoicesDgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            VoicesDgv.Dock = DockStyle.Fill;
            VoicesDgv.Location = new Point(3, 3);
            VoicesDgv.MultiSelect = false;
            VoicesDgv.Name = "VoicesDgv";
            VoicesDgv.ReadOnly = true;
            VoicesDgv.RowHeadersVisible = false;
            VoicesDgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            VoicesDgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            VoicesDgv.Size = new Size(391, 409);
            VoicesDgv.TabIndex = 1;
            VoicesDgv.CellClick += VoicesDgv_CellClick;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Controls.Add(AnalyseButton, 0, 0);
            tableLayoutPanel4.Controls.Add(SaveToDatabaseButton, 0, 1);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(400, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 3;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Size = new Size(391, 409);
            tableLayoutPanel4.TabIndex = 2;
            // 
            // AnalyseButton
            // 
            AnalyseButton.Dock = DockStyle.Fill;
            AnalyseButton.Location = new Point(3, 3);
            AnalyseButton.Name = "AnalyseButton";
            AnalyseButton.Size = new Size(385, 94);
            AnalyseButton.TabIndex = 1;
            AnalyseButton.Text = "Анализировать";
            AnalyseButton.UseVisualStyleBackColor = true;
            AnalyseButton.Click += AnalyseButton_Click;
            // 
            // SaveToDatabaseButton
            // 
            SaveToDatabaseButton.Dock = DockStyle.Fill;
            SaveToDatabaseButton.Location = new Point(3, 103);
            SaveToDatabaseButton.Name = "SaveToDatabaseButton";
            SaveToDatabaseButton.Size = new Size(385, 94);
            SaveToDatabaseButton.TabIndex = 2;
            SaveToDatabaseButton.Text = "Сохранить в базу данных";
            SaveToDatabaseButton.UseVisualStyleBackColor = true;
            SaveToDatabaseButton.Click += SaveToDatabaseButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "Form1";
            Text = "Form1";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)VoicesDgv).EndInit();
            tableLayoutPanel4.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TextBox PcapPathTxb;
        private Button OpenFileButton;
        private TableLayoutPanel tableLayoutPanel3;
        private DataGridView VoicesDgv;
        private TableLayoutPanel tableLayoutPanel4;
        private Button AnalyseButton;
        private Button SaveToDatabaseButton;
    }
}
