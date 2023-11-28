using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations.Schema;

namespace MongoDB.Example.Models.Ads
{
    public class AdModel
    {

        [NotMapped]
        [BsonIgnore]
        public const string CollectionName = "AdsCollection";
        public AdModel()
        {
            CurrentAdState = AdState.Created;
            ModifiedDate= DateTime.Now;
            ChangeLog.Add("Ad Created");
            AdId = AdId == ObjectId.Empty ? ObjectId.GenerateNewId() : AdId;
        }

        public ObjectId AdId { get; set; }
        public string Name { get;private set; }
        public string Description { get;private set; }
        public DateTime PublishDate { get;private set; }
        public DateTime ModifiedDate { get;private set; }
        public List<ObjectId> Categories { get;private set; } = new();
        public AdState  CurrentAdState { get;private set; }
        public List<string> ChangeLog { get; private set; } = new();
        public required ObjectId AdPublisher { get; set; }
        public enum AdState
        {
            Created,
            Published,
            Disabled
        }
        public void SetAdName(string name)
        {
            ArgumentNullException.ThrowIfNull(name);
            Name = name;
            ModifiedDate = DateTime.Now;
            ChangeLog.Add("Ad Name Set Done");
        }
        public void SetAdDescription(string description)
        {
            ArgumentNullException.ThrowIfNull(description);
            Description=description;
            ModifiedDate = DateTime.Now;
            ChangeLog.Add("Ad Decsription Set Done");


        }
        public void SetAdCategories(List<AdCategory> categories)
        {
            Categories.AddRange(categories.Select(a => a.Id));
            ModifiedDate = DateTime.Now;
            ChangeLog.Add("Ad Categories Set");
        }

        public void PublisheAdd()
        {
            CurrentAdState= AdState.Published;
            PublishDate= DateTime.Now;
            ModifiedDate = DateTime.Now;
            ChangeLog.Add("Ad State Changed to Publish");
        }
    }
}
