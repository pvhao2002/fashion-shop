using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Project.Utils
{
    public static class MailUtils
    {
        public static async Task<Dictionary<string, string>> SendEmail(string to, string subject, string body)
        {
            const string from = "hideonbush8405@gmail.com";
            const string pass = "xvqe phsm spay yjov";
            var response = new Dictionary<string, string>();
            try
            {
                var message = new MailMessage(from, to, subject, body)
                {
                    BodyEncoding = Encoding.UTF8,
                    SubjectEncoding = Encoding.UTF8,
                    IsBodyHtml = true,
                };

                message.ReplyToList.Add(new MailAddress(from));
                message.Sender = new MailAddress(from);

                using (var client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.Credentials = new NetworkCredential(from, pass);
                    client.EnableSsl = true;
                    await client.SendMailAsync(message);
                }

                response.Add(Constant.STATUS_RS, Constant.SUCCESS_RS);
                response.Add(Constant.MESSAGE_RS, "Gửi email thành công!");
            }
            catch (SmtpFailedRecipientException)
            {
                // Xử lý lỗi cho email không gửi được tới người nhận cụ thể
                response.Add(Constant.STATUS_RS, Constant.ERROR_RS);
                response.Add(Constant.MESSAGE_RS, "Email không gửi được tới người nhận!");
            }
            catch (Exception)
            {
                // Xử lý lỗi chung
                response.Add(Constant.STATUS_RS, Constant.ERROR_RS);
                response.Add(Constant.MESSAGE_RS, "Hệ thống lỗi. Vui lòng thử lại sau!");
            }
            return response;
        }

        public static string BuildBody()
        {
            return string.Empty;
        }
    }
}