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
    public partial class frmNhanVien : Form
    {
        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
        
            txtMaNV.Enabled = false;
            btnDong.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            loadDataToGridView();
            functions.FIllcombo("SELECT Maque,Tenque FROM tblQue", cboMaque, "Maque", "Tenque");
            cboMaque.SelectedIndex = -1;
            ResetValues();
        }
        private void loadDataToGridView()
        {
            string sql = "select MaNV, TenNV, Gioitinh, Ngaysinh,sdt,Diachi,Maque from tblNhanvien";
            DataTable tblNV = functions.GetDataToTable(sql);
            dgvDMNV.DataSource = tblNV;
            dgvDMNV.Columns[0].HeaderText = "Mã nhân viên";
            dgvDMNV.Columns[1].HeaderText = "Tên nhân viên";
            dgvDMNV.Columns[2].HeaderText = "Giới tính";
            dgvDMNV.Columns[3].HeaderText = "Ngày sinh";
            dgvDMNV.Columns[4].HeaderText = "Điện thoại";
            dgvDMNV.Columns[5].HeaderText = "Địa chỉ";
            dgvDMNV.Columns[6].HeaderText = "Mã quê";

            dgvDMNV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }
        private void ResetValues()
        {
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            ckhGioitinh.Checked = false;
            txtDiachi.Text = "";
            mskDienthoai.Text = "";
            mskNgaysinh.Text = "";
            cboMaque.SelectedItem = null;


        }


        private void dgvDMNV_Click(object sender, EventArgs e)
        {
           
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNV.Focus();
                return;
            }
            txtMaNV.Text = dgvDMNV.CurrentRow.Cells[0].Value.ToString();
            txtTenNV.Text = dgvDMNV.CurrentRow.Cells[1].Value.ToString();
            if (dgvDMNV.CurrentRow.Cells[2].Value.ToString().Trim() == "Nam") ckhGioitinh.Checked = true;
            else ckhGioitinh.Checked = false;
            mskNgaysinh.Text = dgvDMNV.CurrentRow.Cells[3].Value.ToString();
            mskDienthoai.Text = dgvDMNV.CurrentRow.Cells[4].Value.ToString();
            txtDiachi.Text = dgvDMNV.CurrentRow.Cells[5].Value.ToString();
            string ma = dgvDMNV.CurrentRow.Cells[6].Value.ToString();
            cboMaque.Text = functions.GetFileValues("select Tenque from tblQue where Maque = '"+ma+"'"  );

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnHuy.Enabled = true;
            txtTenNV.Enabled = true;

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            txtMaNV.Enabled = false;
            txtMaNV.Focus();
            ResetValues();
            loadDataToGridView();
            txtMaNV.Text = functions.tangkey("select top(1) convert(integer ,substring(MaNV,3,3)+1) as soma from  tblNhanvien order by soma desc", "NV");

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql, gt;
            if (txtMaNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNV.Focus();
                return;
            }
            if (txtTenNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenNV.Focus();
                return;
            }
            if (ckhGioitinh.Checked == true)
                gt = "Nam";
            else
                gt = "Nữ";
            if (mskDienthoai.Text == "(   )     -")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskDienthoai.Focus();
                return;
            }
            if (mskNgaysinh.Text == "  /  /")
            {
                MessageBox.Show("Bạn phải nhập ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgaysinh.Focus();
                return;
            }
            if (!functions.IsDate(mskNgaysinh.Text))
            {
                MessageBox.Show("Bạn phải nhập lại ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgaysinh.Focus();
                return;
            }

            if (txtDiachi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiachi.Focus();
                return;
            }

            if (cboMaque.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn mã quê ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaque.Focus();
                return;
            }
            sql = "UPDATE tblNhanvien SET TenNV = N'" +
            txtTenNV.Text.Trim().ToString()
             + "',Diachi = N'" + txtDiachi.Text.Trim().ToString() + "',sdt='" +
             mskDienthoai.Text.ToString() + "',Gioitinh = N'" + gt + "',Ngaysinh='" +
             functions.CovertToDate(mskNgaysinh.Text) + "',Maque=N'" +
            cboMaque.SelectedValue.ToString()  + "' WHERE MaNV = N'" + txtMaNV.Text +
            "'";
           
            functions.Runsql(sql);
            loadDataToGridView();
            ResetValues();
            btnHuy.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

            string sql;
            if (txtMaNV.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tblNhanvien WHERE MaNV='" + txtMaNV.Text + "'";
                functions.RunsqlDel(sql);
                loadDataToGridView();
                ResetValues();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql, gt;
            if (txtMaNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNV.Focus();
                return;
            }
            if (txtTenNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenNV.Focus();
                return;
            }
            if (ckhGioitinh.Checked == true)
                gt = "Nam";
            else
                gt = "Nữ";
            if (mskDienthoai.Text == "(   )    -")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskDienthoai.Focus();
                return;
            }
            if (mskNgaysinh.Text == "  /  /")
            {
                MessageBox.Show("Bạn phải nhập ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgaysinh.Focus();
                return;
            }
            if (!functions.IsDate(mskNgaysinh.Text))
            {
                MessageBox.Show("Bạn phải nhập lại ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgaysinh.Focus();
                return;
            }

            if (txtDiachi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiachi.Focus();
                return;
            }

            if (cboMaque.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn mã quê ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaque.Focus();
                return;
            }
            sql = "SELECT MaNV FROM tblNhanvien WHERE MaNV = N'" + txtMaNV.Text.Trim() + "'";
            if (functions.Checkey(sql))
            {
                MessageBox.Show("Mã nhân viên này đã tồn tại, bạn phải chọn mã nhân viên khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNV.Focus();
                return;
            }
            sql = "INSERT INTO tblNhanvien VALUES(N'" + txtMaNV.Text.Trim() + "', N'" + txtTenNV.Text.Trim() +
                "',N'" + txtDiachi.Text.Trim() +
                "', N'" + gt +
                 "', '" + functions.CovertToDate(mskNgaysinh.Text) + "',N'" +
                 cboMaque.SelectedValue.ToString() + "', '" +
                 mskDienthoai.Text + "')";
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
            txtMaNV.Enabled = false;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvDMNV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
    }
}
