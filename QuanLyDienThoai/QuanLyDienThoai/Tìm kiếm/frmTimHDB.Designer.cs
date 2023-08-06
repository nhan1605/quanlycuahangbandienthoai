namespace QuanLyDienThoai
{
    partial class frmTimHDB
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.MaHDB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaKH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtMKH = new System.Windows.Forms.TextBox();
            this.txtMNV = new System.Windows.Forms.TextBox();
            this.txtMHD = new System.Windows.Forms.TextBox();
            this.MKH = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ngayban = new System.Windows.Forms.Label();
            this.MHD = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btndong = new System.Windows.Forms.Button();
            this.btntl = new System.Windows.Forms.Button();
            this.btntk = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtThang = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tong = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaHDB,
            this.ngay,
            this.MNV,
            this.TT,
            this.MaKH});
            this.dataGridView1.Location = new System.Drawing.Point(169, 284);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(672, 203);
            this.dataGridView1.TabIndex = 39;
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            // 
            // MaHDB
            // 
            this.MaHDB.DataPropertyName = "MaHDB";
            this.MaHDB.HeaderText = "Mã HDBan";
            this.MaHDB.MinimumWidth = 6;
            this.MaHDB.Name = "MaHDB";
            this.MaHDB.Width = 125;
            // 
            // ngay
            // 
            this.ngay.DataPropertyName = "Ngayban";
            this.ngay.HeaderText = "Ngày bán";
            this.ngay.MinimumWidth = 6;
            this.ngay.Name = "ngay";
            this.ngay.Width = 125;
            // 
            // MNV
            // 
            this.MNV.DataPropertyName = "MaNV";
            this.MNV.HeaderText = "Mã NV";
            this.MNV.MinimumWidth = 6;
            this.MNV.Name = "MNV";
            this.MNV.Width = 125;
            // 
            // TT
            // 
            this.TT.DataPropertyName = "Tongtien";
            this.TT.HeaderText = "Tông tiền";
            this.TT.MinimumWidth = 6;
            this.TT.Name = "TT";
            this.TT.Width = 125;
            // 
            // MaKH
            // 
            this.MaKH.DataPropertyName = "MaKH";
            this.MaKH.HeaderText = "Mã KH";
            this.MaKH.MinimumWidth = 6;
            this.MaKH.Name = "MaKH";
            this.MaKH.Width = 125;
            // 
            // txtMKH
            // 
            this.txtMKH.Location = new System.Drawing.Point(666, 191);
            this.txtMKH.Name = "txtMKH";
            this.txtMKH.Size = new System.Drawing.Size(188, 22);
            this.txtMKH.TabIndex = 38;
            // 
            // txtMNV
            // 
            this.txtMNV.Location = new System.Drawing.Point(666, 134);
            this.txtMNV.Name = "txtMNV";
            this.txtMNV.Size = new System.Drawing.Size(188, 22);
            this.txtMNV.TabIndex = 37;
            // 
            // txtMHD
            // 
            this.txtMHD.Location = new System.Drawing.Point(311, 134);
            this.txtMHD.Name = "txtMHD";
            this.txtMHD.Size = new System.Drawing.Size(164, 22);
            this.txtMHD.TabIndex = 35;
            // 
            // MKH
            // 
            this.MKH.AutoSize = true;
            this.MKH.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MKH.Location = new System.Drawing.Point(512, 191);
            this.MKH.Name = "MKH";
            this.MKH.Size = new System.Drawing.Size(122, 20);
            this.MKH.TabIndex = 34;
            this.MKH.Text = "Mã khách hàng";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(512, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 20);
            this.label4.TabIndex = 33;
            this.label4.Text = "Mã nhân viên";
            // 
            // ngayban
            // 
            this.ngayban.AutoSize = true;
            this.ngayban.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ngayban.Location = new System.Drawing.Point(156, 191);
            this.ngayban.Name = "ngayban";
            this.ngayban.Size = new System.Drawing.Size(79, 20);
            this.ngayban.TabIndex = 32;
            this.ngayban.Text = "Ngày bán";
            // 
            // MHD
            // 
            this.MHD.AutoSize = true;
            this.MHD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MHD.Location = new System.Drawing.Point(156, 136);
            this.MHD.Name = "MHD";
            this.MHD.Size = new System.Drawing.Size(128, 20);
            this.MHD.TabIndex = 31;
            this.MHD.Text = "Mã hóa đơn bán";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(312, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(419, 43);
            this.label1.TabIndex = 30;
            this.label1.Text = "TÌM KIẾM HÓA ĐƠN BÁN";
            // 
            // btndong
            // 
            this.btndong.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndong.Image = global::QuanLyDienThoai.Properties.Resources.btn_Thoat;
            this.btndong.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btndong.Location = new System.Drawing.Point(673, 502);
            this.btndong.Name = "btndong";
            this.btndong.Size = new System.Drawing.Size(117, 57);
            this.btndong.TabIndex = 42;
            this.btndong.Text = "Đóng";
            this.btndong.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btndong.UseVisualStyleBackColor = true;
            this.btndong.Click += new System.EventHandler(this.btndong_Click);
            // 
            // btntl
            // 
            this.btntl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btntl.Image = global::QuanLyDienThoai.Properties.Resources.btnHuybo;
            this.btntl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btntl.Location = new System.Drawing.Point(427, 502);
            this.btntl.Name = "btntl";
            this.btntl.Size = new System.Drawing.Size(117, 57);
            this.btntl.TabIndex = 41;
            this.btntl.Text = "Tìm lại";
            this.btntl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btntl.UseVisualStyleBackColor = true;
            this.btntl.Click += new System.EventHandler(this.btntl_Click);
            // 
            // btntk
            // 
            this.btntk.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btntk.Image = global::QuanLyDienThoai.Properties.Resources.btn_Tìm_kiếm;
            this.btntk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btntk.Location = new System.Drawing.Point(218, 502);
            this.btntk.Name = "btntk";
            this.btntk.Size = new System.Drawing.Size(117, 57);
            this.btntk.TabIndex = 40;
            this.btntk.Text = "Tìm kiếm";
            this.btntk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btntk.UseVisualStyleBackColor = true;
            this.btntk.Click += new System.EventHandler(this.btntk_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Consolas", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label8.Location = new System.Drawing.Point(147, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(699, 43);
            this.label8.TabIndex = 49;
            this.label8.Text = "NHẬP ÍT NHẤT 1 DỮ LIỆU ĐỂ TÌM KIẾM";
            // 
            // txtThang
            // 
            this.txtThang.Location = new System.Drawing.Point(311, 184);
            this.txtThang.Name = "txtThang";
            this.txtThang.Size = new System.Drawing.Size(164, 22);
            this.txtThang.TabIndex = 50;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(327, 238);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 20);
            this.label2.TabIndex = 43;
            this.label2.Text = "Tổng tiền";
            // 
            // tong
            // 
            this.tong.Location = new System.Drawing.Point(427, 238);
            this.tong.Name = "tong";
            this.tong.Size = new System.Drawing.Size(217, 22);
            this.tong.TabIndex = 44;
            this.tong.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tong_KeyPress);
            // 
            // frmTimHDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::QuanLyDienThoai.Properties.Resources.abc;
            this.ClientSize = new System.Drawing.Size(1196, 619);
            this.Controls.Add(this.txtThang);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tong);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btndong);
            this.Controls.Add(this.btntl);
            this.Controls.Add(this.btntk);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtMKH);
            this.Controls.Add(this.txtMNV);
            this.Controls.Add(this.txtMHD);
            this.Controls.Add(this.MKH);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ngayban);
            this.Controls.Add(this.MHD);
            this.Controls.Add(this.label1);
            this.Name = "frmTimHDB";
            this.Text = "frmTimHDB";
            this.Load += new System.EventHandler(this.frmTimHDB_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btndong;
        private System.Windows.Forms.Button btntl;
        private System.Windows.Forms.Button btntk;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaHDB;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngay;
        private System.Windows.Forms.DataGridViewTextBoxColumn MNV;
        private System.Windows.Forms.DataGridViewTextBoxColumn TT;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaKH;
        private System.Windows.Forms.TextBox txtMKH;
        private System.Windows.Forms.TextBox txtMNV;
        private System.Windows.Forms.TextBox txtMHD;
        private System.Windows.Forms.Label MKH;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label ngayban;
        private System.Windows.Forms.Label MHD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtThang;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tong;
    }
}