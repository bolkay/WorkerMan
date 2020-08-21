using System.Collections;
using System.Collections.Generic;
using WorkerMan.CrossCutting.Entities.Core;
using WorkerMan.CrossCutting.Entities.Identity;

namespace WorkerMan.CrossCutting.Entities.Models
{
    public class WorkDone : BaseEntity
    {
        public WorkDone()
        {
            ScreenShots = new HashSet<WorkScreenShot>();
            WorkVideos = new HashSet<WorkVideo>();
        }

        public ICollection<WorkScreenShot> ScreenShots { get; set; }
        public ICollection<WorkVideo> WorkVideos { get; set; }

        public string WorkerManUserId { get; set; }
        public WorkerManUser WorkerManUser { get; set; }
    }
}
