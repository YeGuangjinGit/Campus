using System.Net;
using System.Net.Mail;
using System.Text;

namespace Campus.Common
{
    public static class EmailUtil
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="subject">主题</param>
        /// <param name="body">正文</param>
        /// <param name="email">发送给?</param>
        public static void Send(string subject, string body, string email)
        {
            //设置发送方邮件信息，例如：qq邮箱
            string stmpServer = @"smtp.qq.com";//smtp服务器地址
            string mailAccount = @"54074625@qq.com";//邮箱账号
            string pwd = @"ulufzaitugfybhej";//邮箱密码（qq邮箱此处使用授权码，其他邮箱见邮箱规定使用的是邮箱密码还是授权码）

            //邮件服务设置
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            smtpClient.Host = stmpServer;//指定发送方SMTP服务器
            smtpClient.EnableSsl = true;//使用安全加密连接
            smtpClient.UseDefaultCredentials = false;//不和请求一起发送
            smtpClient.Credentials = new NetworkCredential(mailAccount, pwd);//设置发送账号密码

            MailMessage mailMessage = new MailMessage("ye.guangjin@qq.com", email);//实例化邮件信息实体并设置发送方和接收方
            mailMessage.Subject = subject;//设置发送邮件得标题
            mailMessage.Body = body;//设置发送邮件内容
            mailMessage.BodyEncoding = Encoding.UTF8;//设置发送邮件得编码
            mailMessage.IsBodyHtml = true;//设置标题是否为HTML格式
            mailMessage.Priority = MailPriority.Normal;//设置邮件发送优先级

            try
            {
                smtpClient.Send(mailMessage);//发送邮件
            }
            catch (SmtpException ex)
            {
                throw ex;
            }
        }
    }
}
