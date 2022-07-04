using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glossory_MayBeFinal_.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public float Coast { get; set; }
        public int ProductAmount { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public override string ToString()
        {
            return $"{ProductName} : {Coast} x{ProductAmount}";
        }
    }

}
