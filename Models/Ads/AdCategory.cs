using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace MongoDB.Example.Models.Ads
{
    public class AdCategory
    {

        [NotMapped]
        [BsonIgnore]
        public const string CollectionName = "AdCategories";
        public ObjectId Id { get; set; }
        public string CategoryName { get; set; }    
    }
}
