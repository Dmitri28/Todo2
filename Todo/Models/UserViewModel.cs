using Controllers;

namespace Todo.Models
{
	public class UserViewModel
	{
		public readonly List<TodoViewModel> user = new List<TodoViewModel>();

		public UserViewModel(List<TodoViewModel> users)
		{
			users = user;
		}
	}
}
