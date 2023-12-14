using FluentValidation;
using FluentValidation.AspNetCore;
using FollowingErrors.Data;
using FollowingErrors.Mapper.Base;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionStringSection = builder.Configuration.GetSection("ConnectionStrings:BugsManager");
builder.Services.AddDbContext<BugsManager>(options =>
{
    options.UseSqlServer(connectionStringSection.Value);
});
builder.Services.AddScoped<DataSeeder>();
builder.Services.AddAutoMappers(AutoMapperConfiguration.CreateExpression().AddAutoMapper());

//Add services to validation

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddControllers();
builder.Services.AddCors();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(
               x =>
                   x.AllowAnyMethod()
                       .AllowAnyHeader()
                       .SetIsOriginAllowed(origin => true) // allow any origin
                       .WithExposedHeaders( "Location")
           );

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

//handle migrations
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<BugsManager>();
        context.Database.Migrate(); // apply all migrations

        var dataSeeder = services.GetRequiredService<DataSeeder>();
        dataSeeder.SeedData(); // seed the db
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the DB.");
    }
}

app.Run();
