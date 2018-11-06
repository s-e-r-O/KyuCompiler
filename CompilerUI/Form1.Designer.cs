namespace CompilerUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.interfaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkNigthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkNigthToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moreInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exceptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.codeExampleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TextEditorTextBox = new System.Windows.Forms.RichTextBox();
            this.LineNumberTextBox = new System.Windows.Forms.RichTextBox();
            this.StopButton = new System.Windows.Forms.Button();
            this.PlayButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.OutputLabel = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.windowToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1404, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(147, 26);
            this.openFileToolStripMenuItem.Text = "Open File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(147, 26);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(147, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.interfaceToolStripMenuItem});
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(56, 24);
            this.windowToolStripMenuItem.Text = "Tools";
            // 
            // interfaceToolStripMenuItem
            // 
            this.interfaceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.darkNigthToolStripMenuItem});
            this.interfaceToolStripMenuItem.Name = "interfaceToolStripMenuItem";
            this.interfaceToolStripMenuItem.Size = new System.Drawing.Size(136, 26);
            this.interfaceToolStripMenuItem.Text = "Options";
            // 
            // darkNigthToolStripMenuItem
            // 
            this.darkNigthToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.darkNigthToolStripMenuItem1,
            this.lightToolStripMenuItem});
            this.darkNigthToolStripMenuItem.Name = "darkNigthToolStripMenuItem";
            this.darkNigthToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            this.darkNigthToolStripMenuItem.Text = "Environment";
            // 
            // darkNigthToolStripMenuItem1
            // 
            this.darkNigthToolStripMenuItem1.Name = "darkNigthToolStripMenuItem1";
            this.darkNigthToolStripMenuItem1.Size = new System.Drawing.Size(153, 26);
            this.darkNigthToolStripMenuItem1.Text = "Dark nigth";
            this.darkNigthToolStripMenuItem1.Click += new System.EventHandler(this.darkNigthToolStripMenuItem1_Click);
            // 
            // lightToolStripMenuItem
            // 
            this.lightToolStripMenuItem.Name = "lightToolStripMenuItem";
            this.lightToolStripMenuItem.Size = new System.Drawing.Size(153, 26);
            this.lightToolStripMenuItem.Text = "Light";
            this.lightToolStripMenuItem.Click += new System.EventHandler(this.lightToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moreInfoToolStripMenuItem,
            this.exceptionsToolStripMenuItem,
            this.codeExampleToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // moreInfoToolStripMenuItem
            // 
            this.moreInfoToolStripMenuItem.Name = "moreInfoToolStripMenuItem";
            this.moreInfoToolStripMenuItem.Size = new System.Drawing.Size(187, 26);
            this.moreInfoToolStripMenuItem.Text = "Documentation";
            this.moreInfoToolStripMenuItem.Click += new System.EventHandler(this.moreInfoToolStripMenuItem_Click);
            // 
            // exceptionsToolStripMenuItem
            // 
            this.exceptionsToolStripMenuItem.Name = "exceptionsToolStripMenuItem";
            this.exceptionsToolStripMenuItem.Size = new System.Drawing.Size(187, 26);
            this.exceptionsToolStripMenuItem.Text = "Exceptions";
            this.exceptionsToolStripMenuItem.Click += new System.EventHandler(this.exceptionsToolStripMenuItem_Click);
            // 
            // codeExampleToolStripMenuItem
            // 
            this.codeExampleToolStripMenuItem.Name = "codeExampleToolStripMenuItem";
            this.codeExampleToolStripMenuItem.Size = new System.Drawing.Size(187, 26);
            this.codeExampleToolStripMenuItem.Text = "Code Example";
            this.codeExampleToolStripMenuItem.Click += new System.EventHandler(this.codeExampleToolStripMenuItem_Click);
            // 
            // TextEditorTextBox
            // 
            this.TextEditorTextBox.AcceptsTab = true;
            this.TextEditorTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextEditorTextBox.BackColor = System.Drawing.Color.White;
            this.TextEditorTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextEditorTextBox.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextEditorTextBox.Location = new System.Drawing.Point(72, 48);
            this.TextEditorTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TextEditorTextBox.Name = "TextEditorTextBox";
            this.TextEditorTextBox.Size = new System.Drawing.Size(1307, 474);
            this.TextEditorTextBox.TabIndex = 7;
            this.TextEditorTextBox.Text = "";
            this.TextEditorTextBox.WordWrap = false;
            this.TextEditorTextBox.ReadOnlyChanged += new System.EventHandler(this.TextEditorTextBox_ReadOnlyChanged);
            this.TextEditorTextBox.CursorChanged += new System.EventHandler(this.TextEditorTextBox_CursorChanged);
            this.TextEditorTextBox.TextChanged += new System.EventHandler(this.TextEditorTextBox_TextChanged);
            this.TextEditorTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextEditorTextBox_KeyDown);
            // 
            // LineNumberTextBox
            // 
            this.LineNumberTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.LineNumberTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LineNumberTextBox.Cursor = System.Windows.Forms.Cursors.PanNE;
            this.LineNumberTextBox.ForeColor = System.Drawing.Color.DodgerBlue;
            this.LineNumberTextBox.Location = new System.Drawing.Point(17, 48);
            this.LineNumberTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LineNumberTextBox.Name = "LineNumberTextBox";
            this.LineNumberTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.LineNumberTextBox.Size = new System.Drawing.Size(53, 474);
            this.LineNumberTextBox.TabIndex = 8;
            this.LineNumberTextBox.Text = "";
            this.LineNumberTextBox.ReadOnlyChanged += new System.EventHandler(this.LineNumberTextBox_ReadOnlyChanged);
            // 
            // StopButton
            // 
            this.StopButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.StopButton.Image = global::CompilerUI.Properties.Resources.stop;
            this.StopButton.Location = new System.Drawing.Point(731, 0);
            this.StopButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(56, 31);
            this.StopButton.TabIndex = 9;
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // PlayButton
            // 
            this.PlayButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.PlayButton.BackgroundImage = global::CompilerUI.Properties.Resources.play;
            this.PlayButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PlayButton.Location = new System.Drawing.Point(612, 0);
            this.PlayButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(88, 32);
            this.PlayButton.TabIndex = 3;
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(17, 530);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1360, 222);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.OutputLabel);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Size = new System.Drawing.Size(1352, 191);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Output";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // OutputLabel
            // 
            this.OutputLabel.AutoSize = true;
            this.OutputLabel.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputLabel.Location = new System.Drawing.Point(13, 14);
            this.OutputLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.OutputLabel.Name = "OutputLabel";
            this.OutputLabel.Size = new System.Drawing.Size(0, 15);
            this.OutputLabel.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Controls.Add(this.ErrorLabel);
            this.tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Size = new System.Drawing.Size(1352, 191);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Error List";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.AutoSize = true;
            this.ErrorLabel.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorLabel.Location = new System.Drawing.Point(12, 12);
            this.ErrorLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(0, 15);
            this.ErrorLabel.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1404, 770);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.LineNumberTextBox);
            this.Controls.Add(this.TextEditorTextBox);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "KYU#";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.RichTextBox TextEditorTextBox;
        private System.Windows.Forms.RichTextBox LineNumberTextBox;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.Label OutputLabel;
        private System.Windows.Forms.Label ErrorLabel;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moreInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exceptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem codeExampleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem interfaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darkNigthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darkNigthToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem lightToolStripMenuItem;
    }
}

