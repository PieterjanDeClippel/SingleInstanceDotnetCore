namespace WinFormsApp1
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
            lstFiles = new ListBox();
            SuspendLayout();
            // 
            // lstFiles
            // 
            lstFiles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstFiles.FormattingEnabled = true;
            lstFiles.IntegralHeight = false;
            lstFiles.Location = new Point(12, 12);
            lstFiles.Name = "lstFiles";
            lstFiles.Size = new Size(718, 386);
            lstFiles.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(742, 410);
            Controls.Add(lstFiles);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        internal ListBox lstFiles;
    }
}
