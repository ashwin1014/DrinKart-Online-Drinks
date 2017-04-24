using OnlineShopping.Domain.Abstract;
using OnlineShopping.Domain.Entities;
using System.Net;
using System.Net.Mail;
using System.Text;
using System;
using System.Linq;


namespace OnlineShopping.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "ashwinbordoloi93@gmail.com";
        public string MailFromAddress = "ashwinbordoloi93@gmail.com";
        public bool UseSsl = true;
        public string Username = "ashwinbordoloi93@gmail.com";
        public string Password = "aanchal109";
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"C:\TempMail";
    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;

        public EmailOrderProcessor ( EmailSettings settings )
        {
            emailSettings = settings;
        }

        public void ProcessOrder ( Cart cart, ShippingDetails shippingInfo )
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod
                    = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                string body = GetEmailBody(cart, shippingInfo);

                MailMessage mailMessage = new MailMessage(emailSettings.MailFromAddress, emailSettings.MailToAddress, "New order submitted!", body);

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }

                try
                {
                    smtpClient.Send(mailMessage);
                }


                catch (SmtpFailedRecipientsException ex)
                {
                    for (int i = 0; i < ex.InnerExceptions.Length; i++)
                    {
                        SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                        if (status == SmtpStatusCode.MailboxBusy ||
                            status == SmtpStatusCode.MailboxUnavailable)
                        {
                            Console.WriteLine("Delivery failed - retrying in 5 seconds.");
                            System.Threading.Thread.Sleep(5000);
                            smtpClient.Send(mailMessage);
                        }
                        else
                        {
                            Console.WriteLine("Failed to deliver message to {0}",
                                ex.InnerExceptions[i].FailedRecipient);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception caught in RetryIfBusy(): {0}",
                            ex.ToString());
                }

        }
        }

        private string GetEmailBody ( Cart cart, ShippingDetails shippingInfo )
        {
            StringBuilder body = new StringBuilder()
            .AppendLine("A new order has been submitted")
            .AppendLine("---")
            .AppendLine("Items:");

            foreach (var line in cart.Lines)
            {
                var subtotal = line.Product.Price * line.Quantity;
                body.AppendFormat("{0} x {1} (subtotal: {2:c}", line.Quantity,
                line.Product.Name,
                subtotal);
            }
            body.AppendFormat("Total order value: {0:c}", cart.ComputeTotalValue())
            .AppendLine("---")
            .AppendLine("Ship to:")
            .AppendLine(shippingInfo.Name)
            .AppendLine(shippingInfo.Line1)
            .AppendLine(shippingInfo.Line2 ?? "")
            .AppendLine(shippingInfo.Line3 ?? "")
            .AppendLine(shippingInfo.City)
            .AppendLine(shippingInfo.State ?? "")
            .AppendLine(shippingInfo.Country)
            .AppendLine(shippingInfo.Zip)
            .AppendLine("---")
            .AppendFormat("Gift wrap: {0}",
            shippingInfo.GiftWrap ? "Yes" : "No");

            return body.ToString();
        }
    }
}