using NetXAngularPG.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetXAngularPG.Domain.Entities
{
    public class Order: BaseEntity
    {
        public string Description { get; set; }
        public DateTime DeliveryDateTime { get; set; }


        public ICollection<Product> Products { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
