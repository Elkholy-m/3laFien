using Contracts;
using Entities.Models;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;
        private readonly ILoggerManager _logger;

        public EmailSender(EmailConfiguration emailConfig, ILoggerManager logger)
        {
            _emailConfig = emailConfig;
            _logger = logger;
        }

        public void SendEmail(Message message)
        {
            MimeMessage emilMessage = CreateEmailMSG(message);
            SendEmailMSG(emilMessage);
        }
        public async Task SendEmailAsync(Message message)
        {
            MimeMessage emailMessage = CreateEmailMSG(message);
            await SendEmailMSGAsync(emailMessage);
        }
        private MimeMessage CreateEmailMSG(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            var bodyBuilder = new BodyBuilder() { HtmlBody = string.Format("<h2 style='color:red;'>{0}</h2>", message.Body)};

            byte[] buffer;
            if (message.Attachments is not null && message.Attachments.Any())
            {
                foreach (var attachment in message.Attachments)
                {
                    using(var ms = new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        buffer = ms.ToArray();
                    }
                    bodyBuilder.Attachments.Add(attachment.FileName, buffer, ContentType.Parse(attachment.ContentType));
                }
            }
            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        private async Task SendEmailMSGAsync(MimeMessage emailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.Username, _emailConfig.Password);

                    await client.SendAsync(emailMessage);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private void SendEmailMSG(MimeMessage emailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.Username, _emailConfig.Password);

                    client.Send(emailMessage);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
