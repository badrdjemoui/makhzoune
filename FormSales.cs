using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using AnimalFeedApp.Helpers;

namespace AnimalFeedApp.Forms
{
    public partial class FormSales : Form
    {
        private DataGridView dgvSales;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnRefresh;

        public FormSales()
        {
            InitializeComponent();
            LoadSales();
        }

        private void InitializeComponent()
        {
            this.Text = "إدارة المبيعات";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;
            this.RightToLeft = RightToLeft.Yes;
            this.RightToLeftLayout = true;

            dgvSales = new DataGridView()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                BackgroundColor = Color.White,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            var panelTop = new FlowLayoutPanel()
            {
                Dock = DockStyle.Top,
                Height = 60,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(10),
                BackColor = Color.FromArgb(0, 70, 160)
            };

            btnAdd = CreateButton("➕ إضافة بيع");
            btnEdit = CreateButton("✏️ تعديل بيع");
            btnDelete = CreateButton("🗑️ حذف بيع");
            btnRefresh = CreateButton("🔄 تحديث");

            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnRefresh.Click += BtnRefresh_Click;

            panelTop.Controls.AddRange(new Control[] { btnAdd, btnEdit, btnDelete, btnRefresh });

            this.Controls.Add(dgvSales);
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

        private void LoadSales()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
              


                var query = @"SELECT 
                                Id,
                                CustomerName AS 'اسم الزبون',
                                ItemName AS 'اسم العنصر',
                                Quantity AS 'الكمية',
                                TotalPrice AS 'السعر الإجمالي',
                                SaleDate AS 'تاريخ البيع'
                              FROM Sales";

                using (var adapter = new SQLiteDataAdapter(query, conn))
                {
                    var table = new DataTable();
                    adapter.Fill(table);
                    dgvSales.DataSource = table;
                }
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var addForm = new FormAddSale();
            addForm.ShowDialog();
            LoadSales();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvSales.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvSales.SelectedRows[0].Cells["Id"].Value);
                string customer = dgvSales.SelectedRows[0].Cells["اسم الزبون"].Value.ToString();
                string item = dgvSales.SelectedRows[0].Cells["اسم العنصر"].Value.ToString();
                decimal quantity = Convert.ToDecimal(dgvSales.SelectedRows[0].Cells["الكمية"].Value);
                decimal total = Convert.ToDecimal(dgvSales.SelectedRows[0].Cells["السعر الإجمالي"].Value);

                var editForm = new FormEditSale(id, customer, item, (int)quantity, total);
                editForm.ShowDialog();
                LoadSales();
            }
            else
            {
                MessageBox.Show("يرجى اختيار عملية بيع لتعديلها.", "تنبيه");
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSales.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvSales.SelectedRows[0].Cells["Id"].Value);

                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SQLiteCommand("DELETE FROM Sales WHERE Id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }

                LoadSales();
            }
            else
            {
                MessageBox.Show("يرجى اختيار عملية بيع لحذفها.", "تنبيه");
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadSales();
        }
    }
}
