using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using Todo.Controllers;

namespace Controllers
{
	public sealed class HomeController : Controller
	{
		public IActionResult Index()
		{
			HttpRequest request = HttpContext.Request;
			string username = Request.Cookies["username"];
			string password = Request.Cookies["password"];
			if (username != null
				&& password != null
				&& Database.Instance.DoesExistUser(username)
				&& Database.Instance.IsCorrectUserData(username, password))
			{
				return RedirectToAction(nameof(TodoController.Index), "Todo");
			}
			else
			{
				return RedirectToAction(nameof(RegistrationController.Index), "Registration");
			}
		}

		public IActionResult OnAdd(string username, string password)
		{

			var cookie = new Cookie("username", "pasword");
			if (cookie != null)
			{



				cookie.Name = username;
				cookie.Value = password;
				Response.WriteAsync("Cookie has alredy written" + cookie.Name + " " + cookie.Value);


			}
			return View(cookie);
		}
		public IActionResult Home(string username, string password)
		{
			Response.Cookies.Append("username", username);
			Response.Cookies.Append("password", password);

			return RedirectToAction("Index1", "Home");

		}
		public IActionResult Index1()
		{
			string userCredentials = Request.Cookies["UserCredentials"];
			ViewBag.UserCredentials = userCredentials;



			HttpRequest request = HttpContext.Request;
			string username = Request.Cookies["username"];
			string password = Request.Cookies["password"];
			var cookies = request.Cookies;
			StringBuilder cookieInfo = new StringBuilder();
			foreach (string key in cookies.Keys)
			{
				cookieInfo.Append($"{key}:{cookies[key]}</br>");
			}
			return View();


		}




		public class UserModel
		{

			[Required]
			public string Name { get; set; }
			[Required]
			[DataType(DataType.Password)]
			public string Passowrd { get; private set; }


		}
	}
}

namespace Todo
{
	class HttpCookie
	{
		private string v1;
		private string v2;

		public HttpCookie(string v1, string v2)
		{
			this.v1 = v1;
			this.v2 = v2;
		}



	}
}

