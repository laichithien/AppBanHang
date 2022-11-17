using AppBanHang.DAO;
using AppBanHang.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppBanHang
{
    
    public partial class SellingStuff : Form
    {
        public string prevStateList = "";
        public string stateList = "GU";
        public string mode = "List";
        static DataTable NHACCU;
        public static DataTable GIOHANG;
        public ProductDetail pd;
        public ShoppingCart shoppingCartPanel;
        static int sohoadon;
        public History history;
        public SellingStuff()
        {
            InitializeComponent();
            GIOHANG = new DataTable();
            GIOHANG.Columns.Add("ID", typeof(String));
            GIOHANG.Columns.Add("Amount", typeof(Int32));
            GIOHANG.Columns.Add("Gia", typeof(Int32));
            GIOHANG.Columns.Add("Ten", typeof(String));
            Data_Provider data_Provider = new Data_Provider();
            sohoadon = (int)data_Provider.ExecuteScalar("select count(MAHOADON) from HOADON");
            reload();
        }
        public void Item_Clicked(object sender, EventArgs a)
        {
            Button bt = (Button)sender;
            pd = new ProductDetail(bt.Name);
            this.Controls.Add(pd);
            flowLayoutPanel1.Hide();
            panel2.Hide();
            if (shoppingCartPanel != null)
            {
                shoppingCartPanel.Hide();
            }
            if (history != null)
            {
                history.Hide();
            }
            pd.addToCart.Click += new EventHandler(addProdToCart);
            pd.back.Click += new EventHandler(back);
        }
        private void back(object sender, EventArgs e)
        {
            if (pd != null)
            {
                pd.Hide();
            }
            if (shoppingCartPanel != null)
            {
                shoppingCartPanel.Hide();
            }
            if (history != null)
            {
                history.Hide();
            }
            panel5.Show();
            panel2.Show();
            flowLayoutPanel1.Show();
        }
        private void addProdToCart(object sender, EventArgs a)
        {
            GIOHANG.Rows.Add(pd.ID, pd.amount, pd.price, pd.Tensp);
            if (pd != null)
            {
                pd.Hide();
            }
            if (shoppingCartPanel != null)
            {
                shoppingCartPanel.Hide();
            }
            MessageBox.Show("Đã thành công thêm vào giỏ hàng!");
            panel5.Show();
            panel2.Show();
            flowLayoutPanel1.Show();
        }
        private void loadResultFromQuery(string query)
        {
            Data_Provider provider = new Data_Provider();
            DataTable dt = provider.ExecuteQuery(query);
            flowLayoutPanel1.Enabled = true;
            flowLayoutPanel1.Controls.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string id = dt.Rows[i]["MANHACCU"].ToString();
                id = id.Replace(" ", "");
                string name = dt.Rows[i]["TENSP"].ToString();
                long price = Convert.ToInt64(dt.Rows[i]["GIABAN"]);

                Item it = new Item(id, name, price);
                it.bt.Click += new EventHandler(Item_Clicked);
                flowLayoutPanel1.Controls.Add(it);
            }
            if (dt.Rows.Count == 0)
            {
                Label mess = new Label();
                mess.Text = "Sản phẩm bạn cần tìm hiện không có :((";
                mess.Location = new Point(flowLayoutPanel1.Size.Width / 2, flowLayoutPanel1.Size.Height / 2);
                mess.AutoSize = true;
                mess.Font = new Font("Microsoft Sans Serif", 30, FontStyle.Bold);
                flowLayoutPanel1.Controls.Add(mess);
            }
        }
        private void Search()
        {
            string query = "select * from NHACCU where";
            query = query + (queryHangFilter() + queryGiaFilter() + querySearch()).Substring(4);
            loadResultFromQuery(query);
            prevStateList = stateList;
            stateList = "";
            setMenu();
            loadFilterThuongHieu();
        }
        private void searchButton_Click(object sender, EventArgs e)
        {
            Search();
        }
        private void Category_Button_Click(object sender, EventArgs e)
        {
            Label btn = (Label)sender;
            if (pd != null)
            {
                pd.Hide();
            }
            flowLayoutPanel1.Show();
            panel2.Show();
            prevStateList = stateList;
            stateList = btn.Name.Substring(0, 2);
            //stateList = "VI";
            reload();
        }
        public void loadFlowLayoutPanel()
        {
            flowLayoutPanel1.Enabled = true;
            flowLayoutPanel1.Controls.Clear();
            for (int i = 0; i < NHACCU.Rows.Count; i++)
            {
                string id = NHACCU.Rows[i]["MANHACCU"].ToString();
                id = id.Replace(" ", "");
                string name = NHACCU.Rows[i]["TENSP"].ToString();
                long price = Convert.ToInt64(NHACCU.Rows[i]["GIABAN"]);

                Item it = new Item(id, name, price);
                it.bt.Click += new EventHandler(Item_Clicked);
                flowLayoutPanel1.Controls.Add(it);
            }
        }
        private void loadFilterThuongHieu()
        {
            hangFilter.Items.Clear();
            string query = "select distinct THUONGHIEU from NHACCU where MANHACCU like '" + stateList + "%'";
            Data_Provider provider = new Data_Provider();
            DataTable listThuongHieu = provider.ExecuteQuery(query);
            hangFilter.Items.Add("Tất cả");
            for (int i = 0; i < listThuongHieu.Rows.Count; i++)
            {
                hangFilter.Items.Add(listThuongHieu.Rows[i]["THUONGHIEU"]);
            }
            hangFilter.SelectedIndex = 0;
        }
        private void loadFilterGia()
        {
            giaFilter.Items.Clear();
            
            giaFilter.Items.Add("Tất cả");
            giaFilter.Items.Add("5 - 10 triệu");
            giaFilter.Items.Add("10 - 20 triệu");
            giaFilter.Items.Add("20 - 40 triệu");
            giaFilter.Items.Add("40 - 100 triệu");
            giaFilter.Items.Add("100 - 300 triệu");
            giaFilter.Items.Add("Trên 300 triệu");
            giaFilter.SelectedIndex = 0;
        }
        public void setMenu()
        {
            if (stateList == "" && prevStateList != "")
            {
                panel5.Controls[prevStateList + "Button"].Font = new Font("Microsoft Sans Serif", 14, FontStyle.Regular);
            }
            else if (stateList != "" && prevStateList == "")
            {
                panel5.Controls[stateList + "Button"].Font = new Font("Microsoft Sans Serif", 14, FontStyle.Bold);
            }
            else if (stateList != "" && prevStateList != "")
            {
                panel5.Controls[stateList + "Button"].Font = new Font("Microsoft Sans Serif", 14, FontStyle.Bold);
                panel5.Controls[prevStateList + "Button"].Font = new Font("Microsoft Sans Serif", 14, FontStyle.Regular);

            }
        }
        private void Loading()
        {
            string query = "select * from NHACCU where MANHACCU like '" + stateList + "%'";
            Data_Provider provider = new Data_Provider();
            NHACCU = provider.ExecuteQuery(query);
            //dataGridView1.DataSource = provider.ExecuteQuery(query);
        }
        private void reload()
        {
            setMenu();
            Loading();
            loadFlowLayoutPanel();
            loadFilterThuongHieu();
            loadFilterGia();
            searchField.Clear();
        }
        private string queryHangFilter()
        {
            string query;
            if (hangFilter.SelectedIndex == 0)
            {
                query = "";
            }
            else query = " and THUONGHIEU like '%" + hangFilter.Text + "%'";
            return query;
        }
        private string queryGiaFilter()
        {
            int lowerBound = -1;
            int upperBound = int.MaxValue;
            string query;
            if (giaFilter.SelectedIndex == 0)
            {
                query = "";
            }
            else
            {
                switch (giaFilter.SelectedIndex)
                {
                    case 1:
                        lowerBound = 5000000;
                        upperBound = 10000000;
                        break;
                    case 2:
                        lowerBound = 10000000;
                        upperBound = 20000000;
                        break;
                    case 3:
                        lowerBound = 20000000;
                        upperBound = 40000000;
                        break;
                    case 4:
                        lowerBound = 40000000;
                        upperBound = 100000000;
                        break;
                    case 5:
                        lowerBound = 100000000;
                        upperBound = 300000000;
                        break;
                    case 6:
                        lowerBound = 300000000;
                        upperBound = int.MaxValue;
                        break;
                    default:
                        break;

                }
                query = " and GIABAN >= " + lowerBound + " and GIABAN <= " + upperBound;
            }
            return query;
        }
        private string querySearch()
        {
            string query;
            if (searchField.Text == "")
            {
                query = "";
            }
            else query = " and TENSP like '%" + searchField.Text.ToUpper().Trim() + "%'";
            return query;
        }
        private void activateFilterButton_Click(object sender, EventArgs e)
        {
            Search();
        }
        private void shoppingCartButton_Click(object sender, EventArgs e)
        {
            shoppingCartPanel = new ShoppingCart(GIOHANG);
            shoppingCartPanel.buy.Click += new EventHandler(buy);
            shoppingCartPanel.back.Click += new EventHandler(back);
            flowLayoutPanel1.Hide();
            panel2.Hide();
            panel5.Hide();
            if (pd != null)
            {
                pd.Hide();
            }
            if (history != null)
            {
                history.Hide();
            }
            this.Controls.Add(shoppingCartPanel);
            shoppingCartPanel.Show();
        }
        private void buy(object sender, EventArgs e)
        {
            DataTable hoadon = new DataTable();
            hoadon.Columns.Add("ID", typeof(string));
            hoadon.Columns.Add("Name", typeof(string));
            hoadon.Columns.Add("Price", typeof(Int32));
            hoadon.Columns.Add("Discount", typeof(Int32));
            hoadon.Columns.Add("Amount", typeof(Int16));
            Data_Provider data_Provider = new Data_Provider();
            sohoadon += 1;
            string MAHOADON = "HD"+ (sohoadon).ToString("D3");
            string query;
            int tongtien = 0;
            bool isEmpty = true;
            foreach (var item in shoppingCartPanel.shoppingList.Controls.OfType<ShoppingCartItem>().OrderBy(ee => ee.TabIndex))
            {
                //lastid = item.amount.ToString();
                if (item.isSelected)
                {
                    isEmpty = false;
                    query = "INSERT INTO CHITIET_HOADON(MAHOADON, MANHACCU, TENNHACCU, DONGIA, DISCOUNT, SOTIEN) VALUES ";
                    query += ("('" + MAHOADON + "','"+item.id.ToString().Trim()+"','"+item.name+"',"+item.gia+","+0+","+item.sotien+")");
                    hoadon.Rows.Add(MAHOADON, item.name, item.gia, 0, item.amount);
                    data_Provider.ExecuteNonQuery(query);
                    tongtien += item.sotien;
                }   
            }
            if (!isEmpty)
            {
                query = "INSERT INTO HOADON(MAHOADON, NGAYMUA, TONGTIEN) VALUES ";
                query += ("('" + MAHOADON + "','" + DateTime.Now.ToString() + "'," + tongtien + ")");
                data_Provider.ExecuteNonQuery(query);
            }
            else sohoadon--;
           
            if (pd != null)
            {
                pd.Hide();
            }
            if (shoppingCartPanel != null)
            {
                shoppingCartPanel.Controls.Clear();
                shoppingCartPanel.Hide();
            }
            if (history != null)
            {
                history.Hide();
            }
            BillForm bf = new BillForm(hoadon, DateTime.Now.ToString(), MAHOADON, tongtien.ToString("N0"));
            bf.Show();
            GIOHANG.Clear();
            panel5.Show();
            panel2.Show();
            flowLayoutPanel1.Show();
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            string query = "select * from HOADON";
            Data_Provider dp = new Data_Provider();
            DataTable dt_bills = dp.ExecuteQuery(query);
            history = new History(dt_bills);
            history.back.Click += new EventHandler(back);
            flowLayoutPanel1.Hide();
            panel2.Hide();
            panel5.Hide();
            if (pd != null)
            {
                pd.Hide();
            }
            if (shoppingCartPanel != null)
            {
                shoppingCartPanel.Hide();
            }
            this.Controls.Add(history);
            history.Show();
        }

        private void searchField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (pd != null)
            {
                pd.Hide();
            }
            if (shoppingCartPanel != null)
            {
                shoppingCartPanel.Hide();
            }
            if (history != null)
            {
                history.Hide();
            }
            panel5.Show();
            panel2.Show();
            flowLayoutPanel1.Show();
        }
    }
}
