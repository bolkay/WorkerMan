using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using WorkerMan.CrossCutting.Entities.Interfaces;
using WorkerMan.CrossCutting.Entities.Models;
using WorkerMan.CrossCutting.Enums;

namespace WorkerMan.CrossCutting.Entities.Identity
{
    public class WorkerManUser : IdentityUser<string>, IEntity
    {
        public WorkerManUser()
        {
            WorksDone = new HashSet<WorkDone>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int Age => DateOfBirth != default ? ((int)(DateTime.Now - DateOfBirth).TotalDays / 365) : 0;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public string HomeAddress { get; set; }
        public string OfficeAddress { get; set; }
        public string Country { get; set; }
        public string ProfilePicture { get; set; }
        public AccountType AccountType { get; set; }
        public Guid? AffiliatedToId { get; set; }
        public WorkerCompany AffiliatedTo { get; set; }
        public long TotalHoursWorked { get; set; }
        public ICollection<WorkDone> WorksDone { get; set; }

    }
}
