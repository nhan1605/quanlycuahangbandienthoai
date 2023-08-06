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
    public partial class frmNhanHieu : Form
    {
        public frmNhanHieu()
        {
            InitializeComponent();
        }
        DataTable tblNH;
        private void frmNhanHieu_Load(object sender, EventArgs e)
        {
            txtmanhanhieu.Enabled = false;
            btnluu.Enabled = false;
            btnhuy.Enabled = false;
            load_datagrid();
        }
        private void load_datagrid()
        {
            string sql;
            sql = "select manhanhieu, tennhanhieu from tblNhanhieu";
            tblNH = functions.GetDataToTable(sql);
            dgridNhanHieu.DataSource = tblNH;
            dgridNhanHieu.Columns[0].HeaderText = "Mã nhãn hiệu";
            dgridNhanHieu.Columns[1].HeaderText = "Tên nhãn hiệu";
        }

        private void dgridNhanHieu_Click(object sender, EventArgs e)
        {
            if (btnthem.Enabled == false)
            {
                MessageBox.Show("Bạn đang ở chế độ thêm mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dgridNhanHieu.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            txtmanhanhieu.Text = dgridNhanHieu.CurrentRow.Cells["manhanhieu"].Value.ToString();
            txttennhanhieu.Text = dgridNhanHieu.CurrentRow.Cells["tennhanhieu"].Value.ToString();
            btnsua.Enabled = true;
            btnxoa.Enabled = true;
            btnhuy.Enabled = true;
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            btnthem.Enabled = false;
            btnluu.Enabled = true;
            btnsua.Enabled = false;
            btnxoa.Enabled = false;
            btnhuy.Enabled = true;
            txtmanhanhieu.Enabled = false;
            txttennhanhieu.Focus();
         
            txtmanhanhieu.Text=  functions.tangkey("select TOP(1) (substring(Manhanhieu,3,3)+1) as soma from  tblNhanhieu order by soma desc", "NH");
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            if (txtmanhanhieu.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mã nhãn hiệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtmanhanhieu.Focus();
                return;
            }
            if (txttennhanhieu.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên nhãn hiệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txttennhanhieu.Focus();
                return;
            }
            string sql;
            sql = "select manhanhieu from tblNhanhieu where manhanhieu = N'" + txtmanhanhieu.Text.Trim() + "'";

            if (functions.Checkey(sql))
            {
                MessageBox.Show("Bị trùng mã nhãn hiệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtmanhanhieu.Text = "";
                txtmanhanhieu.Focus();
                return;
            }
            sql = "insert into tblNhanhieu(manhanhieu, tennhanhieu) values (N'" + txtmanhanhieu.Text.Trim() + "', N'" + txttennhanhieu.Text.Trim() + "')";
            functions.Runsql(sql);
            load_datagrid();
            resetvalue();
            btnhuy.Enabled = false;
            btnthem.Enabled = true;
            btnsua.Enabled = true;
            btnxoa.Enabled = true;
            btnluu.Enabled = false;
            txtmanhanhieu.Enabled = false;
        }
        private void resetvalue()
        {
            txtmanhanhieu.Text = "";
            txttennhanhieu.Text = "";
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtmanhanhieu.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dgridNhanHieu.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txttennhanhieu.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tên nhãn hiệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txttennhanhieu.Focus();
                return;
            }


            sql = "update tblNhanhieu set tennhanhieu=N'" + txttennhanhieu.Text.ToString() + "' where manhanhieu=N'" + txtmanhanhieu.Text + "'";
            functions.Runsql(sql);
            load_datagrid();
            resetvalue();
            btnhuy.Enabled = false;

        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (txtmanhanhieu.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dgridNhanHieu.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                string sql;
                sql = "delete tblNhanhieu where manhanhieu=N'" + txtmanhanhieu.Text.Trim() + "'";
                functions.Runsql(sql);
                load_datagrid();
                resetvalue();
            }

        }

        private void btnhuy_Click(object sender, EventArgs e)
        {
            btnhuy.Enabled = false;
            btnthem.Enabled = true;
            btnsua.Enabled = true;
            btnxoa.Enabled = true;
            txtmanhanhieu.Enabled = false;
            txtmanhanhieu.Focus();
            btnluu.Enabled = false;
            resetvalue();

        }

        private void btndong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
