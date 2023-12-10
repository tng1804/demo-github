using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace quanlihosonhansu.Admin__phongban
{
    public partial class phongban : Form

    {
        string macu, dk = "";
        bool ktthem;
        public phongban()
        {
            InitializeComponent();
        }

        private void phongban_Load(object sender, EventArgs e)
        {
            LayNguon();
            lbSoDong.Text = "Danh sách có " + dgDanhSach.RowCount + " dòng.";
            KhoaMo(true);
            
        }
        void LayNguon()
        {
            string sql = "Select id, ten, dia_chi from phongban";
            //if (dk != "")
              //  sql = sql + " where MaBoPhan like '%" + dk + "%' or MaPhong like '%" + dk + "%'";
            @public.GanNguonDataGridView(dgDanhSach, sql);
        }
        void KhoaMo(bool b)
        {
            dgDanhSach.Enabled = b;
            txtID.ReadOnly = b;
            txtTen.ReadOnly = b;
            txtDiaChi.ReadOnly = b;
            txtTimKiem.ReadOnly = !b;

            btnThem.Enabled = b;
            btnSua.Enabled = !b;
            btnXoa.Enabled = !b;
            btnGhi.Enabled = !b;
            btnKhongGhi.Enabled = !b;
            btnTim.Enabled = b;
            btnXuat.Enabled = b;
            btnThoat.Enabled = b;
        }
        void XoaTrang()
        {
   
            txtID.Text = "";
            txtTen.Text = "";
            txtDiaChi.Text = "";
            txtTimKiem.Text = "";
            
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            XoaTrang();
            KhoaMo(false);
            ktthem = true;
            txtTen.Focus();
            txtID.ReadOnly = true;
            txtTimKiem.ReadOnly = true;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
           
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            //if (txtID.Text == "") return;
            KhoaMo(false);
            ktthem = false;
            macu = txtID.Text;
            txtID.ReadOnly = true;
            txtID.Focus();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgDanhSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgDanhSach_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgDanhSach.RowCount <= 0) return;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgDanhSach.Rows[e.RowIndex];
              
                txtID.Text = row.Cells[0].Value.ToString();
                txtTen.Text = row.Cells[1].Value.ToString();
          
                txtDiaChi.Text = row.Cells[2].Value.ToString();
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        private void btnKhongGhi_Click(object sender, EventArgs e)
        {
            XoaTrang();
            KhoaMo(true);
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                //if ((txtTimKiem.Text == "") || (txtTen.Text == "Nhập vào tên phòng ban."))
                //{
                //    MessageBox.Show("Bạn chưa nhập tên phòng ban", "Nhập tên phòng ban", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
                if (txtTimKiem.Text == ""){
                    @public.GanNguonDataGridView(dgDanhSach, "select * from phongban");
                    lbSoDong.Text = "Danh sách có " + dgDanhSach.RowCount + " dòng.";


                }
                else
                {

                    if (i == 1)
                    {
                        @public.GanNguonDataGridView(dgDanhSach, "select * from phongban where id like N'" + txtTimKiem.Text + "%'");
                        lbSoDong.Text = "Danh sách có " + dgDanhSach.RowCount + " dòng.";
                    }
                    if (i == 2)
                    {
                        @public.GanNguonDataGridView(dgDanhSach, "select * from phongban where ten like N'" + txtTimKiem.Text + "%'");
                        lbSoDong.Text = "Danh sách có " + dgDanhSach.RowCount + " dòng.";
                    }
                    if (i == 3)
                    {
                        @public.GanNguonDataGridView(dgDanhSach, "select * from phongban where dia_chi like N'" + txtTimKiem.Text + "%'");
                        lbSoDong.Text = "Danh sách có " + dgDanhSach.RowCount + " dòng.";
                    }

                }
            }
            catch
            {
                MessageBox.Show("Tìm kiếm sai");
            }
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            {
                if (MessageBox.Show("Bạn có muốn xuất danh sách ra EXCEL không ?", "Thông báo",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dgDanhSach.RowCount <= 0) return;
                    @public.ToExcel(dgDanhSach, "PhongBan.xlsx");
                }
            }
        }
           
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "") return;
            macu = txtID.Text;
            if (MessageBox.Show("Bạn có muốn xóa đối tượng đang chọn không?", "Thông Báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                @public.XoaDongDL("phongban", "id", macu);
                MessageBox.Show("Bạn đã xóa thành công", "Thông Báo",
                MessageBoxButtons.OK,MessageBoxIcon.Information);
                LayNguon();
            }
        }
        int i = 0;
        private void rdbID_CheckedChanged(object sender, EventArgs e)
        {
            i = 1;
        }

        private void rdbTen_CheckedChanged(object sender, EventArgs e)
        {
            i = 2;
        }

        private void rdbDiaChi_CheckedChanged(object sender, EventArgs e)
        {
            i = 3;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtTen.Text = "";
            txtDiaChi.Text = "";
            txtTimKiem.Text = "";
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            @public.GanNguonDataGridView(dgDanhSach, "select * from phongban");
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            //if (txtID.Text == "")
            //{
              //  cbmabp.Focus();
              //  return;
            //}
            if (@public.ktTrungMa("phongban", "id", ktthem, txtID.Text, macu))
            {
                MessageBox.Show("Bạn nhập  mã phòng đã tồn tại.", "Thông Báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtID.Focus();
                return;
            }
            string sql = "";
            if (ktthem == true)

                //sql = "SET IDENTITY_INSERT phongban ON " +
                //  "Insert into phongban(id, ten, dia_chi) Values(@1,@2,@3)" +
                //"SET IDENTITY_INSERT phongban OFF ";
                sql = "Insert into phongban(ten, dia_chi) Values(@2,@3)";
                

            else
                sql = "Update phongban set ten=@2,dia_chi=@3 Where id=@1";
            SqlConnection conn = @public.KetNoi();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if(ktthem == true)
            {
                MessageBox.Show("Bạn đã thêm thành công", "Thông Báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Bạn đã cập nhật thành công", "Thông Báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            //cmd.Parameters.Add("@1", SqlDbType.Char).Value = cbmabp.SelectedValue.ToString();
            cmd.Parameters.Add("@1", SqlDbType.Char).Value = txtID.Text;
            cmd.Parameters.Add("@2", SqlDbType.NVarChar).Value = txtTen.Text;
            cmd.Parameters.Add("@3", SqlDbType.NVarChar).Value = txtDiaChi.Text;
            if (ktthem == false) cmd.Parameters.Add("@macu", SqlDbType.Char).Value = macu;
            if (conn.State != ConnectionState.Open) conn.Open();
            cmd.ExecuteNonQuery();
            LayNguon();
            KhoaMo(true);
            XoaTrang();
        }
        




    }

        
    }
