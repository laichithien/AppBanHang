using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using AppBanHang.Resources;
using AppBanHang.DAO;
using System.Data;

namespace AppBanHang
{
    public class ProductDetail : Panel
    {
        string id;
        static DataTable dataTable;
        public ProductDetail(string id)
        {
            this.Location = new Point(98, 152);
            this.Size = new Size(1083, 693);
            this.id = id;
            Loading();

            Label productName = new Label();
            productName.Text = dataTable.Rows[0]["TENSP"].ToString();
            productName.Font = new Font("Arial", 20, FontStyle.Bold);
            productName.AutoSize = true;
            Bitmap bm1 = (Bitmap)NHACCU.ResourceManager.GetObject(id);

            PictureBox pb = new PictureBox();
            pb.Location = new Point(0, 50);
            pb.Size = new Size(300, 300);
            pb.BackgroundImage = bm1;
            pb.BackgroundImageLayout = ImageLayout.Stretch;
            pb.BackColor = Color.White;

            Label productPrice = new Label();
            productPrice.Text = Convert.ToInt64(dataTable.Rows[0]["GIABAN"]).ToString("N0") + " VND";
            productPrice.Location = new Point(310, 50);
            productPrice.Font = new Font("Arial", 20, FontStyle.Bold);
            productPrice.ForeColor = Color.Red;
            productPrice.AutoSize = true;
            
            this.Controls.Add(productName);
            this.Controls.Add(pb);
            this.Controls.Add(productPrice);
        }
        private void Loading()
        {
            string query = "select * from NHACCU where MANHACCU like '" + id + "%'";
            Data_Provider provider = new Data_Provider();
            dataTable = provider.ExecuteQuery(query);
            //dataGridView1.DataSource = provider.ExecuteQuery(query);
        }

    }
}
