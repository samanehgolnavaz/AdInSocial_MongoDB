using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MongoDB.Example.Models.Ads
{
    public class AdPublisher
    {

        [NotMapped]
        [BsonIgnore]
        public const string CollectionName = "AdPublisherCollection";
        
        public string PublisherName { get; set; }
        public required ObjectId Id { get; set; }
    }
}
