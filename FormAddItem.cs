using System;
using System.Data.SQLite;
using System.Windows.Forms;
using AnimalFeedApp.Helpers;

namespace AnimalFeedApp.Forms
{
    public partial class FormAddItem : Form
    {
        private Label lblName;
        private Label lblQuantity;
        private Label lblPrice;
        private TextBox txtName;
        private NumericUpDown numQuantity;
        private NumericUpDown numPrice;
        private Button btnAdd;
        private Button btnCancel;

        public FormAddItem()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblName = new Label();
            this.lblQuantity = new Label();
            this.lblPrice = new Label();
            this.txtName = new TextBox();
            this.numQuantity = new NumericUpDown();
            this.numPrice = new NumericUpDown();
            this.btnAdd = new Button();
            this.btnCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).BeginInit();
            this.SuspendLayout();

            // lblName
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(30, 30);
            this.lblName.Text = "اسم العنصر:";

            // txtName
            this.txtName.Location = new System.Drawing.Point(150, 27);
            this.txtName.Size = new System.Drawing.Size(200, 22);

            // lblQuantity
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(30, 80);
            this.lblQuantity.Text = "الكمية (ق):"; // وحدة القنطار

            // numQuantity
            this.numQuantity.DecimalPlaces = 2;
            this.numQuantity.Location = new System.Drawing.Point(150, 78);
            this.numQuantity.Maximum = 100000;

            // lblPrice
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(30, 130);
            this.lblPrice.Text = "السعر:";

            // numPrice
            this.numPrice.DecimalPlaces = 2;
            this.numPrice.Location = new System.Drawing.Point(150, 128);
            this.numPrice.Maximum = 1000000;

            // btnAdd
            this.btnAdd.BackColor = System.Drawing.Color.SeaGreen;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(150, 190);
            this.btnAdd.Size = new System.Drawing.Size(95, 35);
            this.btnAdd.Text = "💾 إضافة";
            this.btnAdd.Click += BtnAdd_Click;

            // btnCancel
            this.btnCancel.BackColor = System.Drawing.Color.IndianRed;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(255, 190);
            this.btnCancel.Size = new System.Drawing.Size(95, 35);
            this.btnCancel.Text = "❌ إلغاء";
            this.btnCancel.Click += BtnCancel_Click;

            // FormAddItem
            this.ClientSize = new System.Drawing.Size(400, 260);
            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
            this.Controls.Add(lblQuantity);
            this.Controls.Add(numQuantity);
            this.Controls.Add(lblPrice);
            this.Controls.Add(numPrice);
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnCancel);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "إضافة عنصر جديد";

            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("يرجى إدخال اسم العنصر.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (numQuantity.Value <= 0)
            {
                MessageBox.Show("الكمية يجب أن تكون أكبر من صفر.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (numPrice.Value <= 0)
            {
                MessageBox.Show("السعر يجب أن يكون أكبر من صفر.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SQLiteCommand(
                        "INSERT INTO Inventory (ItemName, Quantity, PricePerUnit, DateAdded) VALUES (@name, @qty, @price, @date)",
                        conn
                    );
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@qty", numQuantity.Value);
                    cmd.Parameters.AddWithValue("@price", numPrice.Value);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show($"✅ تم إضافة العنصر '{txtName.Text}' بكمية {numQuantity.Value} ق وسعر {numPrice.Value} بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ أثناء الإضافة:\n" + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
