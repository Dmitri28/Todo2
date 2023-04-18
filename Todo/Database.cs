using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Controllers;


public sealed class Todo
{
	public int Id { get; set; }
	public string Username { get; set; }
	public string Title { get; private set; }
	public string Content { get; private set; }
	public bool IsCompleted { get; private set; }
	public Todo(string username, string title, string content)
	{
		Title = title;
		Content = content;
		Username = username;
	}

	public void SetData(string title, string content)
	{
		Title = title;
		Content = content;


	}
}

public sealed class User
{
	public int Id { get; set; }
	public string Username { get; set; }
	public string Password { get; set; }

	public User(string username, string password)
	{
		Username = username;
		Password = password;
	}
}

public sealed class Database : DbContext
{
	private const string DatabaseName = "todos";
	private const string DatabaseFilePath = $"{DatabaseName}.db";
	public static readonly Database Instance = new Database();

	public List<Todo> Todos => TodosTable.ToList();
	public List<User> Users => UsersTable.ToList();

	private DbSet<Todo> TodosTable => Set<Todo>();
	private DbSet<User> UsersTable => Set<User>();

	public Database()
	{
		File.Delete(DatabaseFilePath);

		Database.EnsureCreated();



	}

	public void TryAdd(Todo newTodo)
	{
		var todos = Todos.ToList();
		var doesExist = todos.Any(todo => todo.Title == newTodo.Title);
		if (doesExist)

			return;

		TodosTable.Add(newTodo);
		SaveChanges();


	}
	public void TryAddUser(User user)
	{
		UsersTable.Add(user);
		SaveChanges();

	}
	public void TryRemove(string title)
	{
		var todoIndex = Todos.FindIndex(todo => todo.Title == title);
		if (todoIndex < 0)
			return;
		var todo = Todos[todoIndex];
		TodosTable.Remove(todo);
		SaveChanges();
	}

	public void TryUpdate(int id, string title, string content)
	{
		var todos = Todos;
		var todoIndex = todos.FindIndex(todo => todo.Id == id);
		if (todoIndex < 0)
			return;

		var todo = todos[todoIndex];
		todo.SetData(title, content);
		TodosTable.Update(todo);
		SaveChanges();
	}

	public bool DoesExistUser(string username)
	{
		return Users.Any(u => u.Username == username);
	}

	public bool IsCorrectUserData(string username, string password)
	{
		var user = Users.Find(u => u.Username == username);

		return user.Password == password;
	}

	protected override void OnConfiguring(DbContextOptionsBuilder options)
	{
		options.UseSqlite($"Data Source={DatabaseName}.db");
	}


}
