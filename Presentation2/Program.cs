using Microsoft.EntityFrameworkCore;
using Presentation2.Data;
using Presentation2.Data.Repositories;
using Presentation2.Interface;
using Presentation2.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var connectionString = Environment.GetEnvironmentVariable("SQLCONNSTR_SqlConnection");
builder.Services.AddDbContext<DataContext>(x =>x.UseSqlServer(connectionString));

builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();

var app = builder.Build();
app.MapOpenApi();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Event Service API");
    c.RoutePrefix = string.Empty;
});
app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
