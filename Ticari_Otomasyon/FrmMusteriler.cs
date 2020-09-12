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
namespace Ticari_Otomasyon
{
    public partial class FrmMusteriler : Form
    {
        public FrmMusteriler()
        {
            InitializeComponent();
        }

        //SQLCONNECTION
        SqlBaglantisi sqlBaglantisi = new SqlBaglantisi();

        public void temizle()
        {
            txtId.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            mskTelefon1.Text = "";
            mskTelefon2.Text = "";
            mskTc.Text = "";
            txtMail.Text = "";
            txtVergiDaire.Text = "";
            rchAdres.Text = "";
        }

        public void musteriListele()
        {
            //LISTING DATA ON THE GRID
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_MUSTERILER", sqlBaglantisi.baglanti());
            da.Fill(dt);
            gridControlMusteriler.DataSource = dt;

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

        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            musteriListele();
            sehirListele();
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

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("insert into TBL_MUSTERILER (AD,SOYAD,TELEFON,TELEFON2,TC,MAIL,IL,ILCE,ADRES,VERGIDAIRE) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)", sqlBaglantisi.baglanti());
            command.Parameters.AddWithValue("@p1", txtAd.Text);
            command.Parameters.AddWithValue("@p2", txtSoyad.Text);
            command.Parameters.AddWithValue("@p3", mskTelefon1.Text); 
            command.Parameters.AddWithValue("@p4", mskTelefon2.Text);
            command.Parameters.AddWithValue("@p5", mskTc.Text);
            command.Parameters.AddWithValue("@p6", txtMail.Text);
            command.Parameters.AddWithValue("@p7", cbxIl.Text);
            command.Parameters.AddWithValue("@p8", cbxIlce.Text);
            command.Parameters.AddWithValue("@p9", rchAdres.Text);
            command.Parameters.AddWithValue("@p10", txtVergiDaire.Text);
            command.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Müşteri sisteme eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            musteriListele();
            temizle();

        }

        private void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dataRow != null)
            {
                txtId.Text = dataRow["ID"].ToString();
                txtAd.Text = dataRow["AD"].ToString();
                txtSoyad.Text = dataRow["SOYAD"].ToString();
                mskTelefon1.Text = dataRow["TELEFON"].ToString();
                mskTelefon2.Text = dataRow["TELEFON2"].ToString();
                mskTc.Text = dataRow["TC"].ToString();
                txtMail.Text = dataRow["MAIL"].ToString();
                cbxIl.Text = dataRow["IL"].ToString(); 
                cbxIlce.Text = dataRow["ILCE"].ToString();
                txtVergiDaire.Text = dataRow["VERGIDAIRE"].ToString();
                rchAdres.Text = dataRow["ADRES"].ToString();
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("DELETE FROM TBL_MUSTERILER where ID=@p1", sqlBaglantisi.baglanti());
            command.Parameters.AddWithValue("@p1", txtId.Text);

            if (DialogResult.Yes == MessageBox.Show("Gerçekten silmek istiyor musunuz?", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                command.ExecuteNonQuery();  // insert update delete
                sqlBaglantisi.baglanti().Close();
                MessageBox.Show("Müşteri Silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            musteriListele();
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("update TBL_MUSTERILER set AD=@p1,SOYAD=@p2,TELEFON=@p3,TELEFON2=@p4,TC=@p5,MAIL=@p6,IL=@p7,ILCE=@p8,ADRES=@p9,VERGIDAIRE=@p10 where ID=@p11", sqlBaglantisi.baglanti());

            command.Parameters.AddWithValue("@p1", txtAd.Text);
            command.Parameters.AddWithValue("@p2", txtSoyad.Text);
            command.Parameters.AddWithValue("@p3", mskTelefon1.Text);
            command.Parameters.AddWithValue("@p4", mskTelefon2.Text);
            command.Parameters.AddWithValue("@p5", mskTc.Text);
            command.Parameters.AddWithValue("@p6", txtMail.Text);
            command.Parameters.AddWithValue("@p7", cbxIl.Text);
            command.Parameters.AddWithValue("@p8", cbxIlce.Text);
            command.Parameters.AddWithValue("@p9", rchAdres.Text);
            command.Parameters.AddWithValue("@p10", txtVergiDaire.Text);
            command.Parameters.AddWithValue("@p11", txtId.Text);
            command.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Müşteri Bilgileri Güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            musteriListele();
            temizle();

        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
