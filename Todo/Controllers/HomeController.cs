using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Diagnostics;
using Todo.Models;
using Todo.ViewModels;

namespace Todo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        _logger.LogInformation(("Index page requested"));
        var todoItems = new List<TodoItem>();
        using (SqliteConnection con = new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                con.Open();
                tableCmd.CommandText = $"Select * from todo";
                try
                {
                    var todoListViewModel = GetAllTodos();
                    return View(todoListViewModel);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }
        _logger.LogInformation(("Index page returned"));
        return View();
    }

    [HttpGet]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet]
    public JsonResult PopulateForm(int id)
    {
        var todo = GetById(id);
        return Json(todo);
    }

    public RedirectResult Insert(TodoItem todo)
    {
        using (SqliteConnection con = new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                con.Open();
                tableCmd.CommandText = $"Insert into todo (name) values ('{todo.Name}')";
                try
                {
                    tableCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            return Redirect("/Home/Index");
        }
    }


    [HttpPost]
    public RedirectResult Delete(int id)
    {
        using (SqliteConnection con = new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                con.Open();
                tableCmd.CommandText = $"Delete from todo where id = {id}";
                try
                {
                    tableCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            return Redirect("/Home/Index");
        }
    }

    [HttpPut]
    public RedirectResult Update(TodoItem todo)
    {
        using (SqliteConnection con =
               new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                con.Open();
                tableCmd.CommandText = $"UPDATE todo SET name = '{todo.Name}' WHERE Id = '{todo.Id}'";
                try
                {
                    tableCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        return Redirect("https://localhost:5001/");
    }

    internal TodoViewModel GetAllTodos()
    {
        List<TodoItem> todoList = new();

        using (SqliteConnection con =
               new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                con.Open();
                tableCmd.CommandText = "SELECT * FROM todo";

                using (var reader = tableCmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            todoList.Add(
                                new TodoItem
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1)
                                });
                        }
                    }
                    else
                    {
                        return new TodoViewModel
                        {
                            TodoList = todoList
                        };
                    }
                }

                ;
            }
        }

        return new TodoViewModel
        {
            TodoList = todoList
        };
    }

    internal TodoItem GetById(int id)
    {
        TodoItem todo = new();

        using (var connection =
               new SqliteConnection("Data Source=db.sqlite"))
        {
            using (var tableCmd = connection.CreateCommand())
            {
                connection.Open();
                tableCmd.CommandText = $"SELECT * FROM todo Where Id = '{id}'";

                using (var reader = tableCmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        todo.Id = reader.GetInt32(0);
                        todo.Name = reader.GetString(1);
                    }
                    else
                    {
                        return todo;
                    }
                }

                ;
            }
        }

        return todo;
    }
}