using Forces.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forces.Application.Models
{
    public class Person : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string NationalNumber { get; set; }
    }
}
