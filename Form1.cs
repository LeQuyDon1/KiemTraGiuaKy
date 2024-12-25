using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KiemTraGiuaKy
{
    public partial class frmSinhVien : Form
    {
        public frmSinhVien()
        {
            InitializeComponent();
        }
        Model1 db = new Model1();
        public List<SinhVienModel> listmodel = new List<SinhVienModel>();
        private bool isEditing = false;


        private void frmSinhVien_Load(object sender, EventArgs e)
        {
            LoadComboBox();
            load();
            btnLuu.Enabled = false;
            btnKLuu.Enabled = false;
        }
        public void load()
        {
            var lop = db.Lops.ToList();
            var list = db.SinhViens.ToList();
            foreach (var item in list)
            {
                SinhVienModel model = new SinhVienModel();
                model.MaSV = item.MaSV;
                model.HoTenSV = item.HoTenSV;
                model.NgaySinh = item.NgaySinh;
                model.TenLop = lop.Where(s => s.MaLop == item.MaLop).FirstOrDefault().TenLop;
                listmodel.Add(model);
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = listmodel;
        }
        private void LoadComboBox()
        {
            var db = new Model1();
            var lop = db.Lops.ToList();
            cbLop.DisplayMember = "TenLop";
            cbLop.ValueMember = "MaLop";
            cbLop.DataSource = lop;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            txtMa.Text = row.Cells[0].Value.ToString();
            txtHoten.Text = row.Cells[1].Value.ToString();
            dtNgaySInh.Text = row.Cells[2].Value.ToString();
            cbLop.Text = row.Cells[3].Value.ToString();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            var newUser = new SinhVien
            {
                MaSV = txtMa.Text,
                HoTenSV = txtHoten.Text,
                NgaySinh = dtNgaySInh.Value,
                MaLop = cbLop.SelectedValue.ToString(),
            };
            db.SinhViens.Add(newUser);
            db.SaveChanges();
            dataGridView1.DataSource = null;
            listmodel.Clear();
            load();
            txtMa.Text = "";
            txtHoten.Text = "";
            dtNgaySInh.Text = "";
            cbLop.Text = "";
            isEditing = true;
            btnLuu.Enabled = true;
            btnKLuu.Enabled = true;
            MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var index = dataGridView1.SelectedRows[0].Index;
                var ma = dataGridView1.Rows[index].Cells[0].Value.ToString();

                var Sua = db.SinhViens.FirstOrDefault(u => u.MaSV == ma);

                if (Sua != null)
                {
                    Sua.MaLop = txtMa.Text;
                    Sua.HoTenSV = txtHoten.Text;
                    Sua.NgaySinh = dtNgaySInh.Value;
                    Sua.MaLop = cbLop.SelectedValue.ToString();
                    db.SaveChanges();
                    dataGridView1.DataSource = null;
                    listmodel.Clear();
                    load();
                    isEditing = true;
                    btnLuu.Enabled = true;
                    btnKLuu.Enabled = true;
                    MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var index = dataGridView1.SelectedRows[0].Index;
            var ma = dataGridView1.Rows[index].Cells[0].Value.ToString();
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này không?", "Xác nhận xóa", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                var xoa = db.SinhViens.FirstOrDefault(u => u.MaSV == ma);

                if (xoa != null)
                {
                    db.SinhViens.Remove(xoa);
                    db.SaveChanges();
                    listmodel.Clear();
                    load();
                    isEditing = true;
                    btnLuu.Enabled = true;
                    btnKLuu.Enabled = true;
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
}

        private void btnLuu_Click(object sender, EventArgs e)
        {
            isEditing = false;
            btnLuu.Enabled = false;
            btnKLuu.Enabled = false;
        }

        private void btnKLuu_Click(object sender, EventArgs e)
        {
            isEditing = false;
            btnLuu.Enabled = false;
            btnKLuu.Enabled = false;
            listmodel.Clear();
            load();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string timTen = txtTim.Text.Trim();
            if (!string.IsNullOrEmpty(timTen))
            {
                var db = new Model1();
                var lop = db.Lops.ToList();
                var list = db.SinhViens.Where(u => u.HoTenSV.Contains(timTen)).ToList();
                listmodel.Clear();
                foreach (var item in list)
                {
                    SinhVienModel model = new SinhVienModel
                    {
                        MaSV = item.MaSV,
                        HoTenSV = item.HoTenSV,
                        NgaySinh = item.NgaySinh,
                        TenLop = lop.Where(s => s.MaLop == item.MaLop).FirstOrDefault().TenLop
                    };
                    listmodel.Add(model);
                }
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = listmodel;
                dataGridView1.Refresh();
            }
            else
            {
                dataGridView1.DataSource = null;
                listmodel.Clear();
                load();
            }
        }

        private void frmSinhVien_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
    }
}
