using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using COMExcel = Microsoft.Office.Interop.Excel;

namespace QuanLyDienThoai
{
    public partial class frmHDB : Form
    {
        public frmHDB()
        {
            InitializeComponent();
        }
        DataTable tblHDB;
        private void frmHDB_Load(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnIn.Enabled = false;
            txtMaHoaDon.ReadOnly = true;
            txtTenNhanVien.ReadOnly = true;
            txtTenKhachHang.ReadOnly = true;
            txtDiaChi.ReadOnly = true;
            btnXoa.Enabled = false;
            txtTenSanPham.ReadOnly = true;
            txtDonGia.ReadOnly = true;
            txtThanhTien.ReadOnly = true;
            txtTongTien.ReadOnly = true;
            txtKhuyenMai.Text = "0";
            txtTongTien.Text = "0";
            functions.FIllcombo("SELECT MaKH, TenKH FROM tblKhach", cboMaKhachhang, "MaKH", "MaKH");
            cboMaKhachhang.SelectedIndex = -1;
            functions.FIllcombo("SELECT MaNV, TenNV FROM tblNhanvien", cboMaNhanVien, "MaNV", "MaNV");
            cboMaNhanVien.SelectedIndex = -1;
            functions.FIllcombo("SELECT MaSP, TenSP FROM tblSanpham", cboMaSanPham, "MaSP", "MaSP");
            cboMaSanPham.SelectedIndex = -1;
            functions.FIllcombo("Select MaHDB from tblHDBan", cbomahdb, "MaHDB", "MaHDB");
            cbomahdb.SelectedIndex = -1;
            //Hiển thị thông tin của một hóa đơn được gọi từ form tìm kiếm
            if (txtMaHoaDon.Text != "")
            {
                LoadInfoHoadon();
                btnHuy.Enabled = true;
                btnIn.Enabled = true;
            }
            LoadDataGridView();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void LoadInfoHoadon()//gọi sự kiện có tác dụng giống resetvalue, tự động đẩy các giá trị 
        {
            string str;
            str = "SELECT Ngayban FROM tblHDBan WHERE MaHDB = N'" + txtMaHoaDon.Text + "'";
            mskNgayBan.Text = (functions.GetFileValues(str));

            str = "SELECT MaNV FROM tblHDBan WHERE MaHDB = N'" + txtMaHoaDon.Text + "'";
            cboMaNhanVien.Text = functions.GetFileValues(str);

            str = "SELECT MaKH FROM tblHDBan WHERE MaHDB = N'" + txtMaHoaDon.Text + "'";
            cboMaKhachhang.Text = functions.GetFileValues(str);

            str = "SELECT Tongtien FROM tblHDBan WHERE MaHDB = N'" + txtMaHoaDon.Text + "'";
            txtTongTien.Text = functions.GetFileValues(str);
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT b.MaSP, b.TenSP, a.soluong, a.dongia, a.khuyenmai,a.thanhtien FROM tblchitietHDBan AS a, tblSanpham AS b WHERE a.MaHDB = N'" + txtMaHoaDon.Text + "' AND a.MaSP=b.MaSP";
            tblHDB = functions.GetDataToTable(sql);
            dgridHoaDonBan.DataSource = tblHDB;
            dgridHoaDonBan.Columns[0].HeaderText = "Mã sản phẩm";
            dgridHoaDonBan.Columns[1].HeaderText = "Tên sản phẩm";
            dgridHoaDonBan.Columns[2].HeaderText = "Số lượng";
            dgridHoaDonBan.Columns[3].HeaderText = "Đơn giá";
            dgridHoaDonBan.Columns[4].HeaderText = "Khuyến mại";
            dgridHoaDonBan.Columns[5].HeaderText = "Thành tiền";
            dgridHoaDonBan.Columns[0].Width = 80;
            dgridHoaDonBan.Columns[1].Width = 130;
            dgridHoaDonBan.Columns[2].Width = 80;
            dgridHoaDonBan.Columns[3].Width = 90;
            dgridHoaDonBan.Columns[4].Width = 90;
            dgridHoaDonBan.Columns[5].Width = 90;
            dgridHoaDonBan.AllowUserToAddRows = false;
            dgridHoaDonBan.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dgridHoaDonBan_Click(object sender, EventArgs e)
        {

        }

        private void dgridHoaDonBan_DoubleClick(object sender, EventArgs e)
        {
            
            string ma;
            ma = dgridHoaDonBan.CurrentRow.Cells[0].Value.ToString();
            DelHang(txtMaHoaDon.Text, ma);
            string thanhtien;
            thanhtien = dgridHoaDonBan.CurrentRow.Cells[5].Value.ToString();
            DelUpdateTongtien(txtMaHoaDon.Text, Convert.ToDouble(thanhtien));
            ResetValuesSP();
           // ResetValues();
            LoadDataGridView();
            ResetValues();
        }
        // Hàm xóa sản phẩm trong tblSanpham khi bán
        private void DelHang(string Mahoadon, string Masp)
        {
            string sql;
            Double s, sl, slCon;
            sql = " select tblchitietHDBan.soluong from tblchitietHDBan where MaHDB = '" + Mahoadon + "' and MaSP = '" + Masp + "'";

            s = Convert.ToDouble(functions.GetFileValues(sql));

            sql = "delete tblchitietHDBan  where MaHDB = '" + Mahoadon + "' and MaSP = '" + Masp + "'";

            functions.RunsqlDel(sql);

            // Cap nhat so luong

            sql = "select Soluong from tblSanpham where MaSP = '" + Masp + "'";
            sl = Convert.ToDouble(functions.GetFileValues(sql));
            slCon = s + sl;
            sql = "update tblSanpham set Soluong = '" + slCon + "' where MaSP = '" + Masp + "'";
            functions.Runsql(sql);
        }
        private void DelUpdateTongtien(string Mahoadon, double Thanhtien)
        {
            Double Tong, Tongmoi;
            string sql;

            sql = "SELECT Tongtien FROM tblHDBan WHERE MaHDB = N'" + Mahoadon + "'";
            Tong = Convert.ToDouble(functions.GetFileValues(sql));
            Tongmoi = Tong + Thanhtien;
            sql = "UPDATE tblHDBan SET Tongtien =" + Tongmoi + " WHERE MaHDB = N'" + Mahoadon + "'";
            functions.Runsql(sql);
            txtTongTien.Text = Tongmoi.ToString();
            lblBangChu.Text = "Bằng chữ: " + functions.ChuyenSoSangChu(Tongmoi.ToString());
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnIn.Enabled = false;
            btnThem.Enabled = false;
            mskNgayBan.Enabled = false;
            txtMaHoaDon.Enabled = false;
            //  mskNgayBan.Text = DateTime.Now.ToString();
            btnHuy.Enabled = true;

            txtMaHoaDon.Text = functions.CreateKey("HDB");
            LoadDataGridView();
        }
        private void ResetValues()
        {
            txtMaHoaDon.Text = "";
            mskNgayBan.Text = DateTime.Now.ToString();
            cboMaNhanVien.Text = "";
            cboMaKhachhang.Text = "";
            txtTongTien.Text = "0";
            txtTenKhachHang.Text = "";
            txtDiaChi.Text = "";
            lblBangChu.Text = "Bằng chữ: ";
            cboMaSanPham.Text = "";
            txtSoLuong.Text = "";
            txtKhuyenMai.Text = "0";
            txtTenNhanVien.Text = ""; ;
            txtThanhTien.Text = "0";
        }
        private void ResetValuesSP()
        {
            cboMaSanPham.Text = "";
            txtSoLuong.Text = "";
            txtKhuyenMai.Text = "0";
            txtThanhTien.Text = "0";
            txtTenSanPham.Text = "";
            txtDonGia.Text = "";
            
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            double sl, SLcon, tong, Tongmoi;
            sql = "SELECT MaHDB FROM tblHDBan WHERE MaHDB=N'" + txtMaHoaDon.Text + "'";
            if (!functions.Checkey(sql))
            {
                // Mã hóa đơn chưa có, tiến hành lưu các thông tin chung
                // Mã HDBan được sinh tự động do đó không có trường hợp trùng khóa
                if (mskNgayBan.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập ngày bán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    mskNgayBan.Focus();
                    return;
                }
                if (cboMaNhanVien.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải chọn mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboMaNhanVien.Focus();
                    return;
                }
                if (cboMaKhachhang.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải chọn khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboMaKhachhang.Focus();
                    return;
                }
                //lưu thông tin chung vào bảng tblhdban    
                sql = "INSERT INTO tblHDBan(MaHDB, Ngayban, MaNV, MaKH, Tongtien) VALUES (N'" + txtMaHoaDon.Text.Trim() + "', '" +
                        functions.CovertToDate(mskNgayBan.Text) + "',N'" + cboMaNhanVien.Text + "',N'" +
                        cboMaKhachhang.Text + "'," + txtTongTien.Text + ")";
                functions.Runsql(sql);
            }

            // Lưu thông tin của các sản phẩm
            if (cboMaSanPham.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn mã sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaSanPham.Focus();
                return;
            }
            if ((txtSoLuong.Text.Trim().Length == 0) || (txtSoLuong.Text == "0"))
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuong.Text = "";
                txtSoLuong.Focus();
                return;
            }
            if (txtKhuyenMai.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập khuyến mại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKhuyenMai.Focus();
                return;
            }
            sql = "SELECT MaSP FROM tblchitietHDBan WHERE MaSP=N'" + cboMaSanPham.SelectedValue + "' AND MaHDB = N'" + txtMaHoaDon.Text.Trim() + "'";
            if (functions.Checkey(sql))
            {
                MessageBox.Show("Mã sản phẩm này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ResetValuesSP();
                cboMaSanPham.Focus();
                return;
            }
            // Kiểm tra xem số lượng hàng trong kho còn đủ để cung cấp không?
            sl = Convert.ToDouble(functions.GetFileValues("SELECT Soluong FROM tblSanpham WHERE MaSP = N'" + cboMaSanPham.SelectedValue + "'"));
            if (Convert.ToDouble(txtSoLuong.Text) > sl)
            {
                MessageBox.Show("Số lượng mặt hàng này chỉ còn " + sl, "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Text = "";
                txtSoLuong.Focus();
                return;
            }
            sql = "INSERT INTO tblchitietHDBan(MaHDB,MaSP,soluong,dongia,thanhtien,khuyenmai) VALUES(N'" + txtMaHoaDon.Text.Trim() + "', N'" + cboMaSanPham.Text + "'," + txtSoLuong.Text + ",'" + txtDonGia.Text + "'," + txtThanhTien.Text + "," + txtKhuyenMai.Text + ")";
            functions.Runsql(sql);
            LoadDataGridView();
            // Cập nhật lại số lượng của mặt hàng vào bảng tblSanpham
            SLcon = sl - Convert.ToDouble(txtSoLuong.Text);
            sql = "UPDATE tblSanpham SET Soluong =" + SLcon + " WHERE MaSP= N'" + cboMaSanPham.SelectedValue + "'";
            functions.Runsql(sql);
            // Cập nhật lại tổng tiền cho hóa đơn bán

            tong = Convert.ToDouble(functions.GetFileValues("SELECT Tongtien FROM tblHDBan WHERE MaHDB = N'" + txtMaHoaDon.Text + "'"));
            Tongmoi = tong + Convert.ToDouble(txtThanhTien.Text);
            sql = "UPDATE tblHDBan SET Tongtien =" + Tongmoi + " WHERE MaHDB = N'" + txtMaHoaDon.Text + "'";
            functions.Runsql(sql);
            txtTongTien.Text = Tongmoi.ToString();
            lblBangChu.Text = "Bằng chữ: " + functions.ChuyenSoSangChu(Tongmoi.ToString());
            ResetValuesSP();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnIn.Enabled = true;
            functions.FIllcombo("Select MaHDB from tblHDBan", cbomahdb, "MaHDB", "MaHDB");
            cbomahdb.SelectedIndex = -1;

        }

        private void cboMaNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboMaNhanVien_TextChanged(object sender, EventArgs e)
        {
            string sql;
            if (cboMaNhanVien.Text == "")
                txtTenNhanVien.Text = "";
            // Khi chọn Mã nhân viên thì tên nhân viên tự động hiện ra
            sql = "Select TenNV from tblNhanvien where MaNV =N'" + cboMaNhanVien.SelectedValue + "'";
            txtTenNhanVien.Text = functions.GetFileValues(sql);
        }

        private void cboMaKhachhang_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboMaKhachhang_TextChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaKhachhang.Text == "")
            {
                txtTenKhachHang.Text = "";
                txtDiaChi.Text = "";

            }
            //Khi kich chon Ma khach thi ten khach, dia chi, dien thoai se tu dong hien ra
            str = "Select TenKH from tblKhach where MaKH = N'" + cboMaKhachhang.SelectedValue + "'";
            txtTenKhachHang.Text = functions.GetFileValues(str);

            str = "Select DiachiKH from tblKhach where MaKH = N'" + cboMaKhachhang.SelectedValue + "'";
            txtDiaChi.Text = functions.GetFileValues(str);
        }

        private void cboMaSanPham_TextChanged(object sender, EventArgs e)
        {

            string sql;
            if (cboMaSanPham.Text == "")
            {
                txtTenSanPham.Text = "";
                txtDonGia.Text = "";
            }
            // Khi chọn mã sản phẩm thì các thông tin về sản phẩm hiện ra
            sql = "SELECT TenSP FROM tblSanpham WHERE MaSP =N'" + cboMaSanPham.SelectedValue + "'";
            txtTenSanPham.Text = functions.GetFileValues(sql);
            sql = "SELECT Giaban FROM tblSanpham WHERE MaSP =N'" + cboMaSanPham.SelectedValue + "'";
            txtDonGia.Text = functions.GetFileValues(sql);
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            double tt, sl, dg, km;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtKhuyenMai.Text == "")
                km = 0;
            else
                km = Convert.ToDouble(txtKhuyenMai.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = sl * dg - sl * dg * km / 100;
            txtThanhTien.Text = tt.ToString();
        }

        private void txtKhuyenMai_TextChanged(object sender, EventArgs e)
        {
            double tt, sl, dg, km;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtKhuyenMai.Text == "")
                km = 0;
            else
                km = Convert.ToDouble(txtKhuyenMai.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = sl * dg - sl * dg * km / 100;
            txtThanhTien.Text = tt.ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa một hóa đơn không?", "Thông báo",
         MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string[] Masp = new string[20];
                string sql;
                int n = 0;
                int i;
                sql = "SELECT MaSP FROM tblchitietHDBan WHERE MaHDB = N'" + txtMaHoaDon.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, functions.con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Masp[n] = reader.GetString(0).ToString();
                    n = n + 1;
                }
                reader.Close();
                //Xóa danh sách các mặt hàng của hóa đơn
                for (i = 0; i <= n - 1; i++)
                    DelHang(txtMaHoaDon.Text, Masp[i]);
                //Xóa hóa đơn
                sql = "DELETE tblHDBan WHERE MaHDB=N'" + txtMaHoaDon.Text + "'";
                functions.RunsqlDel(sql);
                ResetValues();
                mskNgayBan.Text = "";
                LoadDataGridView();
                btnHuy.Enabled = false;
                btnIn.Enabled = false;
                functions.FIllcombo("Select MaHDB from tblHDBan", cbomahdb, "MaHDB", "MaHDB");
                cbomahdb.SelectedIndex = -1;
                btnXoa.Enabled = false;
                btnLuu.Enabled = false;
                dgridHoaDonBan.DataSource = null;
            }

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = false;
            btnIn.Enabled = false;
            btnLuu.Enabled = false;
            txtMaHoaDon.Enabled = false;
            mskNgayBan.Text = "";
            dgridHoaDonBan.DataSource = null;
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            if (cbomahdb.Text == "")
            {
                MessageBox.Show("Bạn phải chọn một mã hóa đơn để tìm", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbomahdb.Focus();
                return;
            }
            txtMaHoaDon.Text = cbomahdb.Text;
            LoadInfoHoadon();
            LoadDataGridView();
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
            btnIn.Enabled = true;
            btnXoa.Enabled = true;
            lblBangChu.Text = "Bằng chữ: " + functions.ChuyenSoSangChu(txtTongTien.Text);

        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtKhuyenMai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {

            // Khởi động chương trình Excel
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
            COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
            COMExcel.Range exRange;
            string sql;
            int hang = 0, cot = 0;
            DataTable tblThongtinHD, tblThongtinSP;
            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];
            // Định dạng chung
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:B3"].Font.Size = 10;
            exRange.Range["A1:B3"].Font.Name = "Times new roman";
            exRange.Range["A1:B3"].Font.Bold = true;
            exRange.Range["A1:B3"].Font.ColorIndex = 5; //Màu xanh da trời
            exRange.Range["A1:A1"].ColumnWidth = 7;
            exRange.Range["B1:B1"].ColumnWidth = 15;
            exRange.Range["A1:B1"].MergeCells = true;
            exRange.Range["A1:B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A1:B1"].Value = "Cửa hàng bán điện thoại";

            exRange.Range["A2:B2"].MergeCells = true;
            exRange.Range["A2:B2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:B2"].Value = "Chùa Bộc - Hà Nội";

            exRange.Range["A3:B3"].MergeCells = true;
            exRange.Range["A3:B3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A3:B3"].Value = "Điện thoại: 0999888444";


            exRange.Range["C2:E2"].Font.Size = 16;
            exRange.Range["C2:E2"].Font.Name = "Times new roman";
            exRange.Range["C2:E2"].Font.Bold = true;
            exRange.Range["C2:E2"].Font.ColorIndex = 3; //Màu đỏ
            exRange.Range["C2:E2"].MergeCells = true;
            exRange.Range["C2:E2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C2:E2"].Value = "HÓA ĐƠN BÁN";
            // Biểu diễn thông tin chung của hóa đơn bán
            sql = "SELECT a.MaHDB, a.Ngayban, a.Tongtien, b.TenKH, b.DiachiKH, c.TenNV FROM tblHDBan AS a, tblKhach AS b, tblNhanvien AS c WHERE a.MaHDB = N'" + txtMaHoaDon.Text + "' AND a.MaKH = b.MaKH AND a.MaNV =c.MaNV";
            tblThongtinHD = functions.GetDataToTable(sql);
            exRange.Range["B6:C9"].Font.Size = 12;
            exRange.Range["B6:C9"].Font.Name = "Times new roman";
            exRange.Range["B6:B6"].Value = "Mã hóa đơn:";
            exRange.Range["C6:E6"].MergeCells = true;
            exRange.Range["C6:E6"].Value = tblThongtinHD.Rows[0][0].ToString();
            exRange.Range["B7:B7"].Value = "Khách hàng:";
            exRange.Range["C7:E7"].MergeCells = true;
            exRange.Range["C7:E7"].Value = tblThongtinHD.Rows[0][3].ToString();
            exRange.Range["B8:B8"].Value = "Địa chỉ:";
            exRange.Range["C8:E8"].MergeCells = true;
            exRange.Range["C8:E8"].Value = tblThongtinHD.Rows[0][4].ToString();
            /*  exRange.Range["B9:B9"].Value = "Điện thoại:";
              exRange.Range["C9:C9"].Value = "'" + tblThongtinHD.Rows[0][5].ToString();*/

            //Lấy thông tin các mặt hàng
            sql = "SELECT b.TenSP, a.soluong, b.Giaban, a.khuyenmai, a.thanhtien " +
                  "FROM tblchitietHDBan AS a , tblSanpham AS b WHERE a.MaHDB = N'" +
                  txtMaHoaDon.Text + "' AND a.MaSP = b.MaSP";
            tblThongtinSP = functions.GetDataToTable(sql);
            //Tạo dòng tiêu đề bảng
            exRange.Range["A11:F11"].Font.Bold = true;
            exRange.Range["A11:F11"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C11:F11"].ColumnWidth = 12;
            exRange.Range["A11:A11"].Value = "STT";
            exRange.Range["B11:B11"].Value = "Tên sản phẩm";
            exRange.Range["C11:C11"].Value = "Số lượng";
            exRange.Range["D11:D11"].Value = "Đơn giá";
            exRange.Range["E11:E11"].Value = "Khuyến mại";
            exRange.Range["F11:F11"].Value = "Thành tiền";
            for (hang = 0; hang <= tblThongtinSP.Rows.Count - 1; hang++)
            {
                //Điền số thứ tự vào cột 1 từ dòng 12
                exSheet.Cells[1][hang + 12] = hang + 1;
                for (cot = 0; cot <= tblThongtinSP.Columns.Count - 1; cot++)
                    //Điền thông tin hàng từ cột thứ 2, dòng 12
                    exSheet.Cells[cot + 2][hang + 12] = tblThongtinSP.Rows[hang][cot].ToString();
            }
            exRange = exSheet.Cells[cot][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = "Tổng tiền:";
            exRange = exSheet.Cells[cot + 1][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = tblThongtinHD.Rows[0][2].ToString();
            exRange = exSheet.Cells[1][hang + 15]; //Ô A1 
            exRange.Range["A1:F1"].MergeCells = true;
            exRange.Range["A1:F1"].Font.Bold = true;
            exRange.Range["A1:F1"].Font.Italic = true;
            exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;
            exRange.Range["A1:F1"].Value = "Bằng chữ: " + functions.ChuyenSoSangChu(tblThongtinHD.Rows[0][2].ToString());
            exRange = exSheet.Cells[4][hang + 17]; //Ô A1 
            exRange.Range["A1:C1"].MergeCells = true;
            exRange.Range["A1:C1"].Font.Italic = true;
            exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            DateTime d = Convert.ToDateTime(tblThongtinHD.Rows[0][1]);
            exRange.Range["A1:C1"].Value = "Hà Nội, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
            exRange.Range["A2:C2"].MergeCells = true;
            exRange.Range["A2:C2"].Font.Italic = true;
            exRange.Range["A2:C2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:C2"].Value = "Nhân viên";
            exRange.Range["A6:C6"].MergeCells = true;
            exRange.Range["A6:C6"].Font.Italic = true;
            exRange.Range["A6:C6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A6:C6"].Value = tblThongtinHD.Rows[0][5];
            exSheet.Name = "Hóa đơn bán";
            exApp.Visible = true;
        }

        private void dgridHoaDonBan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    }

