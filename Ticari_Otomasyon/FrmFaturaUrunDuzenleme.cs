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
    public partial class FrmFaturaUrunDuzenleme : Form
    {
        public FrmFaturaUrunDuzenleme()
        {
            InitializeComponent();
        }
        public string urunId;

        SqlBaglantisi sqlBaglantisi = new SqlBaglantisi();

        public void faturaUrunListele()
        {
            SqlCommand command = new SqlCommand("Select * from TBL_FATURADETAY where FATURAURUNID=@p1", sqlBaglantisi.baglanti());
            command.Parameters.AddWithValue("@p1", urunId);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                txtFiyat.Text = dataReader[3].ToString();
                txtMiktar.Text = dataReader[2].ToString();
                txtTutar.Text = dataReader[4].ToString();
                txtUrunAdi.Text = dataReader[1].ToString();

                sqlBaglantisi.baglanti().Close();
            }
        }

        private void FrmFaturaUrunDuzenleme_Load(object sender, EventArgs e)
        {
            txtUrunId.Text = urunId;
            faturaUrunListele();
        }

        private void btnFaturaBilgisiGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("update TBL_FATURADETAY set URUNAD=@p1,MIKTAR=@p2,FIYAT=@p3,TUTAR=@p4 where FATURAURUNID=@p5", sqlBaglantisi.baglanti());
            command.Parameters.AddWithValue("@p1", txtUrunAdi.Text);
            command.Parameters.AddWithValue("@p2", txtMiktar.Text);
            command.Parameters.AddWithValue("@p3",decimal.Parse(txtFiyat.Text));
            command.Parameters.AddWithValue("@p4", decimal.Parse(txtTutar.Text));
            command.Parameters.AddWithValue("@p5", txtUrunId.Text);
            command.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Veriler Başarıyla Güncellenmiştir!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnFaturaBilgisiSil_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Delete FROM TBL_FATURADETAY WHERE FATURAURUNID=@p1", sqlBaglantisi.baglanti());
            command.Parameters.AddWithValue("@p1", txtUrunId.Text);
            command.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Ürün Silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
