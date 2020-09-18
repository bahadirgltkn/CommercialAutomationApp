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
using DevExpress.Data.Async;

namespace Ticari_Otomasyon
{
    public partial class FrmFaturalar : Form
    {
        public FrmFaturalar()
        {
            InitializeComponent();
        }
        SqlBaglantisi sqlBaglantisi = new SqlBaglantisi();

        public void faturaListele()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from TBL_FATURABILGI", sqlBaglantisi.baglanti());
            dataAdapter.Fill(dataTable);
            gridControlFaturalar.DataSource = dataTable;
        }

        public void temizle()
        {
            txtId.Text = "";
            txtSeri.Text = "";
            txtSıraNo.Text = "";
            mskTarih.Text = "";
            mskSaat.Text = "";
            txtTeslimAlan.Text = "";
            txtTeslimEden.Text = "";
            txtVergiDaire.Text = "";
            txtAlici.Text = "";
        }

        private void FrmFaturalar_Load(object sender, EventArgs e)
        {

            faturaListele();
            temizle();

        }

        private void btnFaturaBilgileriKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("insert into TBL_FATURABILGI (SERI,SIRANO,TARIH,SAAT,VERGIDAIRE,ALICI,TESLIMEDEN,TESLIMALAN) values" +
                                                  "(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", sqlBaglantisi.baglanti());
            command.Parameters.AddWithValue("@p1", txtSeri.Text);
            command.Parameters.AddWithValue("@p2", txtSıraNo.Text);
            command.Parameters.AddWithValue("@p3", mskTarih.Text);
            command.Parameters.AddWithValue("@p4", mskSaat.Text);
            command.Parameters.AddWithValue("@p5", txtVergiDaire.Text);
            command.Parameters.AddWithValue("@p6", txtAlici.Text);
            command.Parameters.AddWithValue("@p7", txtTeslimEden.Text);
            command.Parameters.AddWithValue("@p8", txtTeslimAlan.Text);
            command.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Fatura Bilgisi Sisteme Kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            faturaListele();
        }

        private void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dataRow != null)
            {
                txtId.Text = dataRow["FATURABILGIID"].ToString();
                txtSeri.Text = dataRow["SERI"].ToString();
                txtSıraNo.Text = dataRow["SIRANO"].ToString();
                mskTarih.Text = dataRow["TARIH"].ToString();
                mskSaat.Text = dataRow["SAAT"].ToString();
                txtVergiDaire.Text = dataRow["VERGIDAIRE"].ToString();
                txtAlici.Text = dataRow["ALICI"].ToString();
                txtTeslimEden.Text = dataRow["TESLIMEDEN"].ToString();
                txtTeslimAlan.Text = dataRow["TESLIMALAN"].ToString();
            }
        }

        private void btnFaturaBilgiTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnFaturaDetayKaydet_Click(object sender, EventArgs e)
        {
            double miktar, tutar, fiyat;
            fiyat = Convert.ToDouble(txtFiyat.Text);
            miktar = Convert.ToDouble(txtMiktar.Text);
            tutar = miktar * fiyat;
            txtTutar.Text = tutar.ToString();

            SqlCommand command = new SqlCommand("insert into TBL_FATURADETAY (URUNAD,MIKTAR,FIYAT,TUTAR,FATURAID) values (@p1,@p2,@p3,@p4,@p5)", sqlBaglantisi.baglanti());
            command.Parameters.AddWithValue("@p1", txtUrunAdi.Text);
            command.Parameters.AddWithValue("@p2", txtMiktar.Text);
            command.Parameters.AddWithValue("@p3", txtFiyat.Text);
            command.Parameters.AddWithValue("@p4", txtTutar.Text);
            command.Parameters.AddWithValue("@p5", txtFaturaId.Text);
            command.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Faturaya Ait Ürün Kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        private void btnFaturaBilgisiSil_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Delete from TBL_FATURABILGI where FATURABILGIID=@p1", sqlBaglantisi.baglanti());
            command.Parameters.AddWithValue("@p1", txtId.Text);
            command.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Fatura Verileri Silinmiştir!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            faturaListele();
            temizle();
        }

        private void btnFaturaBilgisiGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Update TBL_FATURABILGI set SERI=@p1,SIRANO=@p2,TARIH=@p3,SAAT=@p4,VERGIDAIRE=@p5,ALICI=@p6,TESLIMEDEN=@p7,TESLIMALAN=@p8 " +
                                                  "WHERE FATURABILGIID=@p9", sqlBaglantisi.baglanti());
           
            command.Parameters.AddWithValue("@p1", txtSeri.Text);
            command.Parameters.AddWithValue("@p2", txtSıraNo.Text);
            command.Parameters.AddWithValue("@p3", mskTarih.Text);
            command.Parameters.AddWithValue("@p4", mskSaat.Text);
            command.Parameters.AddWithValue("@p5", txtVergiDaire.Text);
            command.Parameters.AddWithValue("@p6", txtAlici.Text);
            command.Parameters.AddWithValue("@p7", txtTeslimEden.Text);
            command.Parameters.AddWithValue("@p8", txtTeslimAlan.Text);
            command.Parameters.AddWithValue("@p9", txtId.Text);
            command.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Fatura Bilgisi Güncellenmiştir!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            faturaListele();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunler frmFaturaUrunler = new FrmFaturaUrunler();
            DataRow dataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if(dataRow != null)
            {
                frmFaturaUrunler.id = dataRow["FATURABILGIID"].ToString();
            }

            frmFaturaUrunler.Show();
        }
    }
}
