using AnimalFeedApp.Helpers;
using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace AnimalFeedApp
{
    public partial class FormAddSale : Form
    {
        private Label lblTitle;
        private Label lblCustomer;
        private Label lblProduct;
        private Label lblQuantity;
        private Label lblPrice;
        private TextBox txtCustomer;
        private ComboBox cmbProduct;
        private NumericUpDown numQuantity;
        private NumericUpDown numPrice;
        private Button btnSave;
        private Button btnCancel;

        public FormAddSale()
        {
            InitializeComponent();
            LoadProductsFromDatabase();
        }

       

        private void InitializeComponent()
        {
            this.Text = "إضافة عملية بيع جديدة";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 12F);
            this.RightToLeft = RightToLeft.Yes;
            this.RightToLeftLayout = true;

            lblTitle = new Label
            {
                Text = "إضافة عملية بيع جديدة 🐄",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 80,
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(0, 70, 160)
            };

            lblCustomer = new Label { Text = "اسم الزبون:", AutoSize = true };
            lblProduct = new Label { Text = "المنتج:", AutoSize = true };
            lblQuantity = new Label { Text = "الكمية:", AutoSize = true };
            lblPrice = new Label { Text = "السعر:", AutoSize = true };

            txtCustomer = new TextBox { Width = 300 };
            cmbProduct = new ComboBox { Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbProduct.Items.AddRange(new string[] { "علف أبقار", "علف دجاج", "علف أغنام" });

            numQuantity = new NumericUpDown { Width = 100, Minimum = 1, Maximum = 10000, Value = 1 };
            numPrice = new NumericUpDown { Width = 100, Minimum = 1, Maximum = 100000, Value = 100 };

            btnSave = new Button
            {
                Text = "💾 حفظ",
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Width = 120
            };
            btnSave.Click += btnSave_Click;

            btnCancel = new Button
            {
                Text = "❌ إلغاء",
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Width = 120
            };
            btnCancel.Click += (s, e) => this.Close();

            // layout
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(40),
                ColumnCount = 2,
                RowCount = 6
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));

            layout.Controls.Add(lblCustomer, 0, 0);
            layout.Controls.Add(txtCustomer, 1, 0);
            layout.Controls.Add(lblProduct, 0, 1);
            layout.Controls.Add(cmbProduct, 1, 1);
            layout.Controls.Add(lblQuantity, 0, 2);
            layout.Controls.Add(numQuantity, 1, 2);
            layout.Controls.Add(lblPrice, 0, 3);
            layout.Controls.Add(numPrice, 1, 3);

            FlowLayoutPanel buttonsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft
            };
            buttonsPanel.Controls.Add(btnSave);
            buttonsPanel.Controls.Add(btnCancel);

            layout.Controls.Add(buttonsPanel, 1, 5);

            this.Controls.Add(layout);
            this.Controls.Add(lblTitle);
        }

   
    private void btnSave_Click(object sender, EventArgs e)
        {
            string customer = txtCustomer.Text.Trim();
            string itemName = cmbProduct.Text.Trim();  // المنتج
            int quantity = (int)numQuantity.Value;
            decimal price = numPrice.Value;
            DateTime date = DateTime.Now;

            if (string.IsNullOrEmpty(customer) || string.IsNullOrEmpty(itemName))
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
                        // ✅ 1. إضافة العملية إلى جدول المبيعات
                        string insertQuery = @"
                    INSERT INTO Sales (CustomerName, ItemName, Quantity, TotalPrice, SaleDate)
                    VALUES (@CustomerName, @ItemName, @Quantity, @TotalPrice, @SaleDate)";
                        using (var cmd = new SQLiteCommand(insertQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@CustomerName", customer);
                            cmd.Parameters.AddWithValue("@ItemName", itemName);
                            cmd.Parameters.AddWithValue("@Quantity", quantity);
                            cmd.Parameters.AddWithValue("@TotalPrice", price);
                            cmd.Parameters.AddWithValue("@SaleDate", date);
                            cmd.ExecuteNonQuery();
                        }

                        // ✅ 2. إنقاص الكمية من جدول Inventory
                        string updateQuery = @"
                    UPDATE Inventory
                    SET Quantity = Quantity - @Quantity
                    WHERE ItemName = @ItemName";
                        using (var cmd = new SQLiteCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@Quantity", quantity);
                            cmd.Parameters.AddWithValue("@ItemName", itemName);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show("✅ تم حفظ عملية البيع وتحديث المخزون بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        /*********************************************************************/


        private void LoadProductsFromDatabase()

        {
            cmbProduct.Items.Clear();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT ItemName FROM Inventory ORDER BY ItemName ASC";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbProduct.Items.Add(reader["ItemName"].ToString());
                    }
                }
            }

            if (cmbProduct.Items.Count > 0)
                cmbProduct.SelectedIndex = 0; // تحديد أول منتج افتراضيًا
        }

    }




    /*********************************************************************/
}

