using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EmailSenderProgram
{
    internal class Program
    {
        /// <summary>
        /// This application is run everyday
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            //Two types of errors are defined
            bool errorType1 = false, errorType2 = false;
            //Call the method that do the work for me, I.E. sending the mails
            Console.WriteLine("Send Welcomemail");
            errorType1 = WelcomeEmail();

#if DEBUG
            //Debug mode, always send Comeback mail
            Console.WriteLine("Send Comebackmail");
            errorType2 = ComeBackEmail("ConsulenceComebackToUs");
#else
			//Every Sunday run Comeback mail
			if (DateTime.Now.DayOfWeek.Equals(DayOfWeek.Monday))
			{
				Console.WriteLine("Send Comebackmail");
			    errorType2 = ComeBackEmail("ConsulenceComebackToUs");
			}
#endif

            //Check if the sending went OK
            if (errorType1 == false && errorType2 == false)
                Console.WriteLine("Congratulations, all emails have been sent successfully");
            else
                Console.Error.WriteLine("Something has gone wrong and we are working to fix it");

            Console.ReadKey();
        }

        /// <summary>
        /// Send Welcome mail
        /// </summary>
        /// <returns></returns>
        public static bool WelcomeEmail()
        {
            try
            {
                //List all customers
                List<Customer> customers = DataLayer.ListCustomers();

                //loop through list of new customers
                foreach (Customer customer in customers)
                {
                    //If the customer is newly registered, one day back in time
                    if (customer.CreatedDateTime > DateTime.Now.AddDays(-1))
                    {
                        EmailType1 mailMessage = new(customer.Email);
                        SendMails(mailMessage.Message, customer.Email);
                    }
                }
                Console.WriteLine();
                //All mails are sent! Success!
                return false;
            }
            catch (Exception e)
            {
                //Something went wrong :(
                Console.Error.WriteLine(e.Message);
                return true;
            }
        }

        /// <summary>
        /// Send Customer ComebackMail
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private static bool ComeBackEmail(string name)
        {
            try
            {
                //List all customers 
                List<Customer> customers = DataLayer.ListCustomers();
                //List all orders
                List<Order> orders = DataLayer.ListOrders();
                HashSet<string> sendList = new();

                //loop through list of customers
                foreach (Customer customer in customers)
                {
                    // We send mail if customer hasn't put an order
                    bool Send = true;
                    //loop through list of orders to see if customer don't exist in that list
                    foreach (Order order in orders)
                    {
                        // Email exists in order list
                        if (customer.Email == order.CustomerEmail)
                        {
                            //We don't send email to that customer
                            Send = false;
                        }
                    }

                    //Send if customer hasn't put order
                    if (Send == true)
                    {
                        EmailType2 mailMessage = new(name, customer.Email);
                        SendMails(mailMessage.Message, customer.Email);
                    }
                }
                Console.WriteLine();
                //All mails are sent! Success!
                return false;
            }
            catch (Exception e)
            {
                //Something went wrong :(
                Console.Error.WriteLine(e.Message);
                return true;
            }
        }

        
        public static void SendMails(System.Net.Mail.MailMessage mail, string email)
        {
        #if DEBUG
            //Don't send mails in debug mode, just write the emails in console
            Console.WriteLine("	Send mail to:" + email);
        #else
	        //Create a SmtpClient to our smtphost: yoursmtphost
			System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("yoursmtphost");
			//Send mail
			smtp.Send(mail);
        #endif
        }

    }
}