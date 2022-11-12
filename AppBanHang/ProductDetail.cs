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
using System.Drawing.Imaging;

namespace AppBanHang
{
    public class ProductDetail : Panel
    {
        string id;
        static DataTable dataTable;
        public int amount = 0;
        public Label amountNum;
        public Panel paymentZone;
        public Button addToCart;
        public string ID;
        public Int64 price;
        public string Tensp;
        public ProductDetail(string id)
        {
            this.Location = new Point(98, 152);
            this.Size = new Size(1083, 693);
            this.id = id;
            //this.Dock = DockStyle.Fill;
            Loading();

            ID = dataTable.Rows[0]["MANHACCU"].ToString();
            
            Label productName = new Label();
            Tensp = dataTable.Rows[0]["TENSP"].ToString();
            productName.Text = Tensp;
            productName.Font = new Font("Arial", 20, FontStyle.Bold);
            productName.AutoSize = true;
            Bitmap bm1 = (Bitmap)NHACCU.ResourceManager.GetObject(id);

            Label back = new Label();
            back.Location = new Point(650, 0);
            back.AutoSize = true;
            back.Text = "Quay lại";
            back.Font = new Font("Arial", 12, FontStyle.Italic | FontStyle.Underline);


            PictureBox pb = new PictureBox();
            pb.Location = new Point(0, 50);
            pb.Size = new Size(400, 400);
            pb.BackgroundImage = bm1;
            pb.BackgroundImageLayout = ImageLayout.Stretch;
            pb.BackColor = Color.White;

            Label productPrice = new Label();
            price = Convert.ToInt64(dataTable.Rows[0]["GIABAN"]);
            productPrice.Text = price.ToString("N0") + " VND";
            productPrice.Location = new Point(410, 50);
            productPrice.Font = new Font("Arial", 20, FontStyle.Bold);
            productPrice.ForeColor = Color.Red;
            productPrice.AutoSize = true;

            Label titleFeature = new Label();
            titleFeature.Location = new Point(410, 80);
            titleFeature.Text = "Đặc điểm nổi bật";
            titleFeature.Font = new Font("Arial", 18, FontStyle.Bold);
            titleFeature.AutoSize = true;

            FlowLayoutPanel Features = new FlowLayoutPanel();
            Features.Location = new Point(410, 110);
            Features.Size = new Size(300, 270);
            Features.BackColor = Color.LightGray;

            paymentZone = new Panel();
            paymentZone.Size = new Size(300, 60);
            paymentZone.Location = new Point(410, 390);
            //paymentZone.BackColor = Color.Gray;

            Label amountLabel = new Label();
            amountLabel.Text = "Số lượng";
            amountLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            amountLabel.AutoSize = true;
            
            Button minusButton = new Button();
            minusButton.Text = "-";
            minusButton.AutoSize = true;
            minusButton.Size = new Size(28, 28);
            minusButton.Location = new Point(0, 20);
            minusButton.BackColor = Color.White;
            minusButton.Click += new EventHandler(modAmount);
            minusButton.Name = "minusButton";

            amountNum = new Label();
            amountNum.Text = amount.ToString();
            amountNum.Size = new Size(25, 25);
            amountNum.Location = new Point(30, 21);
            amountNum.BackColor = Color.White;
            amountNum.TextAlign = ContentAlignment.MiddleCenter;

            Button plusButton = new Button();
            plusButton.Text = "+";
            plusButton.AutoSize = true;
            plusButton.Size = new Size(28, 28);
            plusButton.Location = new Point(2*28, 20);
            plusButton.BackColor = Color.White;
            plusButton.Click += new EventHandler(modAmount);
            plusButton.Name = "plusButton";

            addToCart = new Button();
            addToCart.Text = "Thêm vào giỏ hàng";
            addToCart.Size = new Size(185, 45);
            addToCart.Location = new Point(100 ,8);

            paymentZone.Controls.Add(amountNum);
            paymentZone.Controls.Add(amountLabel);
            paymentZone.Controls.Add(minusButton);  
            paymentZone.Controls.Add(plusButton);
            paymentZone.Controls.Add(addToCart);

            this.Controls.Add(back);
            this.Controls.Add(productName);
            this.Controls.Add(pb);
            this.Controls.Add(productPrice);
            this.Controls.Add(Features);
            this.Controls.Add(titleFeature);
            this.Controls.Add(paymentZone);
        }
        public void reloadAmount()
        {
            amountNum.Text = amount.ToString();
        }
        public void modAmount(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            if (bt.Name == "minusButton")
            {
                amount--;
            }
            else amount++;
            reloadAmount();
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
