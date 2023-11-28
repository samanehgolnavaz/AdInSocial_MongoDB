using MongoDB.Bson;

namespace MongoDB.Example.Models.Ads
{
    public class AdPublisher
    {
        public string PublisherName { get; set; }
        public required ObjectId Id { get; set; }
    }
}
