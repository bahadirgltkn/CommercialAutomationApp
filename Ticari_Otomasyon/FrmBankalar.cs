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
using DevExpress.Utils.MVVM.Internal;

namespace Ticari_Otomasyon
{
    public partial class FrmBankalar : Form
    {
        public FrmBankalar()
        {
            InitializeComponent();
        }

        SqlBaglantisi sqlBaglantisi = new SqlBaglantisi();

        public void bankaListele()
        {

            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Execute BankaBilgileri", sqlBaglantisi.baglanti());    //PROCEDURE --> BankaBilgileri
            dataAdapter.Fill(dataTable);
            gridControlBankalar.DataSource = dataTable;

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

        public void firmaListele()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select ID,AD from TBL_FIRMALAR", sqlBaglantisi.baglanti());
            dataAdapter.Fill(dataTable);
            lueFirma.Properties.ValueMember = "ID";
            lueFirma.Properties.DisplayMember = "AD";
            lueFirma.Properties.DataSource = dataTable;
            
        }

        public void temizle()
        {
            txtId.Text = "";
            txtBankaAdi.Text = "";
            cbxIl.Text = "";
            cbxIlce.Text = "";
            txtSube.Text = "";
            mskIban.Text = "";
            txtHesapNo.Text = "";
            txtYetkili.Text = "";
            mskTel.Text = "";
            mskTarih.Text = "";
            txtHesapTuru.Text = "";
            
        }

        private void FrmBankalar_Load(object sender, EventArgs e)
        {

            bankaListele();
            sehirListele();
            firmaListele();
            temizle();

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("insert into TBL_BANKALAR " +
                "(BANKAADI,IL,ILCE,SUBE,IBAN,HESAPNO,YETKILI,TELEFON,TARIH,HESAPTURU,FIRMAID) values " +
                "(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", sqlBaglantisi.baglanti());
           
            command.Parameters.AddWithValue("@p1", txtBankaAdi.Text);
            command.Parameters.AddWithValue("@p2", cbxIl.Text);
            command.Parameters.AddWithValue("@p3", cbxIlce.Text);
            command.Parameters.AddWithValue("@p4", txtSube.Text);
            command.Parameters.AddWithValue("@p5", mskIban.Text);
            command.Parameters.AddWithValue("@p6", txtHesapNo.Text);
            command.Parameters.AddWithValue("@p7", txtYetkili.Text);
            command.Parameters.AddWithValue("@p8", mskTelefon.Text);
            command.Parameters.AddWithValue("@p9", mskTarih.Text);
            command.Parameters.AddWithValue("@p10", txtHesapTuru.Text);
            command.Parameters.AddWithValue("@p11", lueFirma.EditValue);
            command.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            bankaListele();
            MessageBox.Show("Banka Bilgisi Sisteme Eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            //we got the data using the procedure
            //note that the procedure is spelled correctly

            DataRow dataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
           
            if (dataRow != null)
            {
                txtId.Text = dataRow["ID"].ToString();
                txtBankaAdi.Text = dataRow["BANKAADI"].ToString();
                cbxIl.Text = dataRow["IL"].ToString();
                cbxIlce.Text = dataRow["ILCE"].ToString();
                txtSube.Text = dataRow["SUBE"].ToString();
                mskIban.Text = dataRow["IBAN"].ToString();
                txtHesapNo.Text = dataRow["HESAPNO"].ToString();
                txtYetkili.Text = dataRow["YETKILI"].ToString();
                mskTel.Text = dataRow["TELEFON"].ToString();
                mskTarih.Text = dataRow["TARIH"].ToString();
                txtHesapTuru.Text = dataRow["HESAPTURU"].ToString();
                lueFirma.Text = dataRow["AD"].ToString(); 
                
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Delete from TBL_BANKALAR where ID=@p1", sqlBaglantisi.baglanti());
            command.Parameters.AddWithValue("@p1", txtId.Text);
            command.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Banka Bilgileri Başarıyla Silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            bankaListele();
            temizle();

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {

            SqlCommand command = new SqlCommand("Update TBL_BANKALAR set BANKAADI=@p1,IL=@p2,ILCE=@p3,SUBE=@p4,IBAN=@p5,HESAPNO=@p6," +
                                                 "YETKILI=@p7,TELEFON=@p8,TARIH=@p9,HESAPTURU=@p10,FIRMAID=@p11 WHERE ID=@p12", sqlBaglantisi.baglanti());
            command.Parameters.AddWithValue("@p1", txtBankaAdi.Text);
            command.Parameters.AddWithValue("@p2", cbxIl.Text);
            command.Parameters.AddWithValue("@p3", cbxIlce.Text);
            command.Parameters.AddWithValue("@p4", txtSube.Text);
            command.Parameters.AddWithValue("@p5", mskIban.Text);
            command.Parameters.AddWithValue("@p6", txtHesapNo.Text);
            command.Parameters.AddWithValue("@p7", txtYetkili.Text);
            command.Parameters.AddWithValue("@p8", mskTel.Text);
            command.Parameters.AddWithValue("@p9", mskTarih.Text);
            command.Parameters.AddWithValue("@p10", txtHesapTuru.Text);
            command.Parameters.AddWithValue("@p11", lueFirma.EditValue);
            command.Parameters.AddWithValue("@p12", txtId.Text);
            command.ExecuteNonQuery();
            sqlBaglantisi.baglanti().Close();
            MessageBox.Show("Banka Bilgileri Güncellenmiştir", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bankaListele(); 
           
        }
    }
}
