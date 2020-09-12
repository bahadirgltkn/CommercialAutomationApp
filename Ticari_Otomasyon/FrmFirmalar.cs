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
using System.Security.Policy;
using DevExpress.ClipboardSource.SpreadsheetML;

namespace Ticari_Otomasyon
{
    public partial class FrmFirmalar : Form
    {


        public FrmFirmalar()
        {
            InitializeComponent();
        }

        SqlBaglantisi sqlBaglantisi = new SqlBaglantisi();

        public void sehirListele()
        {
            SqlCommand command = new SqlCommand("Select SEHIR from TBL_ILLER ", sqlBaglantisi.baglanti());
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                cbxIl.Properties.Items.Add(dr[0]);
            }
            sqlBaglantisi.baglanti().Close();
        }

        public void firmaListele()
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from TBL_FIRMALAR", sqlBaglantisi.baglanti());
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            gridControlFirmalar.DataSource = dataTable; 
        }

        public void temizle()
        {
            txtAd.Text = "";
            txtId.Text = "";
            txtKod1.Text = "";
            txtKod2.Text = "";
            txtKod3.Text = "";
            txtMail.Text = "";
            txtSektor.Text = "";
            txtVergiDaire.Text = "";
            txtYetkili.Text = "";
            txtYetkiliGorev.Text = "";
            mskFax.Text = "";
            mskTc.Text = "";
            mskTelefon_1.Text = "";
            mskTelefon_2.Text = "";
            mskTelefon_3.Text = "";
            rchAdres.Text = "";

        }
        public void codeExplain()
        {
            SqlCommand command = new SqlCommand("Select FIRMAKOD1 from TBL_KODLAR", sqlBaglantisi.baglanti());
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                rchKod1.Text = dataReader[0].ToString();
            }
            sqlBaglantisi.baglanti().Close();
        }


        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            firmaListele();
            sehirListele();
            codeExplain();
            temizle();
        }

        private void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            //Getting company information from the grid

            DataRow dataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dataRow != null)
            {
                txtId.Text = dataRow["ID"].ToString();
                txtAd.Text = dataRow["AD"].ToString();
                txtYetkiliGorev.Text = dataRow["YETKILISTATU"].ToString();
                txtYetkili.Text = dataRow["YETKILIADSOYAD"].ToString();
                mskTc.Text = dataRow["YETKILITC"].ToString();
                txtSektor.Text = dataRow["SEKTOR"].ToString();
                mskTelefon_1.Text = dataRow["TELEFON1"].ToString();
                mskTelefon_2.Text = dataRow["TELEFON2"].ToString();
                mskTelefon_3.Text = dataRow["TELEFON3"].ToString();
                txtMail.Text = dataRow["MAIL"].ToString();
                mskFax.Text = dataRow["FAX"].ToString();
                cbxIl.Text = dataRow["IL"].ToString();
                cbxIlce.Text = dataRow["ILCE"].ToString();
                txtVergiDaire.Text = dataRow["VERGIDAIRE"].ToString();
                rchAdres.Text = dataRow["ADRES"].ToString();
                txtKod1.Text = dataRow["OZELKOD1"].ToString();
                txtKod2.Text = dataRow["OZELKOD2"].ToString();
                txtKod3.Text = dataRow["OZELKOD3"].ToString();

            }
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            //Adding a new company to database

            SqlCommand command = new SqlCommand("insert into TBL_FIRMALAR " +
                "(AD,YETKILISTATU,YETKILIADSOYAD,YETKILITC,SEKTOR,TELEFON1,TELEFON2,TELEFON3,MAIL,FAX,IL,ILCE,VERGIDAIRE,ADRES,OZELKOD1,OZELKOD2,OZELKOD3)" +
                "values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17)", sqlBaglantisi.baglanti());

            command.Parameters.AddWithValue("@p1", txtAd.Text);
            command.Parameters.AddWithValue("@p2", txtYetkiliGorev.Text);
            command.Parameters.AddWithValue("@p3", txtYetkili.Text);
            command.Parameters.AddWithValue("@p4", mskTc.Text);
            command.Parameters.AddWithValue("@p5", txtSektor.Text);
            command.Parameters.AddWithValue("@p6", mskTelefon_1.Text);
            command.Parameters.AddWithValue("@p7", mskTelefon_2.Text);
            command.Parameters.AddWithValue("@p8", mskTelefon_3.Text);
            command.Parameters.AddWithValue("@p9", txtMail.Text);
            command.Parameters.AddWithValue("@p10", mskFax.Text);
            command.Parameters.AddWithValue("@p11", cbxIl.Text);
            command.Parameters.AddWithValue("@p12", cbxIlce.Text);
            command.Parameters.AddWithValue("@p13", txtVergiDaire.Text);
            command.Parameters.AddWithValue("@p14", rchAdres.Text);
            command.Parameters.AddWithValue("@p15", txtKod1.Text);
            command.Parameters.AddWithValue("@p16", txtKod2.Text);
            command.Parameters.AddWithValue("@p17", txtKod3.Text);
            command.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Firma Sisteme Kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            firmaListele();
            temizle();
        }

        private void cbxIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //when the cbxIl value changes, the value in the cbxIlce is reset   

            cbxIlce.Text = "";
            cbxIlce.Properties.Items.Clear();

            SqlCommand command = new SqlCommand("Select ilce from TBL_ILCELER where sehir=@p1", sqlBaglantisi.baglanti());
            command.Parameters.AddWithValue("@p1", cbxIl.SelectedIndex + 1);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                cbxIlce.Properties.Items.Add(dataReader[0]);
            }
            sqlBaglantisi.baglanti().Close();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Delete from TBL_FIRMALAR where ID=@p1", sqlBaglantisi.baglanti());
            command.Parameters.AddWithValue("@p1", txtId.Text);
            command.ExecuteNonQuery(); //delete update insert 
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Firma listeden silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            firmaListele();
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Update TBL_FIRMALAR set AD=@p1,YETKILISTATU=@p2,YETKILIADSOYAD=@p3,YETKILITC=@p4,SEKTOR=@p5,TELEFON1=@p6,TELEFON2=@p7,TELEFON3=@p8,MAIL=@p9,FAX=@p10,IL=@p11,ILCE=@p12,VERGIDAIRE=@p13,ADRES=@p14,OZELKOD1=@p15,OZELKOD2=@p16,OZELKOD3=@p17 where ID=@p18", sqlBaglantisi.baglanti());


                                               

            command.Parameters.AddWithValue("@p1", txtAd.Text);
            command.Parameters.AddWithValue("@p2", txtYetkiliGorev.Text);
            command.Parameters.AddWithValue("@p3", txtYetkili.Text);
            command.Parameters.AddWithValue("@p4", mskTc.Text);
            command.Parameters.AddWithValue("@p5", txtSektor.Text);
            command.Parameters.AddWithValue("@p6", mskTelefon_1.Text);
            command.Parameters.AddWithValue("@p7", mskTelefon_2.Text);
            command.Parameters.AddWithValue("@p8", mskTelefon_3.Text);
            command.Parameters.AddWithValue("@p9", txtMail.Text);
            command.Parameters.AddWithValue("@p10", mskFax.Text);
            command.Parameters.AddWithValue("@p11", cbxIl.Text);
            command.Parameters.AddWithValue("@p12", cbxIlce.Text);
            command.Parameters.AddWithValue("@p13", txtVergiDaire.Text);
            command.Parameters.AddWithValue("@p14", rchAdres.Text);
            command.Parameters.AddWithValue("@p15", txtKod1.Text);
            command.Parameters.AddWithValue("@p16", txtKod2.Text);
            command.Parameters.AddWithValue("@p17", txtKod3.Text);
            command.Parameters.AddWithValue("@p18", txtId.Text);
            command.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Firma bilgileri güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            firmaListele();
            temizle();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
