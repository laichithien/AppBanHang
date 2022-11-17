using AppBanHang.DAO;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppBanHang
{
    public partial class BillForm : Form
    {
        DataTable dt;
        string date;
        string ID_HD;
        string total;
        public BillForm(DataTable dt, string date, string ID_HD, string total)
        {
            InitializeComponent();
            this.dt = dt;
            this.date = date;
            this.ID_HD = ID_HD;
            this.total = total;
        }

        private void BillForm_Load(object sender, EventArgs e)
        {
            LoadReport();
            this.reportViewer1.RefreshReport();
        }
        //private void LoadReport()
        //{
        //    string query = "SELECT * FROM CHITIET_HOADON";
        //    Data_Provider dp = new Data_Provider();
        //    DataTable dt = dp.ExecuteQuery(query);
        //    DataSet ds = new DataSet();
        //    ds.Tables.Add(dt);

        //    reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
        //    reportViewer1.LocalReport.ReportPath = "Report1.rdlc";
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        ReportDataSource rds = new ReportDataSource();
        //        rds.Name = "DataSet1";
        //        rds.Value = ds.Tables[0];
        //        reportViewer1.LocalReport.DataSources.Clear();
        //        reportViewer1.LocalReport.DataSources.Add(rds);
        //        //reportViewer1.RefreshReport();
        //    }
        //}
        public void LoadReport()
        {
            reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            reportViewer1.LocalReport.ReportPath = "..\\..\\ReportHoaDon.rdlc";

            ReportDataSource dts = new ReportDataSource();
            dts.Name = "DataSet1";
            dts.Value = this.dt;
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(dts);

            ReportParameter para1 = new ReportParameter();
            para1.Name = "Date";
            para1.Values.Add(this.date);
            ReportParameter para2 = new ReportParameter();
            para2.Name = "ID_HD";
            para2.Values.Add(this.ID_HD);
            ReportParameter para3 = new ReportParameter();
            para3.Name = "TongTien";
            para3.Values.Add(this.total);
            
            reportViewer1.LocalReport.SetParameters(new ReportParameter[] { para1, para2, para3 });
        }
    }
}
