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
    public partial class frmLoai : Form
    {
        public frmLoai()
        {
            InitializeComponent();
        }

        private void frmLoai_Load(object sender, EventArgs e)
        {
            txtMaloai.Enabled = false;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            Load_DataGridView();
        }
        DataTable tblLoai;
        private void Load_DataGridView()
        {
            string sql;
            sql = "SELECT Maloai, Tenloai FROM tblLoai";
            tblLoai = functions.GetDataToTable(sql);

            dgrLoai.DataSource = tblLoai;
            functions.Runsql(sql);
            // Không cho phép thêm mới dữ liệu trực tiếp trên lưới

            // Không cho phép sửa dữ liệu trực tiếp trên lưới
        }

        private void dgrLoai_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Dang o che do them moi!");
                txtMaloai.Focus();
                return;
            }
            if (tblLoai.Rows.Count == 0)
            {
                MessageBox.Show("Khong co du lieu");
                return;
            }
            txtMaloai.Text = dgrLoai.CurrentRow.Cells[0].Value.ToString();
            txtTenloai.Text = dgrLoai.CurrentRow.Cells[1].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtTenloai.Focus();
            txtMaloai.Text = functions.tangkey("select TOP(1) (substring(Maloai,3,3)+1) as soma from  tblLoai order by soma desc", "ML");

        }
        private void ResetValues()
        {
            txtMaloai.Text = "";
            txtTenloai.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaloai.Text == "")
            {
                MessageBox.Show("ban phai nhap ma loai", "thong bao");
                txtMaloai.Focus();
                return;
            }
            if (txtTenloai.Text.Trim().Length == 0)
            {
                MessageBox.Show("ban phai nhap ten loai");
                txtTenloai.Focus();
                return;
            }
            sql = $"SELECT Maloai FROM tblLoai WHERE Maloai = '{txtMaloai.Text.Trim()}'";
            if (functions.Checkey(sql))
            {
                MessageBox.Show("ma nay da co, ban phai nhap ma khac");
                txtMaloai.Focus();
                txtMaloai.Text = "";
                return;
            }
            else
            {
                sql = "INSERT INTO tblLoai(Maloai,Tenloai) VALUES(N'" + txtMaloai.Text.Trim() + "',N'" + txtTenloai.Text.Trim() + "')";
                functions.Runsql(sql);
                Load_DataGridView();
                ResetValues();
                btnXoa.Enabled = true;
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnHuy.Enabled = false;
                btnLuu.Enabled = false;
                txtMaloai.Enabled = false;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblLoai.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                return;
            }
            if (txtMaloai.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenloai.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên loại", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenloai.Focus();
                return;
            }
            sql = "UPDATE tblLoai SET Tenloai=N'" + txtTenloai.Text.ToString() +
"' WHERE Maloai=N'" + txtMaloai.Text + "'";
            functions.Runsql(sql);
            Load_DataGridView();
            ResetValues();
            btnHuy.Enabled = false;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            txtMaloai.Enabled = false;
            ResetValues();
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblLoai.Rows.Count == 0)
            {
                MessageBox.Show("Khong con du lieu!");
                return;
            }
            if (txtMaloai.Text == "")
            {
                MessageBox.Show("Vui long chon loai can xoa!");
                return;
            }
            if (MessageBox.Show("Ban co muon xoa khong!", "Thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "delete tblLoai where Maloai =N'" + txtMaloai.Text + "'";
                functions.RunsqlDel(sql);
                Load_DataGridView();
                ResetValues();
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
