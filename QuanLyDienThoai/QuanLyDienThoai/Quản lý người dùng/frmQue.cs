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
    public partial class frmQue : Form
    {
        public frmQue()
        {
            InitializeComponent();
        }
        DataTable tblque;
        private void frmQue_Load(object sender, EventArgs e)
        {
            txtTenque.Focus();
            txtMaque.Enabled = false;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;

            loadDataToGridView();
        }
        private void loadDataToGridView()
        {
            string sql = "select * from" +
                " tblQue";
            tblque = functions.GetDataToTable(sql);
            dgvQue.DataSource = tblque;
            dgvQue.Columns[0].HeaderText = "Mã quê";
            dgvQue.Columns[1].HeaderText = "Tên quê";
            dgvQue.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }
        private void ResetValues()
        {
            txtMaque.Text = "";
            txtTenque.Text = "";
        }
        private void TBNhapDayDu()
        {
            if (txtMaque.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã quê của nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaque.Focus();
                return;
            }
            if (txtTenque.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên quê của nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenque.Focus();
                return;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            txtMaque.Enabled = false;
            txtMaque.Focus();
            txtTenque.Enabled = true;
            ResetValues();
            loadDataToGridView();
            txtMaque.Text = functions.tangkey("select top(1) (substring(Maque,3,3)+1) as soma from  tblQue order by soma desc", "MQ");

        }

        private void btnSua_Click(object sender, EventArgs e)
        {

            if (tblque.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                return;
            }
            if (txtMaque.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenque.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên chất liệu", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenque.Focus();
                return;
            }
            string sql = "update tblQue set Tenque=N'" + txtTenque.Text.Trim() + "' WHERE Maque='" +
                txtMaque.Text + "'";

            functions.Runsql(sql);
            loadDataToGridView();
            ResetValues();
            btnHuy.Enabled = false;
            txtMaque.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaque.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tblQue WHERE Maque='" + txtMaque.Text + "'";
                functions.RunsqlDel(sql);
                loadDataToGridView();
                ResetValues();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

            string sql;
            if (txtMaque.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mã quê", "thong bao");
                txtMaque.Focus();
                return;
            }
            if (txtTenque.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tên quê");
                txtMaque.Focus();
                return;
            }
            sql = "SELECT Maque FROM tblQue WHERE Maque='" + txtMaque.Text.Trim() + "'";
            if (functions.Checkey(sql))
            {
                MessageBox.Show("ma nay da co, ban phia nhap ma khac");
                txtMaque.Focus();
                txtMaque.Text = "";
                return;
            }
            else
            {
                sql = "INSERT INTO tblQue(Maque, Tenque) VALUES('" + txtMaque.Text.Trim() + "', " +

                 "N'" + txtTenque.Text.Trim() + "')";
                functions.Runsql(sql);

                loadDataToGridView();
                ResetValues();
                btnXoa.Enabled = true;
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnHuy.Enabled = false;
                btnLuu.Enabled = false;
                txtTenque.Enabled = false;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnHuy.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaque.Enabled = false;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvQue_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaque.Focus();
                return;
            }
            txtMaque.Text = dgvQue.CurrentRow.Cells[0].Value.ToString();
            txtTenque.Text = dgvQue.CurrentRow.Cells[1].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            txtTenque.Enabled = true;
            btnHuy.Enabled = true;
        }
    }
}
