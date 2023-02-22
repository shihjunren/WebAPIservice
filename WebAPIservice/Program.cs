using Microsoft.EntityFrameworkCore;
using WebAPIservice.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container. (DI....)
string NorthwindConnectionString = builder.Configuration.GetConnectionString("Northwind");
builder.Services.AddDbContext<NorthwindContext>(options =>
{
    options.UseSqlServer(connectionString: NorthwindConnectionString);
});


builder.Services.AddControllers();
var MyAllowSpecificOrigins = "AllowAny";
builder.Services.AddCors(options =>{
    options.AddPolicy(
        name: MyAllowSpecificOrigins,
        policy => policy.WithOrigins("https://localhost:7029").WithHeaders("*").WithMethods("*"));
    
} 
    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
