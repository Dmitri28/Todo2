using Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace Todo.Controllers
{
	public class RegistrationController : Controller
	{


		public IActionResult Index()
		{
			return View();
		}
	

		public IActionResult Register(string username, string password)
		{
			var doesExist = Database.Instance.DoesExistUser(username);
			if (doesExist)
				return RedirectToAction(nameof(Index));

			var user = new User(username, password);
			Database.Instance.TryAddUser(user);

			var cookies = Response.Cookies;
			cookies.Delete(nameof(username));
			cookies.Delete(nameof(password));

			cookies.Append(nameof(username), username);
			cookies.Append(nameof(password), password);

			return RedirectToAction(nameof(Index), "Todo");
		}

		public IActionResult Login()
		{

			return View(RedirectToAction(nameof(Index), nameof(TodoController)));
		}
		[HttpPost]
		public IActionResult SetCookies(string username)
		{
			Response.Cookies.Append("username", username);
				return View(nameof(Index));
			

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

		public IActionResult Registration(string username, string password)
		{
			Response.Cookies.Append("username", username);
			Response.Cookies.Append("password", password);
			return RedirectToAction("Index", "Registration");

		}
	}







	public class UserModel
	{

		[Required]
		public string Name { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Passowrd { get; private set; }


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
}