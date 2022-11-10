using AppBanHang.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppBanHang
{
    
    public partial class SellingStuff : Form
    {
        public string prevStateList = "";
        public string stateList = "PI";
        public string mode = "List";
        static DataTable NHACCU;
        public ProductDetail pd;
        public SellingStuff()
        {
            InitializeComponent();
            setMenu();
            Loading();
            loadFlowLayoutPanel();
        }
        public void setMenu()
        {
            panel5.Controls[stateList+"Button"].Font = new Font("Microsoft Sans Serif", 14, FontStyle.Bold);
            if (prevStateList != "" && prevStateList != stateList)
            {
                panel5.Controls[prevStateList + "Button"].Font = new Font("Microsoft Sans Serif", 14, FontStyle.Regular);
            }
            
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
        public void loadSearch(DataTable searchData)
        {
            flowLayoutPanel1.Enabled = true;
            flowLayoutPanel1.Controls.Clear();
            for (int i = 0; i < searchData.Rows.Count; i++)
            {
                string id = searchData.Rows[i]["MANHACCU"].ToString();
                id = id.Replace(" ", "");
                string name = searchData.Rows[i]["TENSP"].ToString();
                long price = Convert.ToInt64(searchData.Rows[i]["GIABAN"]);

                Item it = new Item(id, name, price);
                it.bt.Click += new EventHandler(Item_Clicked);
                flowLayoutPanel1.Controls.Add(it);
            }
            if (searchData.Rows.Count == 0)
            {
                Label mess = new Label();
                mess.Text = "Sản phẩm bạn cần tìm hiện không có :((";
                mess.Location = new Point(flowLayoutPanel1.Size.Width / 2, flowLayoutPanel1.Size.Height / 2);
                mess.AutoSize = true;
                mess.Font = new Font("Microsoft Sans Serif", 30, FontStyle.Bold);
                flowLayoutPanel1.Controls.Add(mess);
            }
        }
        public void Item_Clicked(object sender, EventArgs a)
        {
            Button bt = (Button)sender;
            pd = new ProductDetail(bt.Name);
            this.Controls.Add(pd);
            flowLayoutPanel1.Hide();
        }
        private void Loading()
        {
            string query = "select * from NHACCU where MANHACCU like '"+stateList+"%'";
            Data_Provider provider = new Data_Provider();
            NHACCU = provider.ExecuteQuery(query);
            //dataGridView1.DataSource = provider.ExecuteQuery(query);
        }
        private DataTable Search(string keywords)
        {
            string query = "select * from NHACCU where TENSP like '%"+keywords+"%'";
            Data_Provider provider = new Data_Provider();
            DataTable search = provider.ExecuteQuery(query);
            //dataGridView1.DataSource = provider.ExecuteQuery(query);
            return search;
        }
        private void VIButton_Click(object sender, EventArgs e)
        {
            if (pd != null)
            {
                pd.Hide();
            }
            flowLayoutPanel1.Show();
            prevStateList = stateList;
            stateList = "VI";
            Loading();
            setMenu();
            loadFlowLayoutPanel();
        }

        private void TRButton_Click(object sender, EventArgs e)
        {
            if (pd != null)
            {
                pd.Hide();
            }
            flowLayoutPanel1.Show();
            prevStateList = stateList;
            stateList = "TR";
            Loading();
            setMenu();
            loadFlowLayoutPanel();
        }

        private void DRButton_Click(object sender, EventArgs e)
        {
            if (pd != null)
            {
                pd.Hide();
            }
            flowLayoutPanel1.Show();
            prevStateList = stateList;
            stateList = "DR";
            Loading();
            setMenu();
            loadFlowLayoutPanel();
        }

        private void PIButton_Click(object sender, EventArgs e)
        {
            if (pd != null)
            {
                pd.Hide();
            }
            flowLayoutPanel1.Show();
            prevStateList = stateList;
            stateList = "PI";
            Loading();
            setMenu();
            loadFlowLayoutPanel();
        }

        private void CAButton_Click(object sender, EventArgs e)
        {
            if (pd != null)
            {
                pd.Hide();
            }
            flowLayoutPanel1.Show();
            prevStateList = stateList;
            stateList = "CA";
            Loading();
            setMenu();
            loadFlowLayoutPanel();
        }

        private void UKButton_Click(object sender, EventArgs e)
        {
            if (pd != null)
            {
                pd.Hide();
            }
            flowLayoutPanel1.Show();
            prevStateList = stateList;
            stateList = "UK";
            Loading();
            setMenu();
            loadFlowLayoutPanel();
        }

        private void GUButton_Click(object sender, EventArgs e)
        {
            if (pd != null)
            {
                pd.Hide();
            }
            flowLayoutPanel1.Show();
            prevStateList = stateList;
            stateList = "GU";
            Loading();
            setMenu();
            loadFlowLayoutPanel();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loadSearch(Search(searchField.Text));
        }
    }
}
