using System;
using System.Collections.Generic;
using System.Text;

namespace HRSystem.DAL.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
