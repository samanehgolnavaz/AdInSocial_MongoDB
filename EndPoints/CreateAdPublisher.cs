using Carter;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Example.Models.Ads;

namespace MongoDB.Example.EndPoints
{
    public class CreateAdPublisher : ICarterModule
    {
        private readonly IMongoDatabase _mongoDatabase;
        public record CreateAdPublisherModel(string PublishName);
        public CreateAdPublisher(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/CreatePublisher", async (CreateAdPublisherModel model) =>
            {
                var collection = _mongoDatabase.GetCollection<AdPublisher>(AdPublisher.CollectionName);
                var adPublisher = new AdPublisher()
                {
                    PublisherName = model.PublishName, Id = ObjectId.GenerateNewId()
                };
                await collection.InsertOneAsync(adPublisher);
                return Results.Created($"/GetAdPublisher/{adPublisher.Id.ToString()}",adPublisher);

            });
        }
    }
}
