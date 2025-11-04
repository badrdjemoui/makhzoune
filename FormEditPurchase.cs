using AnimalFeedApp.Helpers;
using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace AnimalFeedApp.Forms
{
    public partial class FormEditPurchase : Form
    {
        private int purchaseId;
        private TextBox txtSupplier;
        private ComboBox cmbProduct;
        private NumericUpDown numQuantity, numPrice;
        private Button btnSave, btnCancel;

        public FormEditPurchase(int id, string supplier, string item, double quantity, double totalCost, System.Data.DataTable suppliers)
        {
            purchaseId = id;
            InitializeComponent();

            txtSupplier.Text = supplier;
            cmbProduct.Text = item;
            numQuantity.Value = (decimal)Math.Max(1, quantity);
            numPrice.Value = (decimal)Math.Max(1, totalCost);

            LoadProducts();
        }

        private void InitializeComponent()
        {
            // إعداد خصائص الفورم
            this.Text = "تعديل عملية شراء";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 12F);
            this.RightToLeft = RightToLeft.Yes;
            this.RightToLeftLayout = true;

            // جعل الحجم متناسب مع الشاشة (60% عرض و70% ارتفاع)
            this.Size = new Size(
                (int)(Screen.PrimaryScreen.WorkingArea.Width * 0.6),
                (int)(Screen.PrimaryScreen.WorkingArea.Height * 0.7)
            );

            // تعريف التسميات
            Label lblSupplier = new Label { Text = "اسم المورد:", AutoSize = true, Anchor = AnchorStyles.Right };
            Label lblProduct = new Label { Text = "المنتج:", AutoSize = true, Anchor = AnchorStyles.Right };
            Label lblQuantity = new Label { Text = "الكمية:", AutoSize = true, Anchor = AnchorStyles.Right };
            Label lblPrice = new Label { Text = "السعر الإجمالي:", AutoSize = true, Anchor = AnchorStyles.Right };

            // عناصر الإدخال
            txtSupplier = new TextBox { Width = 300, Anchor = AnchorStyles.Left | AnchorStyles.Right };
            cmbProduct = new ComboBox { Width = 300, DropDownStyle = ComboBoxStyle.DropDownList, Anchor = AnchorStyles.Left | AnchorStyles.Right };
            numQuantity = new NumericUpDown { Width = 150, Minimum = 1, Maximum = 10000, Anchor = AnchorStyles.Left };
            numPrice = new NumericUpDown { Width = 150, Minimum = 1, Maximum = 1000000, DecimalPlaces = 2, Anchor = AnchorStyles.Left };

            // الأزرار
            btnSave = new Button
            {
                Text = "💾 حفظ التغييرات",
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Width = 180,
                Height = 45
            };
            btnSave.Click += btnSave_Click;

            btnCancel = new Button
            {
                Text = "❌ إلغاء",
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Width = 150,
                Height = 45
            };
            btnCancel.Click += (s, e) => this.Close();

            // إنشاء التخطيط العام
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(50),
                ColumnCount = 2,
                RowCount = 5
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));

            layout.Controls.Add(lblSupplier, 0, 0);
            layout.Controls.Add(txtSupplier, 1, 0);
            layout.Controls.Add(lblProduct, 0, 1);
            layout.Controls.Add(cmbProduct, 1, 1);
            layout.Controls.Add(lblQuantity, 0, 2);
            layout.Controls.Add(numQuantity, 1, 2);
            layout.Controls.Add(lblPrice, 0, 3);
            layout.Controls.Add(numPrice, 1, 3);

            // لوحة الأزرار
            FlowLayoutPanel buttonsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                AutoSize = true
            };
            buttonsPanel.Controls.Add(btnSave);
            buttonsPanel.Controls.Add(btnCancel);
            layout.Controls.Add(buttonsPanel, 1, 4);

            this.Controls.Add(layout);
        }

        private void LoadProducts()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT ItemName FROM Inventory";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbProduct.Items.Add(reader["ItemName"].ToString());
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string supplier = txtSupplier.Text.Trim();
            string itemName = cmbProduct.Text.Trim();
            int quantity = (int)numQuantity.Value;
            decimal totalCost = numPrice.Value;

            if (string.IsNullOrEmpty(supplier) || string.IsNullOrEmpty(itemName))
            {
                MessageBox.Show("❌ يرجى ملء جميع الحقول.", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string updateQuery = @"
                    UPDATE Purchases
                    SET SupplierName=@Supplier, ItemName=@Item, Quantity=@Quantity, TotalCost=@TotalCost
                    WHERE Id=@Id";
                using (var cmd = new SQLiteCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Supplier", supplier);
                    cmd.Parameters.AddWithValue("@Item", itemName);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@TotalCost", totalCost);
                    cmd.Parameters.AddWithValue("@Id", purchaseId);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("✅ تم حفظ التعديلات بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
