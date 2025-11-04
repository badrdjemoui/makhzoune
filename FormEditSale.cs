using System;
using System.Drawing;
using System.Windows.Forms;

namespace AnimalFeedApp
{
    public partial class FormEditSale : Form
    {
        private TextBox txtCustomer;
        private ComboBox cmbProduct;
        private NumericUpDown numQuantity;
        private NumericUpDown numPrice;
        private Button btnUpdate;
        private Button btnCancel;

       
            public FormEditSale(int id, string customer, string product, int quantity, decimal price)
        {
            InitializeComponent();

            // تعبئة القيم القديمة
            txtCustomer.Text = customer;
            cmbProduct.Text = product;
            numQuantity.Value = quantity;
            numPrice.Value = price;
        }

        private void InitializeComponent()
        {
            this.Text = "تعديل عملية بيع";
            this.Size = new Size(600, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 12F);
            this.RightToLeft = RightToLeft.Yes;
            this.RightToLeftLayout = true;

            Label lblCustomer = new Label { Text = "اسم الزبون:", AutoSize = true };
            Label lblProduct = new Label { Text = "المنتج:", AutoSize = true };
            Label lblQuantity = new Label { Text = "الكمية:", AutoSize = true };
            Label lblPrice = new Label { Text = "السعر:", AutoSize = true };

            txtCustomer = new TextBox { Width = 300 };
            cmbProduct = new ComboBox { Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbProduct.Items.AddRange(new string[] { "علف أبقار", "علف دجاج", "علف أغنام" });

            numQuantity = new NumericUpDown { Width = 100, Minimum = 1, Maximum = 10000 };
            numPrice = new NumericUpDown { Width = 100, Minimum = 1, Maximum = 100000 };

            btnUpdate = new Button
            {
                Text = "💾 تحديث",
                BackColor = Color.SeaGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Width = 120
            };
            btnUpdate.Click += BtnUpdate_Click;

            btnCancel = new Button
            {
                Text = "❌ إلغاء",
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Width = 120
            };
            btnCancel.Click += (s, e) => this.Close();

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(40),
                ColumnCount = 2,
                RowCount = 5
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));

            layout.Controls.Add(lblCustomer, 0, 0);
            layout.Controls.Add(txtCustomer, 1, 0);
            layout.Controls.Add(lblProduct, 0, 1);
            layout.Controls.Add(cmbProduct, 1, 1);
            layout.Controls.Add(lblQuantity, 0, 2);
            layout.Controls.Add(numQuantity, 1, 2);
            layout.Controls.Add(lblPrice, 0, 3);
            layout.Controls.Add(numPrice, 1, 3);

            FlowLayoutPanel buttonsPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft };
            buttonsPanel.Controls.Add(btnUpdate);
            buttonsPanel.Controls.Add(btnCancel);

            layout.Controls.Add(buttonsPanel, 1, 4);

            this.Controls.Add(layout);
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("✅ تم تحديث بيانات البيع بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
