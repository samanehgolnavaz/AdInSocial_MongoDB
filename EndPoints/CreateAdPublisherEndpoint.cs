using Carter;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Example.Models.Ads;

namespace MongoDB.Example.EndPoints
{
    public class CreateAdPublisherEndpoint : ICarterModule
    {
        private readonly IMongoDatabase _mongoDatabase;
        public record CreateAdPublisherModel(string PublishName,Guid PublisherId);
        public CreateAdPublisherEndpoint(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/CreatePublisher", async (CreateAdPublisherModel model) =>
            {
                var collection = _mongoDatabase.GetCollection<AdPublisher>("AdPublisherCollection");
                var adPublisher = new AdPublisher()
                {
                    PublisherName=model.PublishName,Id=ObjectId.Parse(model.PublisherId.ToString())
                };
                await collection.InsertOneAsync(adPublisher);
                return Results.Created($"/GetAdPublisher/{adPublisher.Id.ToString()}",adPublisher);

            });
        }
    }
}
