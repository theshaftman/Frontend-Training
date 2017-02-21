using NewAppl.Models.BusinessLayer.Structure;
using NewAppl.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace NewAppl.Models.Data
{
    internal class EmailModel : IEmailModel
    {
        internal EmailModel()
        {

        }

        Result IEmailModel.SendMail(FormCollection model)
        {
            string name = "Mariyan";
            string subject = "Тестови имейл";
            string body = "Здравей, изпращам ти тестови имейл :)\nTest";

            var fromAddress = new MailAddress(Constant.FROM_EMAIL, name);
            var toAddress = new MailAddress(Constant.TO_EMAIL, Constant.TO_NAME);
            const string fromPassword = Constant.PASSWORD;
            Result result = new Result();

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                try
                {
                    smtp.Send(message);
                    result.Data = "Successful send!";
                    result.Status = "success";
                }
                catch (Exception e)
                {
                    result.Data = "Failure!";
                    result.Status = "fail";
                }

                return result;
            }
        }
    }
}