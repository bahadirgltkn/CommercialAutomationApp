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
            command.Parameters.AddWithValue("@p1", TxtAd.Text);
            command.Parameters.AddWithValue("@p2", TxtMarka.Text);
            command.Parameters.AddWithValue("@p3", TxtModel.Text);
            command.Parameters.AddWithValue("@p4", MskYil.Text);
            command.Parameters.AddWithValue("@p5", int.Parse((NudAdet.Value).ToString()));
            command.Parameters.AddWithValue("@p6", decimal.Parse(TxtAlis.Text));
            command.Parameters.AddWithValue("@p7", decimal.Parse(TxtSatis.Text));
            command.Parameters.AddWithValue("@p8", RchDetay.Text);
            command.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Ürün sisteme eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            //DELETING THE PRODUCT ACCORDING TO THE ENTERING ID VALUE

            SqlCommand command = new SqlCommand("Delete from TBL_URUNLER where ID=@p1", sqlBaglantisi.baglanti());
            command.Parameters.AddWithValue("@p1", TxtId.Text);
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
                TxtId.Text = dataRow["ID"].ToString();
                TxtAd.Text = dataRow["URUNAD"].ToString();
                TxtMarka.Text = dataRow["MARKA"].ToString();
                TxtModel.Text = dataRow["MODEL"].ToString();
                MskYil.Text = dataRow["YIL"].ToString();
                NudAdet.Value = decimal.Parse(dataRow["ADET"].ToString());
                TxtAlis.Text = dataRow["ALISFIYAT"].ToString();
                TxtSatis.Text = dataRow["SATISFIYAT"].ToString();
                RchDetay.Text = dataRow["DETAY"].ToString();

            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("UPDATE TBL_URUNLER set URUNAD=@p1 ,MARKA=@p2,MODEL=@p3,YIL=@p4,ADET=@p5,ALISFIYAT=@p6,SATISFIYAT=@p7,DETAY=@p8 where ID=@p9", sqlBaglantisi.baglanti());
            command.Parameters.AddWithValue("@p1", TxtAd.Text);
            command.Parameters.AddWithValue("@p2", TxtMarka.Text);
            command.Parameters.AddWithValue("@p3", TxtModel.Text);
            command.Parameters.AddWithValue("@p4", MskYil.Text);
            command.Parameters.AddWithValue("@p5", int.Parse((NudAdet.Value).ToString()));
            command.Parameters.AddWithValue("@p6", decimal.Parse(TxtAlis.Text));
            command.Parameters.AddWithValue("@p7", decimal.Parse(TxtSatis.Text));
            command.Parameters.AddWithValue("@p8", RchDetay.Text);
            command.Parameters.AddWithValue("@p9", TxtId.Text);
            command.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Ürün Bilgisi Güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Listele();
        }
    }
}
