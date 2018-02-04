using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApplication23.Models;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace WebApplication23.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

		[HttpPost]
		public ActionResult SendMessage(string smodel)
							
		{

			var js = new JavaScriptSerializer();
			EMail model = js.Deserialize<EMail>(smodel);
			//Enter the UserId
			MailAddress ma_from = new MailAddress("techsystechsolutions@gmail.com", "Name");
			MailAddress ma_to = new MailAddress("techsystechsolutions@gmail.com", "Name");
			//ENter the password
			string s_password = "april_11";
			string s_subject = model.Subject;
			string s_body = "Hello " + "deepika" + ",";

			s_body += "@You have got a message from a new would be Client";
			s_body += "@";
			s_body += System.Environment.NewLine + "Name:" + model.Name + System.Environment.NewLine + "EMail: " +  model.Email.Replace("@","##");
			//s_body += "@"+"EMail:"  + model.Email ;
			s_body += "@" + System.Environment.NewLine + "Subject:" + model.Subject + System.Environment.NewLine;
			s_body += "@" + "Message:" + model.Body + System.Environment.NewLine;
			s_body += "@Thanks";
			s_body = s_body.Replace("@", "" + System.Environment.NewLine);
			s_body = s_body.Replace("##", "@");


			SmtpClient smtp = new SmtpClient
			{
				Host = "smtp.gmail.com",
				Port = 587,
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				//UseDefaultCredentials = false,

				Credentials = new NetworkCredential(ma_from.Address, s_password)
			};


			using (MailMessage mail = new MailMessage(ma_from, ma_to)
			{
				Subject = s_subject,
				Body = s_body

			})

				smtp.Send(mail);
			return new JsonResult { Data = "Success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
		}

			
		}
	}
