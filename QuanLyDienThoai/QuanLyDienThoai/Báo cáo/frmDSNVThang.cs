using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using COMExecl = Microsoft.Office.Interop.Excel;

namespace QuanLyDienThoai
{
    public partial class frmDSNVThang : Form
    {
        public frmDSNVThang()
        {
            InitializeComponent();
        }
        DataTable tblBCDSNV;

        private void frmDSNVThang_Load(object sender, EventArgs e)
        {
  
            cbothang.Enabled = false;
            txtnam.Enabled = false;
            btnBaocao.Enabled = true;
            btnDong.Enabled = true;
            btnIn.Enabled = false;
            btnHienthi.Enabled = false;

            dgridBCDSNV.DataSource = null;
            //Đổ 4 quí vào ComboBox
            cbothang.Items.Add("01");
            cbothang.Items.Add("02");
            cbothang.Items.Add("03");
            cbothang.Items.Add("04");
            cbothang.Items.Add("05");
            cbothang.Items.Add("06");
            cbothang.Items.Add("07");
            cbothang.Items.Add("08");
            cbothang.Items.Add("09");
            cbothang.Items.Add("10");
            cbothang.Items.Add("11");
            cbothang.Items.Add("12");

        }

        private void btnBaocao_Click(object sender, EventArgs e)
        {
            cbothang.Enabled = true;
            txtnam.Enabled = true;
            btnHienthi.Enabled = true;
            txtnam.Text = "";
            cbothang.Text = "";
            cbothang.Focus();
            dgridBCDSNV.DataSource = null;
        }


        private void btnIn_Click(object sender, EventArgs e)
        {
            if (txtnam.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập năm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtnam.Focus();
                return;
            }
            if (cbothang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn quý", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbothang.Focus();
                return;
            }

            COMExecl.Application exApp = new COMExecl.Application();
            COMExecl.Workbook exBook;
            COMExecl.Worksheet exSheet;
            COMExecl.Range exRange;
            string sql;
            int hang = 0, cot = 0;
            DataTable danhsach;

            exBook = exApp.Workbooks.Add(COMExecl.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:Z300"].Font.Name = "Times new roman";
            exRange.Range["A1:B3"].Font.Size = 11;
            exRange.Range["A1:B3"].Font.Bold = true;
            exRange.Range["A1:B3"].Font.ColorIndex = 5;//đặt màu cho chữ
            exRange.Range["A1:A1"].ColumnWidth = 10;//độ rộng cột
            exRange.Range["B1:B1"].ColumnWidth = 16;
            exRange.Range["A1:B1"].MergeCells = true;
            exRange.Range["A1:B1"].HorizontalAlignment = COMExecl.XlHAlign.xlHAlignCenter;
            exRange.Range["A1:B1"].Value = "Cửa hàng Điện thoại";
            exRange.Range["A2:B2"].MergeCells = true;
            exRange.Range["A2:B2"].HorizontalAlignment = COMExecl.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:B2"].Value = "Chùa Bộc - Hà Nội";
            exRange.Range["A3:B3"].MergeCells = true;
            exRange.Range["A3:B3"].HorizontalAlignment = COMExecl.XlHAlign.xlHAlignCenter;
            exRange.Range["A3:B3"].Value = "Điện thoại: 0999888666";
            exRange.Range["C2:G2"].Font.Size = 16;
            exRange.Range["C2:G2"].Font.Bold = true;
            exRange.Range["C2:G2"].Font.ColorIndex = 3;
            exRange.Range["C2:G2"].MergeCells = true;
            exRange.Range["C2:G2"].HorizontalAlignment = COMExecl.XlHAlign.xlHAlignCenter;
            exRange.Range["C2:G2"].Value = "BÁO CÁO TOP DANH SÁCH NHÂN VIÊN BÁN ÍT NHẤT THEO THÁNG";
            exRange.Range["D3:F3"].Font.Size = 14;
            exRange.Range["D3:F3"].Font.Bold = true;
            exRange.Range["D3:F3"].Font.ColorIndex = 3;
            exRange.Range["D3:F3"].MergeCells = true;
            exRange.Range["D3:F3"].HorizontalAlignment = COMExecl.XlHAlign.xlHAlignCenter;
            exRange.Range["D3:F3"].Value = "Tháng " + cbothang.SelectedItem + " Năm " + txtnam.Text;
            sql = "SELECT Top(5) a.MaNV, a.TenNV, SUM(c.Tongtien) AS Tongtien from tblNhanvien as a, tblchitietHDBan as b, tblHDBan as c " +
                "where (a.MaNV = c.MaNV) AND (b.MaHDB = c.MaHDB) AND (Year(c.Ngayban) = '" + txtnam.Text + "') and (month(c.Ngayban) = '" + cbothang.Text + "') " +
                "group by a.MaNV,a.TenNV order by Tongtien ";

            danhsach = functions.GetDataToTable(sql);//đổ dữ liệu từ lệnh sql vào biến "danhsach"

            exRange.Range["B5:F5"].Font.Bold = true;//In đậm các chữ 
            exRange.Range["B5:F5"].HorizontalAlignment = COMExecl.XlHAlign.xlHAlignCenter;
            exRange.Range["B5:B5"].ColumnWidth = 14;
            exRange.Range["C5:C5"].ColumnWidth = 13;
            exRange.Range["D5:D5"].ColumnWidth = 26;
            exRange.Range["E5:E5"].ColumnWidth = 26;
            exRange.Range["G9:G9"].ColumnWidth = 26;

            exRange.Range["E5:E5"].Font.Bold = true;
            exRange.Range["E5:E5"].HorizontalAlignment = COMExecl.XlHAlign.xlHAlignCenter;
            exRange.Range["B5:B5"].Value = "STT";
            exRange.Range["C5:C5"].Value = "Mã nhân viên";
            exRange.Range["D5:D5"].Value = "Tên nhân viên";
            exRange.Range["E5:E5"].Value = "Tổng tiền";
            //exRange.Range["F5:F5"].Value = "Quí " + cboQui.SelectedItem;

            //vòng lặp để đổ dữ liệu từ biến "danhsach" vào excel
            for (hang = 0; hang < danhsach.Rows.Count; hang++)
            {
                exSheet.Cells[2][hang + 6] = hang + 1;//điền số thứ tự vào cột 2 bắt đầu từ hàng 6 (mở excel ra hình dung)
                for (cot = 0; cot < danhsach.Columns.Count; cot++)
                {
                    exSheet.Cells[cot + 3][hang + 6] = danhsach.Rows[hang][cot].ToString();//điền thông tin các cột còn lại từ dữ liệu đã đc đổ vào từ biến "danhsach" bắt đầu từ cột 3, hàng 6
                }
            }


            exRange = exSheet.Cells[2][hang + 8];//chỗ này là đánh dấu vị trí viết cái dòng "Hà Nội, ngày..."
            exRange.Range["D1:F1"].MergeCells = true;
            exRange.Range["D1:F1"].Font.Italic = true;
            exRange.Range["D1:F1"].HorizontalAlignment = COMExecl.XlHAlign.xlHAlignCenter;
            exRange.Range["D1:F1"].Value = "Hà Nội, Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            exSheet.Name = "Báo cáo";
            exApp.Visible = true;

        }

        private void btnHienthi_Click(object sender, EventArgs e)
        {
            if (cbothang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn tháng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbothang.Focus();
                return;
            }
            if (txtnam.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập năm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtnam.Focus();
                return;
            }

            string sql = "SELECT Top(5) a.MaNV, a.TenNV, SUM(c.Tongtien) AS Tongtien from tblNhanvien as a, tblchitietHDBan as b, tblHDBan as c " +
                "where (a.MaNV = c.MaNV) AND (b.MaHDB = c.MaHDB) AND (Year(c.Ngayban) = '" + txtnam.Text + "') and (month(c.Ngayban) = '" + cbothang.Text + "') " +
                "group by a.MaNV,a.TenNV order by Tongtien ";

            tblBCDSNV = functions.GetDataToTable(sql);
            dgridBCDSNV.DataSource = tblBCDSNV;

            //Chiều rộng cột phù hợp với nội dung tất cả các ô kể cả tiêu đề


            cbothang.Enabled = false;
            txtnam.Enabled = false;
            btnBaocao.Enabled = true;
            btnIn.Enabled = true;
            btnHienthi.Enabled = false;

        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void txtnam_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                    e.Handled = false;
                else
                    e.Handled = true;
            }
        }

        
    }
}
  
