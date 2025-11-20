using Microsoft.AspNetCore.Http;
using MimeKit;
using System.Runtime.Serialization;

namespace Entities.Models
{
    public class Message
    {
        public IEnumerable<MailboxAddress>? To { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public IFormFileCollection? Attachments { get; set; }

        public Message(IEnumerable<string> to, string subject, string body)
        {
            To = to.Select(x => new MailboxAddress("email" ,x));
            Subject = subject;
            Body = body;
        }

        public Message(IEnumerable<string> to, string subject, string body, IFormFileCollection attachments)
        {
            To = to.Select(x => new MailboxAddress("email", x));
            Subject = subject;
            Body = body;
            Attachments = attachments;
        }

    }
}
