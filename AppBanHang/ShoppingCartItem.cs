using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using AppBanHang.Resources;

namespace AppBanHang
{
    internal class ShoppingCartItem : Panel
    {
        Label amountNum;
        public int amount;
        Label totalPrice;
        public Label deleteButton;
        public string id;
        public ShoppingCartItem(string id, string name, int gia, int amount)
        {
            this.id = id.Trim();
            this.amount = amount;
            this.Size = new Size(900, 150);
            this.BackColor = Color.LightGray;

            CheckBox cb = new CheckBox();
            cb.Location = new Point(0, this.Height / 2 - 10);
            cb.BackColor = Color.White;
            cb.Width = 20;

            PictureBox pb = new PictureBox();
            Bitmap bm1 = (Bitmap)NHACCU.ResourceManager.GetObject(this.id);
            pb.BackgroundImage = bm1;
            pb.Size = new Size(120, 120);
            pb.BackgroundImageLayout = ImageLayout.Stretch;
            pb.BackColor = Color.Transparent;
            pb.Location = new Point(25, 25);

            Label prodName = new Label();
            prodName.Text = name;
            prodName.Location = new Point(25, 6);
            prodName.AutoSize = true;
            prodName.Font = new Font("Arial", 12, FontStyle.Bold);

            Label donGia = new Label();
            donGia.Text = gia.ToString("N0") + " VND";
            donGia.Location = new Point(230, this.Height / 2 - 5);
            donGia.AutoSize = true;
            donGia.Font = new Font("Arial", 12, FontStyle.Bold);

            Button minusButton = new Button();
            minusButton.Text = "-";
            minusButton.AutoSize = true;
            minusButton.Size = new Size(28, 28);
            minusButton.Location = new Point(370, this.Height / 2 - 5);
            minusButton.BackColor = Color.White;
            minusButton.Click += new EventHandler(modAmount);
            minusButton.Name = "minusButton";

            amountNum = new Label();
            amountNum.Text = amount.ToString();
            amountNum.Size = new Size(25, 25);
            amountNum.Location = new Point(370 + 29, this.Height / 2 - 4);
            amountNum.BackColor = Color.White;
            amountNum.TextAlign = ContentAlignment.MiddleCenter;

            Button plusButton = new Button();
            plusButton.Text = "+";
            plusButton.AutoSize = true;
            plusButton.Size = new Size(28, 28);
            plusButton.Location = new Point(370 + 29 + 29, this.Height / 2 - 5);
            plusButton.BackColor = Color.White;
            plusButton.Click += new EventHandler(modAmount);
            plusButton.Name = "plusButton";

            totalPrice = new Label();
            totalPrice.Text = (gia*amount).ToString("N0") + " VND";
            totalPrice.Location = new Point(500, this.Height / 2 - 5);
            totalPrice.AutoSize = true;
            totalPrice.Font = new Font("Arial", 12, FontStyle.Bold);

            deleteButton = new Label();
            deleteButton.Text = "Xoá";
            deleteButton.Location = new Point(650, this.Height / 2 - 5);
            deleteButton.AutoSize = true;
            deleteButton.Font = new Font("Arial", 12, FontStyle.Bold);
            deleteButton.Name = this.id.Trim();

            this.Controls.Add(cb);
            this.Controls.Add(pb);
            this.Controls.Add(prodName);
            this.Controls.Add(donGia);
            this.Controls.Add(minusButton);
            this.Controls.Add(plusButton);
            this.Controls.Add(amountNum);
            this.Controls.Add(totalPrice);
            this.Controls.Add(deleteButton);
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
        public void reloadAmount()
        {
            amountNum.Text = amount.ToString();
        }

    }
}
