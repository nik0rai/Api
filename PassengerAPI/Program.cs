using Microsoft.EntityFrameworkCore;
using HotelBookingAPI.Data;
using Microsoft.AspNetCore.Mvc.Formatters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.OutputFormatters.RemoveType(typeof(SystemTextJsonOutputFormatter));
    options.ReturnHttpNotAcceptable = true;
}).AddXmlSerializerFormatters();

builder.Services.AddDbContext<ApiContext>
    (opt => opt.UseInMemoryDatabase("PassengersDB"));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
#if DEBUG
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#endif

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


// /curl -X 'POST' \
//  'https://localhost:7106/api/Passenger/CreateRandom' \
//  -H 'accept: */*' \
//  -d ''
