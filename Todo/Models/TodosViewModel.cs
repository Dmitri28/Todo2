namespace Controllers
{

	public sealed class TodosViewModel
	{
		public readonly List<Todo> _todos = new List<Todo>();
		public TodosViewModel(List<Todo> todos)
		{
			_todos = todos;
		}

	}


}
