using MongoDB.Driver;

namespace MongoDB.Example.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services,
            string connectionString,string databaseName)
        {
            var mongoClinet=new MongoClient(connectionString);
            var mongoDatabase=mongoClinet.GetDatabase(databaseName);
            services.AddSingleton(mongoDatabase);
            return services;
        }
    }
}
