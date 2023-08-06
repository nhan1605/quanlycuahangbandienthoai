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
    public partial class frmKhachHang : Form
    {
        public frmKhachHang()
        {
            InitializeComponent();
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            txtMaKH.Enabled = false;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            loadDataToGridView();
        }
        private void resetvalue()
        {
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtDiachi.Text = "";
        }
        DataTable tblKH;
        private void loadDataToGridView()
        {
            string sql;
            sql = "select * from tblKhach";
            tblKH = functions.GetDataToTable(sql);
            dgridDMKH.DataSource = tblKH;
            dgridDMKH.Columns[0].HeaderText = "Mã khách";
            dgridDMKH.Columns[1].HeaderText = "Tên khách";
            dgridDMKH.Columns[2].HeaderText = "Địa chỉ khách hàng";
        }

        private void dgridDMKH_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Bạn đang ở chế độ thêm mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dgridDMKH.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtMaKH.Text = dgridDMKH.CurrentRow.Cells["MaKH"].Value.ToString();
            txtTenKH.Text = dgridDMKH.CurrentRow.Cells["TenKH"].Value.ToString();
            txtDiachi.Text = dgridDMKH.CurrentRow.Cells["DiachiKH"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnHuy.Enabled = true;

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;

            if (txtMaKH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaKH.Focus();
                return;
            }
            if (txtTenKH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenKH.Focus();
                return;
            }
            if (txtDiachi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDiachi.Focus();
                return;
            }
            sql = "SELECT MaKH FROM tblKhach WHERE MaKH=N'" + txtMaKH.Text.Trim() + "'";
            if (functions.Checkey(sql))
            {
                MessageBox.Show("Mã khách này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaKH.Focus();
                txtMaKH.Text = "";
                return;
            }
            sql = "insert into tblKhach(MaKH, TenKH, DiachiKH) values (N'" + txtMaKH.Text.Trim() + "',N'" + txtTenKH.Text.Trim() + "',N'" + txtDiachi.Text.Trim() + "')";
            functions.Runsql(sql);
            loadDataToGridView();
            resetvalue();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            txtMaKH.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {

            string sql;
            if (txtMaKH.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dgridDMKH.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtTenKH.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tên khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenKH.Focus();
                return;
            }

            if (txtDiachi.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập điện chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDiachi.Focus();
                return;
            }

            sql = "UPDATE tblKhach SET  TenKH=N'" + txtTenKH.Text.Trim().ToString() +
                    "',DiachiKH=N'" + txtDiachi.Text.Trim().ToString() +
                    "'WHERE MaKH=N'" + txtMaKH.Text + "'";
            if (functions.Checkey(sql))
            {
                MessageBox.Show("Mã khách này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaKH.Focus();
                txtMaKH.Text = "";
                return;
            }

            functions.Runsql(sql);
            loadDataToGridView();
            resetvalue();
            btnHuy.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            resetvalue();
            txtTenKH.Focus();

            loadDataToGridView();
            txtMaKH.Text = functions.tangkey("select top(1) (substring(MaKH,3,3)+1) as soma from  tblKhach order by soma desc", "KH");

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaKH.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dgridDMKH.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                string sql;
                sql = "DELETE tblKhach WHERE MaKH=N'" + txtMaKH.Text + "'";
                functions.RunsqlDel(sql);
                loadDataToGridView();
                resetvalue();
            }

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaKH.Enabled = false;

            resetvalue();

        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
