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
        static DataTable NHACCU;
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
            flowLayoutPanel1.Controls.Clear();
            for (int i = 0; i < NHACCU.Rows.Count; i++)
            {
                string id = NHACCU.Rows[i]["MANHACCU"].ToString();
                id = id.Replace(" ", "");
                string name = NHACCU.Rows[i]["TENSP"].ToString();
                long price = Convert.ToInt64(NHACCU.Rows[i]["GIABAN"]);

                Item it = new Item(id, name, price);
                flowLayoutPanel1.Controls.Add(it);
            }
        }
        private void Loading()
        {
            string query = "select * from NHACCU where MANHACCU like '"+stateList+"%'";
            Data_Provider provider = new Data_Provider();
            NHACCU = provider.ExecuteQuery(query);
            //dataGridView1.DataSource = provider.ExecuteQuery(query);

        }

        private void guitarButton_Click(object sender, EventArgs e)
        {
            prevStateList = stateList;
            stateList = "GU";
            Loading();
            setMenu();
            loadFlowLayoutPanel();
        }

        private void VIButton_Click(object sender, EventArgs e)
        {
            prevStateList = stateList;
            stateList = "VI";
            Loading();
            setMenu();
            loadFlowLayoutPanel();
        }

        private void TRButton_Click(object sender, EventArgs e)
        {
            prevStateList = stateList;
            stateList = "TR";
            Loading();
            setMenu();
            loadFlowLayoutPanel();
        }

        private void DRButton_Click(object sender, EventArgs e)
        {
            prevStateList = stateList;
            stateList = "DR";
            Loading();
            setMenu();
            loadFlowLayoutPanel();
        }

        private void PIButton_Click(object sender, EventArgs e)
        {
            prevStateList = stateList;
            stateList = "PI";
            Loading();
            setMenu();
            loadFlowLayoutPanel();
        }

        private void CAButton_Click(object sender, EventArgs e)
        {
            prevStateList = stateList;
            stateList = "CA";
            Loading();
            setMenu();
            loadFlowLayoutPanel();
        }

        private void UKButton_Click(object sender, EventArgs e)
        {
            prevStateList = stateList;
            stateList = "UK";
            Loading();
            setMenu();
            loadFlowLayoutPanel();
        }

        private void GUButton_Click(object sender, EventArgs e)
        {
            prevStateList = stateList;
            stateList = "GU";
            Loading();
            setMenu();
            loadFlowLayoutPanel();
        }
    }
}
