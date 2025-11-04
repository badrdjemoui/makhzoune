using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using AnimalFeedApp.Helpers;

namespace AnimalFeedApp.Forms
{
    public partial class FormInventory : Form
    {
        private DataGridView dgvInventory;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnRefresh;

        public FormInventory()
        {
            InitializeComponent();
            LoadInventory();
        }

        private void InitializeComponent()
        {
            this.Text = "إدارة المخزون";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;
            this.RightToLeft = RightToLeft.Yes;
            this.RightToLeftLayout = true;

            dgvInventory = new DataGridView()
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

            btnAdd = CreateButton("➕ إضافة");
            btnEdit = CreateButton("✏️ تعديل");
            btnDelete = CreateButton("🗑️ حذف");
            btnRefresh = CreateButton("🔄 تحديث");

            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnRefresh.Click += BtnRefresh_Click;

            panelTop.Controls.AddRange(new Control[] { btnAdd, btnEdit, btnDelete, btnRefresh });

            this.Controls.Add(dgvInventory);
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

        private void LoadInventory()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var query = "SELECT Id, ItemName AS 'اسم العنصر', Quantity AS 'الكمية', PricePerUnit AS 'السعر للوحدة', DateAdded AS 'تاريخ الإضافة' FROM Inventory";

               
                using (var adapter = new SQLiteDataAdapter(query, conn))
                {
                    var table = new DataTable();
                    adapter.Fill(table);
                    dgvInventory.DataSource = table;
                }
            }
        }


        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var addForm = new FormAddItem();
            addForm.ShowDialog();
            LoadInventory(); // إعادة تحميل الجدول بعد الإضافة
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvInventory.SelectedRows[0].Cells["Id"].Value);
                string itemName = dgvInventory.SelectedRows[0].Cells["اسم العنصر"].Value.ToString();
                decimal quantity = Convert.ToDecimal(dgvInventory.SelectedRows[0].Cells["الكمية"].Value);
                decimal price = Convert.ToDecimal(dgvInventory.SelectedRows[0].Cells["السعر للوحدة"].Value);

                var editForm = new FormEditItem(id, itemName, quantity, price);
                editForm.ShowDialog();

                LoadInventory();
            }
            else
            {
                MessageBox.Show("يرجى اختيار عنصر لتعديله.", "تنبيه");
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvInventory.SelectedRows[0].Cells["Id"].Value);
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SQLiteCommand("DELETE FROM Inventory WHERE Id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                LoadInventory();
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadInventory();
        }
    }
}
