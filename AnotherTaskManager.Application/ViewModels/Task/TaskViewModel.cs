using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AnotherTaskManager.App.Models.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace AnotherTaskManager.App.ViewModels.Task
{
    public class TaskViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name must be between 3 and 50 characters", MinimumLength = 3)]
        public string Name { get; set; }

        public string StatusDescription { get; set; }

        public int StatusId { get; set; }

        [BindProperty]
        public int StatusChanges { get; set; }

        public List<TaskStatus> TaskStatuses { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }
    }
}
