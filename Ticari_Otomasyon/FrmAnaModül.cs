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


        FrmUrunler fr;
        private void BtnUrunler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(fr == null) {
                fr = new FrmUrunler();
                fr.MdiParent = this;
                fr.Show();
            }
            

        }

        FrmMusteriler musteri;
        private void BtnMusteriler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(musteri == null)
            {
                musteri = new FrmMusteriler();
                musteri.MdiParent = this;
                musteri.Show();
            }
        }


        FrmFirmalar firma;
        private void BtnFirmalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(firma == null)
            {
                firma = new FrmFirmalar();
                firma.MdiParent = this;
                firma.Show();
            }
        }

        FrmPersoneller personel;
        private void BtnPersoneller_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(personel == null)
            {
                personel = new FrmPersoneller();
                personel.MdiParent = this;
                personel.Show();
            }
        }

        FrmRehber rehber;
        private void BtnRehber_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(rehber == null)
            {
                rehber = new FrmRehber();
                rehber.MdiParent = this;
                rehber.Show();
            }
        }

        FrmGiderler gider;
        private void BtnGiderler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(gider == null)
            {
                gider = new FrmGiderler();
                gider.MdiParent = this;
                gider.Show();
            }
        }
    }
}
