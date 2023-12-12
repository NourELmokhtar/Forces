using Forces.Application.Enums;
using Forces.Application.Interfaces.Common;
using Forces.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forces.Application.Models
{

    public class House : AuditableEntity<int>

    { 
       public string HouseName { get; set; }
        public string HouseCode { get; set; }
        public PersonRank? Rank { get; set; }

        public int BaseId { get; set; }
        [ForeignKey("BaseId")]
        public virtual Bases Base { get; set; }
        public virtual ICollection<Person> Persons { get; set; }

    }

}

