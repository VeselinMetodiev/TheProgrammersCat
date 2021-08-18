using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using Dapper.Contrib.Extensions;
using TodoList.ViewModels;
using TodoList.Helpers;

namespace TodoList.Controllers
{
    public class ToDoListController : Controller
    {
        public IActionResult Index()
        {
            TodoListViewModel viewModel = new TodoListViewModel();
            return View("Index", viewModel);
        }

        public IActionResult Edit(int id)
        {
            TodoListViewModel viewModel = new TodoListViewModel();
            viewModel.EditableItem = viewModel.TodoItems.FirstOrDefault(x => x.Id == id);
            return View("Index", viewModel);
        }

        public IActionResult Delete(int id)
        {
            using (var db = DbConnector.GetConnection())
            {
                TodoListItem item = db.Get<TodoListItem>(id);
                if (item != null)
                    db.Delete(item);
                return RedirectToAction("Index");
            }
        }

        public IActionResult CreateUpdate(TodoListViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var db = DbConnector.GetConnection())
                {
                    if (viewModel.EditableItem.Id <= 0)
                    {
                        viewModel.EditableItem.AddDate = DateTime.Now;
                        db.Insert<TodoListItem>(viewModel.EditableItem);
                    }
                    else
                    {
                        TodoListItem dbItem = db.Get<TodoListItem>(viewModel.EditableItem.Id);
                        TryUpdateModelAsync<TodoListItem>(dbItem, "EditableItem");
                        db.Update<TodoListItem>(dbItem);
                    }
                }
                return RedirectToAction("Index");
            }
            else
                return View("Index", new TodoListViewModel());
        }

        public IActionResult ToggleIsDone(int id)
        {
            using (var db = DbConnector.GetConnection())
            {
                TodoListItem item = db.Get<TodoListItem>(id);
                if (item != null)
                {
                    item.IsDone = !item.IsDone;
                    db.Update<TodoListItem>(item);
                }
                return RedirectToAction("Index");
            }
        }
    }
}