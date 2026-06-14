using Microsoft.EntityFrameworkCore;
using WebApplication5.Services;
using WebApplication5.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.PropertyNamingPolicy = null;
    o.JsonSerializerOptions.DictionaryKeyPolicy = null; 
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StudentContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<IStudentService, StudentService>();

var app = builder.Build();

// Swagger всегда включён (нужно для DAST)
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();