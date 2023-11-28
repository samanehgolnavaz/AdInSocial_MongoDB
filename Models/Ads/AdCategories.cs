using MongoDB.Bson;

namespace MongoDB.Example.Models.Ads
{
    public class AdCategories
    {
        public ObjectId Id { get; set; }
        public string CategoryName { get; set; }    
    }
}
