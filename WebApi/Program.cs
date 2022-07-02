using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// MY CODE - for returning "Http status code 406 Not Acceptable" if Accept Header is not application/json
// MY CODE - for supporting Accept Header applicaiton/xml request 
// builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true).AddXmlSerializerFormatters();
builder.Services.AddControllers().AddXmlSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// MY CODE
builder.Services.AddDbContext<MovieStoreDbContext>(options =>options.UseInMemoryDatabase("MovieStoreDb"));
builder.Services.AddScoped<IMovieStoreDbContext>(provider => provider.GetService<MovieStoreDbContext>());
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
// MY CODE - for preventing JSON Loop error "A possible object cycle was detected which is not supported." in Eager Loading for Many-To-Many relationships 
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


//-------------------------------------------------------------------------------------
var app = builder.Build();

// MY CODE
using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DataGenerator.Initialize(services);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// MY CODE
app.UseCustomExceptionMiddleware();

app.MapControllers();

app.Run();
