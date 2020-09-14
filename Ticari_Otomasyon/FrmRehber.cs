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
    public partial class FrmRehber : Form
    {
        public FrmRehber()
        {
            InitializeComponent();
        }
        SqlBaglantisi sqlBaglantisi = new SqlBaglantisi();


        private void FrmRehber_Load(object sender, EventArgs e)
        {
            // Customer datas
            DataTable dataTable1 = new DataTable();
            SqlDataAdapter dataAdapter1 = new SqlDataAdapter("Select AD,SOYAD,TELEFON,TELEFON2,MAIL from TBL_MUSTERILER", sqlBaglantisi.baglanti());
            dataAdapter1.Fill(dataTable1);
            gridControlRehberMusteriler.DataSource = dataTable1;

            //COMPANY datas
            DataTable dataTable2 = new DataTable();
            SqlDataAdapter dataAdapter2 = new SqlDataAdapter("Select AD,YETKILIADSOYAD,TELEFON1,TELEFON2,MAIL,FAX from TBL_FIRMALAR", sqlBaglantisi.baglanti());
            dataAdapter2.Fill(dataTable2);
            gridControlRehberFirmalar.DataSource = dataTable2;

        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmMail person = new FrmMail();
            DataRow dataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if(dataRow != null)
            {
                person.mail = dataRow["MAIL"].ToString();
            }
            person.Show();
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            FrmMail company = new FrmMail();
            DataRow dataRow = gridView2.GetDataRow(gridView2.FocusedRowHandle);

            if (dataRow != null)
            {
                company.mail = dataRow["MAIL"].ToString();
            }
            company.Show();
        }
    }
}
