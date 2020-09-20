using DevExpress.XtraEditors.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ticari_Otomasyon
{
    public partial class FrmAnaModül : Form
    {
        public FrmAnaModül()
        {
            InitializeComponent();
        }


        FrmUrunler urun;
        private void BtnUrunler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(urun == null || urun.IsDisposed) {
                urun = new FrmUrunler();
                urun.MdiParent = this;
                urun.Show();
            }
            

        }

        FrmMusteriler musteri;
        private void BtnMusteriler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(musteri == null || musteri.IsDisposed)
            {
                musteri = new FrmMusteriler();
                musteri.MdiParent = this;
                musteri.Show();
            }
        }


        FrmFirmalar firma;
        private void BtnFirmalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(firma == null || firma.IsDisposed)
            {
                firma = new FrmFirmalar();
                firma.MdiParent = this;
                firma.Show();
            }
        }

        FrmPersoneller personel;
        private void BtnPersoneller_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(personel == null || personel.IsDisposed)
            {
                personel = new FrmPersoneller();
                personel.MdiParent = this;
                personel.Show();
            }
        }

        FrmRehber rehber;
        private void BtnRehber_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(rehber == null || rehber.IsDisposed)
            {
                rehber = new FrmRehber();
                rehber.MdiParent = this;
                rehber.Show();
            }
        }

        FrmGiderler gider;
        private void BtnGiderler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(gider == null || gider.IsDisposed)
            {
                gider = new FrmGiderler();
                gider.MdiParent = this;
                gider.Show();
            }
        }

        FrmBankalar banka;
        private void BtnBankalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(banka == null || banka.IsDisposed)
            {
                banka = new FrmBankalar();
                banka.MdiParent = this;
                banka.Show();
            }
        }

        FrmFaturalar fatura;
        private void BtnFaturalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(fatura == null || fatura.IsDisposed)
            {
                fatura = new FrmFaturalar();
                fatura.MdiParent = this;
                fatura.Show();
            }
        }

        FrmNotlar not;
        private void BtnNotlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(not == null || not.IsDisposed)
            {
                not = new FrmNotlar();
                not.MdiParent = this;
                not.Show();
            }
        }

        private void FrmAnaModül_Load(object sender, EventArgs e)
        {

        }


    }
}
