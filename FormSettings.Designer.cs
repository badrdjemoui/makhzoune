namespace AnimalFeedApp.Forms
{
    partial class FormSettings
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelDb = new System.Windows.Forms.Label();
            this.labelBackup = new System.Windows.Forms.Label();
            this.labelLang = new System.Windows.Forms.Label();
            this.txtDbPath = new System.Windows.Forms.TextBox();
            this.txtBackupPath = new System.Windows.Forms.TextBox();
            this.comboLanguage = new System.Windows.Forms.ComboBox();
            this.btnBrowseDb = new System.Windows.Forms.Button();
            this.btnBrowseBackup = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.labelTitle.Location = new System.Drawing.Point(70, 20);
            this.labelTitle.Size = new System.Drawing.Size(400, 40);
            this.labelTitle.Text = "⚙️ إعدادات النظام";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelDb
            // 
            this.labelDb.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelDb.Location = new System.Drawing.Point(40, 80);
            this.labelDb.Size = new System.Drawing.Size(150, 25);
            this.labelDb.Text = "📦 قاعدة البيانات:";
            // 
            // txtDbPath
            // 
            this.txtDbPath.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDbPath.Location = new System.Drawing.Point(190, 80);
            this.txtDbPath.Size = new System.Drawing.Size(230, 30);
            // 
            // btnBrowseDb
            // 
            this.btnBrowseDb.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBrowseDb.Location = new System.Drawing.Point(430, 80);
            this.btnBrowseDb.Size = new System.Drawing.Size(70, 30);
            this.btnBrowseDb.Text = "استعراض";
            this.btnBrowseDb.Click += new System.EventHandler(this.btnBrowseDb_Click);
            // 
            // labelBackup
            // 
            this.labelBackup.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelBackup.Location = new System.Drawing.Point(40, 130);
            this.labelBackup.Size = new System.Drawing.Size(150, 25);
            this.labelBackup.Text = "💾 مجلد النسخ الاحتياطي:";
            // 
            // txtBackupPath
            // 
            this.txtBackupPath.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBackupPath.Location = new System.Drawing.Point(190, 130);
            this.txtBackupPath.Size = new System.Drawing.Size(230, 30);
            // 
            // btnBrowseBackup
            // 
            this.btnBrowseBackup.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBrowseBackup.Location = new System.Drawing.Point(430, 130);
            this.btnBrowseBackup.Size = new System.Drawing.Size(70, 30);
            this.btnBrowseBackup.Text = "استعراض";
            this.btnBrowseBackup.Click += new System.EventHandler(this.btnBrowseBackup_Click);
            // 
            // labelLang
            // 
            this.labelLang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelLang.Location = new System.Drawing.Point(40, 180);
            this.labelLang.Size = new System.Drawing.Size(120, 25);
            this.labelLang.Text = "🌐 اللغة:";
            // 
            // comboLanguage
            // 
            this.comboLanguage.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboLanguage.Location = new System.Drawing.Point(190, 180);
            this.comboLanguage.Size = new System.Drawing.Size(230, 30);
            this.comboLanguage.Items.AddRange(new object[] { "العربية", "English" });
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTestConnection.Location = new System.Drawing.Point(40, 230);
            this.btnTestConnection.Size = new System.Drawing.Size(150, 35);
            this.btnTestConnection.Text = "🔍 اختبار الاتصال";
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSave.Location = new System.Drawing.Point(210, 230);
            this.btnSave.Size = new System.Drawing.Size(120, 35);
            this.btnSave.Text = "💾 حفظ";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnClose.Location = new System.Drawing.Point(350, 230);
            this.btnClose.Size = new System.Drawing.Size(120, 35);
            this.btnClose.Text = "إغلاق";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FormSettings
            // 
            this.ClientSize = new System.Drawing.Size(540, 300);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.labelDb);
            this.Controls.Add(this.txtDbPath);
            this.Controls.Add(this.btnBrowseDb);
            this.Controls.Add(this.labelBackup);
            this.Controls.Add(this.txtBackupPath);
            this.Controls.Add(this.btnBrowseBackup);
            this.Controls.Add(this.labelLang);
            this.Controls.Add(this.comboLanguage);
            this.Controls.Add(this.btnTestConnection);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "إعدادات النظام";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelDb;
        private System.Windows.Forms.Label labelBackup;
        private System.Windows.Forms.Label labelLang;
        private System.Windows.Forms.TextBox txtDbPath;
        private System.Windows.Forms.TextBox txtBackupPath;
        private System.Windows.Forms.ComboBox comboLanguage;
        private System.Windows.Forms.Button btnBrowseDb;
        private System.Windows.Forms.Button btnBrowseBackup;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnTestConnection;
    }
}
