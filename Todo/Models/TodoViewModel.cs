namespace Controllers
{
	public sealed class TodoViewModel
	{

		public Todo Todo { get; private set; }

		public TodoViewModel(Todo todo)
		{
			Todo = todo;
		}

	}
}
