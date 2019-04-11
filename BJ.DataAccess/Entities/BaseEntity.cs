using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJ.DataAccess.Entities
{
    public class BaseEntity
    {
        [ExplicitKey]
        public Guid Id { get; set; }

        public DateTime CreationAt { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreationAt = DateTime.UtcNow;
        }
    }
}
