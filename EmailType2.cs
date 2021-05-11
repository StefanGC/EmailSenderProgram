namespace EmailSenderProgram
{
    class EmailType2 : Email
    {
        string Name { get; set; }

        public EmailType2(string name, string email)
        {
            Message = new System.Net.Mail.MailMessage();
            Name = name;
            EmailAddress = email;
        }

        public override void SendMessage()
        {
            Message.To.Add(EmailAddress);
            Message.Subject = "We miss you as a customer";
            Message.From = new System.Net.Mail.MailAddress("info@consulence.com");
            Message.Body = $"Hi {EmailAddress} <br>We miss you as a customer. Our shop is filled with nice products. Here is a voucher that gives you 50 kr to shop for.<br>Voucher: {Name} <br><br>Best Regards,<br>Consulence Team";
        }
    }
}
