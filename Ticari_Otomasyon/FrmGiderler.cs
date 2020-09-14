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
    public partial class FrmGiderler : Form
    {
        public FrmGiderler()
        {
            InitializeComponent();
        }

        SqlBaglantisi sqlBaglantisi = new SqlBaglantisi();

        public void temizle()
        {
            txtId.Text = "";
            txtDogalgaz.Text = "";
            txtEkstra.Text = "";
            txtSu.Text = "";
            txtElektrik.Text = "";
            txtInternet.Text = "";
            txtMaaslar.Text = "";
            rchNotlar.Text = "";
            cbxAy.Text = "";
            cbxYil.Text = "";
        }

        public void giderleriListele()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from TBL_GIDERLER", sqlBaglantisi.baglanti());
            dataAdapter.Fill(dataTable);
            gridControlGiderler.DataSource = dataTable;
        }

        private void FrmGiderler_Load(object sender, EventArgs e)
        {
            giderleriListele();
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Insert into TBL_GIDERLER (AY,YIL,ELEKTRIK,SU,DOGALGAZ,INTERNET,MAASLAR,EKSTRA,NOTLAR) " +
                "values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9) ", sqlBaglantisi.baglanti());

            command.Parameters.AddWithValue("@p1", cbxAy.Text);
            command.Parameters.AddWithValue("@p2", cbxYil.Text);
            command.Parameters.AddWithValue("@p3", decimal.Parse(txtElektrik.Text));
            command.Parameters.AddWithValue("@p4", decimal.Parse(txtSu.Text));
            command.Parameters.AddWithValue("@p5", decimal.Parse(txtDogalgaz.Text));
            command.Parameters.AddWithValue("@p6", decimal.Parse(txtInternet.Text));
            command.Parameters.AddWithValue("@p7", decimal.Parse(txtMaaslar.Text));
            command.Parameters.AddWithValue("@p8", decimal.Parse(txtEkstra.Text));
            command.Parameters.AddWithValue("@p9", rchNotlar.Text);
            command.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Gider tabloya eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            giderleriListele();
        }

        private void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DataRow dataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if(dataRow != null)
            {
                txtId.Text = dataRow["ID"].ToString();
                cbxAy.Text = dataRow["Ay"].ToString();
                cbxYil.Text = dataRow["YIL"].ToString();
                txtElektrik.Text = dataRow["ELEKTRIK"].ToString();
                txtSu.Text = dataRow["SU"].ToString();
                txtDogalgaz.Text = dataRow["DOGALGAZ"].ToString();
                txtInternet.Text = dataRow["INTERNET"].ToString();
                txtMaaslar.Text = dataRow["MAASLAR"].ToString();
                txtEkstra.Text = dataRow["EKSTRA"].ToString();
                rchNotlar.Text = dataRow["NOTLAR"].ToString();
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Delete from TBL_GIDERLER where ID=@p1", sqlBaglantisi.baglanti());
            command.Parameters.AddWithValue("@p1", txtId.Text);

            if (DialogResult.Yes == MessageBox.Show("Veriyi silmek istiyor musunuz?", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                command.ExecuteNonQuery();
                sqlBaglantisi.baglanti().Close();
                giderleriListele();
                MessageBox.Show("Gider tablodan silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            temizle();

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Update TBL_GIDERLER set AY=@p1,YIL=@p2,ELEKTRIK=@p3,SU=@p4,DOGALGAZ=@p5,INTERNET=@p6,MAASLAR=@p7,EKSTRA=@p8,NOTLAR=@p9 where ID=@p10 ", sqlBaglantisi.baglanti());

            command.Parameters.AddWithValue("@p1", cbxAy.Text);
            command.Parameters.AddWithValue("@p2", cbxYil.Text);
            command.Parameters.AddWithValue("@p3", decimal.Parse(txtElektrik.Text));
            command.Parameters.AddWithValue("@p4", decimal.Parse(txtSu.Text));
            command.Parameters.AddWithValue("@p5", decimal.Parse(txtDogalgaz.Text));
            command.Parameters.AddWithValue("@p6", decimal.Parse(txtInternet.Text));
            command.Parameters.AddWithValue("@p7", decimal.Parse(txtMaaslar.Text));
            command.Parameters.AddWithValue("@p8", decimal.Parse(txtEkstra.Text));
            command.Parameters.AddWithValue("@p9", rchNotlar.Text);
            command.Parameters.AddWithValue("@p10", txtId.Text);
            command.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Gider verisi güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            giderleriListele();
            

        }
    }
}
