using System.Net.Mail;

namespace EmailSenderProgram
{
    abstract class Email
	{
		public MailMessage Message { get; set; }
		public string EmailAddress { get; set; }

		public abstract void SendMessage();
	}
}