using MyEventApp.Api.Services;
using MyEventApp.Core.Services;
using MyEventApp.Data;
using MyEventApp.Data.Repositories;
using NHibernate;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<NHibernateHelper>();

var conn = builder.Configuration.GetConnectionString("SQLite");
var nhHelper = builder.Services.BuildServiceProvider().GetRequiredService<NHibernateHelper>();
var sf = nhHelper.CreateSessionFactory();
builder.Services.AddSingleton(sf);
builder.Services.AddScoped(sp => sp.GetRequiredService<ISessionFactory>().OpenSession());

// Repositories
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<ITicketSaleRepository, TicketSaleRepository>();
// Services
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<ITicketService, TicketService>();

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://localhost:4200/")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();


// Use CORS
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
