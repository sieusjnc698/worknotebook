using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace demo10.Order
{
    public partial class FormMain : Form
    {
        worknotebookEntities db = new worknotebookEntities();
        public FormMain()
        {
            InitializeComponent();
            sieusin.DataSource = db.data.ToList();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void btnBrower_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = open.Filter = "JPEG files (*.jpg) |*.jpg|All files (*.*)|*.*";
            open.FilterIndex = 1;
            open.RestoreDirectory = true;
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = open.FileName;
                txtPath.Text = open.FileName;
            }
        }

        private void btnRe_Click(object sender, EventArgs e)
        {
            datum entity = sieusin.Current as datum;
            if (entity.id == 0)
                sieusin.RemoveCurrent();
            else
                sieusin.ResetCurrentItem();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Tên khách hàng không được để trống ! ");
                return;
            }
            sieusin.EndEdit();
            datum entity = sieusin.Current as datum;
            if (entity.id == 0)
                db.data.Add(entity);
            else
                db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            datum entity = sieusin.Current as datum;
            int icountSelectedRow = Main.SelectedRows.Count;
            if (icountSelectedRow == 0)
                MessageBox.Show("Bạn hãy chọn dòng cần xoá!");
            else
                foreach (DataGridViewRow row in Main.SelectedRows)
                    if (!row.IsNewRow) Main.Rows.Remove(row);
            db.data.Remove(entity);
            sieusin.ResetCurrentItem();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new Form1();
            form.ShowDialog();
        }


    }
}
