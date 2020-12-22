namespace MetaFolderMaker
{
    partial class MainForm
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
            this.tbSource = new System.Windows.Forms.TextBox();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.gbRadioButtons = new System.Windows.Forms.GroupBox();
            this.rbMove = new System.Windows.Forms.RadioButton();
            this.rbCopy = new System.Windows.Forms.RadioButton();
            this.btnSourceBrowse = new System.Windows.Forms.Button();
            this.btnOutputBrowse = new System.Windows.Forms.Button();
            this.gbTag = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbTagList = new System.Windows.Forms.ComboBox();
            this.gbFolders = new System.Windows.Forms.GroupBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.ofdSource = new System.Windows.Forms.OpenFileDialog();
            this.ofdOutput = new System.Windows.Forms.OpenFileDialog();
            this.gbRadioButtons.SuspendLayout();
            this.gbTag.SuspendLayout();
            this.gbFolders.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbSource
            // 
            this.tbSource.Location = new System.Drawing.Point(64, 30);
            this.tbSource.Name = "tbSource";
            this.tbSource.Size = new System.Drawing.Size(230, 23);
            this.tbSource.TabIndex = 0;
            // 
            // tbOutput
            // 
            this.tbOutput.Location = new System.Drawing.Point(64, 60);
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.Size = new System.Drawing.Size(230, 23);
            this.tbOutput.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Source";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Output";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Tag";
            // 
            // gbRadioButtons
            // 
            this.gbRadioButtons.Controls.Add(this.rbMove);
            this.gbRadioButtons.Controls.Add(this.rbCopy);
            this.gbRadioButtons.Location = new System.Drawing.Point(241, 120);
            this.gbRadioButtons.Name = "gbRadioButtons";
            this.gbRadioButtons.Size = new System.Drawing.Size(125, 76);
            this.gbRadioButtons.TabIndex = 5;
            this.gbRadioButtons.TabStop = false;
            this.gbRadioButtons.Text = "File Destination";
            // 
            // rbMove
            // 
            this.rbMove.AutoSize = true;
            this.rbMove.Location = new System.Drawing.Point(6, 47);
            this.rbMove.Name = "rbMove";
            this.rbMove.Size = new System.Drawing.Size(81, 19);
            this.rbMove.TabIndex = 0;
            this.rbMove.TabStop = true;
            this.rbMove.Text = "Move Files";
            this.rbMove.UseVisualStyleBackColor = true;
            // 
            // rbCopy
            // 
            this.rbCopy.AutoSize = true;
            this.rbCopy.Location = new System.Drawing.Point(6, 22);
            this.rbCopy.Name = "rbCopy";
            this.rbCopy.Size = new System.Drawing.Size(79, 19);
            this.rbCopy.TabIndex = 0;
            this.rbCopy.TabStop = true;
            this.rbCopy.Text = "Copy Files";
            this.rbCopy.UseVisualStyleBackColor = true;
            // 
            // btnSourceBrowse
            // 
            this.btnSourceBrowse.Location = new System.Drawing.Point(301, 29);
            this.btnSourceBrowse.Name = "btnSourceBrowse";
            this.btnSourceBrowse.Size = new System.Drawing.Size(29, 23);
            this.btnSourceBrowse.TabIndex = 6;
            this.btnSourceBrowse.Text = "...";
            this.btnSourceBrowse.UseVisualStyleBackColor = true;
            this.btnSourceBrowse.Click += new System.EventHandler(this.btnSourceBrowse_Click);
            // 
            // btnOutputBrowse
            // 
            this.btnOutputBrowse.Location = new System.Drawing.Point(301, 59);
            this.btnOutputBrowse.Name = "btnOutputBrowse";
            this.btnOutputBrowse.Size = new System.Drawing.Size(29, 23);
            this.btnOutputBrowse.TabIndex = 6;
            this.btnOutputBrowse.Text = "...";
            this.btnOutputBrowse.UseVisualStyleBackColor = true;
            this.btnOutputBrowse.Click += new System.EventHandler(this.btnOutputBrowse_Click);
            // 
            // gbTag
            // 
            this.gbTag.Controls.Add(this.label1);
            this.gbTag.Controls.Add(this.cbTagList);
            this.gbTag.Controls.Add(this.label4);
            this.gbTag.Location = new System.Drawing.Point(20, 120);
            this.gbTag.Name = "gbTag";
            this.gbTag.Size = new System.Drawing.Size(215, 76);
            this.gbTag.TabIndex = 7;
            this.gbTag.TabStop = false;
            this.gbTag.Text = "Tag";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 15);
            this.label1.TabIndex = 3;
            // 
            // cbTagList
            // 
            this.cbTagList.AutoCompleteCustomSource.AddRange(new string[] {
            "Artist",
            "Album",
            "Title",
            "Year"});
            this.cbTagList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTagList.FormattingEnabled = true;
            this.cbTagList.Location = new System.Drawing.Point(64, 30);
            this.cbTagList.Name = "cbTagList";
            this.cbTagList.Size = new System.Drawing.Size(133, 23);
            this.cbTagList.TabIndex = 2;
            // 
            // gbFolders
            // 
            this.gbFolders.Controls.Add(this.tbSource);
            this.gbFolders.Controls.Add(this.tbOutput);
            this.gbFolders.Controls.Add(this.btnOutputBrowse);
            this.gbFolders.Controls.Add(this.label2);
            this.gbFolders.Controls.Add(this.btnSourceBrowse);
            this.gbFolders.Controls.Add(this.label3);
            this.gbFolders.Location = new System.Drawing.Point(20, 12);
            this.gbFolders.Name = "gbFolders";
            this.gbFolders.Size = new System.Drawing.Size(346, 103);
            this.gbFolders.TabIndex = 8;
            this.gbFolders.TabStop = false;
            this.gbFolders.Text = "Input and Output Folders";
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(20, 202);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(346, 41);
            this.btnRun.TabIndex = 9;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // ofdSource
            // 
            this.ofdSource.FileName = "ofdSource";
            // 
            // ofdOutput
            // 
            this.ofdOutput.FileName = "ofdOutput";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 262);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.gbFolders);
            this.Controls.Add(this.gbTag);
            this.Controls.Add(this.gbRadioButtons);
            this.MaximumSize = new System.Drawing.Size(391, 301);
            this.MinimumSize = new System.Drawing.Size(391, 301);
            this.Name = "MainForm";
            this.RightToLeftLayout = true;
            this.Text = "Meta Folder Maker";
            this.gbRadioButtons.ResumeLayout(false);
            this.gbRadioButtons.PerformLayout();
            this.gbTag.ResumeLayout(false);
            this.gbTag.PerformLayout();
            this.gbFolders.ResumeLayout(false);
            this.gbFolders.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbSource;
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbRadioButtons;
        private System.Windows.Forms.RadioButton rbMove;
        private System.Windows.Forms.RadioButton rbCopy;
        private System.Windows.Forms.Button btnSourceBrowse;
        private System.Windows.Forms.Button btnOutputBrowse;
        private System.Windows.Forms.GroupBox gbTag;
        private System.Windows.Forms.ComboBox cbTagList;
        private System.Windows.Forms.GroupBox gbFolders;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.OpenFileDialog ofdSource;
        private System.Windows.Forms.OpenFileDialog ofdOutput;
    }
}

