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
                    Task.Title = collection["Title"];
                    Task.Description = collection["Description"];
                      Task.Project = collection["Project"];
                    Task.Priority = Convert.ToInt32(collection["Priority"]);
                    Task.Date = collection["Date"];
                    Task2.Title = Task.Title;
                  
                };

                Singleton.Instance.TaskHashtable.Add(Task.Title, Task);
                Singleton.Instance.TaskIndex.insert(Task2, Task.Priority);
                //ya (: 

                return RedirectToAction(nameof(View));
            }
            catch
            {
                return View();
            }
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
