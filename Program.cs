using File_Upload.Data;
using File_Upload.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//DBContext
builder.Services.AddDbContext<SocialDbContext>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
);

builder.Services.AddTransient<IPostRepository, PostRepository>();


const string AllowAllHeadersPolicy = "AllowAllHeadersPolicy";


builder.Services.AddCors(options =>

{
    options.AddPolicy(AllowAllHeadersPolicy,
        builder =>
        {
            builder.WithOrigins("http://localhost:7296")
                    .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});


var app = builder.Build();

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
