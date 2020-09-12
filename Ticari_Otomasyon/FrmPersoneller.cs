using DevExpress.ClipboardSource.SpreadsheetML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ticari_Otomasyon
{
    public partial class FrmPersoneller : Form
    {
        public FrmPersoneller()
        {
            InitializeComponent();
        }
        SqlBaglantisi sqlBaglantisi = new SqlBaglantisi();

        public void temizle()
        {
            txtId.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            mskTelefon.Text = "";
            mskTc.Text = "";
            txtMail.Text = "";
            rchAdres.Text = "";
            txtGorev.Text = "";
        }

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

        public void personelListele()
        {
            //LISTING STAFF DATA ON THE GRID
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from TBL_PERSONELLER", sqlBaglantisi.baglanti());
            dataAdapter.Fill(dataTable);
            gridControlPersoneller.DataSource = dataTable;
        }

        private void FrmPersoneller_Load(object sender, EventArgs e)
        {
            personelListele();
            sehirListele();
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("insert into TBL_PERSONELLER (AD,SOYAD,TELEFON,TC,MAIL,IL,ILCE,ADRES,GOREV) values" +
                                                "(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", sqlBaglantisi.baglanti());
            
            command.Parameters.AddWithValue("@p1", txtAd.Text);
            command.Parameters.AddWithValue("@p2", txtSoyad.Text);
            command.Parameters.AddWithValue("@p3", mskTelefon.Text);
            command.Parameters.AddWithValue("@p4", mskTc.Text);
            command.Parameters.AddWithValue("@p5", txtMail.Text);
            command.Parameters.AddWithValue("@p6", cbxIl.Text);
            command.Parameters.AddWithValue("@p7", cbxIlce.Text);
            command.Parameters.AddWithValue("@p8", rchAdres.Text);
            command.Parameters.AddWithValue("@p9", txtGorev.Text);
            command.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Personel Listeye Eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            personelListele();

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

        private void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dataRow != null)
            {
                txtId.Text = dataRow["ID"].ToString();
                txtAd.Text = dataRow["AD"].ToString();
                txtSoyad.Text = dataRow["SOYAD"].ToString();
                mskTelefon.Text = dataRow["TELEFON"].ToString();
                mskTc.Text = dataRow["TC"].ToString();
                txtMail.Text = dataRow["MAIL"].ToString();
                cbxIl.Text = dataRow["IL"].ToString();
                cbxIlce.Text = dataRow["ILCE"].ToString();
                rchAdres.Text = dataRow["ADRES"].ToString();
                txtGorev.Text = dataRow["GOREV"].ToString();
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Delete from TBL_PERSONELLER where ID=@p1", sqlBaglantisi.baglanti());
            command.Parameters.AddWithValue("@p1", txtId.Text);
            if(DialogResult.Yes == MessageBox.Show("Personel Listeden Silinsin Mi?", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                command.ExecuteNonQuery();
                sqlBaglantisi.baglanti().Close();
                MessageBox.Show("Personel Başarıyla Silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            personelListele();  
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Update TBL_PERSONELLER set AD=@p1,SOYAD=@p2,TELEFON=@p3,TC=@p4,MAIL=@p5,IL=@p6,ILCE=@p7,ADRES=@p8,GOREV=@p9 where ID=@p10", sqlBaglantisi.baglanti());

            command.Parameters.AddWithValue("@p1", txtAd.Text);
            command.Parameters.AddWithValue("@p2", txtSoyad.Text);
            command.Parameters.AddWithValue("@p3", mskTelefon.Text);
            command.Parameters.AddWithValue("@p4", mskTc.Text);
            command.Parameters.AddWithValue("@p5", txtMail.Text);
            command.Parameters.AddWithValue("@p6", cbxIl.Text);
            command.Parameters.AddWithValue("@p7", cbxIlce.Text);
            command.Parameters.AddWithValue("@p8", rchAdres.Text);
            command.Parameters.AddWithValue("@p9", txtGorev.Text);
            command.Parameters.AddWithValue("@p10", txtId.Text);
            command.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Personel Bilgileri Güncellenmiştir!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            personelListele();
        }
    }
}
