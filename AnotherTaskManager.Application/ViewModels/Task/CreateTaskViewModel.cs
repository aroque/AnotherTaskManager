using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AnotherTaskManager.App.Models.DataModels;

namespace AnotherTaskManager.App.ViewModels.Task
{
    public class CreateViewModel
    {
        [Required]
        [Display(Name = "Task Name")]
        public string Name { get; set; }

        public List<TaskStatus> TaskStatuses { get; set; }

        [Required]
        public int Status { get; set; }
    }
}
