
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Utils
{
    public class Mail
    {
        public static ILog log = log4net.LogManager.GetLogger("Utils");

        public static void SendMail(string toEmail, string subject, string body)
        {

            try
            {
                String[] allrecepeints = toEmail.Split(',');
                SmtpClient mySmtpClient = new SmtpClient(""); //ex: "mail.companyname.com" 

                // set smtp-client with basicAuthentication
                mySmtpClient.UseDefaultCredentials = false;
                //NetworkCredential basicAuthenticationInfo = new NetworkCredential("username", "password");
                //mySmtpClient.Credentials = basicAuthenticationInfo;

                // add from,to mailaddresses
                MailAddress from = new MailAddress("myemail@company.com");
                //MailAddress to = new MailAddress(toEmail);
                //MailMessage myMail = new System.Net.Mail.MailMessage(from, to);
                MailMessage myMail = new System.Net.Mail.MailMessage();
                myMail.From = from;
                foreach (var recepient in allrecepeints)
                {
                    myMail.To.Add(recepient);
                }

                // add ReplyTo
                MailAddress replyto = new MailAddress("myemail@company.com");
                //myMail.ReplyTo = replyto;

                // set subject and encoding
                myMail.Subject = subject;
                myMail.SubjectEncoding = System.Text.Encoding.UTF8;

                // set body-message and encoding
                myMail.Body = body;
                myMail.BodyEncoding = System.Text.Encoding.UTF8;
                // text or html
                myMail.IsBodyHtml = true;

                mySmtpClient.Send(myMail);
            }

            catch (SmtpException ex)
            {
                log.Error(ex.ToString());
                throw new ApplicationException
                  ("SmtpException has occured: " + ex.Message);
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                throw ex;
            }
        }
    }
}


