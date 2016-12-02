namespace VM.ClassType
{
    partial class ClassMethod
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
            this.virtualScreen1 = new VM.ClassType.VirtualScreen();
            this.SuspendLayout();
            // 
            // virtualScreen1
            // 
            this.virtualScreen1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.virtualScreen1.Location = new System.Drawing.Point(12, 12);
            this.virtualScreen1.Name = "virtualScreen1";
            this.virtualScreen1.Size = new System.Drawing.Size(740, 434);
            this.virtualScreen1.TabIndex = 0;
            this.virtualScreen1.Paint += new System.Windows.Forms.PaintEventHandler(this.virtualScreen1_Paint);
            // 
            // ClassMethod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 458);
            this.Controls.Add(this.virtualScreen1);
            this.Name = "ClassMethod";
            this.Text = "ClassMethod";
            this.ResumeLayout(false);

        }

        #endregion

        private VirtualScreen virtualScreen1;
    }
}