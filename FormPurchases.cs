using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using AnimalFeedApp.Helpers;

namespace AnimalFeedApp.Forms
{
    public partial class FormPurchases : Form
    {
        private DataGridView dgvPurchases;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnRefresh;

        public FormPurchases()
        {
            InitializeComponent();
            LoadPurchases();
        }

        private void InitializeComponent()
        {
            this.Text = "📦 إدارة المشتريات";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;
            this.RightToLeft = RightToLeft.Yes;
            this.RightToLeftLayout = true;

            // 🟦 إنشاء DataGridView
            dgvPurchases = new DataGridView()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                BackgroundColor = Color.White,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ColumnHeadersVisible = true,
                EnableHeadersVisualStyles = false
            };

            // 🎨 تنسيق رؤوس الأعمدة
            dgvPurchases.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dgvPurchases.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPurchases.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 70, 160);
            dgvPurchases.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPurchases.ColumnHeadersHeight = 40;

            // 🎨 تنسيق الخلايا
            dgvPurchases.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPurchases.DefaultCellStyle.Font = new Font("Segoe UI", 11F);

            // 🔹 لوحة الأزرار في الأعلى
            var panelTop = new FlowLayoutPanel()
            {
                Dock = DockStyle.Top,
                Height = 60,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(10),
                BackColor = Color.FromArgb(0, 70, 160)
            };

            // 🔘 إنشاء الأزرار
            btnAdd = CreateButton("➕ إضافة شراء");
            btnEdit = CreateButton("✏️ تعديل شراء");
            btnDelete = CreateButton("🗑️ حذف شراء");
            btnRefresh = CreateButton("🔄 تحديث");

            // 🔗 ربط الأحداث
            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnRefresh.Click += BtnRefresh_Click;

            // ➕ إضافة الأزرار إلى اللوحة
            panelTop.Controls.AddRange(new Control[] { btnAdd, btnEdit, btnDelete, btnRefresh });

            // ➕ إضافة المكونات إلى الفورم
            this.Controls.Add(dgvPurchases);
            this.Controls.Add(panelTop);
        }

        private Button CreateButton(string text)
        {
            return new Button()
            {
                Text = text,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(0, 120, 215),
                FlatStyle = FlatStyle.Flat,
                AutoSize = true,
                Margin = new Padding(10),
                Padding = new Padding(10),
                Anchor = AnchorStyles.Top
            };
        }

        private void LoadPurchases()
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    var query = @"
                        SELECT 
                            Id,
                            SupplierName AS 'اسم المورد',
                            ItemName AS 'اسم المنتج',
                            Quantity AS 'الكمية',
                            TotalCost AS 'السعر الإجمالي',
                            PurchaseDate AS 'تاريخ الشراء'
                        FROM Purchases";

                    using (var adapter = new SQLiteDataAdapter(query, conn))
                    {
                        var table = new DataTable();
                        adapter.Fill(table);
                        dgvPurchases.DataSource = table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("⚠️ خطأ أثناء تحميل المشتريات:\n" + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

       

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPurchases.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("هل أنت متأكد من حذف عملية الشراء؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dgvPurchases.SelectedRows[0].Cells["Id"].Value);

                    using (var conn = DatabaseHelper.GetConnection())
                    {
                        conn.Open();
                        var cmd = new SQLiteCommand("DELETE FROM Purchases WHERE Id = @id", conn);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }

                    LoadPurchases();
                    MessageBox.Show("🗑️ تم حذف عملية الشراء بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("⚠️ يرجى اختيار عملية شراء لحذفها.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadPurchases();
        }

        private DataTable GetSuppliers()
        {
            DataTable table = new DataTable();
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT DISTINCT SupplierName FROM Purchases ORDER BY SupplierName ASC";
                    using (var adapter = new SQLiteDataAdapter(query, conn))
                    {
                        adapter.Fill(table);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("⚠️ خطأ أثناء تحميل الموردين:\n" + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return table;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var suppliers = GetSuppliers(); // ✅ تحميل الموردين
            var addForm = new FormAddPurchase(suppliers); // تمريرهم إلى الفورم
            addForm.ShowDialog();
            LoadPurchases();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPurchases.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvPurchases.SelectedRows[0].Cells["Id"].Value);
                string supplier = dgvPurchases.SelectedRows[0].Cells["اسم المورد"].Value.ToString();
                string item = dgvPurchases.SelectedRows[0].Cells["اسم المنتج"].Value.ToString();
                double quantity = Convert.ToDouble(dgvPurchases.SelectedRows[0].Cells["الكمية"].Value);
                double totalCost = Convert.ToDouble(dgvPurchases.SelectedRows[0].Cells["السعر الإجمالي"].Value);

                var suppliers = GetSuppliers(); // ✅ نفس الشيء عند التعديل
                var editForm = new FormEditPurchase(id, supplier, item, quantity, totalCost, suppliers);
                editForm.ShowDialog();
                LoadPurchases();
            }
            else
            {
                MessageBox.Show("⚠️ يرجى اختيار عملية شراء لتعديلها.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




    }
}
