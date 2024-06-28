using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTodoApp.Entities;
using NHibernate.Linq;
using MvcTodoApp.ViewModels;

namespace MvcTodoApp.Controllers
{
    public class TaskController : Controller
    {
        //
        // GET: /Task/

        public ActionResult Index()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var tasks = session.Query<Task>().ToList();
                var viewModel = new TaskViewModel
                {
                    Tasks = tasks,
                    TotalTasks = tasks.Count
                };
                return View(viewModel);
            }
        }

        [HttpPost]
        public JsonResult AddTask(string description)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    var task = new Task { Description = description };
                    session.Save(task);
                    transaction.Commit();
                    return Json(task);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }


        [HttpPost]
        public JsonResult RemoveTask(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    var task = session.Get<Task>(id);
                    if (task != null)
                    {
                        session.Delete(task);
                        transaction.Commit();
                        return Json(new { success = true });
                    }
                    return Json(new { success = false, message = "Task not found" });
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }
    }
}
