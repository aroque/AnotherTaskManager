using System;
using System.Collections.Generic;
using System.Linq;
using AnotherTaskManager.App.Models.DataModels;
using AnotherTaskManager.App.Models.Enumerators;
using AnotherTaskManager.App.ViewModels.Task;
using Microsoft.EntityFrameworkCore;

namespace AnotherTaskManager.App.Models.Repositories
{
    public class TaskRepository
    {
        readonly ATMContext _atmContext;

        public TaskRepository(){
            _atmContext = new ATMContext();
        }

        public TaskRepository (ATMContext context){
            _atmContext = context;
        }
        public List<TaskStatus> GetTaskStatuses(){
            List<TaskStatus> statuses;
            statuses = _atmContext.TaskStatus.ToList();

            return statuses;
        }

        public Task GetTask(int taskId){
            return _atmContext.Task.Include(t => t.StatusNavigation).SingleOrDefault(t => t.Id == taskId);
        }

        public bool CreateTask(Task task){
            task.Status = (int)TaskStatusEnumeration.OPEN;
            task.DateCreated = DateTime.Now;
            task.DateModified = DateTime.Now;

            _atmContext.Task.Add(task);
            return _atmContext.SaveChanges() > 0;
        }

        public bool EditTask(Task task)
        {

            Task oldTask = _atmContext.Task.SingleOrDefault(t => t.Id == task.Id);

            if (task.StatusChanges != oldTask.StatusChanges)
                throw new Exception("The task was changed by another process");

            task.DateModified = DateTime.Now;

            foreach (var fromProp in typeof(Task).GetProperties())
            {
                var toProp = typeof(Task).GetProperty(fromProp.Name);
                var toValue = toProp.GetValue(task, null);
                if (toValue != null)
                {
                    fromProp.SetValue(oldTask, toValue, null);
                }
            }
            return _atmContext.SaveChanges() > 0;

        }

        public List<Task> GetTasks(){

            return _atmContext.Task.Include(t => t.StatusNavigation).ToList();
        }

        public List<Task> GetOpenTasks(){

            return _atmContext.Task.Where(t => t.Status == (int)TaskStatusEnumeration.OPEN).Include(t => t.StatusNavigation).ToList();
        }

        public bool CheckIfNameExists(string name){

            bool result = _atmContext.Task.Any(t => t.Name == name && t.Status == (int)TaskStatusEnumeration.OPEN);

            return result;

        }
    }
}
