using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.ClipboardSource.SpreadsheetML;

namespace Ticari_Otomasyon
{
    public partial class FrmFaturaUrunler : Form
    {
        public FrmFaturaUrunler()
        {
            InitializeComponent();
        }
        public string id;
        SqlBaglantisi sqlBaglantisi = new SqlBaglantisi();

        public void faturaDetayListele()
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from TBL_FATURADETAY where FATURAID='"+id+"'", sqlBaglantisi.baglanti());
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            gridControl1.DataSource = dataTable;
        }

        private void FrmFaturaUrunler_Load(object sender, EventArgs e)
        {
            faturaDetayListele();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunDuzenleme faturaUrunDuzenleme = new FrmFaturaUrunDuzenleme();
            DataRow dataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dataRow != null)
            {
                faturaUrunDuzenleme.urunId = dataRow["FATURAURUNID"].ToString();
            }
            faturaUrunDuzenleme.Show();
        }
    }
}
