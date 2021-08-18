using System.Collections.Generic;
using System.Linq;
using TodoList.Models;
using Dapper;
using TodoList.Helpers;

namespace TodoList.ViewModels
{
    public class TodoListViewModel
    {
        public TodoListViewModel()
        {
            using (var db = DbConnector.GetConnection())
            {
                this.EditableItem = new TodoListItem();
                this.TodoItems = db.Query<TodoListItem>("SELECT * FROM TodoListItems ORDER BY IsDone ASC, addDate DESC").ToList();
            }
        }

        public List<TodoListItem> TodoItems { get; set; }

        public TodoListItem EditableItem { get; set; }
    }
}