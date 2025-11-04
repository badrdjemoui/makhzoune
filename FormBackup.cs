using System;
using System.IO;
using System.Windows.Forms;

namespace AnimalFeedApp.Forms
{
    public partial class FormBackup : Form
    {
        private string dbPath;
        private string backupFolder;

        public FormBackup()
        {
            InitializeComponent();
            LoadSettings(); // تحميل الإعدادات عند فتح الفورم
        }

        private void LoadSettings()
        {
            string settingsFile = "appsettings.txt";
            if (!File.Exists(settingsFile))
            {
                MessageBox.Show("⚠️ ملف الإعدادات غير موجود. يرجى ضبط الإعدادات أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (var line in File.ReadAllLines(settingsFile))
            {
                if (line.StartsWith("DB_PATH="))
                    dbPath = line.Substring(8);
                else if (line.StartsWith("BACKUP_PATH="))
                    backupFolder = line.Substring(12);
            }

            if (string.IsNullOrEmpty(dbPath) || !File.Exists(dbPath))
            {
                MessageBox.Show("⚠️ مسار قاعدة البيانات غير صالح! يرجى ضبط الإعدادات.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (string.IsNullOrEmpty(backupFolder) || !Directory.Exists(backupFolder))
            {
                MessageBox.Show("⚠️ مسار النسخ الاحتياطي غير صالح! يرجى ضبط الإعدادات.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // زر النسخ الاحتياطي
        private void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(dbPath) || string.IsNullOrEmpty(backupFolder))
                {
                    MessageBox.Show("⚠️ تأكد من ضبط الإعدادات أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string backupFile = Path.Combine(backupFolder, $"backup_{DateTime.Now:yyyyMMdd_HHmmss}.db");
                File.Copy(dbPath, backupFile, true);

                MessageBox.Show($"✅ تم إنشاء النسخة الاحتياطية بنجاح!\nالمسار:\n{backupFile}", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ حدث خطأ أثناء النسخ الاحتياطي:\n" + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // زر الاسترجاع
        private void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(dbPath) || string.IsNullOrEmpty(backupFolder))
                {
                    MessageBox.Show("⚠️ تأكد من ضبط الإعدادات أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = backupFolder;
                ofd.Filter = "Database Backup (*.db)|*.db";
                ofd.Title = "اختر النسخة الاحتياطية للاسترجاع";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    File.Copy(ofd.FileName, dbPath, true);
                    MessageBox.Show("✅ تم استرجاع النسخة الاحتياطية بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ حدث خطأ أثناء الاسترجاع:\n" + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
