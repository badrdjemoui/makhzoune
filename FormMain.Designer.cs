using System;
using System.Drawing;
using System.Windows.Forms;

namespace AnimalFeedApp
{
    partial class FormMain
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnInventory;
        private Button btnSales;
        private Button btnPurchases;
        private Button btnReports;
        private Button btnBackup;
        private Button btnSettings;
        private Label lblTitle;
        private Panel panelTop;
        private TableLayoutPanel panelButtons;
        private PictureBox logoBox;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelTop = new Panel();
            this.lblTitle = new Label();
            this.logoBox = new PictureBox();
            this.panelButtons = new TableLayoutPanel();
            this.btnInventory = new Button();
            this.btnSales = new Button();
            this.btnPurchases = new Button();
            this.btnReports = new Button();
            this.btnBackup = new Button();
            this.btnSettings = new Button();

            // 
            // FormMain
            // 
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Name = "FormMain";
            this.Text = "نظام إدارة الأعلاف";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.BackColor = Color.WhiteSmoke;
            this.RightToLeft = RightToLeft.Yes;
            this.RightToLeftLayout = true;

            // 
            // panelTop
            // 
            this.panelTop.Dock = DockStyle.Top;
            this.panelTop.Height = 100;
            this.panelTop.BackColor = Color.FromArgb(0, 70, 160);
            this.panelTop.Padding = new Padding(20);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Controls.Add(this.logoBox);

            // 
            // lblTitle
            // 
            this.lblTitle.Dock = DockStyle.Right;
            this.lblTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTitle.Text = "🐄 نظام إدارة الأعلاف";

            // 
            // logoBox
            // 
            this.logoBox.Dock = DockStyle.Left;
            this.logoBox.SizeMode = PictureBoxSizeMode.Zoom;
            // يمكنك لاحقاً إضافة الصورة:
            // this.logoBox.Image = Image.FromFile("logo.png");

            // 
            // panelButtons
            // 
            this.panelButtons.Dock = DockStyle.Fill;
            this.panelButtons.BackColor = Color.FromArgb(230, 240, 255);
            this.panelButtons.Padding = new Padding(50);
            this.panelButtons.ColumnCount = 1;
            this.panelButtons.RowCount = 6;
            this.panelButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            for (int i = 0; i < 6; i++)
            {
                this.panelButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 16F)); // نسبياً متساوية
            }

            // 
            // إعداد الأزرار
            // 
            Button[] buttons = { btnInventory, btnSales, btnPurchases, btnReports, btnBackup, btnSettings };
            string[] texts = { "المخزون", "المبيعات", "المشتريات", "التقارير", "نسخ احتياطي", "الإعدادات" };
            EventHandler[] handlers =
            {
                btnInventory_Click, btnSales_Click, btnPurchases_Click,
                btnReports_Click, btnBackup_Click, btnSettings_Click
            };

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Text = texts[i];
                buttons[i].Font = new Font("Segoe UI", 14F, FontStyle.Bold);
                buttons[i].BackColor = Color.FromArgb(0, 120, 215);
                buttons[i].ForeColor = Color.White;
                buttons[i].FlatStyle = FlatStyle.Flat;
                buttons[i].FlatAppearance.BorderSize = 0;
                buttons[i].Dock = DockStyle.Fill; // ✅ يجعل الزر يتمدد مع حجم الخلية
                buttons[i].Click += handlers[i];
                this.panelButtons.Controls.Add(buttons[i], 0, i);
            }

            // 
            // إضافة العناصر للنموذج
            // 
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelTop);
            this.ResumeLayout(false);
        }
    }
}
