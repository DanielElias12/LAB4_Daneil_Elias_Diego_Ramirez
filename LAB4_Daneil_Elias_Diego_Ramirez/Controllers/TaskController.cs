using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LAB4_Daneil_Elias_Diego_Ramirez.Models.Data;
namespace LAB4_Daneil_Elias_Diego_Ramirez.Controllers
{

    public class TaskController : Controller
    {
        public static string currentDeveloper;
        // GET: TaskController
        public ActionResult Index()
        {
            return View();
        }


        // GET: TaskController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult CreateDeveloper()
        {
            return View();
        }

        // POST: TaskController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDeveloper(IFormCollection collection)
        {
            try
            {
                var newDeveloper = new Models.Data.Developer();
                {
                    newDeveloper.User = collection["User"];
                    newDeveloper.Password = collection["Password"];
                };

                Singleton.Instance.DevelopersList.Add(newDeveloper);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return Index();
            }
        }

        public ActionResult LoginDeveloper()
        {
            return View();
        }

        // POST: TaskController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginDeveloper(IFormCollection collection)
        {

            bool success = false;
                var logged = new Models.Data.Developer();
                {
                    logged.User = collection["User"];
                    logged.Password = collection["Password"];
                };

                for (int i = 0; i< Singleton.Instance.DevelopersList.Count; i++)
                {
                    if (Singleton.Instance.DevelopersList.ElementAt(i).User == logged.User)
                    {
                        if (Singleton.Instance.DevelopersList.ElementAt(i).Password == logged.Password)
                        {

                            success = true;
                            currentDeveloper = logged.User;
                        }
                    }
                }

            if (success)
            {
                return RedirectToAction(nameof(DeveloperTasks)); ;
            }
            else
            {
                return View();
            }
            
           
        }

        public ActionResult DeveloperTasks()
        {
            return View();
        }
        public ActionResult ViewAllTasks()
        {
            return View(Singleton.Instance.VisibleTasks);
        }
        public ActionResult GetCurrentTask()
        {
            return View();
        }
        // GET: TaskController/Create
        public ActionResult CreateTask()
        {
            return View();
        }

        // POST: TaskController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTask(IFormCollection collection)
        {
            try
            {
                var Task = new Models.Data.Task();
                var Task2 = new Models.Data.Task();
                {
                    Task.Developer = currentDeveloper;
                    Task.Title = collection["Title"];
                    Task.Description = collection["Description"];
                    Task.Project = collection["Project"];
                    Task.Priority = Convert.ToInt32(collection["Priority"]);
                    Task.Date = collection["Date"];
                    Task2.Title = Task.Title;
                    Task.Developer = currentDeveloper;
                  
                };

                Singleton.Instance.TaskHashtable.Add(Task.Title, Task);
                Singleton.Instance.TaskIndex.insert(Task2, Task.Priority);
                //ya (: 

                return RedirectToAction(nameof(DeveloperTasks));
            }
            catch
            {
                return View();
            }
        }

        //MANAGER METHODS

        public ActionResult ManagerView()
        {
            return View(Singleton.Instance.DevelopersList);
        }




















        // GET: TaskController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TaskController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TaskController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TaskController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
