using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanLyDienThoai
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            functions.Connect();
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNhanVien a = new frmNhanVien();
            a.Show();
        }

        private void nhàCungCấpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNhaCungCap a = new frmNhaCungCap();
            a.Show();
        }

        private void frmLoai_Click(object sender, EventArgs e)
        {
            frmLoai a = new frmLoai();
            a.Show();
        }

        private void frmNhanhieu_Click(object sender, EventArgs e)
        {
            frmNhanHieu a = new frmNhanHieu();
            a.Show();
        }

        private void frmNhanvien_Click(object sender, EventArgs e)
        {
            frmNhanVien a = new frmNhanVien();
            a.Show();
        }

        private void frmKhachhang_Click(object sender, EventArgs e)
        {
            frmKhachHang a = new frmKhachHang();
            a.Show();
        }

        private void frmNhaCungCap_Click(object sender, EventArgs e)
        {
            frmNhaCungCap a=new frmNhaCungCap();
            a.Show();
        }

        private void frmQue_Click(object sender, EventArgs e)
        {
            frmQue a = new frmQue();
            a.Show();
        }

        private void frmHDN_Click(object sender, EventArgs e)
        {
            frmHDN a = new frmHDN();
            a.Show();
        }

        private void frmHDB_Click(object sender, EventArgs e)
        {
            frmHDB a=new frmHDB();
            a.Show();
        }

        private void frmTKHDN_Click(object sender, EventArgs e)
        {
            frmTimHDN a = new frmTimHDN();
            a.Show();
        }

        private void frmTKHDB_Click(object sender, EventArgs e)
        {
            frmTimHDB a = new frmTimHDB();
            a.Show();
        }

        private void frmSP_Click(object sender, EventArgs e)
        {
            frmSanpham a=new frmSanpham();
            a.Show();
        }

        private void frmMH_Click(object sender, EventArgs e)
        {
            frmManHinh a=new frmManHinh();
            a.Show();
        }

        private void frmDSSP_Click(object sender, EventArgs e)
        {
            frmDSSPThang a=new frmDSSPThang();
            a.Show();
        }

        private void frmDSHDN_Click(object sender, EventArgs e)
        {
            frmDSHDNQuy a=new frmDSHDNQuy();
            a.Show();
        }

        private void frmDSHDB_Click(object sender, EventArgs e)
        {
            frmDSHDBNam a=new frmDSHDBNam();
            a.Show();
        }

        private void frmDSNV_Click(object sender, EventArgs e)
        {
            frmDSNVThang a=new frmDSNVThang();
            a.Show();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
