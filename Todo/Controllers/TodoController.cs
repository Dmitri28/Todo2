using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
	public sealed class TodoController : Controller
	{


		public IActionResult Index()
		{

			return View(new TodosViewModel(Database.Instance.Todos));
		}
		

		public IActionResult OnAdd(string title, string content)
		{


			var username = Request.Cookies["username"];
			if (username == null)
				return View(nameof(Index));
			var todo = new Todo(username, title, content);
			Database.Instance.TryAdd(todo);
			return RedirectToAction(nameof(Index));
		}

		public IActionResult OnDelete(string title)
		{
			Database.Instance.TryRemove(title);
			return RedirectToAction(nameof(Index));

		}


		public IActionResult Update(string title)
		{
			var todo = Database.Instance.Todos.Find(todo => todo.Title == title);
			if (todo == null)
				return View(nameof(Index));

			TodoViewModel todoViewModel = new(todo);

			return View(nameof(Update), todoViewModel);
		}

		public IActionResult OnUpdate(int id, string title, string content)
		{
			Database.Instance.TryUpdate(id, title, content);

			return View(nameof(Index));
		}

		public IActionResult Save(string title)
		{
			Database.Instance.SaveChanges();
			return RedirectToAction(nameof(Save));
		}

		public IActionResult Cancel(TodosViewModel model, string title)
		{
			if (Cancel != null)
			{

				return RedirectToAction("Update");
			}
			return View();
		}

	}
}
