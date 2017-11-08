using myOwnWebApiNet_4._5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace myOwnWebApiNet_4._5.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    public class MailController : ApiController
    {
        public string get()
        {
            return "dies ist der mail controller";
        }
        public string gettest()
        {
            return "dies ist ein test";
        }
        [Route("api/mail/sendmail/")]
        [HttpPost]
        public async Task<IHttpActionResult> Sendmail([FromBody] MailModel model)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("noreply@niklas-grieger.de");
                mailMessage.To.Add(new MailAddress("developer@niklas-grieger.de"));
                mailMessage.Body = "<ul>" +
                                        "<li><b>Name:</b> " + model.username + "</li>" +
                                        "<li><b>Email Adresse:</b> " + model.mailAdress + "</li>" +
                                        "<li><b>Telefonnummer:</b> " + model.phone + "</li>" +
                                   "</ul><br/>" +
                                   "<b>Nachricht:</b><br/>" + model.message + "<br/><br/>"+
                                   "<b>Mit freundlichen Grüßen<b><br/><br/><b>"+ model.username + "<b>";
                mailMessage.Subject = "Neue Email von meiner Seite - niklas-grieger.de";
                mailMessage.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.1und1.com");

                await smtp.SendMailAsync(mailMessage);

                MailMessage mailMessageCopy = new MailMessage();
                mailMessageCopy.From = new MailAddress("noreply@niklas-grieger.de");
                mailMessageCopy.To.Add(new MailAddress(model.mailAdress));

                mailMessageCopy.Body = mailMessage.Body;
                mailMessageCopy.Subject = "Kopie - Ihre Nachricht an www.niklas-grieger.de";
                mailMessageCopy.IsBodyHtml = true;

                await smtp.SendMailAsync(mailMessageCopy);


                return Ok();
                
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
