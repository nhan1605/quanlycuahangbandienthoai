using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDienThoai
{
    public partial class frmTimHDN : Form
    {
        public frmTimHDN()
        {
            InitializeComponent();
        }
        DataTable tblHDN;
        private void frmTimHDN_Load(object sender, EventArgs e)
        {
            ResetValues();
            dgvTìmkiemHĐN.DataSource = null;
        }
        private void ResetValues()
        {
            foreach (Control Ctl in this.Controls)
                if (Ctl is TextBox)
                    Ctl.Text = "";
            txtMaHDN.Focus();
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtMaHDN.Text == "") && (txtMaNV.Text == "") &&
               (txtThang.Text == "") && (txtMaNCC.Text == "") &&
               (txtTongtien.Text == ""))
            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!!", "Yêu cầu ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * FROM tblHDNhap WHERE 1=1";
            if (txtMaHDN.Text != "")
                sql = sql + " AND MaHDN Like N'%" + txtMaHDN.Text + "%'";
            if (txtThang.Text != "")
                sql = sql + " AND MONTH(Ngaynhap) =" + txtThang.Text;
            if (txtMaNV.Text != "")
                sql = sql + " AND MaNV Like N'%" + txtMaNV.Text + "%'";
            if (txtMaNCC.Text != "")
                sql = sql + " AND MaNCC Like N'%" + txtMaNCC.Text + "%'";

            tblHDN = functions.GetDataToTable(sql);
            if (tblHDN.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetValues();
            }
            else
                MessageBox.Show("Có " + tblHDN.Rows.Count + " bản ghi thỏa mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvTìmkiemHĐN.DataSource = tblHDN;
            LoadDataGridView();

        }
        private void LoadDataGridView()
        {
            dgvTìmkiemHĐN.Columns[0].HeaderText = "Mã HĐN";
            dgvTìmkiemHĐN.Columns[1].HeaderText = "Ngày nhập";
            dgvTìmkiemHĐN.Columns[2].HeaderText = "MaNV";
            dgvTìmkiemHĐN.Columns[3].HeaderText = "Mã NCC";
            dgvTìmkiemHĐN.Columns[4].HeaderText = "Tổng tiền";
            dgvTìmkiemHĐN.AllowUserToAddRows = false;
            dgvTìmkiemHĐN.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btnTimlai_Click(object sender, EventArgs e)
        {
            ResetValues();
            dgvTìmkiemHĐN.DataSource = null;
        }

        private void txtTongtien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else
                e.Handled = true;


        }

        private void dgvTìmkiemHĐN_Click(object sender, EventArgs e)
        {
         
            if (MessageBox.Show("Bạn có muốn hiển thị thông tin chi tiết?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                txtMaHDN.Text = dgvTìmkiemHĐN.CurrentRow.Cells[0].Value.ToString();
                txtThang.Text = functions.GetFileValues("select month(Ngaynhap) from tblHDNhap where MaHDN = '" + txtMaHDN.Text + "'");
                txtMaNCC.Text = dgvTìmkiemHĐN.CurrentRow.Cells[3].Value.ToString();
                txtTongtien.Text = dgvTìmkiemHĐN.CurrentRow.Cells[4].Value.ToString();
                txtMaNV.Text = dgvTìmkiemHĐN.CurrentRow.Cells[1].Value.ToString();
            }

        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
