using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmProduceManagement.Models.Entities
{
    public class Category : BaseEntity
    {
        public string NameOfCategory {get;set;}
        public string DescriptionOfCategory {get;set;}
        public ICollection<Produce> Produces {get;set;} = new HashSet<Produce>();
    }
}