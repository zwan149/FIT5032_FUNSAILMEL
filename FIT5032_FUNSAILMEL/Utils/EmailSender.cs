using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FIT5032_FUNSAILMEL.Utils
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "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";

        public void Send(String toEmail, String subject, String contents, HttpPostedFileBase postedFile)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("noreply@funsailmel.com.au", "FunSailMel Admin");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                try
                {
                    byte[] file = new byte[postedFile.ContentLength];
                    postedFile.InputStream.Read(file, 0, file.Length);
                    var attachment = new Attachment()
                    {
                        Content = Convert.ToBase64String(file),
                        Type = Convert.ToString(postedFile.ContentType),
                        Filename = Path.GetFileName(postedFile.FileName)
                    };
                    msg.AddAttachment(attachment);
                }
                catch (Exception) { }
            }
            var response = client.SendEmailAsync(msg);
        }

    }
}