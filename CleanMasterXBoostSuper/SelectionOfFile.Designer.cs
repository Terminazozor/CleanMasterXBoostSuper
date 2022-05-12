namespace CleanMasterXBoostSuper
{
    partial class SelectionOfFile
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
            this.checkedListFile = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkedListFile
            // 
            this.checkedListFile.FormattingEnabled = true;
            this.checkedListFile.Location = new System.Drawing.Point(-1, 78);
            this.checkedListFile.Name = "checkedListFile";
            this.checkedListFile.Size = new System.Drawing.Size(408, 289);
            this.checkedListFile.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(-1, 364);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(408, 85);
            this.button1.TabIndex = 1;
            this.button1.Text = "valider";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SelectionOfFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkedListFile);
            this.Name = "SelectionOfFile";
            this.Text = "SelectionOfFile";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListFile;
        private System.Windows.Forms.Button button1;
    }
}