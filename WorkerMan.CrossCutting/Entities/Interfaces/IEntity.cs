using System;

namespace WorkerMan.CrossCutting.Entities.Interfaces
{
    public interface IEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
