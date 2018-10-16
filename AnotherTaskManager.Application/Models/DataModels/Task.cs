using System;
using System.Collections.Generic;

namespace AnotherTaskManager.App.Models.DataModels
{
    public partial class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public int StatusChanges { get; set; }

        public TaskStatus StatusNavigation { get; set; }
    }
}
