using System;
using System.Collections.Generic;
using WorkerMan.CrossCutting.Entities.Core;
using WorkerMan.CrossCutting.Entities.Identity;
using WorkerMan.CrossCutting.Entities.Interfaces;

namespace WorkerMan.CrossCutting.Entities.Models
{
    public class WorkerCompany : BaseEntity, IEntity
    {
        public WorkerCompany()
        {
            StaffMembers = new HashSet<WorkerManUser>();
        }
        public bool CompanyVerified { get; set; }
        public string OfficialName { get; set; }
        public string CountryOfOperation { get; set; }
        public string OfficialEmail { get; set; }
        public string OfficialPhone { get; set; }
        public string OfficalLogo { get; set; }
        public bool Registered { get; set; }
        public string OfficialWebsite { get; set; }
        public string OfficalOfficeAddress { get; set; }
        public string EstimatedStaffStrength { get; set; }
        public Guid CreatedById { get; set; }
        public ICollection<WorkerManUser> StaffMembers { get; set; }

    }
}
