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
    public partial class frmSanpham : Form
    {
        public frmSanpham()
        {
            InitializeComponent();
        }
        DataTable tblSP;
        private void loadDataToGridView()
        {
            string sql = "select MaSP,TenSP,Maloai,Manhanhieu,Gianhap,Giaban,Soluong,Thoigianbaohanh,MaMH from tblSanpham";
            tblSP = functions.GetDataToTable(sql);
            dgvDMSanpham.DataSource = tblSP;
            dgvDMSanpham.Columns[0].HeaderText = "Mã sản phẩm";
            dgvDMSanpham.Columns[1].HeaderText = "Tên sản phẩm";
            dgvDMSanpham.Columns[2].HeaderText = "Mã loại";
            dgvDMSanpham.Columns[3].HeaderText = "Mã nhãn hiệu";
            dgvDMSanpham.Columns[4].HeaderText = "Giá nhập";
            dgvDMSanpham.Columns[5].HeaderText = "Giá bán";
            dgvDMSanpham.Columns[6].HeaderText = "Số lượng";
            dgvDMSanpham.Columns[7].HeaderText = "Thời gian bảo hành";
            dgvDMSanpham.Columns[8].HeaderText = "Mã màn hình";

            dgvDMSanpham.AllowUserToAddRows = false;
            dgvDMSanpham.EditMode = DataGridViewEditMode.EditProgrammatically;

        }

        private void frmSanpham_Load(object sender, EventArgs e)
        {
            functions.Connect();
            txtMasp.Enabled = false;
            btnDong.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            loadDataToGridView();
            functions.FIllcombo("SELECT Maloai,Tenloai FROM tblLoai", cboMaloai, "Maloai", "Tenloai");
            cboMaloai.SelectedIndex = -1;
            functions.FIllcombo("SELECT MaMH,TenMH FROM tblManhinh", cboMamanhinh, "MaMH", "TenMH");
            cboMamanhinh.SelectedIndex = -1;
            functions.FIllcombo("SELECT Manhanhieu,Tennhanhieu FROM tblNhanhieu", cboManhanhieu, "Manhanhieu", "Tennhanhieu");
            cboManhanhieu.SelectedIndex = -1;
        }
        private void ResetValues()
        {
            txtMasp.Text = "";
            txtTensp.Text = "";
            cboMaloai.SelectedItem = null;
            cboMamanhinh.SelectedItem = null;
            cboManhanhieu.SelectedItem = null;
            txtSoluong.Text = "";
            txtDongianhap.Text = "";
            txtDongiaban.Text = "";
            txtThoigianBH.Text = "";
            txtAnh.Text = "";
            picAnh.Image = null;    
        }

        private void dgvDMSanpham_Click(object sender, EventArgs e)
        {
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnTimkiem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            txtMasp.Enabled = false;
            txtTensp.Focus();
            ResetValues();
            loadDataToGridView();
            txtMasp.Text = functions.tangkey("select top(1) convert(integer ,substring(MaSP,3,3)+1) as soma from  tblSanpham order by soma desc", "SP");
            picAnh.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMasp.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMasp.Focus();
                return;
            }
            if (txtTensp.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTensp.Focus();
                return;
            }
            if (cboMaloai.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn loại sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaloai.Focus();
                return;
            }
            if (cboMamanhinh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn mã màn hình", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMamanhinh.Focus();
                return;
            }
            if (cboManhanhieu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn mã nhãn hiệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboManhanhieu.Focus();
                return;
            }
            if (txtSoluong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoluong.Focus();
                return;
            }
            if (txtDongianhap.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập đơn giá nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDongianhap.Focus();
                return;
            }
            if (txtDongiaban.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập đơn giá bán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDongiaban.Focus();
                return;
            }
            if (txtThoigianBH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập thời gian bảo hành", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtThoigianBH.Focus();
                return;
            }
            sql = "Update tblSanpham set  TenSP = N'" + txtTensp.Text + "', Maloai = '" + cboMaloai.SelectedValue + "', Manhanhieu = '" + cboManhanhieu.SelectedValue + "', Gianhap ='" + Convert.ToDouble(txtDongianhap.Text) + "'," +
                 "Giaban = '" + Convert.ToDouble(txtDongiaban.Text) + "',Soluong = '" + Convert.ToDouble(txtSoluong.Text) + "', Thoigianbaohanh = '" + Convert.ToDouble(txtThoigianBH.Text) + "',MaMH ='" + cboMamanhinh.SelectedValue + "', Anh =N'" + txtAnh.Text + "' where MaSP ='" + txtMasp.Text + "'";
            functions.Runsql(sql);
            loadDataToGridView();
            ResetValues();
            btnHuy.Enabled = false;

        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void txtDongianhap_TextChanged(object sender, EventArgs e)
        {
            double gianhap = 0, giaxuat;

            if (txtDongianhap.Text == "")
            {
                gianhap = 0;
            }
            else
            {
                gianhap = Convert.ToDouble(txtDongianhap.Text);
            }
            giaxuat = Math.Round(gianhap * 110 / 100, 2);
            txtDongiaban.Text = giaxuat.ToString();
            txtDongiaban.Enabled = false;
        }

        private void txtSoluong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtDongianhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtThoigianBH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMasp.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tblSanpham WHERE MaSP='" + txtMasp.Text + "'";
                functions.RunsqlDel(sql);
                loadDataToGridView();
                ResetValues();
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
            txtMasp.Enabled = false;
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtMasp.Text == "") && (txtTensp.Text == "") && (cboMaloai.Text == "") && (cboMamanhinh.Text == "") && (cboManhanhieu.Text == ""))
            {
                MessageBox.Show("Bạn hãy nhập điều kiện tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * from tblSanpham WHERE 1=1";
            if (txtMasp.Text != "")
                sql += " AND MaSP LIKE N'%" + txtMasp.Text + "%'";
            if (txtTensp.Text != "")
                sql += " AND TenSP LIKE N'%" + txtTensp.Text + "%'";
            if (cboMaloai.Text != "")
                sql += " AND Maloai LIKE N'%" + cboMaloai.SelectedValue + "%'";
            if (cboMamanhinh.Text != "")
                sql += " AND Mamanhinh LIKE N'%" + cboMamanhinh.SelectedValue + "%'";
            if (cboManhanhieu.Text != "")
                sql += " AND Manhanhieu LIKE N'%" + cboManhanhieu.SelectedValue + "%'";
            if (txtThoigianBH.Text != "")
                sql += " AND Thoigianbaohanh LIKE N'%" + txtThoigianBH.Text + "%'";
            tblSP = functions.GetDataToTable(sql);
            if (tblSP.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Có " + tblSP.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvDMSanpham.DataSource = tblSP;
            ResetValues();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "bitmap(*.bmp)|*.bmp|Gif(*.gif)|*.gif|All files(*.*)|*.*";
            dlgOpen.InitialDirectory = "E:\\";
            dlgOpen.FilterIndex = 4;
            dlgOpen.Title = "Chon hinh anh de hien thi";
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                picAnh.Image = Image.FromFile(dlgOpen.FileName);
                txtAnh.Text = dlgOpen.FileName;
            }

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMasp.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMasp.Focus();
                return;
            }
            if (txtTensp.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTensp.Focus();
                return;
            }
            if (cboMaloai.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn loại sản phẩm ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaloai.Focus();
                return;
            }
            if (cboManhanhieu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn mã nhãn hiệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboManhanhieu.Focus();
                return;
            }
            if (cboMamanhinh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn mã màn hình ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMamanhinh.Focus();
                return;
            }
            if (txtSoluong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoluong.Focus();
                return;
            }
            if (txtDongianhap.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập giá nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDongianhap.Focus();
                return;
            }
            if (txtDongiaban.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập giá bán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDongiaban.Focus();
                return;
            }
            if (txtThoigianBH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập thời gian bảo hành", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtThoigianBH.Focus();
                return;
            }
            if (txtAnh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn ảnh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnOpen.Focus();
                return;
            }
            sql = "SELECT MaSP FROM tblSanpham WHERE MaSP = N'" + txtMasp.Text.Trim() + "'";
            if (functions.Checkey(sql))
            {
                MessageBox.Show("Mã sản phẩm này đã tồn tại, bạn phải chọn mã sản phẩm khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMasp.Focus();
                return;
            }
            sql = "INSERT INTO tblSanpham(MaSP, TenSP,Maloai, Manhanhieu, Gianhap,Giaban,Soluong,Thoigianbaohanh,MaMH,Anh)" +
                 " VALUES('" + txtMasp.Text.Trim() + "',N'" + txtTensp.Text.Trim() +
                 "','" + cboMaloai.SelectedValue.ToString() + "','" +
                 cboManhanhieu.SelectedValue.ToString() + "','"
                  + txtDongianhap.Text.Trim() + "','" +
                 txtDongiaban.Text.Trim() + "','" +
                 txtSoluong.Text.Trim() + "','" +
                 txtThoigianBH.Text.Trim() + "','" +
                 cboMamanhinh.SelectedValue.ToString() + "',N'"
                 + txtAnh.Text.Trim() + "')";

            functions.Runsql(sql);
            loadDataToGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
        }

        private void btnHienthi_Click(object sender, EventArgs e)
        {
            ResetValues();
            loadDataToGridView();
            btnThem.Enabled = true ;
        }

        private void dgvDMSanpham_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (btnThem.Enabled == false)
            {
                MessageBox.Show("bạn đang ở chế độ thêm mới", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMasp.Focus();
                return;
            }
            if (dgvDMSanpham.Rows.Count == 0)
            {
                MessageBox.Show("không có dữ liệu", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            txtMasp.Text = dgvDMSanpham.CurrentRow.Cells[0].Value.ToString();
            txtTensp.Text = dgvDMSanpham.CurrentRow.Cells[1].Value.ToString();
            string ma = dgvDMSanpham.CurrentRow.Cells[2].Value.ToString();
            cboMaloai.Text = functions.GetFileValues("select Tenloai from tblLoai where Maloai=" + "'" + ma + "'");
            string manh = dgvDMSanpham.CurrentRow.Cells[3].Value.ToString();
            cboManhanhieu.Text = functions.GetFileValues("select Tennhanhieu from tblNhanhieu where Manhanhieu=" + "'" + manh + "'");
            txtDongianhap.Text = dgvDMSanpham.CurrentRow.Cells[4].Value.ToString();
            txtDongiaban.Text = dgvDMSanpham.CurrentRow.Cells[5].Value.ToString();
            txtSoluong.Text = dgvDMSanpham.CurrentRow.Cells[6].Value.ToString();
            txtThoigianBH.Text = dgvDMSanpham.CurrentRow.Cells[7].Value.ToString();
            string mamh = dgvDMSanpham.CurrentRow.Cells[8].Value.ToString();
            cboMamanhinh.Text = functions.GetFileValues("select TenMH from tblManhinh where MaMH='" + mamh + "'");

            txtAnh.Text = functions.GetFileValues("SELECT Anh FROM tblSanpham WHERE MaSP = N'" + txtMasp.Text + "'");

            picAnh.Image = Image.FromFile(txtAnh.Text);
           
            dgvDMSanpham.AllowUserToAddRows = false;
            dgvDMSanpham.EditMode = DataGridViewEditMode.EditProgrammatically;
            picAnh.SizeMode = PictureBoxSizeMode.Zoom;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnHuy.Enabled = true;
        }
    }

}
    
