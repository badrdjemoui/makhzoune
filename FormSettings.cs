using System;
using System.IO;
using System.Windows.Forms;

namespace AnimalFeedApp.Forms
{
    public partial class FormSettings : Form
    {
        private string settingsFile = "appsettings.txt";

        public FormSettings()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void btnBrowseDb_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Database Files (*.db)|*.db|All Files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtDbPath.Text = ofd.FileName;
            }
        }

        private void btnBrowseBackup_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtBackupPath.Text = fbd.SelectedPath;
            }
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(txtDbPath.Text))
                {
                    MessageBox.Show("❌ قاعدة البيانات غير موجودة!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var conn = new System.Data.SQLite.SQLiteConnection($"Data Source={txtDbPath.Text};Version=3;"))
                {
                    conn.Open();
                    MessageBox.Show("✅ تم الاتصال بقاعدة البيانات بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("⚠️ فشل الاتصال بقاعدة البيانات:\n" + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(settingsFile,
                    $"DB_PATH={txtDbPath.Text}\n" +
                    $"BACKUP_PATH={txtBackupPath.Text}\n" +
                    $"LANGUAGE={comboLanguage.Text}");

                MessageBox.Show("✅ تم حفظ الإعدادات بنجاح!", "تم الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("⚠️ خطأ أثناء الحفظ:\n" + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSettings()
        {
            if (!File.Exists(settingsFile)) return;

            foreach (var line in File.ReadAllLines(settingsFile))
            {
                if (line.StartsWith("DB_PATH="))
                    txtDbPath.Text = line.Substring(8);
                else if (line.StartsWith("BACKUP_PATH="))
                    txtBackupPath.Text = line.Substring(12);
                else if (line.StartsWith("LANGUAGE="))
                    comboLanguage.Text = line.Substring(9);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
