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

namespace QuanLyDienThoai
{
    public partial class frmDSHDBNam : Form
    {
        public frmDSHDBNam()
        {
            InitializeComponent();
        }
        DataTable tblBCTN;
        private void frmDSHDBNam_Load(object sender, EventArgs e)
        {
            txtnam.Enabled = false;
            ResetValues();
            btnHienthi.Enabled = false;

        }
        private void ResetValues()
        {

            foreach (Control Ctl in this.Controls)
                if (Ctl is TextBox)
                    Ctl.Text = "";
            txtnam.Focus();
        }

        private void btnHienthi_Click(object sender, EventArgs e)
        {
            string sql, sql1;
            if (txtnam.Text == "")
            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!!", "Yeu cau ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            sql = "SELECT a.MaHDB, a.MaNV,a.MaKH ,b.MaSP, b.soluong, a.Ngayban, b.thanhtien " +
                "FROM tblHDBan AS a, tblchitietHDBan AS b, " +
                "tblKhach AS c WHERE  a.MaHDB = b.MaHDB AND a.MaKH =c.MaKH ";


            if (txtnam.Text != "")
                sql = sql + " AND YEAR(a.Ngayban) =" + txtnam.Text;

            tblBCTN = functions.GetDataToTable(sql);
            if (tblBCTN.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtnam.Focus();
            }
            else
                MessageBox.Show("Có " + tblBCTN.Rows.Count + " bản ghi thỏa mãn điều kiện!!!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgrBCTN.DataSource = tblBCTN;

            sql1 = "select Sum(Tongtien) from tblHDBan where ((YEAR(Ngayban) = '" + txtnam.Text + "')) ";
            txtTongtien.Text = functions.GetFileValues(sql1);
            Load_DataGridView();
            txtnam.Enabled = false;
            btnHienthi.Enabled = false;
            btnBaocao.Enabled = true;
        }
        private void Load_DataGridView()
        {
            dgrBCTN.Columns[0].HeaderText = "Mã hóa đơn bán";
            dgrBCTN.Columns[1].HeaderText = "Mã nhân viên";
            dgrBCTN.Columns[2].HeaderText = "Mã khách hàng";
            dgrBCTN.Columns[3].HeaderText = "Mã sản phẩm";
            dgrBCTN.Columns[4].HeaderText = "Số lượng";
            dgrBCTN.Columns[5].HeaderText = "Ngày bán";
            dgrBCTN.Columns[6].HeaderText = "Thành tiền";

            dgrBCTN.AllowUserToAddRows = false;
            dgrBCTN.EditMode = DataGridViewEditMode.EditProgrammatically;

        }

        private void btnTimlai_Click(object sender, EventArgs e)
        {
            ResetValues();
            dgrBCTN.DataSource = null;
        }

        private void btnBaocao_Click(object sender, EventArgs e)
        {
            txtnam.Enabled = true;
          
            btnHienthi.Enabled = true;
            txtnam.Text = "";
            btnBaocao.Enabled = false;
            txtnam.Focus();
            dgrBCTN.DataSource = null;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {


            if (txtnam.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập năm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtnam.Focus();
                return;
            }

            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook;
            COMExcel.Worksheet exSheet;
            COMExcel.Range exRange;
            string sql;
            int hang = 0, cot = 0;
            DataTable danhsach;

            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:Z300"].Font.Name = "Times new roman";
            exRange.Range["A1:B3"].Font.Size = 10;
            exRange.Range["A1:B3"].Font.Bold = true;
            exRange.Range["A1:B3"].Font.ColorIndex = 5;
            exRange.Range["A1:A1"].ColumnWidth = 7;
            exRange.Range["B1:B1"].ColumnWidth = 15;
            exRange.Range["A1:B1"].MergeCells = true;
            exRange.Range["A1:B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A1:B1"].Value = "Cửa hàng điện thoại";
            exRange.Range["A2:B2"].MergeCells = true;
            exRange.Range["A2:B2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:B2"].Value = "Chùa Bộc - Hà Nội";
            exRange.Range["A3:B3"].MergeCells = true;
            exRange.Range["A3:B3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A3:B3"].Value = "Điện thoại: 0999888666";
            exRange.Range["C2:G2"].Font.Size = 12;
            exRange.Range["C2:G2"].Font.Bold = true;
            exRange.Range["C2:G2"].Font.ColorIndex = 3;
            exRange.Range["C2:G2"].MergeCells = true;
            exRange.Range["C2:G2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C2:G2"].Value = "BÁO CÁO HÓA ĐƠN BÁN VÀ TỔNG TIỀN BÁN HÀNG THEO NĂM " + txtnam.Text;
            exRange.Range["C4:C4"].Font.Bold = true;
            exRange.Range["C4:D4"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;

            string sql1;
            sql1 = "select Sum(Tongtien) from tblHDBan where  ((YEAR(Ngayban) = '" + txtnam.Text + "')) ";

            string s = functions.GetFileValues(sql1);
            exRange.Range["C4:C4"].Value = "Tổng tiền bán hàng : ";
            exRange.Range["D4:D4"].Value = s;
            sql = "select * from tblHDBan  where  (YEAR(Ngayban) = '" + txtnam.Text + "')";
            danhsach = functions.GetDataToTable(sql);

            exRange.Range["B5:J5"].Font.Bold = true;
            exRange.Range["B5:J5"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["B5:B5"].ColumnWidth = 12;
            exRange.Range["C5:C5"].ColumnWidth = 20;
            exRange.Range["D5:D5"].ColumnWidth = 12;
            exRange.Range["E5:E5"].ColumnWidth = 30;
            exRange.Range["F5:F5"].ColumnWidth = 11;
            exRange.Range["G5:G5"].ColumnWidth = 11;
            exRange.Range["H5:H5"].ColumnWidth = 12;
            exRange.Range["I5:I5"].ColumnWidth = 12;

            sql = "SELECT a.MaHDB, a.MaNV, c.TenKH, b.MaSP, b.soluong, a.Ngayban," +
                " b.thanhtien FROM tblHDBan AS a, tblchitietHDBan AS b, tblKhach AS c WHERE a.MaHDB = b.MaHDB AND a.MaKH =c.MaKH";
            danhsach = functions.GetDataToTable(sql);

            exRange.Range["E5:E5"].Font.Bold = true;
            exRange.Range["E5:E5"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["B5:B5"].Value = "STT";
            exRange.Range["C5:C5"].Value = "Mã hóa đơn bán";
            exRange.Range["D5:D5"].Value = "Mã nhân viên";
            exRange.Range["E5:E5"].Value = "Mã khách";
            exRange.Range["F5:F5"].Value = "Mã sản phẩm";
            exRange.Range["G5:G5"].Value = "Số lượng";
            exRange.Range["H5:H5"].Value = "Ngày bán";
            exRange.Range["I5:I5"].Value = "Thành tiền";



            for (hang = 0; hang < danhsach.Rows.Count; hang++)
            {
                exSheet.Cells[2][hang + 6] = hang + 1;
                for (cot = 0; cot < danhsach.Columns.Count; cot++)
                {
                    exSheet.Cells[cot + 3][hang + 6] = danhsach.Rows[hang][cot].ToString();
                }
            }


            exRange = exSheet.Cells[2][hang + 8];
            exRange.Range["D1:F1"].MergeCells = true;
            exRange.Range["D1:F1"].Font.Italic = true;
            exRange.Range["D1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["D1:F1"].Value = "Hà Nội, Ngày " + DateTime.Now.ToShortDateString();
            exSheet.Name = "Báo cáo";
            exApp.Visible = true;
        }
    }
}
