using System;
using System.Data.SQLite;
using System.Windows.Forms;
using AnimalFeedApp.Helpers;

namespace AnimalFeedApp.Forms
{
    public partial class FormEditItem : Form
    {
        private int itemId;
        private Label lblName;
        private Label lblQuantity;
        private Label lblPrice;
        private TextBox txtName;
        private NumericUpDown numQuantity;
        private NumericUpDown numPrice;
        private Button btnSave;
        private Button btnCancel;
    

        public FormEditItem(int id, string itemName = "", decimal quantity = 0, decimal price = 0)
        {
            InitializeComponent();

            numQuantity.Minimum = 0;
            numQuantity.Maximum = 100000;


            itemId = id;
            txtName.Text = itemName;

            //************************************
            if (quantity < numQuantity.Minimum)
                numQuantity.Value = numQuantity.Minimum;
            else if (quantity > numQuantity.Maximum)
                numQuantity.Value = numQuantity.Maximum;
            else
                numQuantity.Value = quantity;
            //***********************
            numPrice.Value = price;
        }

        private void InitializeComponent()
        {
            this.lblName = new Label();
            this.lblQuantity = new Label();
            this.lblPrice = new Label();
            this.txtName = new TextBox();
            this.numQuantity = new NumericUpDown();
            this.numPrice = new NumericUpDown();
            this.btnSave = new Button();
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
            this.lblQuantity.Text = "الكمية:";

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

            // btnSave
            this.btnSave.BackColor = System.Drawing.Color.SeaGreen;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(150, 190);
            this.btnSave.Size = new System.Drawing.Size(95, 35);
            this.btnSave.Text = "💾 حفظ";
            this.btnSave.Click += BtnSave_Click;

            // btnCancel
            this.btnCancel.BackColor = System.Drawing.Color.IndianRed;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(255, 190);
            this.btnCancel.Size = new System.Drawing.Size(95, 35);
            this.btnCancel.Text = "❌ إلغاء";
            this.btnCancel.Click += BtnCancel_Click;

            // FormEditItem
            this.ClientSize = new System.Drawing.Size(400, 260);
            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
            this.Controls.Add(lblQuantity);
            this.Controls.Add(numQuantity);
            this.Controls.Add(lblPrice);
            this.Controls.Add(numPrice);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "تعديل عنصر في المخزون";

            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
           
            decimal quantity = numQuantity.Value;
           

            // ✅ التحقق من أن الكمية غير سالبة
            if (quantity < 0)
            {
                MessageBox.Show("🚫 لا يمكن أن تكون الكمية سالبة.", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("UPDATE Inventory SET ItemName=@name, Quantity=@qty, PricePerUnit=@price WHERE Id=@id", conn);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@qty", numQuantity.Value);
                cmd.Parameters.AddWithValue("@price", numPrice.Value);
                cmd.Parameters.AddWithValue("@id", itemId);
                cmd.ExecuteNonQuery();
            }
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
