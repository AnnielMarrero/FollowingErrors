using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using FollowingErrors;
using FollowingErrors.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using FollowingErrors.Data;
using FollowingErrors.Dtos;
using FollowingErrors.Mapper.Base;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionStringSection = builder.Configuration.GetSection("ConnectionStrings:BugsManager");
builder.Services.AddDbContext<BugsManager>(options =>
{
    options.UseSqlServer(connectionStringSection.Value);
});
builder.Services.AddScoped<DataSeeder>();
builder.Services.AddAutoMappers(
               AutoMapperConfiguration.CreateExpression().AddAutoMapper()
           );

//Add services to validation

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddControllers();
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


public static class BugsEndpoints
{

	public static void MapBugsEndpoints (this IEndpointRouteBuilder routes)
    {
        //var group = routes.MapGroup("/api/bugs").WithTags(nameof(Bug));

        //group.MapGet("/", async ([FromServices]BugsManager db) =>
        //{
        //    return await db.Bug.ToListAsync();
        //})
        //.WithName("GetAllBugs")
        //.WithOpenApi();

        //group.MapGet("/{id}", async Task<Results<Ok<Bug>, NotFound>> (int id, BugsManager db) =>
        //{
        //    return await db.Bug.AsNoTracking()
        //        .FirstOrDefaultAsync(model => model.Id == id)
        //        is Bug model
        //            ? TypedResults.Ok(model)
        //            : TypedResults.NotFound();
        //})
        //.WithName("GetBugById")
        //.WithOpenApi();

        //group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Bug bug, BugsManager db) =>
        //{
        //    var affected = await db.Bug
        //        .Where(model => model.Id == id)
        //        .ExecuteUpdateAsync(setters => setters
        //          .SetProperty(m => m.Description, bug.Description)
        //          .SetProperty(m => m.CreationDate, bug.CreationDate)
        //          .SetProperty(m => m.UserId, bug.UserId)
        //          .SetProperty(m => m.ProjectId, bug.ProjectId)
        //          .SetProperty(m => m.Id, bug.Id)
        //          );
        //    return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        //})
        //.WithName("UpdateBug")
        //.WithOpenApi();

        //group.MapPost("/", async ( [FromBody] AddBugDto bugDto, [FromServices] BugsManager db) =>
        //{
        //    db.Bug.Add(bug);
        //    await db.SaveChangesAsync();
        //    return TypedResults.Created($"/api/bugs/{bug.Id}", bug);
        //})
        //.WithName("CreateBug")
        //.WithOpenApi();

        //group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, BugsManager db) =>
        //{
        //    var affected = await db.Bug
        //        .Where(model => model.Id == id)
        //        .ExecuteDeleteAsync();
        //    return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        //})
        //.WithName("DeleteBug")
        //.WithOpenApi();
    }
}