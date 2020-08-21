using System;
using WorkerMan.CrossCutting.Entities.Core;

namespace WorkerMan.CrossCutting.Entities.Models
{
    public class WorkVideo : BaseEntity
    {
        public string VideoPath { get; set; }
        public Guid WorkDoneId { get; set; }
        public WorkDone WorkDone { get; set; }
    }
}
