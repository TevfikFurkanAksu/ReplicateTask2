using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Replicateimza2.Controllers
{
    public class TrialController : Controller
    {
        // GET: Trial
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult trialsave(string firstName, string lastName, string email, string phone, string job, string industry)
        {
            string conString = @"conn string";
            SqlConnection baglanti = new SqlConnection(conString);
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();
                // Bağlantımızı kontrol ediyoruz, eğer kapalıysa açıyoruz.
                string kayit = "insert into tabloadi(firstname,lastname,email,phone,job,industry) values (@firstname,@lastname,@email,@phone,@job,@industry)";
                // müşteriler tablomuzun ilgili alanlarına kayıt ekleme işlemini gerçekleştirecek sorgumuz.
                SqlCommand komut = new SqlCommand(kayit, baglanti);
                //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
                komut.Parameters.AddWithValue("@firstname", firstName);
                komut.Parameters.AddWithValue("@lastname", lastName);
                komut.Parameters.AddWithValue("@email", email);
                komut.Parameters.AddWithValue("@phone", phone);
                komut.Parameters.AddWithValue("@job", job);

                komut.Parameters.AddWithValue("@industry", industry);



                //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
                komut.ExecuteNonQuery();
                //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
                baglanti.Close();

            }
            catch (Exception hata)
            {
            }



            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            var mail = new MailMessage();
            mail.From = new MailAddress("a.yilmaz0852@gmail.com");
            mail.To.Add(email);
            mail.Subject = "dosya gönderme";
            mail.IsBodyHtml = true;
            string htmlBody;
            htmlBody = "MailBody";
            mail.Body = htmlBody;
            Attachment attachment;
            attachment = new Attachment(@"dosya yolunu giriniz");
            mail.Attachments.Add(attachment);
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("a.yilmaz0852@gmail.com", "********");
            SmtpServer.EnableSsl = true;
            SmtpServer.Timeout = int.MaxValue;
            SmtpServer.Send(mail);








            return RedirectToAction("TrialForm", "Home");


        }
    }
}