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
using DevExpress.Utils.Serializing;
using DevExpress.XtraEditors.Design;

namespace Ticari_Otomasyon
{
    public partial class FrmNotlar : Form
    {
        public FrmNotlar()
        {
            InitializeComponent();
        }

        SqlBaglantisi sqlBaglantisi = new SqlBaglantisi();


        public void temizle()
        {
            txtNotId.Text = "";
            txtBaslik.Text = "";
            txtHitap.Text = "";
            txtOlusturan.Text = "";
            mskSaat.Text = "";
            mskTarih.Text = "";
            rchDetay.Text = "";
        }

        public void notlariListele()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from TBL_NOTLAR", sqlBaglantisi.baglanti());
            dataAdapter.Fill(dataTable);
            gridControlNotlar.DataSource = dataTable;
        }

        private void FrmNotlar_Load(object sender, EventArgs e)
        {
            notlariListele();
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("insert into TBL_NOTLAR (TARIH,SAAT,BASLIK,DETAY,OLUSTURAN,HITAP) values (@p1,@p2,@p3,@p4,@p5,@p6) ", sqlBaglantisi.baglanti());
            command.Parameters.AddWithValue("@p1", mskTarih.Text);
            command.Parameters.AddWithValue("@p2", mskSaat.Text);
            command.Parameters.AddWithValue("@p3", txtBaslik.Text);
            command.Parameters.AddWithValue("@p4", rchDetay.Text);
            command.Parameters.AddWithValue("@p5", txtOlusturan.Text);
            command.Parameters.AddWithValue("@p6", txtHitap.Text);
            command.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Not Bilgisi Sisteme Eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            notlariListele();

        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dataRow != null)
            {
                txtNotId.Text = dataRow["NOTID"].ToString();
                txtBaslik.Text = dataRow["BASLIK"].ToString();
                rchDetay.Text = dataRow["DETAY"].ToString();
                txtOlusturan.Text = dataRow["OLUSTURAN"].ToString();
                txtHitap.Text = dataRow["HITAP"].ToString();
                mskTarih.Text = dataRow["TARIH"].ToString();
                mskSaat.Text = dataRow["SAAT"].ToString();
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Delete from TBL_NOTLAR WHERE NOTID=@p1", sqlBaglantisi.baglanti());
            command.Parameters.AddWithValue("@p1", txtNotId.Text);
            command.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Not Silinmiştir!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            notlariListele();
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Update TBL_NOTLAR SET TARIH=@p1,SAAT=@p2,BASLIK=@p3,DETAY=@p4,OLUSTURAN=@p5,HITAP=@p6 where NOTID=@p7", sqlBaglantisi.baglanti());
            command.Parameters.AddWithValue("@p1", mskTarih.Text);
            command.Parameters.AddWithValue("@p2", mskSaat.Text);
            command.Parameters.AddWithValue("@p3", txtBaslik.Text);
            command.Parameters.AddWithValue("@p4", rchDetay.Text);
            command.Parameters.AddWithValue("@p5", txtOlusturan.Text);
            command.Parameters.AddWithValue("@p6", txtHitap.Text);
            command.Parameters.AddWithValue("@p7", txtNotId.Text);
            command.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Not Bilgileri Güncellenmiştir!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            notlariListele();

        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmNotDetay notDetay = new FrmNotDetay();

            DataRow dataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dataRow != null)
            {
                notDetay.txt = dataRow["DETAY"].ToString();
            }
            notDetay.Show();
        }
    }
}
