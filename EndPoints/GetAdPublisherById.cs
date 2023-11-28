using Carter;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Example.Models.Ads;

namespace MongoDB.Example.EndPoints
{
    public class GetAdPublisherById :ICarterModule
    {
        private readonly IMongoDatabase _database;
        public GetAdPublisherById(IMongoDatabase database)
        {
            _database = database;
        }
        public record GetAdPublisherResultModel(string PublisherId,string PublishName);
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/GetAdPublisher/{publisherId}", async (string publisherId) =>
            {
                var collection=_database.GetCollection<AdPublisher>(AdPublisher.CollectionName);
                if (!ObjectId.TryParse(publisherId, out var objectId))
                    return Results.BadRequest();
             
                var filter = Builders<AdPublisher>.Filter.Eq(c => c.Id, objectId);
                var adPublisher = await collection.Find(filter).FirstOrDefaultAsync();
                if (adPublisher is null)
                    return Results.NotFound();
                return Results.Ok(new GetAdPublisherResultModel(adPublisher.PublisherName,
                    adPublisher.Id.ToString()));
            });
        }
    }
}
