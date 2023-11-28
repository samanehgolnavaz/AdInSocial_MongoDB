using Carter;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCarter();
app.MapGet("/", () => "Hello World!");

app.UseSwagger();
app.UseSwaggerUI();
app.MapCarter(); 
app.Run();
