using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AppBanHang
{
    public class ShoppingCart : Panel
    {
        public Panel title;
        public FlowLayoutPanel shoppingList;
        public DataTable dt;
        public Button buy;
        public Label back;
        public Panel Summary;
        public ShoppingCart(DataTable dt)
        {
            back = new Label();
            back.AutoSize = true;
            back.Text = "Quay lại";
            back.Font = new Font("Arial", 10, FontStyle.Italic | FontStyle.Underline);
            back.Dock = DockStyle.Right;

            this.Controls.Add(back);

            title = new Panel();
            title.Dock = DockStyle.Top;
            title.Height = 50;
            title.Width = 1000;

            CheckBox checkBox = new CheckBox();
            checkBox.Location = new Point(0, 15);
            checkBox.Size = new Size(15, 15);

            Label prodCol = new Label();
            prodCol.Text = "Tất cả";
            prodCol.AutoSize = true;
            prodCol.Font = new Font("Arial", 15, FontStyle.Bold);
            prodCol.Location = new Point(15, 12);

            Label priceCol = new Label();
            priceCol.Text = "Đơn giá";
            priceCol.AutoSize = true;
            priceCol.Font = new Font("Arial", 15, FontStyle.Bold);
            priceCol.Location = new Point(230, 12);

            Label amount = new Label();
            amount.Text = "Số lượng";
            amount.AutoSize = true;
            amount.Font = new Font("Arial", 15, FontStyle.Bold);
            amount.Location = new Point(370, 12);

            Label totalPrice = new Label();
            totalPrice.Text = "Số tiền";
            totalPrice.AutoSize = true;
            totalPrice.Font = new Font("Arial", 15, FontStyle.Bold);
            totalPrice.Location = new Point(500, 12);

            Label manipulation = new Label();
            manipulation.Text = "Thao tác";
            manipulation.AutoSize = true;
            manipulation.Font = new Font("Arial", 15, FontStyle.Bold);
            manipulation.Location = new Point(650, 12);

            buy = new Button();
            buy.Text = "Mua ngay";
            buy.Location = new Point(800, 6);
            buy.Size = new Size(100, 40);

            //title.Controls.Add(checkBox);
            title.Controls.Add(prodCol);
            title.Controls.Add(priceCol);
            title.Controls.Add(amount);
            title.Controls.Add(totalPrice);
            title.Controls.Add(manipulation);
            title.Controls.Add(buy);


            shoppingList = new FlowLayoutPanel();
            shoppingList.AutoScroll = true;
            shoppingList.Size = new Size(1000, 450);
            shoppingList.Location = new Point(0, 60);

            this.Size = new Size(1000, 653);
            this.Location = new Point(25, 90);
            this.Anchor = AnchorStyles.None;
            this.dt = dt;
            //this.BackColor = Color.Aqua;

            this.Controls.Add(title);

            //DataGridView dgv = new DataGridView();
            //dgv.DataSource = dt;
            //dgv.AutoSize = true;
            //dgv.Location = new Point(0, 0);
            //this.Controls.Add(dgv);
            loadShoppingCartItems();
            loadSummary();

        }
        public void loadShoppingCartItems()
        {
            this.Controls.Add(shoppingList);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string id = dt.Rows[i]["ID"].ToString();
                int amount = Convert.ToInt32(dt.Rows[i]["Amount"]);
                int gia = Convert.ToInt32(dt.Rows[i]["Gia"]);
                string name = dt.Rows[i]["Ten"].ToString();
                ShoppingCartItem it = new ShoppingCartItem(id, name, gia, amount);
                it.Name = id.Trim();
                it.deleteButton.Click += new EventHandler(delete);
                shoppingList.Controls.Add(it);
            }

        }
        public void delete(object sender, EventArgs e)
        {
            Label button = (Label)sender;
            this.shoppingList.Controls[button.Name].Dispose();
        }
        public void loadSummary()
        {
            Summary = new Panel();
            Summary.Dock = DockStyle.Bottom;
            Summary.BackColor = Color.LightGray;
            Summary.Height = 100;

            Label tongThanhToan = new Label();
            tongThanhToan.Text = "Tổng thanh toán";
            tongThanhToan.Font = new Font("Arial", 14, FontStyle.Bold);
            tongThanhToan.AutoSize = true;
            tongThanhToan.Location = new Point(500,6);

            
            Summary.Controls.Add(tongThanhToan);

            this.Controls.Add(Summary);
        }
    }
}
