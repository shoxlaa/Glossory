using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glossory_MayBeFinal_.Models
{

    [BsonIgnoreExtraElements]
    public class Product
    {
        [BsonElement("_id")]
        public ObjectId ProductId { get; set; }

        [BsonElement("product_name")]
        public string? ProductName { get; set; } 

        [BsonElement("coast")]
        public float Coast { get; set; } 
        [BsonElement("product_amount")]
        public int ProductAmount { get; set; }
        [BsonElement("category")]
        public string? Category { get; set; } 

        [BsonElement("description")]
        public string? Description { get; set; }

        public override string ToString()
        {
            return $"{ProductName} : {Coast} x{ProductAmount}";
        }
    }

}
