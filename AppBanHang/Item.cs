using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Resources;
using AppBanHang.Resources; 
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AppBanHang
{
    internal class Item : Panel
    {
        int size = 246;
        public Button bt;
        public bool isClicked = false;
        public Item(string id, string nameOfItem, long priceOfItem)
        {
            this.Size = new Size(size, size+(size/4));
            this.BackColor = Color.White;
            Bitmap bm1 = (Bitmap)NHACCU.ResourceManager.GetObject(id);
            
            PictureBox pb = new PictureBox();
            pb.Size = new Size(size, size);
            pb.BackgroundImage = bm1;
            pb.BackgroundImageLayout = ImageLayout.Stretch;

            Panel itemName = new Panel();
            itemName.Size = new Size(size, size/4);
            itemName.Location = new Point(0, size);
            itemName.BackColor = Color.DimGray;

            bt = new Button();
            bt.Text = "Xem thêm";
            bt.Size = new Size(size/4 + size/ 8, size / 8);
            bt.Location = new Point(size/2 + size / 4 - size/8 - size/16, size / 8 - size / 16);
            bt.BackColor = Color.LightGray;
            bt.Name = id;
            itemName.Controls.Add(bt);

            Label name = new Label();
            name.Text = nameOfItem; 
            name.Location = new Point(0, 8);
            name.Font = new Font("Arial", 10, FontStyle.Bold);
            name.Size = new Size(size / 2 + size / 16, size / 8 - size / 16);
            name.ForeColor = Color.LightGray;
            name.AutoSize = false;
            itemName.Controls.Add(name);

            Label price = new Label();
            price.Text = priceOfItem.ToString("N0") + " VND";
            price.Location = new Point(0, size / 8);
            price.Font = new Font("Arial", 12, FontStyle.Bold);
            price.Size = new Size(size / 2 + size / 16, size / 8);
            price.ForeColor = Color.White;
            itemName.Controls.Add(price);
            

            this.Controls.Add(itemName);
            this.Controls.Add(pb);
        }
    }
}
