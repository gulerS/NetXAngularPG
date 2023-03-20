using FluentValidation.AspNetCore;
using NetXAngularPG.Domain.Entities;
using NetXAngularPG.Infrastructure.Filters;
using NetXAngularPG.Persistance;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

// Add services to the container.
builder.Services.AddPersistanceServices();




builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
 .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<Product>())
  .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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