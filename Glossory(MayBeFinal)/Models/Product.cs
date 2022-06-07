using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glossory_MayBeFinal_.Models
{
    public  class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public float Coast { get; set; } // я хз почему но  у менея double работет криво 
        public int ProductAmount { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
    }
}
