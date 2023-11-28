using Carter;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Example.Models.Ads;

namespace MongoDB.Example.EndPoints
{
    public class CreateAdCategory : ICarterModule
    {
        private readonly IMongoDatabase _mongoDatabase;
        public CreateAdCategory(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }
        public record CreateAdCategoryResultModel(string CategoryName,string CategoryId);
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/CreateAdCategory", async (CreateAdCategoryResultModel model) =>
            {
                var collection = _mongoDatabase.GetCollection<AdCategory>(AdCategory.CollectionName);
                var adCategory = new AdCategory() {CategoryName=model.CategoryName,Id=ObjectId.GenerateNewId() };
                await collection.InsertOneAsync(adCategory);
                return Results.Created($"/GetCategory/{adCategory.Id.ToString()}",
                    new CreateAdCategoryResultModel(adCategory.CategoryName,adCategory.Id.ToString()));
            });
        }
    }
}
