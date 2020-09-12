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
using System.Diagnostics;
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.Utils.Svg;
using DevExpress.Utils.Automation;

namespace Ticari_Otomasyon
{
    public partial class FrmUrunler : Form
    {
        public FrmUrunler()
        {
            InitializeComponent();
        }

        SqlBaglantisi sqlBaglantisi = new SqlBaglantisi();

        public void temizle()
        {
            txtId.Text = "";
            txtAd.Text = "";
            txtMarka.Text = "";
            txtModel.Text = "";
            mskYil.Text = "";
            txtAlis.Text = "";
            txtSatis.Text = "";
            rchDetay.Text = "";
        }

        public void Listele()
        {

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_URUNLER", sqlBaglantisi.baglanti());
            da.Fill(dt);
            gridControlUrunler.DataSource = dt; 

        }

        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            //Listing products in the grid
            Listele();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            //ADD PRODUCTS TO DATABASE 
            //TYPE CONVERSIONS WERE MADE ACCORDING TO THE DATABASE

            SqlCommand command = new SqlCommand("insert into TBL_URUNLER (URUNAD,MARKA,MODEL,YIL,ADET,ALISFIYAT,SATISFIYAT,DETAY) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", sqlBaglantisi.baglanti());
            command.Parameters.AddWithValue("@p1", txtAd.Text);
            command.Parameters.AddWithValue("@p2", txtMarka.Text);
            command.Parameters.AddWithValue("@p3", txtModel.Text);
            command.Parameters.AddWithValue("@p4", mskYil.Text);
            command.Parameters.AddWithValue("@p5", int.Parse((nudAdet.Value).ToString()));
            command.Parameters.AddWithValue("@p6", decimal.Parse(txtAlis.Text));
            command.Parameters.AddWithValue("@p7", decimal.Parse(txtSatis.Text));
            command.Parameters.AddWithValue("@p8", rchDetay.Text);
            command.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Ürün sisteme eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            //DELETING THE PRODUCT ACCORDING TO THE ENTERING ID VALUE

            SqlCommand command = new SqlCommand("Delete from TBL_URUNLER where ID=@p1", sqlBaglantisi.baglanti());
            command.Parameters.AddWithValue("@p1", txtId.Text);
            command.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Ürün silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Listele();

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //DATA CAME AS NULL 
            //SO I USED FocusedRowObjectChanged

        }

        private void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dataRow != null)
            {
                txtId.Text = dataRow["ID"].ToString();
                txtAd.Text = dataRow["URUNAD"].ToString();
                txtMarka.Text = dataRow["MARKA"].ToString();
                txtModel.Text = dataRow["MODEL"].ToString();
                mskYil.Text = dataRow["YIL"].ToString();
                nudAdet.Value = decimal.Parse(dataRow["ADET"].ToString());
                txtAlis.Text = dataRow["ALISFIYAT"].ToString();
                txtSatis.Text = dataRow["SATISFIYAT"].ToString();
                rchDetay.Text = dataRow["DETAY"].ToString();

            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("UPDATE TBL_URUNLER set URUNAD=@p1 ,MARKA=@p2,MODEL=@p3,YIL=@p4,ADET=@p5,ALISFIYAT=@p6,SATISFIYAT=@p7,DETAY=@p8 where ID=@p9", sqlBaglantisi.baglanti());
            command.Parameters.AddWithValue("@p1", txtAd.Text);
            command.Parameters.AddWithValue("@p2", txtMarka.Text);
            command.Parameters.AddWithValue("@p3", txtModel.Text);
            command.Parameters.AddWithValue("@p4", mskYil.Text);
            command.Parameters.AddWithValue("@p5", int.Parse((nudAdet.Value).ToString()));
            command.Parameters.AddWithValue("@p6", decimal.Parse(txtAlis.Text));
            command.Parameters.AddWithValue("@p7", decimal.Parse(txtSatis.Text));
            command.Parameters.AddWithValue("@p8", rchDetay.Text);
            command.Parameters.AddWithValue("@p9", txtId.Text);
            command.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Ürün Bilgisi Güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Listele();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
