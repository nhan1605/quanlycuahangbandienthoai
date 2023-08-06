using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using COMExcel = Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
namespace QuanLyDienThoai
{
    public partial class frmHDN : Form
    {
        public frmHDN()
        {
            InitializeComponent();
        }
        DataTable tblHDN;
        private void frmHDN_Load(object sender, EventArgs e)
        {
            btnThemHD.Enabled = true;
            btnLuuHD.Enabled = false;
            btnHuyHD.Enabled = false;
            btnInHD.Enabled = false;
            btnxoa.Enabled = false;
            txtMaHDN.ReadOnly = true;
            txtTenNV.ReadOnly = true;
            txtTenNCC.ReadOnly = true;
            txtDiachi.ReadOnly = true;
            mskDienthoai.ReadOnly = true;
            txtTenSP.ReadOnly = true;
            txtDongia.ReadOnly = true;
            txtTongtien.ReadOnly = true;
            txtThanhtien.ReadOnly = true;
            txtKhuyenmai.Text = "0";
            txtTongtien.Text = "0";
            functions.FIllcombo("Select MaHDN from tblHDNhap", cboSoHDN, "MaHDN", "MaHDN");
            cboSoHDN.SelectedIndex = -1;
            functions.FIllcombo("Select MaNV, TenNV from tblNhanvien", cboMaNV, "MaNV", "MaNV");
            cboMaNV.SelectedIndex = -1;
            functions.FIllcombo("Select MaNCC, TenNCC from tblNhacungcap", cboMaNCC, "MaNCC", "MaNCC");
            cboMaNCC.SelectedIndex = -1;
            functions.FIllcombo("Select MaSP from tblSanPham ", cboMaSP, "MaSP", "MaSP");
            cboMaSP.SelectedIndex = -1;
           
            if (txtMaHDN.Text != "")
            {
                LoadInfoHoaDon();
                btnHuyHD.Enabled = true;
                btnInHD.Enabled = true;
            }
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "Select a.MaSP, b.TenSP, a.Soluong, a.Dongia, a.Khuyenmai, a.Thanhtien from tblchitietHDNhap as a," +
                " tblSanpham as b where a.MaSP=b.MaSP and MaHDN='" + txtMaHDN.Text + "'";
            tblHDN = functions.GetDataToTable(sql);
            dgvHĐN.DataSource = tblHDN;
            dgvHĐN.Columns[0].HeaderText = "Mã SP";
            dgvHĐN.Columns[1].HeaderText = "Tên SP";
            dgvHĐN.Columns[2].HeaderText = "Số lượng";
            dgvHĐN.Columns[3].HeaderText = "Đơn giá";
            dgvHĐN.Columns[4].HeaderText = "Khuyến mại";
            dgvHĐN.Columns[5].HeaderText = "Thành tiền";
            dgvHĐN.Columns[0].Width = 80;
            dgvHĐN.Columns[1].Width = 130;
            dgvHĐN.Columns[2].Width = 80;
            dgvHĐN.Columns[3].Width = 90;
            dgvHĐN.Columns[4].Width = 90;
            dgvHĐN.Columns[5].Width = 90;
            dgvHĐN.EditMode = DataGridViewEditMode.EditProgrammatically;

        }
        private void LoadInfoHoaDon()
        {
            string str;
            str = "select Ngaynhap from tblHDNhap where MaHDN = '" + txtMaHDN.Text + "'";
            mskNgaynhap.Text = functions.GetFileValues(str);

            str = "Select MaNV from tblHDNhap where MaHDN=N'" + txtMaHDN.Text + "'";
            cboMaNV.Text = functions.GetFileValues(str);

            str = "Select MaNCC from tblHDNhap where MaHDN=N'" + txtMaHDN.Text + "'";
            cboMaNCC.Text = functions.GetFileValues(str);

            str = "Select Tongtien from tblHDNhap where MaHDN=N'" + txtMaHDN.Text + "'";
            txtTongtien.Text = functions.GetFileValues(str);
        }

        private void btnThemHD_Click(object sender, EventArgs e)
        {
            btnHuyHD.Enabled = true;
            btnxoa.Enabled = false;
            btnLuuHD.Enabled = true;
            btnInHD.Enabled = false;
            btnThemHD.Enabled = false;
            txtMaHDN.Enabled = false;
            txtMaHDN.Focus();
            ResetValues();
            mskNgaynhap.Text = DateTime.Now.ToString();
            txtMaHDN.Text = functions.CreateKey("HDN");
            LoadDataGridView();
        }
        private void ResetValues()
        {
            txtMaHDN.Text = "";
            mskNgaynhap.Text = DateTime.Today.ToString();
            lblBangchu.Text = "Bằng chữ :";
            cboMaNV.Text = "";
            txtTenNV.Text = "";

            cboMaNCC.Text = "";
            txtTongtien.Text = "0";
            cboMaSP.Text = "";
            txtSoluong.Text = "";
            txtKhuyenmai.Text = "0";
            txtThanhtien.Text = "0";
            mskNgaynhap.Text = "";
            
        }

        private void btnLuuHD_Click(object sender, EventArgs e)
        {
            string sql;
            double sl, SLcon, tong, Tongmoi;
            sql = "SELECT MaHDN FROM tblHDNhap WHERE MaHDN=N'" + txtMaHDN.Text + "'";
            if (!functions.Checkey(sql))
            {
                // Mã hóa đơn chưa có, tiến hành lưu các thông tin chung
                // Mã HDBan được sinh tự động do đó không có trường hợp trùng khóa
                if (mskNgaynhap.Text == "  -  -")
                {
                    MessageBox.Show("Bạn phải nhập ngày nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    mskNgaynhap.Focus();
                    return;
                }
                if (!functions.IsDate(mskNgaynhap.Text))
                {
                    MessageBox.Show("Bạn phải nhập lại ngày nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    mskNgaynhap.Focus();
                    return;
                }
                if (cboMaNV.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập nhân viên", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboMaNV.Focus();
                    return;
                }
                if (cboMaNCC.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập nhà cung cấp", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboMaNCC.Focus();
                    return;
                }
                sql = "INSERT INTO tblHDNhap(MaHDN,Ngaynhap, MaNV, MaNCC, Tongtien) VALUES(N'" + txtMaHDN.Text.Trim() + "',N'" +
                     functions.CovertToDate(mskNgaynhap.Text.Trim()) +
                    "',N'" + cboMaNV.SelectedValue + "', '" +
                    cboMaNCC.SelectedValue + "'," + txtTongtien.Text + ")";
                functions.Runsql(sql);

            }
            // Lưu thông tin của các mặt hàng
            if (cboMaSP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã sản phẩm", "Thông báo", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                cboMaSP.Focus();
                return;
            }
            if ((txtSoluong.Text.Trim().Length == 0) || (txtSoluong.Text == "0"))
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtSoluong.Text = "";
                txtSoluong.Focus();
                return;
            }
            if (txtKhuyenmai.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập khuyến mại", "Thông báo", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtKhuyenmai.Focus();
                return;
            }
            sql = "SELECT MaSP FROM tblchitietHDNhap WHERE MaSP=N'" +
                    cboMaSP.SelectedValue + "' AND MaHDN = N'" + txtMaHDN.Text.Trim() + "'";
            if (functions.Checkey(sql))
            {
                MessageBox.Show("Mã sản phẩm này đã có, bạn phải nhập mã khác", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ResetValuesHang();
                cboMaSP.Focus();
                return;
            }

            sl = Convert.ToDouble(functions.GetFileValues("SELECT Soluong FROM tblSanpham WHERE MaSP = N'" + cboMaSP.SelectedValue + "'"));

            sql = "INSERT INTO tblchitietHDNhap(MaHDN,MaSP,Soluong,Dongia, Thanhtien,Khuyenmai) VALUES(N'" + txtMaHDN.Text.Trim() + "', N'" + cboMaSP.SelectedValue +
                    "'," + txtSoluong.Text + "," + txtDongia.Text + "," + txtThanhtien.Text + "," + txtKhuyenmai.Text + ")";
            functions.Runsql(sql);
            LoadDataGridView();


            SLcon = sl + Convert.ToDouble(txtSoluong.Text);
            sql = "UPDATE tblSanpham SET Soluong =" + SLcon + " WHERE MaSP= N'" + cboMaSP.SelectedValue + "'";
            functions.Runsql(sql);

            //Cập nhật lại tổng tiền cho hóa đơn bán
            tong = Convert.ToDouble(functions.GetFileValues("SELECT Tongtien FROM tblHDNhap WHERE MaHDN = N'" + txtMaHDN.Text + "'"));
            Tongmoi = tong + Convert.ToDouble(txtThanhtien.Text);
            sql = "UPDATE tblHDNhap SET Tongtien =" + Tongmoi + " WHERE MaHDN = N'" + txtMaHDN.Text + "'";
            functions.Runsql(sql);
            txtTongtien.Text = Tongmoi.ToString();
            lblBangchu.Text = "Bằng chữ: " + functions.ChuyenSoSangChu(Tongmoi.ToString());
            ResetValuesHang();
            btnHuyHD.Enabled = false;
            btnxoa.Enabled = true;
            btnThemHD.Enabled = true;
            btnInHD.Enabled = true;
            LoadDataGridView();

        }
        private void ResetValuesHang()
        {
            cboMaSP.SelectedIndex = -1;
            txtSoluong.Text = "";
            txtKhuyenmai.Text = "0";
            txtThanhtien.Text = "0";
            txtTenSP.Text = "";
            txtDongia.Text = "0";
        }

        private void dgvHĐN_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cboMaSP.Text = dgvHĐN.CurrentRow.Cells[0].Value.ToString();
            txtSoluong.Text = dgvHĐN.CurrentRow.Cells[1].Value.ToString();
            txtDongia.Text = dgvHĐN.CurrentRow.Cells[2].Value.ToString();
            txtKhuyenmai.Text = dgvHĐN.CurrentRow.Cells[3].Value.ToString();
            txtThanhtien.Text = dgvHĐN.CurrentRow.Cells[4].Value.ToString();
        }

        private void dgvHĐN_DoubleClick(object sender, EventArgs e)
        {

            string ma;
            ma = dgvHĐN.CurrentRow.Cells[0].Value.ToString();
            DelHang(txtMaHDN.Text, ma);
            string thanhtien;
            thanhtien = dgvHĐN.CurrentRow.Cells[5].Value.ToString();
            DelUpdateTongtien(txtMaHDN.Text, Convert.ToDouble(thanhtien));
            LoadDataGridView();
        }
        // Hàm thêm phẩm trong tblSanpham khi nhập
        private void DelHang(string Mahoadon, string Mahang)
        {
            Double s, sl, SLcon;
            string sql;
            sql = "SELECT Soluong FROM tblchitietHDNhap WHERE MaHDN = N'" + Mahoadon + "' AND MaSP = N'" + Mahang + "'";
            s = Convert.ToDouble(functions.GetFileValues(sql));
            sql = "DELETE tblchitietHDNhap WHERE MaHDN=N'" + Mahoadon + "' AND MaSP = N'"
                    + Mahang + "'";
            functions.RunsqlDel(sql);
            // Cập nhật lại số lượng cho các mặt hàng
            sql = "SELECT Soluong FROM tblSanpham WHERE MaSP = N'" + Mahang + "'";
            sl = Convert.ToDouble(functions.GetFileValues(sql));
            SLcon = sl - s;
            sql = "UPDATE tblSanpham SET Soluong =" + SLcon + " WHERE MaSP= N'" + Mahang + "'";
            functions.Runsql(sql);
        }
        private void DelUpdateTongtien(string Mahoadon, double Thanhtien)
        {
            Double Tong, Tongmoi;
            string sql;
            sql = "SELECT Tongtien FROM tblHDNhap WHERE MaHDN = N'" + Mahoadon + "'";
            Tong = Convert.ToDouble(functions.GetFileValues(sql));
            Tongmoi = Tong + Thanhtien;
            sql = "UPDATE tblHDNhap SET Tongtien =" + Tongmoi + " WHERE MaHDN = N'" +
                    Mahoadon + "'";
            functions.Runsql(sql);
            txtTongtien.Text = Tongmoi.ToString();
            lblBangchu.Text = "Bằng chữ : " + functions.ChuyenSoSangChu(Tongmoi.ToString());
        }

        private void btnHuyHD_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnHuyHD.Enabled = false;
            btnThemHD.Enabled = true;
            btnxoa.Enabled = false;
            btnInHD.Enabled = false;
            btnLuuHD.Enabled = false;
            txtMaHDN.Enabled = false;
            dgvHĐN.DataSource = null;
            cboSoHDN.SelectedIndex = -1;
        }

        private void cboMaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;

            // Khi kich chon Ma nhan vien thi ten nhan vien se tu dong hien ra
            str = "Select TenNV from tblNhanvien where MaNV =N'" + cboMaNV.SelectedValue + "'";
            txtTenNV.Text = functions.GetFileValues(str);
            if (cboMaNV.Text == "")
                txtTenNV.Text = "";
        }

        private void cboMaNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;

            //Khi kich chon Ma ncc thi ten ncc, dia chi, dien thoai se tu dong hien ra
            str = "Select TenNCC from tblNhacungcap where MaNCC = N'" + cboMaNCC.SelectedValue
                + "'";
            txtTenNCC.Text = functions.GetFileValues(str);
            str = "Select DiachiNCC from tblNhacungcap where MaNCC = N'" + cboMaNCC.SelectedValue
                + "'";
            txtDiachi.Text = functions.GetFileValues(str);
            str = "Select Sdt from tblNhacungcap where MaNCC= N'" + cboMaNCC.SelectedValue
                + "'";
            mskDienthoai.Text = functions.GetFileValues(str);
            if (cboMaNCC.Text == "")
            {
                txtTenNCC.Text = "";
                txtDiachi.Text = "";
                mskDienthoai.Text = "";
            }
        }
        private void cboMaSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;

            // Khi kich chon Ma bình thi ten bình và giá bán sẽ tự động hiện ra
            str = "SELECT TenSP FROM tblSanpham WHERE MaSP =N'" + cboMaSP.SelectedValue
                + "'";
            txtTenSP.Text = functions.GetFileValues(str);
            str = "SELECT Gianhap FROM tblSanpham WHERE MaSP =N'" + cboMaSP.SelectedValue
                + "'";
            txtDongia.Text = functions.GetFileValues(str);
            if (cboMaSP.Text == "")
            {
                txtTenSP.Text = "";
                txtDongia.Text = "";
            }

        }

        private void txtSoluong_TextChanged(object sender, EventArgs e)
        {
            //Khi thay doi So luong, Giam gia thi Thanh tien tu dong cap nhat lai gia tri
            double tt, sl, dg, gg;
            if (txtSoluong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoluong.Text);
            if (txtKhuyenmai.Text == "")
                gg = 0;
            else
                gg = Convert.ToDouble(txtKhuyenmai.Text);
            if (txtDongia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDongia.Text);
            tt = sl * dg - sl * dg * gg / 100;
            txtThanhtien.Text = tt.ToString();
        }

        private void txtKhuyenmai_TextChanged(object sender, EventArgs e)
        {
            //Khi thay doi So luong, Giam gia thi Thanh tien tu dong cap nhat lai gia tri
            double tt, sl, dg, km;
            if (txtSoluong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoluong.Text);
            if (txtKhuyenmai.Text == "")
                km = 0;
            else
                km = Convert.ToDouble(txtKhuyenmai.Text);
            if (txtDongia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDongia.Text);
            tt = sl * dg - sl * dg * km / 100;
            txtThanhtien.Text = tt.ToString();
        }

        private void txtSoluong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtKhuyenmai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else
                e.Handled = true;

        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInHD_Click(object sender, EventArgs e)
        {
            // Khởi động chương trình Excel
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
            COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
            COMExcel.Range exRange;
            string sql;
            int hang = 0, cot = 0;
            DataTable tblThongtinHD, tblThongtinHang;
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
            exRange.Range["C2:E2"].Value = "HÓA ĐƠN NHẬP";
            // Biểu diễn thông tin chung của hóa đơn bán
            sql = "SELECT a.MaHDN, a.Ngaynhap, a.Tongtien, b.TenNCC, b.DiachiNCC, b.Sdt, c.TenNV FROM tblHDNhap AS a, tblNhacungcap AS b, tblNhanvien AS c WHERE a.MaHDN = N'"
                + txtMaHDN.Text + "' AND a.MaNCC = b.MaNCC AND a.MaNV =c.MaNV";
            tblThongtinHD = functions.GetDataToTable(sql);
            exRange.Range["B6:C9"].Font.Size = 12;
            exRange.Range["B6:C9"].Font.Name = "Times new roman";
            exRange.Range["B6:B6"].Value = "Mã hóa đơn:";
            exRange.Range["C6:E6"].MergeCells = true;
            exRange.Range["C6:E6"].Value = tblThongtinHD.Rows[0][0].ToString();
            exRange.Range["B7:B7"].Value = "Nhà cung cấp:";
            exRange.Range["C7:E7"].MergeCells = true;
            exRange.Range["C7:E7"].Value = tblThongtinHD.Rows[0][3].ToString();
            exRange.Range["B8:B8"].Value = "Địa chỉ:";
            exRange.Range["C8:E8"].MergeCells = true;
            exRange.Range["C8:E8"].Value = tblThongtinHD.Rows[0][4].ToString();
            exRange.Range["B9:B9"].Value = "Điện thoại:";
            exRange.Range["C9:C9"].Value = "'" + tblThongtinHD.Rows[0][5].ToString();

            //Lấy thông tin các mặt hàng
            sql = "SELECT b.TenSP, a.Soluong, b.Gianhap, a.Khuyenmai, a.Thanhtien " +
                  "FROM tblChitietHDNhap AS a , tblSanpham AS b WHERE a.MaHDN = N'" +
                  txtMaHDN.Text + "' AND a.MaSP = b.MaSP";
            tblThongtinHang = functions.GetDataToTable(sql);
            //Tạo dòng tiêu đề bảng
            exRange.Range["A11:F11"].Font.Bold = true;
            exRange.Range["A11:F11"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C11:F11"].ColumnWidth = 12;
            exRange.Range["A11:A11"].Value = "STT";
            exRange.Range["B11:B11"].Value = "Tên sản phẩm";
            exRange.Range["C11:C11"].Value = "Số lượng";
            exRange.Range["D11:D11"].Value = "Đơn giá";
            exRange.Range["E11:E11"].Value = "Khuyến mãi";
            exRange.Range["F11:F11"].Value = "Thành tiền";
            for (hang = 0; hang <= tblThongtinHang.Rows.Count - 1; hang++)
            {
                //Điền số thứ tự vào cột 1 từ dòng 12
                exSheet.Cells[1][hang + 12] = hang + 1;
                for (cot = 0; cot <= tblThongtinHang.Columns.Count - 1; cot++)
                    //Điền thông tin hàng từ cột thứ 2, dòng 12
                    exSheet.Cells[cot + 2][hang + 12] = tblThongtinHang.Rows[hang][cot].ToString();
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
            exRange.Range["A6:C6"].Value = tblThongtinHD.Rows[0][6];
            exSheet.Name = "Hóa đơn nhập";
            exApp.Visible = true;
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            if (cboSoHDN.Text == "")
            {
                MessageBox.Show("Bạn phải chọn một mã hóa đơn để tìm", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboSoHDN.Focus();
                return;
            }
            txtMaHDN.Text = cboSoHDN.Text;
            LoadInfoHoaDon();
            LoadDataGridView();
            btnHuyHD.Enabled = true;
            btnLuuHD.Enabled = true;
            btnInHD.Enabled = true;
            lblBangchu.Text = "Bằng chữ: " + functions.ChuyenSoSangChu(txtTongtien.Text);
            btnxoa.Enabled = true;
        }

        private void cboSoHDN_DropDown(object sender, EventArgs e)
        {
            functions.FIllcombo("SELECT MaHDN FROM tblHDNhap", cboSoHDN, "MaHDN", "MaHDN");
            cboSoHDN.SelectedIndex = -1;
        }

        private void cboMaNV_TextChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaNV.Text == "")
            {
                cboMaNV.Text = "";
            }
            else
            {
                str = "select TenNV from tblNhanvien where MaNV = N'" + cboMaNV.SelectedValue + "'";
                txtTenNV.Text = functions.GetFileValues(str);
            }
        }

        private void cboMaNCC_TextChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaNCC.Text == "")
            {
                cboMaNCC.Text = "";
                txtTenNCC.Text = "";
                txtDiachi.Text = "";
                mskDienthoai.Text = "";
            }
            else
            {
                str = "select TenNCC from tblNhacungcap where MaNCC = N'" + cboMaNCC.SelectedValue + "'";
                txtTenNCC.Text = functions.GetFileValues(str);
                str = "select DiachiNCC from tblNhacungcap where MaNCC = N'" + cboMaNCC.SelectedValue + "'";
                txtDiachi.Text = functions.GetFileValues(str);
                str = "select Sdt from tblNhacungcap where MaNCC = N'" + cboMaNCC.SelectedValue + "'";
                mskDienthoai.Text = functions.GetFileValues(str);
            }
        }

        private void frmHDN_TextChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaSP.Text == "")
            {
                cboMaSP.Text = "";
                txtTenSP.Text = "";
                txtDongia.Text = "";

            }
            else
            {
                str = "select TenSP from tblSanpham where MaSP = N'" + cboMaSP.SelectedValue + "'";
                txtTenSP.Text = functions.GetFileValues(str);
                str = "select Diachi from tblNhacungcap where MaNCC = N'" + cboMaSP.SelectedValue + "'";
                txtDongia.Text = functions.GetFileValues(str);
            }
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa một hóa đơn không?", "Thông báo",
                     MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string[] Masp = new string[20];
                string sql;
                int n = 0;
                int i;
                sql = "SELECT MaSP FROM tblchitietHDNhap WHERE MaHDN = N'" + txtMaHDN.Text + "'";
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
                    DelHang(txtMaHDN.Text, Masp[i]);
                //Xóa hóa đơn
                sql = "DELETE tblHDNhap WHERE MaHDN=N'" + txtMaHDN.Text + "'";
                functions.RunsqlDel(sql);
                ResetValues();
                ResetValuesHang();
                btnLuuHD.Enabled = false;
                btnxoa.Enabled = false;
                
                btnHuyHD.Enabled = false;
                mskNgaynhap.Text = "";
                LoadDataGridView();
                btnHuyHD.Enabled = false;
                btnInHD.Enabled = false;
                functions.FIllcombo("Select MaHDN from tblHDNhap", cboSoHDN, "MaHDN", "MaHDN");
                cboSoHDN.SelectedIndex = -1;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cboMaSP_TextChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaSP.Text == "")
            {
                cboMaSP.Text = "";
                txtTenSP.Text = "";
                txtDongia.Text = "";

            }
            else
            {
                str = "select TenSP from tblSanpham where MaSP = N'" + cboMaSP.Text + "'";
                txtTenSP.Text = functions.GetFileValues(str);
                str = "select Gianhap from tblSanpham where MaSP = N'" + cboMaSP.Text + "'";
                txtDongia.Text = functions.GetFileValues(str);
            }
        }
    }
}
