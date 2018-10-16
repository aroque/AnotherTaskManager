using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using AnotherTaskManager.Api.ServiceModels;
using AnotherTaskManager.App.Models.DataModels;
using AnotherTaskManager.App.Models.Mappers;
using AnotherTaskManager.App.Models.Repositories;
using AnotherTaskManager.App.ViewModels.Task;
using Microsoft.AspNetCore.Mvc;

namespace AnotherTaskManager.App.Controllers
{
    public class TaskController : Controller
    {
        static readonly string baseUrl = "http://atmapi.azurewebsites.net";
        static readonly string apiUrl = "api/Validator?name={0}";

        TaskRepository taskRepository;

        public TaskController(){
            taskRepository = new TaskRepository();
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            IEnumerable<Task> tasks = taskRepository.GetTasks();
            return View(tasks);
        }

        // GET: /Task/Create
        public IActionResult Create(){
            TaskViewModel viewModel = new TaskViewModel
            {
                TaskStatuses = taskRepository.GetTaskStatuses()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ActionName("Create")]
        public async System.Threading.Tasks.Task<IActionResult> CreatePost(TaskViewModel vm){

            ValidatorResponse result = null;

            if (ModelState.IsValid)
            {
                Task task = TaskMapper.ConvertToModel(vm);
                using (HttpClient client = new HttpClient()){
                    client.BaseAddress = new Uri(baseUrl);
                    var response = await client.GetAsync(string.Format(apiUrl, task.Name));

                    if(response.IsSuccessStatusCode){
                        result = await response.Content.ReadAsAsync<ValidatorResponse>();
                        if(result.Valid)
                            if (taskRepository.CreateTask(task))
                                return RedirectToAction("Index");
                    }

                }
            }

            ModelState.AddModelError(
                result.ParameterName, 
                !string.IsNullOrEmpty(result.ErrorMessage) ? result.ErrorMessage : "Unknown Error"
            );

            return View(vm);
        }

        public IActionResult Edit(int Id){
            Task task = taskRepository.GetTask(Id);

            TaskViewModel viewModel = TaskMapper.ConvertToViewModel(task);
            viewModel.TaskStatuses = taskRepository.GetTaskStatuses();

            return View(viewModel);
        }

        [HttpPost]
        [ActionName("Edit")]
        public async System.Threading.Tasks.Task<IActionResult> EditPost(TaskViewModel vm)
        {
            ValidatorResponse result = null;
            string errorMessage = null;

            if (ModelState.IsValid){
                Task task = TaskMapper.ConvertToModel(vm);
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUrl);
                        var response = await client.GetAsync(string.Format(apiUrl, task.Name));

                        if (response.IsSuccessStatusCode)
                        {
                            result = await response.Content.ReadAsAsync<ValidatorResponse>();
                            if (result.Valid)
                                if (taskRepository.EditTask(task))
                                    return RedirectToAction("Index");
                        }

                    }

                }
                catch(Exception ex){
                    errorMessage = ex.Message;
                }
            }

            ModelState.AddModelError(
                result.ParameterName,
                !string.IsNullOrEmpty(errorMessage) ? errorMessage 
                    : !string.IsNullOrEmpty(result.ErrorMessage) ? result.ErrorMessage : "Unknown Error"
            );

            vm.TaskStatuses = taskRepository.GetTaskStatuses();
            return View(vm);
        }

        public IActionResult Details(int Id)
        {
            Task task = taskRepository.GetTask(Id);

            TaskViewModel viewModel = new TaskViewModel
            {
                Name = task.Name,
                StatusId = task.Status,
                DateCreated = task.DateCreated,
                DateModified = task.DateModified,
                TaskStatuses = taskRepository.GetTaskStatuses()
            };

            return View(viewModel);
        }
    }
}
