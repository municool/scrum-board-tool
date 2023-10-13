using Microsoft.EntityFrameworkCore;
using scrum_board_tool.Server.Model;
using System.Text.Json.Serialization;
using Pomelo.EntityFrameworkCore.MySql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddRazorPages();

builder.Services.AddDbContextFactory<ScrumBoardDbContext>(
        options =>
            options.UseMySql(builder.Configuration.GetConnectionString("scrumboardDb"), new MySqlServerVersion(new Version(8, 1, 0)))
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

using (var db = app.Services.GetRequiredService<IDbContextFactory<ScrumBoardDbContext>>().CreateDbContext())
{
    db.Database.Migrate();
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
