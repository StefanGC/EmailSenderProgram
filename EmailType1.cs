namespace EmailSenderProgram
{
    class EmailType1 : Email
    {
        public EmailType1(string email)
        {
            Message = new System.Net.Mail.MailMessage();
            EmailAddress = email;
        }

        public override void SendMessage()
        {
            Message.To.Add(EmailAddress);
            Message.Subject = "Welcome as a new customer at Consulence!";
            Message.From = new System.Net.Mail.MailAddress("info@consulence.com");
            Message.Body = $"Hi {EmailAddress} <br>We would like to welcome you as customer on our site!<br><br>Best Regards,<br>Consulence Team";
        }
    }
}
