using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AnimalFeedApp.Helpers;

namespace AnimalFeedApp.Forms
{
    public partial class FormReports : Form
    {
        public FormReports()
        {
            InitializeComponent();
            this.Resize += FormReports_Resize;
        }

        private void FormReports_Load(object sender, EventArgs e)
        {
            comboReportType.Items.AddRange(new string[] { "المخزون", "المبيعات", "المشتريات" });
            comboDuration.Items.AddRange(new string[] { "يومي", "شهري", "سنوي" });

            comboReportType.SelectedIndex = 0;
            comboDuration.SelectedIndex = 0;

            LoadReport();
            CenterAndResizeUI();
        }

        private void FormReports_Resize(object sender, EventArgs e)
        {
            CenterAndResizeUI();
        }

        private void CenterAndResizeUI()
        {
            float screenW = this.ClientSize.Width;
            float screenH = this.ClientSize.Height;

            float fontScale = Math.Max(screenW, screenH) / 1200f;
            Font baseFont = new Font("Segoe UI", 10f * fontScale, FontStyle.Bold);

            comboReportType.Font = baseFont;
            comboDuration.Font = baseFont;
            dataGridView1.Font = new Font("Segoe UI", 10f * fontScale);
            dataGridTotals.Font = new Font("Segoe UI", 10f * fontScale);
            btnClose.Font = baseFont;

            int controlWidth = (int)(screenW * 0.25);
            int controlHeight = (int)(40 * fontScale);

            comboReportType.Width = comboDuration.Width = controlWidth;
            comboReportType.Height = comboDuration.Height = controlHeight;

            comboReportType.Left = (int)(screenW / 2 - controlWidth - 10);
            comboDuration.Left = (int)(screenW / 2 + 10);
            comboReportType.Top = comboDuration.Top = (int)(screenH * 0.05);

            dataGridView1.Width = (int)(screenW * 0.9);
            dataGridView1.Height = (int)(screenH * 0.5);
            dataGridView1.Left = (int)(screenW * 0.05);
            dataGridView1.Top = (int)(screenH * 0.15);

            dataGridTotals.Width = (int)(screenW * 0.9);
            dataGridTotals.Left = (int)(screenW * 0.05);
            dataGridTotals.Top = dataGridView1.Top + dataGridView1.Height + 10;

            btnClose.Width = (int)(controlWidth * 0.8);
            btnClose.Height = controlHeight;
            btnClose.Left = (int)(screenW / 2 - btnClose.Width / 2);
            btnClose.Top = dataGridTotals.Bottom + 20;
        }

        private void comboReportType_SelectedIndexChanged(object sender, EventArgs e) => LoadReport();

        private void comboDuration_SelectedIndexChanged(object sender, EventArgs e) => LoadReport();

        private void LoadReport()
        {
            if (comboReportType.SelectedItem == null || comboDuration.SelectedItem == null)
                return;

            string type = comboReportType.SelectedItem.ToString();
            string duration = comboDuration.SelectedItem.ToString();

            string query = "";

            if (type == "المخزون")
                query = "SELECT ItemName AS 'الصنف', Quantity AS 'الكمية', PricePerUnit AS 'سعر الوحدة', (Quantity * PricePerUnit) AS 'السعر الإجمالي', DateAdded AS 'تاريخ الإضافة' FROM Inventory";
            else if (type == "المبيعات")
                query = BuildSalesQuery(duration);
            else if (type == "المشتريات")
                query = BuildPurchasesQuery(duration);

            DataTable dt = DatabaseHelper.GetDataTable(query);
            dataGridView1.DataSource = dt;

            CalculateTotals(dt, type);
        }

        private string BuildSalesQuery(string duration)
        {
            string baseQuery = "SELECT CustomerName AS 'الزبون', ItemName AS 'الصنف', Quantity AS 'الكمية', TotalPrice AS 'السعر الإجمالي', SaleDate AS 'تاريخ البيع' FROM Sales";

            if (duration == "يومي")
                baseQuery += " WHERE date(SaleDate) = date('now')";
            else if (duration == "شهري")
                baseQuery += " WHERE strftime('%Y-%m', SaleDate) = strftime('%Y-%m', 'now')";
            else if (duration == "سنوي")
                baseQuery += " WHERE strftime('%Y', SaleDate) = strftime('%Y', 'now')";

            baseQuery += " ORDER BY SaleDate DESC";
            return baseQuery;
        }

        private string BuildPurchasesQuery(string duration)
        {
            string baseQuery = "SELECT SupplierName AS 'المورد', ItemName AS 'الصنف', Quantity AS 'الكمية', TotalCost AS 'التكلفة الإجمالية', PurchaseDate AS 'تاريخ الشراء' FROM Purchases";

            if (duration == "يومي")
                baseQuery += " WHERE date(PurchaseDate) = date('now')";
            else if (duration == "شهري")
                baseQuery += " WHERE strftime('%Y-%m', PurchaseDate) = strftime('%Y-%m', 'now')";
            else if (duration == "سنوي")
                baseQuery += " WHERE strftime('%Y', PurchaseDate) = strftime('%Y', 'now')";

            baseQuery += " ORDER BY PurchaseDate DESC";
            return baseQuery;
        }

        private void CalculateTotals(DataTable dt, string type)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                dataGridTotals.DataSource = null;
                return;
            }

            if (type == "المخزون" && dt.Columns.Contains("السعر الإجمالي"))
            {
                double total = dt.AsEnumerable().Where(r => r["السعر الإجمالي"] != DBNull.Value).Sum(r => Convert.ToDouble(r["السعر الإجمالي"]));
                dataGridTotals.DataSource = new[] { new { الوصف = "📦 إجمالي قيمة المخزون", القيمة = $"{total:F2} دج" } };
            }
            else if (type == "المبيعات" && dt.Columns.Contains("السعر الإجمالي"))
            {
                double total = dt.AsEnumerable().Where(r => r["السعر الإجمالي"] != DBNull.Value).Sum(r => Convert.ToDouble(r["السعر الإجمالي"]));
                dataGridTotals.DataSource = new[] { new { الوصف = "💰 إجمالي المبيعات", القيمة = $"{total:F2} دج" } };
            }
            else if (type == "المشتريات")
            {
                double totalQuantity = 0;
                double totalCost = 0;

                foreach (DataRow row in dt.Rows)
                {
                    if (double.TryParse(row["الكمية"]?.ToString(), out double q))
                        totalQuantity += q;
                    if (double.TryParse(row["التكلفة الإجمالية"]?.ToString(), out double c))
                        totalCost += c;
                }

                double percent = totalCost * 0.05;
                double totalWithPercent = totalCost + percent;

                dataGridTotals.DataSource = new[]
                {
                    new { الوصف = "🧾 إجمالي المشتريات", القيمة = $"{totalCost:F2} دج" },
                    new { الوصف = "📦 مجموع الكمية", القيمة = $"{totalQuantity:F2} قنطار" },
                    new { الوصف = "💸 نسبة 5%", القيمة = $"{percent:F2} دج" },
                    new { الوصف = "💰 المجموع الكلي مع الزيادة", القيمة = $"{totalWithPercent:F2} دج" }
                };
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}
