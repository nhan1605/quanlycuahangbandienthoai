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
    public partial class frmNhaCungCap : Form
    {
        public frmNhaCungCap()
        {
            InitializeComponent();
        }
        DataTable tblNCC;
        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            txtTenNCC.Focus();
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            loadDataToGridView();
        }
        private void loadDataToGridView()
        {
            string sql = "select * from tblNhacungcap";
            tblNCC = functions.GetDataToTable(sql);
            dgvDMNCC.DataSource = tblNCC; //
            dgvDMNCC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvDMNCC.Columns[0].HeaderText = "Mã nhà cung cấp";
            dgvDMNCC.Columns[1].HeaderText = "Tên nhà cung cấp";
            dgvDMNCC.Columns[2].HeaderText = "Địa chỉ";
            dgvDMNCC.Columns[3].HeaderText = "Điện thoại";

        }
        private void ResetValues()
        {
            txtMaNCC.Text = "";
            txtTenNCC.Text = "";
            txtDiachi.Text = "";
            mskDienThoai.Text = "";
        }

        private void dgvDMNCC_Click(object sender, EventArgs e)
        {

            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNCC.Focus();
                return;
            }
            if (tblNCC.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            txtMaNCC.Text = dgvDMNCC.CurrentRow.Cells[0].Value.ToString();
            txtTenNCC.Text = dgvDMNCC.CurrentRow.Cells[1].Value.ToString();
            txtDiachi.Text = dgvDMNCC.CurrentRow.Cells[2].Value.ToString();
            mskDienThoai.Text = dgvDMNCC.CurrentRow.Cells[3].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnHuy.Enabled = true;
            txtTenNCC.Enabled = true;
            txtDiachi.Enabled = true;
            mskDienThoai.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            txtMaNCC.Enabled = false;
            txtMaNCC.Focus();

            ResetValues();
            loadDataToGridView();
            txtMaNCC.Text = functions.tangkey("select top(1) convert(integer ,substring(MaNCC,3,3)+1) as soma from  tblNhacungcap order by soma desc", "CC");

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaNCC.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNCC.Focus();
                return;
            }
            if (txtTenNCC.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenNCC.Focus();
                return;
            }
            if (txtDiachi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiachi.Focus();
                return;
            }
            if (mskDienThoai.Text == "(   )    -")
            {
                MessageBox.Show("Bạn phải nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mskDienThoai.Focus();
                return;
            }
            sql = "update tblNhacungcap set TenNCC=N'" + txtTenNCC.Text.Trim().ToString() +
                "',DiachiNCC=N'" + txtDiachi.Text.Trim().ToString() +
                "' ,Sdt='" + mskDienThoai.Text.Trim() +
                "'WHERE MaNCC=N'" + txtMaNCC.Text + "'";
            functions.Runsql(sql);
            loadDataToGridView();
            txtMaNCC.Enabled = false;
            ResetValues();
            btnHuy.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaNCC.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tblNhacungcap WHERE MaNCC='" + txtMaNCC.Text + "'";
                functions.RunsqlDel(sql);
                loadDataToGridView();
                ResetValues();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

            string sql;
            if (txtMaNCC.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNCC.Focus();
                return;
            }
            if (txtTenNCC.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenNCC.Focus();
                return;
            }
            if (txtDiachi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiachi.Focus();
                return;
            }
            if (mskDienThoai.Text == "(   )    -")
            {
                MessageBox.Show("Bạn phải nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mskDienThoai.Focus();
                return;
            }
            sql = "SELECT MaNCC FROM tblNhacungcap WHERE MaNCC='" + txtMaNCC.Text.Trim() + "'";
            if (functions.Checkey(sql))
            {
                MessageBox.Show("Mã nhà cung cấp này đã tồn tại, bạn phải chọn mã nhà cung cấp khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNCC.Focus();
                return;
            }
            //Chèn thêm
            sql = "INSERT INTO tblNhacungcap VALUES (N'" + txtMaNCC.Text.Trim() +
                "',N'" + txtTenNCC.Text.Trim() + "',N'" + txtDiachi.Text.Trim() + "','" + mskDienThoai.Text + "')";

            functions.Runsql(sql);
            loadDataToGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaNCC.Enabled = false;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
