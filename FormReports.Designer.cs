namespace AnimalFeedApp.Forms
{
    partial class FormReports
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ComboBox comboReportType;
        private System.Windows.Forms.ComboBox comboDuration;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridTotals;
        private System.Windows.Forms.Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.comboReportType = new System.Windows.Forms.ComboBox();
            this.comboDuration = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridTotals = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTotals)).BeginInit();
            this.SuspendLayout();
            // 
            // comboReportType
            // 
            this.comboReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboReportType.FormattingEnabled = true;
            this.comboReportType.Location = new System.Drawing.Point(180, 25);
            this.comboReportType.Name = "comboReportType";
            this.comboReportType.Size = new System.Drawing.Size(200, 28);
            this.comboReportType.TabIndex = 0;
            this.comboReportType.SelectedIndexChanged += new System.EventHandler(this.comboReportType_SelectedIndexChanged);
            // 
            // comboDuration
            // 
            this.comboDuration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDuration.FormattingEnabled = true;
            this.comboDuration.Location = new System.Drawing.Point(410, 25);
            this.comboDuration.Name = "comboDuration";
            this.comboDuration.Size = new System.Drawing.Size(200, 28);
            this.comboDuration.TabIndex = 1;
            this.comboDuration.SelectedIndexChanged += new System.EventHandler(this.comboDuration_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(40, 75);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(720, 350);
            this.dataGridView1.TabIndex = 2;
            // 
            // dataGridTotals
            // 
            this.dataGridTotals.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.dataGridTotals.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridTotals.BackgroundColor = System.Drawing.Color.LightYellow;
            this.dataGridTotals.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridTotals.ColumnHeadersVisible = false;
            this.dataGridTotals.Location = new System.Drawing.Point(40, 440);
            this.dataGridTotals.Name = "dataGridTotals";
            this.dataGridTotals.ReadOnly = true;
            this.dataGridTotals.RowHeadersVisible = false;
            this.dataGridTotals.Size = new System.Drawing.Size(720, 90);
            this.dataGridTotals.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(330, 540);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(140, 40);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "إغلاق";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FormReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dataGridTotals);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.comboDuration);
            this.Controls.Add(this.comboReportType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MinimumSize = new System.Drawing.Size(820, 640);
            this.Name = "FormReports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "📊 التقارير والإحصائيات";
            this.Load += new System.EventHandler(this.FormReports_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTotals)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
