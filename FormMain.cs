using System;
using System.Windows.Forms;
using AnimalFeedApp.Forms;
using AnimalFeedApp.Helpers;


namespace AnimalFeedApp
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        // يُنفذ عند تحميل الواجهة
        private void FormMain_Load(object sender, EventArgs e)
        {
            lblTitle.Text = "🐄 نظام إدارة الأعلاف";
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            FormInventory FormInventory1 = new FormInventory();
            FormInventory1.ShowDialog(); // أو .Show() إذا كنت لا تريد تعطيل النافذة الرئيسية
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            FormSales fs = new FormSales();
            fs.ShowDialog();
        }

        private void btnPurchases_Click(object sender, EventArgs e)
        {
            FormPurchases form = new FormPurchases();
            form.ShowDialog();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            FormReports formReports = new FormReports();
            formReports.ShowDialog(); // فتح نافذة التقارير
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            FormBackup formBackup = new FormBackup();
            formBackup.ShowDialog();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            FormSettings formSettings = new FormSettings();
            formSettings.ShowDialog();
        }
    }
}
