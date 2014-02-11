using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace Common
{
    public class Mailing
    {
        /// <summary>
        /// Mails the Administrator on Low Product
        /// Level: Common
        /// </summary>
        /// <param name="ProductName">The Name of the product.</param>
        /// <param name="SupplierName">The Name of the supplier.</param>
        /// <param name="AdministratorEmail">The administrators Email Address.</param>
        public void LowProductMail(string ProductName, string SupplierName, string AdministratorEmail)
        {
            MailMessage myMailMessage = new MailMessage();
            myMailMessage.To.Add(new MailAddress(AdministratorEmail));
            myMailMessage.From = new MailAddress("thegreatsupermarketmail@gmail.com");
            myMailMessage.Subject = "Low Stock Notification!";
            myMailMessage.Body = "Supplier: " + SupplierName + " | Product: " + ProductName;

            SmtpClient myClient = new SmtpClient("smtp.gmail.com", 587);
            myClient.EnableSsl = true;
            myClient.UseDefaultCredentials = false;
            myClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            myClient.Credentials = new System.Net.NetworkCredential("thegreatsupermarketmail@gmail.com", "unlockGMAIL");
            myClient.Send(myMailMessage);
        }

        /// <summary>
        /// Mails the User and Administrator on purchase.
        /// Level: Common
        /// </summary>
        /// <param name="Username">The users Username.</param>
        /// <param name="UserEmail">The users Email Address.</param>
        /// <param name="OrderID">The Order ID.</param>
        /// <param name="AdministratorEmail">The administrators Email Address.</param>
        public void PurchaseMail(string Username, string UserEmail, string OrderID, string AdministratorEmail)
        {
            MailMessage myMailMessage = new MailMessage();
            myMailMessage.To.Add(new MailAddress(AdministratorEmail));
            myMailMessage.To.Add(new MailAddress(UserEmail));
            myMailMessage.From = new MailAddress("thegreatsupermarketmail@gmail.com");
            myMailMessage.Subject = "Purchase Notification!";
            myMailMessage.Body = "Username: " + Username + " | Your order has been placed. | Order ID: " + OrderID;

            SmtpClient myClient = new SmtpClient("smtp.gmail.com", 587);
            myClient.EnableSsl = true;
            myClient.UseDefaultCredentials = false;
            myClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            myClient.Credentials = new System.Net.NetworkCredential("thegreatsupermarketmail@gmail.com", "unlockGMAIL");
            myClient.Send(myMailMessage);
        }
    }
}
