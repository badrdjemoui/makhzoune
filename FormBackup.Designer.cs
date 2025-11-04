namespace AnimalFeedApp.Forms
{
    partial class FormBackup
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnBackup = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.labelTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // 
            // labelTitle
            // 
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.labelTitle.Location = new System.Drawing.Point(50, 20);
            this.labelTitle.Size = new System.Drawing.Size(400, 40);
            this.labelTitle.Text = "🔄 النسخ الاحتياطي و الاسترجاع";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // btnBackup
            // 
            this.btnBackup.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnBackup.Location = new System.Drawing.Point(100, 90);
            this.btnBackup.Size = new System.Drawing.Size(300, 50);
            this.btnBackup.Text = "📦 إنشاء نسخة احتياطية";
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);

            // 
            // btnRestore
            // 
            this.btnRestore.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnRestore.Location = new System.Drawing.Point(100, 160);
            this.btnRestore.Size = new System.Drawing.Size(300, 50);
            this.btnRestore.Text = "♻️ استرجاع نسخة احتياطية";
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);

            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnClose.Location = new System.Drawing.Point(180, 240);
            this.btnClose.Size = new System.Drawing.Size(150, 40);
            this.btnClose.Text = "إغلاق";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // 
            // FormBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(500, 310);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.btnBackup);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormBackup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "النسخ الاحتياطي والاسترجاع";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label labelTitle;
    }
}
