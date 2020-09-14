using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using DevExpress.Accessibility;

namespace Ticari_Otomasyon
{
    public partial class FrmMail : Form
    {
        public FrmMail()
        {
            InitializeComponent();
        }

        public string mail;
        private void FrmMail_Load(object sender, EventArgs e)
        {
            txtMailAdres.Text = mail;
        }

        private void btnGonder_Click(object sender, EventArgs e)
        {
            //   !!! ADD System.Net// System.Net.Mail
           
            MailMessage message = new MailMessage();
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("Mail Adress", "Password");
            client.Port = 587;
            client.Host = "smpt.gmail.com";
            client.EnableSsl = true;
            message.To.Add(rchMesaj.Text);
            message.From = new MailAddress("Mail adress");
            message.Subject = txtKonu.Text;
            message.Body = rchMesaj.Text;
            client.Send(message);

        }
    }
}
