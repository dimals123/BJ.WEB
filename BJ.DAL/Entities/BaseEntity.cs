using System;

namespace BJ.DAL.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreationAT { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreationAT = DateTime.UtcNow;
        }
    }
}
