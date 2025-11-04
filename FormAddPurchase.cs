using AnimalFeedApp.Helpers;
using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace AnimalFeedApp.Forms
{
    public partial class FormAddPurchase : Form
    {
        private Label lblTitle, lblSupplier, lblProduct, lblQuantity, lblPrice;
        private TextBox txtSupplier;
        private ComboBox cmbProduct;
        private NumericUpDown numQuantity, numPrice;
        private Button btnSave, btnCancel;

        public FormAddPurchase(System.Data.DataTable suppliers)
        {
            InitializeComponent();
            LoadProducts();
        }
        /************************************************************************************/
        private void InitializeComponent()
        {
            this.Text = "إضافة عملية شراء جديدة";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 12F);
            this.RightToLeft = RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.AutoScroll = true; // ✅ في حال صغر الشاشة

            // 🔵 العنوان
            lblTitle = new Label
            {
                Text = "إضافة عملية شراء جديدة 🛒",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 80,
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(0, 70, 160)
            };

            // 🏷️ الحقول
            lblSupplier = new Label { Text = "اسم المورد:", AutoSize = true };
            lblProduct = new Label { Text = "المنتج:", AutoSize = true };
            lblQuantity = new Label { Text = "الكمية:", AutoSize = true };
            lblPrice = new Label { Text = "السعر الإجمالي:", AutoSize = true };

            txtSupplier = new TextBox { Width = 300 };
            cmbProduct = new ComboBox { Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };
            numQuantity = new NumericUpDown { Width = 120, Minimum = 1, Maximum = 10000, Value = 1 };
            numPrice = new NumericUpDown { Width = 120, Minimum = 1, Maximum = 1000000, Value = 100 };

            // 🧩 الأزرار
            btnSave = new Button
            {
                Text = "💾 حفظ",
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Width = 120,
                Height = 45,
                Margin = new Padding(10)
            };
            btnSave.Click += btnSave_Click;

            btnCancel = new Button
            {
                Text = "❌ إلغاء",
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Width = 120,
                Height = 45,
                Margin = new Padding(10)
            };
            btnCancel.Click += (s, e) => this.Close();

            // 📋 المحتوى
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(40),
                ColumnCount = 2,
                RowCount = 4
            };

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));

            for (int i = 0; i < 4; i++)
                layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));

            layout.Controls.Add(lblSupplier, 0, 0);
            layout.Controls.Add(txtSupplier, 1, 0);
            layout.Controls.Add(lblProduct, 0, 1);
            layout.Controls.Add(cmbProduct, 1, 1);
            layout.Controls.Add(lblQuantity, 0, 2);
            layout.Controls.Add(numQuantity, 1, 2);
            layout.Controls.Add(lblPrice, 0, 3);
            layout.Controls.Add(numPrice, 1, 3);

            // 🔘 شريط الأزرار أسفل الفورم
            FlowLayoutPanel buttonsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(10),
                Height = 80,
                BackColor = Color.WhiteSmoke
            };
            buttonsPanel.Controls.Add(btnSave);
            buttonsPanel.Controls.Add(btnCancel);

            // 🧱 Panel لاحتواء المحتوى
            Panel contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.WhiteSmoke
            };
            contentPanel.Controls.Add(layout);

            // 🧩 الترتيب النهائي
            this.Controls.Add(contentPanel);
            this.Controls.Add(buttonsPanel); // ✅ أسفل
            this.Controls.Add(lblTitle);     // ✅ أعلى
        }


        /*******************************************************************************/

        // تحميل قائمة المنتجات من قاعدة البيانات
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

        // زر الحفظ
        private void btnSave_Click(object sender, EventArgs e)
        {
            string supplier = txtSupplier.Text.Trim();
            string itemName = cmbProduct.Text.Trim();
            int quantity = (int)numQuantity.Value;
            decimal totalCost = numPrice.Value;
            DateTime date = DateTime.Now;

            if (string.IsNullOrEmpty(supplier) || string.IsNullOrEmpty(itemName))
            {
                MessageBox.Show("❌ يرجى ملء جميع الحقول قبل الحفظ.", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 1️⃣ إضافة عملية الشراء
                        string insertQuery = @"
                            INSERT INTO Purchases (SupplierName, ItemName, Quantity, TotalCost, PurchaseDate)
                            VALUES (@SupplierName, @ItemName, @Quantity, @TotalCost, @PurchaseDate)";
                        using (var cmd = new SQLiteCommand(insertQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@SupplierName", supplier);
                            cmd.Parameters.AddWithValue("@ItemName", itemName);
                            cmd.Parameters.AddWithValue("@Quantity", quantity);
                            cmd.Parameters.AddWithValue("@TotalCost", totalCost);
                            cmd.Parameters.AddWithValue("@PurchaseDate", date);
                            cmd.ExecuteNonQuery();
                        }

                        // 2️⃣ تحديث المخزون
                        string updateQuery = @"
                            UPDATE Inventory
                            SET Quantity = Quantity + @Quantity
                            WHERE ItemName = @ItemName";
                        using (var cmd = new SQLiteCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@Quantity", quantity);
                            cmd.Parameters.AddWithValue("@ItemName", itemName);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show("✅ تم حفظ عملية الشراء وتحديث المخزون بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("حدث خطأ أثناء الحفظ:\n" + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
