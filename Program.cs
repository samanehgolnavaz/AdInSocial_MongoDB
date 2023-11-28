using Carter;
using MongoDB.Example.Extensions;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCarter();
builder.Services.AddMongoDb(builder.Configuration["MongoDbConfig:ConnectionString"]!,
    builder.Configuration["MongoDbConfig:DatabaseName"]!);
app.MapGet("/", () => "Hello World!");

app.UseSwagger();
app.UseSwaggerUI();
app.MapCarter(); 
app.Run();
