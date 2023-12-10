using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace quanlihosonhansu.Admin__phongban
{
    internal class @public
    {
        public static SqlConnection conn;
        public static SqlCommand cmd;
        public static string sql = "";

        //SqlDataReader sqldr;

        public static SqlConnection KetNoi()
        {
            // Data Source : Nguon Du lieu
            // connS : Chuỗi kết nối
            string connS = @"Data Source=LAPTOP-0S8UO1DE\MAYAO;Initial Catalog=qlinhansu;Persist Security Info=True;User ID=sa;Password=1";
            conn = new SqlConnection(connS); // ket noi Vs với sql
            return conn;
        }
        public static DataTable LayNguon(string sql)
        {
            // Truy xuất dữ liệu bằng SQL thông qua SqlDataAdapter
            SqlDataAdapter da = new SqlDataAdapter(sql, KetNoi());
            DataTable dt = new DataTable(); // lưu dữ liệu lấy từ truy xuất
            da.Fill(dt); // truyền dữ liệu thực hiện từ câu truy vấn vào dth
            return dt;
        }

        public static void GanNguonDataGridView(DataGridView dgData, string sql)
        {
            dgData.DataSource = LayNguon(sql);

        }
        public static void GanNguonCombo(ComboBox cbo, string tenbang, string truonghienthi, string truongma)
        {
            string sql = "Select " + truongma + "," + truonghienthi + " from " + tenbang + "";
            cbo.DataSource = LayNguon(sql); // Gán nguồn dữ liệu để hiển thị lên điều khiển
            cbo.DisplayMember = truonghienthi; // Trường dữ liệu được hiển thị trên điều khiển (name)
            cbo.ValueMember = truongma; // Trường dữ liệu ẩn bên dưới kèm theo DisplayMember
        }
        public static void GanNguonCombo1(ComboBox cbo, string tenbang, string truonghienthi, string truongma, string mabh)
        {
            string sql = "Select " + truongma + "," + truonghienthi + " from " + tenbang + " where " + mabh + " IS NULL ";
            cbo.DataSource = LayNguon(sql); // Gán nguồn dữ liệu để hiển thị lên điều khiển
            cbo.DisplayMember = truonghienthi; // Trường dữ liệu được hiển thị trên điều khiển (name)
            cbo.ValueMember = truongma; // Trường dữ liệu ẩn bên dưới kèm theo DisplayMember
        }

        public static void ThucThiSQL(string sql)
        {
            cmd = new SqlCommand(sql, KetNoi()); // thực thi câu lệnh sql
            if (conn.State != ConnectionState.Open) // Nếu kết nối sql chưa mở
                conn.Open();
            cmd.ExecuteNonQuery();  // thực thi câu lệnh sql và trả về số bản ghi bị tác động cập nhập
        }

        public static void XoaDongDL(string sTenBang, string sTenTruong, string macu)
        {
            sql = "Delete  From " + sTenBang + " Where " + sTenTruong + " = '" + macu + "'";
            cmd = new SqlCommand(sql, KetNoi());
            if (conn.State != ConnectionState.Open) conn.Open();
            cmd.ExecuteNonQuery(); // thực thi câu lệnh SQL và trả về số bản ghi bị tác động cập nhập
        }
        // " + gt + " : truyền gtri
        public static bool ktTrungMa(string sTenBang, string sTenTruong, bool ktThem, string mamoi, string macu)
        {
            sql = "select " + sTenTruong + " from " + sTenBang + " where " + sTenTruong + "='" + mamoi + "'";
            if (ktThem == false) // nếu sửa
                sql = sql + " and " + sTenTruong + "<>'" + macu + "'";
            DataTable dt = LayNguon(sql);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }



        public static void ToExcel(DataGridView dataGridView1, string fileName)
        {
            //khai báo thư viện hỗ trợ Microsoft.Office.Interop.Excel
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook workbook;
            Microsoft.Office.Interop.Excel.Worksheet worksheet;
            try
            {
                //Tạo đối tượng COM.
                excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = true;
                excel.DisplayAlerts = false;
                //tạo mới một Workbooks bằng phương thức add()
                workbook = excel.Workbooks.Add(Type.Missing);
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets["Sheet1"];
                //đặt tên cho sheet
                worksheet.Name = "Phòng Ban";

                // export header trong DataGridView
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    worksheet.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;
                }
                // export nội dung trong DataGridView
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
                // sử dụng phương thức SaveAs() để lưu workbook với filename
                workbook.SaveAs(fileName);
                //đóng workbook
                //workbook.Close();
                //excel.Quit();
                MessageBox.Show("Xuất dữ liệu ra Excel thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                workbook = null;
                worksheet = null;
            }
        }
       

    }
}
