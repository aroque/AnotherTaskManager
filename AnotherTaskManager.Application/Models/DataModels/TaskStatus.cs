using System;
using System.Collections.Generic;

namespace AnotherTaskManager.App.Models.DataModels
{
    public partial class TaskStatus
    {
        public TaskStatus()
        {
            Task = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public ICollection<Task> Task { get; set; }
    }
}
