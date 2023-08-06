using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyDienThoai
{
    internal class functions
    {
        public static SqlConnection con;
        public static string conString;

        // tạo kết nối
        public static void Connect()
        {
            conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Download\Nhóm 5 - Lớp 02\QuanLyDienThoai\QuanLyDienThoai\DataBase\CuaHangDienThoai.mdf;Integrated Security=True";
            // doi tuong ket noi
            con = new SqlConnection(conString);
            con.Open();

        }
        // huyr kết nối
        public static void Disconnect()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();   	//Đóng kết nối
                con.Dispose();     //Giải phóng tài nguyên
                con = null;
            }
        }
        public static DataTable GetDataToTable(string sql)
        {
            SqlDataAdapter Mydata = new SqlDataAdapter();
            Mydata.SelectCommand = new SqlCommand(sql, functions.con);
            DataTable table = new DataTable();
            Mydata.Fill(table);
            return table;
        }
        public static bool Checkey(string sql)
        {
            SqlDataAdapter Mydata = new SqlDataAdapter(sql, functions.conString);
            DataTable table = new DataTable();
            Mydata.Fill(table);
            if (table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void Runsql(string sql)
        {
            SqlCommand cmd;
            cmd = new SqlCommand(sql, con);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            cmd.Dispose();
            cmd = null;
        }
        public static void RunsqlDel(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception)
            {
                MessageBox.Show("Dữ liệu đang được dùng không thế xóa!");
            }
            cmd.Dispose();
            cmd = null;
        }
        public static bool IsDate(string s)
        {
            string[] datetime = s.Split('/');
            if (Convert.ToInt32(datetime[0]) < 0 && Convert.ToInt32(datetime[0]) > 31 && Convert.ToInt32(datetime[1]) < 0 && Convert.ToInt32(datetime[1]) > 12 && Convert.ToInt32(datetime[2]) < 1900)
            {
                return false;
            }
            else
            {
                return true;
            }


        }
        public static string CovertToDate(string s)
        {
            string[] parts = s.Split('/');
            string dt = String.Format("{0}/{1}/{2}", parts[1], parts[0], parts[2]);
            return dt;
        }
        public static void FIllcombo(string sql, ComboBox macl, string ma, string ten)
        {
            SqlDataAdapter adp = new SqlDataAdapter(sql, con);
            DataTable table = new DataTable();
            adp.Fill(table);
            macl.DataSource = table;
            macl.ValueMember = ma;
            macl.DisplayMember = ten;
        }
        public static string GetFileValues(string sql)
        {
            string ma = "";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ma = reader.GetValue(0).ToString();
            }
            reader.Close();
            return ma;
        }
        public static string CreateKey(string tiento)
        {
            string key = tiento;
            string[] partsday;
            partsday = DateTime.Now.ToShortDateString().Split('/');
            string d = string.Format("{0}{1}{2}", partsday[0], partsday[1], partsday[2]);
            key = key + d;
            string[] partsTime;//8:52:43 pm  
            partsTime = DateTime.Now.ToLongTimeString().Split(':');
            if (partsTime[2].Substring(2).Trim() == "PM" || partsTime[2].Substring(2).Trim() == "CH")
            {
                partsTime[0] = ConvertTimeTo24(partsTime[0]);
            }
            if (partsTime[2].Substring(2).Trim() == "AM" || partsTime[2].Substring(2).Trim() == "SA")
            {
                if (partsTime[0].Length == 1)
                {
                    partsTime[0] = "0" + partsTime[0];
                }
               partsTime[2] = partsTime[2].Remove(2, 3);
            } 
            
            string t;
            t = string.Format("_{0}{1}{2}", partsTime[0], partsTime[1], partsTime[2]);
            key = key + t;
            return key;
        }
        public static string ConvertTimeTo24(string a)
        {
            string h = "";
            switch (a)
            {
                case "1":
                    h = "13";
                    break;
                case "2":
                    h = "14";
                    break;
                case "3":
                    h = "15";
                    break;
                case "4":
                    h = "16";
                    break;
                case "5":
                    h = "17";
                    break;
                case "6":
                    h = "18";
                    break;
                case "7":
                    h = "19";
                    break;
                case "8":
                    h = "20";
                    break;
                case "9":
                    h = "21";
                    break;
                case "10":
                    h = "22";
                    break;
                case "11":
                    h = "23";
                    break;
                case "12":
                    h = "0";
                    break;
            }
            return h;

        }
        public static string ChuyenSoSangChu(string sNumber)
        {
            int mLen, mDigit;
            string mTemp = "";
            string[] mNumText;
            //Xóa các dấu "," nếu có
            sNumber = sNumber.Replace(",", "");
            mNumText = "không;một;hai;ba;bốn;năm;sáu;bảy;tám;chín".Split(';');
            mLen = sNumber.Length - 1; // trừ 1 vì thứ tự đi từ 0
            for (int i = 0; i <= mLen; i++)//  
            {
                mDigit = Convert.ToInt32(sNumber.Substring(i, 1));
                mTemp = mTemp + " " + mNumText[mDigit];
                if (mLen == i) // Chữ số cuối cùng không cần xét tiếp
                    break;
                switch ((mLen - i) % 9)
                {
                    case 0:
                        mTemp = mTemp + " tỷ";
                        if (sNumber.Substring(i + 1, 3) == "000")
                            i = i + 3;
                        if (sNumber.Substring(i + 1, 3) == "000")
                            i = i + 3;
                        if (sNumber.Substring(i + 1, 3) == "000")
                            i = i + 3;
                        break;
                    case 6:
                        mTemp = mTemp + " triệu";
                        if (sNumber.Substring(i + 1, 3) == "000")
                            i = i + 3;
                        if (sNumber.Substring(i + 1, 3) == "000")
                            i = i + 3;
                        break;
                    case 3:
                        mTemp = mTemp + " nghìn";
                        if (sNumber.Substring(i + 1, 3) == "000")
                            i = i + 3;
                        break;
                    default:
                        switch ((mLen - i) % 3)
                        {
                            case 2:
                                mTemp = mTemp + " trăm";
                                break;
                            case 1:
                                mTemp = mTemp + " mươi";
                                break;
                        }
                        break;
                }
            }
            //Loại bỏ trường hợp x00
            mTemp = mTemp.Replace("không mươi không ", "");
            mTemp = mTemp.Replace("không mươi không", "");
            //Loại bỏ trường hợp 00x
            mTemp = mTemp.Replace("không mươi ", "linh ");
            //Loại bỏ trường hợp x0, x>=2
            mTemp = mTemp.Replace("mươi không", "mươi");
            //Fix trường hợp 10
            mTemp = mTemp.Replace("một mươi", "mười");
            //Fix trường hợp x4, x>=2
            mTemp = mTemp.Replace("mươi bốn", "mươi tư");
            //Fix trường hợp x04
            mTemp = mTemp.Replace("linh bốn", "linh tư");
            //Fix trường hợp x5, x>=2
            mTemp = mTemp.Replace("mươi năm", "mươi lăm");
            //Fix trường hợp x1, x>=2
            mTemp = mTemp.Replace("mươi một", "mươi mốt");
            //Fix trường hợp x15
            mTemp = mTemp.Replace("mười năm", "mười lăm");
            //Bỏ ký tự space
            mTemp = mTemp.Trim();
            //Viết hoa ký tự đầu tiên
            mTemp = mTemp.Substring(0, 1).ToUpper() + mTemp.Substring(1) + " đồng";
            return mTemp;
        }
        public static string tangkey(string sql, string tiento)
        {
            string key = tiento;
            SqlDataAdapter Mydata = new SqlDataAdapter(sql, functions.con);
            DataTable table = new DataTable();
            Mydata.Fill(table);
            if (table.Rows.Count <= 0)
            {
                key = tiento + "1";
            }
            else
            {
                string soma = table.Rows[0][0].ToString();
                key = key + soma;
            }
            return key;
        }
    }
}
