using System;
using WorkerMan.CrossCutting.Entities.Core;

namespace WorkerMan.CrossCutting.Entities.Models
{
    public class WorkScreenShot : BaseEntity
    {
        public string ImagePath { get; set; }
        public Guid WorkDoneId { get; set; }
        public WorkDone WorkDone { get; set; }
    }
}
