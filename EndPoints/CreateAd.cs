using Carter;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Example.Models.Ads;

namespace MongoDB.Example.EndPoints
{
    public class CreateAd : ICarterModule
    {
        private readonly IMongoDatabase _mongoDatabase;
        public CreateAd(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase= mongoDatabase;
        }
        public record CreateAdModel(string Name,string Description,string PublisherId,List<string> Categories);
        public record CreateAdModelResult(string CreatedAdId);
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/CreateAd", async (CreateAdModel model) =>
            {
                if (!ObjectId.TryParse(model.PublisherId, out var publisherId))
                    return Results.BadRequest("invalid Publisher Id");
                var categoryIds = new List<ObjectId>();
                foreach (var modelCategory in model.Categories)
                {
                    if (!ObjectId.TryParse(modelCategory, out var categoryId))
                        return Results.BadRequest("Invalid Category Id");
                    categoryIds.Add(categoryId);
                }
                var adCategoryCollection = _mongoDatabase.GetCollection<AdCategory>(AdCategory.CollectionName);
                var adFilter = Builders<AdCategory>.Filter.In(category => category.Id, categoryIds);
                var adCategories = await adCategoryCollection.Find(adFilter).ToListAsync();
                if (adCategories is null)
                    return Results.NotFound("No Ad Categories Found");
                var ad = new AdModel() { AdPublisher = publisherId };
                ad.SetAdName(model.Name);
                ad.SetAdDescription(model.Description);
                ad.SetAdCategories(adCategories);
                var adCollection=_mongoDatabase.GetCollection<AdModel>(AdModel.CollectionName);
                await adCollection.InsertOneAsync(ad);
                return Results.Created($"/GetAdById/{ad.AdId}", new CreateAdModelResult(ad.AdId.ToString()));

               
            });
        }
    }
}
