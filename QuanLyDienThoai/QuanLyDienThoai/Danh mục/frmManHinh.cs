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
    public partial class frmManHinh : Form
    {
        public frmManHinh()
        {
            InitializeComponent();
        }

        private void frmManHinh_Load(object sender, EventArgs e)
        {
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            Load_DataGridView();
        }
        DataTable tblMH;
        private void Load_DataGridView()
        {
            string sql = "select * from tblManhinh";
            tblMH = functions.GetDataToTable(sql);
            dgrManhinh.DataSource = tblMH;
            dgrManhinh.Columns[0].HeaderText = "Mã màn hình";
            dgrManhinh.Columns[1].HeaderText = "Tên màn hình";
            dgrManhinh.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        private void frmManHinh_Click(object sender, EventArgs e)
        {

        }

        private void dgrManhinh_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Dang o che do them moi!");
                txtMaMH.Focus();
                return;
            }
            if (tblMH.Rows.Count == 0)
            {
                MessageBox.Show("Khong co du lieu");
                return;
            }
            txtMaMH.Text = dgrManhinh.CurrentRow.Cells[0].Value.ToString();
            txtTenMH.Text = dgrManhinh.CurrentRow.Cells[1].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnHuy.Enabled = true;
        }
        private void ResetValues()
        {
            txtMaMH.Text = "";
            txtTenMH.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaMH.Text == "")
            {
                MessageBox.Show("ban phai nhap ma man hinh", "thong bao");
                txtMaMH.Focus();
                return;
            }
            if (txtTenMH.Text.Trim().Length == 0)
            {
                MessageBox.Show("ban phai nhap ten man hinh");
                txtTenMH.Focus();
                return;
            }
            sql = $"SELECT MaMH FROM tblManhinh WHERE MaMH = '{txtMaMH.Text.Trim()}'";
            if (functions.Checkey(sql))
            {
                MessageBox.Show("ma nay da co, ban phia nhap ma khac");
                txtMaMH.Focus();
                txtMaMH.Text = "";
                return;
            }
            else
            {
                sql = "INSERT INTO tblManhinh(MaMH,TenMH) VALUES(N'" + txtMaMH.Text.Trim() + "',N'" + txtTenMH.Text.Trim() + "')";
                functions.Runsql(sql);
                Load_DataGridView();
                ResetValues();
                btnXoa.Enabled = true;
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnHuy.Enabled = false;
                btnLuu.Enabled = false;
                txtMaMH.Enabled = false;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblMH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                return;
            }
            if (txtMaMH.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenMH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên màn hình", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenMH.Focus();
                return;
            }
            sql = "UPDATE tblManhinh SET TenMH=N'" + txtTenMH.Text.ToString() +
"' WHERE MaMH=N'" + txtMaMH.Text + "'";
            functions.Runsql(sql);
            Load_DataGridView();
            ResetValues();
            btnHuy.Enabled = false;
            txtMaMH.Enabled = false;

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            txtMaMH.Enabled = false;
            ResetValues();
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblMH.Rows.Count == 0)
            {
                MessageBox.Show("Khong con du lieu!");
                return;
            }
            if (txtMaMH.Text == "")
            {
                MessageBox.Show("Vui long chon man hinh can xoa!");
                return;
            }
            if (MessageBox.Show("Ban co muon xoa khong!", "Thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "delete tblManhinh where MaMH =N'" + txtMaMH.Text + "'";
                functions.RunsqlDel(sql);
                Load_DataGridView();
                ResetValues();
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnLuu.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;

            txtTenMH.Focus();
            txtMaMH.Enabled = false;
            ResetValues();

            txtMaMH.Text = functions.tangkey("select TOP(1) (substring(MaMH,3,3)+1) as soma from  tblManhinh order by soma desc", "MH");

        }
    }
}
