using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using CsvHelper;
using Microsoft.VisualBasic.FileIO;
using LAB4_Daneil_Elias_Diego_Ramirez.Models.Data;

namespace LAB4_Daneil_Elias_Diego_Ramirez.Controllers
{

    public class TaskController : Controller
    {
        public static string currentDeveloper;
        public static string csvdevelopers = "";
        public static string csvTasksHash = "";
        public static bool firstread;
        public static bool refreshAllTasks = true;

        
        public void ReadDevelopersList()
        {
            try
            {
                    string[] lines = System.IO.File.ReadAllLines($"{hostingEnvironment.WebRootPath}\\csv\\developers.csv");
                    TextReader reader = new StreamReader($"{hostingEnvironment.WebRootPath}\\csv\\developers.csv");
                    TextFieldParser csvReader = new TextFieldParser(reader);
                    csvReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
                    csvReader.SetDelimiters(",");
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] fields;
                    
                while (!csvReader.EndOfData)
                {
                    try
                    {
                        fields = csvReader.ReadFields();
                        var newDev = new Developer();

                        newDev.User = fields[0];
                        newDev.Password = fields[1];
                        csvdevelopers += $"{fields[0]},{fields[1]}\n";
                        Singleton.Instance.DevelopersList.Add(newDev);

                    }
                    catch
                    {

                    }
                }
                reader.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void ReadTasksHashtable()
        {
            try
            {
                string[] lines = System.IO.File.ReadAllLines($"{hostingEnvironment.WebRootPath}\\csv\\tasks.csv");
                TextReader reader = new StreamReader($"{hostingEnvironment.WebRootPath}\\csv\\tasks.csv");
                TextFieldParser csvReader = new TextFieldParser(reader);
                csvReader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
                csvReader.SetDelimiters(",");
                csvReader.HasFieldsEnclosedInQuotes = true;
                string[] fields;
                while (!csvReader.EndOfData)
                {
                    try
                    {
                        fields = csvReader.ReadFields();
                        var newTask = new Models.Data.Task();
                        var node = new Models.Data.PriorityNode<Models.Data.Task>();

                        newTask.Developer = fields[0];
                        newTask.Title = fields[1];
                        newTask.Description = fields[2];
                        newTask.Project = fields[3];
                        newTask.Priority = Convert.ToInt32(fields[4]);
                        newTask.Date = fields[5];
                        csvTasksHash += $"{fields[0]},{fields[1]},{fields[2]},{fields[3]},{fields[4]},{fields[5]}\n";

                        node.Data = newTask;
                        node.prioridad = newTask.Priority;
                        Singleton.Instance.TaskHashtable.Add(newTask.Title, newTask);
                        Singleton.Instance.heap.Add(node);

                    }
                    catch
                    {

                    }
                }
                reader.Close();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        void WriteDevelopersList(Developer dev)
        {
            
            TextWriter writer = new StreamWriter($"{hostingEnvironment.WebRootPath}\\csv\\developers.csv");
            
            csvdevelopers += $"{dev.User},{dev.Password}\n";
            writer.Write(csvdevelopers);
            writer.Close();
        }

        void WriteTasksHashtable(Models.Data.Task task)
        {
            
            TextWriter writer = new StreamWriter($"{hostingEnvironment.WebRootPath}\\csv\\tasks.csv");

            csvTasksHash += $"{task.Developer},{task.Title},{task.Description},{task.Project},{task.Priority},{task.Date}\n";
            writer.Write(csvTasksHash);
            writer.Close();
        }

        IWebHostEnvironment hostingEnvironment;
        public TaskController(IWebHostEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;

        }


        // GET: TaskController
        public ActionResult Index()
        {
            if (firstread == false)
            {
                ReadDevelopersList();
                ReadTasksHashtable();
                firstread = true;
            }
            
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
                WriteDevelopersList(newDeveloper);
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

     
        public ActionResult ViewAllTasks(string dev)
        {
            Singleton.Instance.VisibleTasks.Clear();
            var heap = Singleton.Instance.heap;
            var hashtable = Singleton.Instance.TaskHashtable;


            for (int i = 0; i < heap.elements.Count; i++)
            {
                string key = heap.elements[i].Data.Title;
                var newTask = new Models.Data.Task();
                newTask = hashtable.GetNode(key);
                Singleton.Instance.VisibleTasks.Add(newTask);

            }
            listPriority(Singleton.Instance.VisibleTasks);
            
            var User = from x in Singleton.Instance.VisibleTasks select x;
            User = User.Where(x => x.Developer.Contains(currentDeveloper));

            return View(User);
            
        }
        public ActionResult ViewTaskManager(string dev)
        {
            ViewData["GetDev"] = dev; 
            Singleton.Instance.VisibleTasks.Clear();
            var heap = Singleton.Instance.heap;
            var hashtable = Singleton.Instance.TaskHashtable;

            var User = from x in Singleton.Instance.VisibleTasks select x;
            if (!String.IsNullOrEmpty(dev))
            {
                for (int i = 0; i < heap.elements.Count; i++)
                {
                    string key = heap.elements[i].Data.Title;
                    var newTask = new Models.Data.Task();
                    newTask = hashtable.GetNode(key);
                    Singleton.Instance.VisibleTasks.Add(newTask);

                }
                listPriority(Singleton.Instance.VisibleTasks);


                User = User.Where(x => x.Developer.Contains(dev));
            }
            return View(User);

        }
        public ActionResult GetCurrentTask()
        {
            Singleton.Instance.VisibleTasks.Clear();
            var heap = Singleton.Instance.heap;
            var hashtable = Singleton.Instance.TaskHashtable;
           
            for (int i = 0; i < heap.elements.Count;i++)
            {
               
             
                if (heap.elements[i].Data.Developer == currentDeveloper)
                {
                    string key = heap.elements[i].Data.Title;
                    var newTask = new Models.Data.Task();
                    newTask = hashtable.GetNode(key);
                    Singleton.Instance.VisibleTasks.Add(newTask);
                }
                listPriority(Singleton.Instance.VisibleTasks);
               
            }
            return View(Singleton.Instance.VisibleTasks[0]);
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
                var node = new Models.Data.PriorityNode<Models.Data.Task>();
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
                node.Data = Task;
                node.prioridad = Task.Priority;
                WriteTasksHashtable(Task);
                Singleton.Instance.TaskHashtable.Add(Task.Title, Task);
                Singleton.Instance.TaskIndex.insert(Task2, Task.Priority);
                Singleton.Instance.heap.Add(node);
                refreshAllTasks = true;
              
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

        public ActionResult DeleteTask(string Title)
        {

            Singleton.Instance.VisibleTasks.Clear();
            var heap = Singleton.Instance.heap;
            var hashtable = Singleton.Instance.TaskHashtable;

            for (int i = 0; i < heap.elements.Count; i++)
            {


                if (heap.elements[i].Data.Developer == currentDeveloper)
                {
                    string key = heap.elements[i].Data.Title;
                    var newTask = new Models.Data.Task();
                    newTask = hashtable.GetNode(key);
                    Singleton.Instance.VisibleTasks.Add(newTask);
                }
                listPriority(Singleton.Instance.VisibleTasks);

            }
            return View(Singleton.Instance.VisibleTasks[0]);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTask(string id, IFormCollection collection)
        {
            var heap = Singleton.Instance.heap;
            var hashtable = Singleton.Instance.TaskHashtable;
            var newTask = new Models.Data.Task();
            try
            {
                for (int i = 0; i < heap.elements.Count; i++)
                {

                    if (heap.elements[i].Data.Title == id)
                    {
                        var node = new Models.Data.PriorityNode<Models.Data.Task>();
                        node = heap.elements[i];

                        string key = heap.elements[i].Data.Title;
                        Singleton.Instance.heap.Delete(node);
                        Singleton.Instance.TaskHashtable.Remove(key);


                        break;
                    }
                }
                return RedirectToAction(nameof(DeveloperTasks));
            }
            catch
            {
                return View();
            }
        }
        public void listPriority(List<Models.Data.Task> list)
        {
            Models.Data.Task temp;
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list.Count - 1; j++)
                {
                    if (list[j].Priority > (list[j + 1].Priority))
                    {
                        temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }
        }



    }
}
