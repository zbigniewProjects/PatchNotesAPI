using DestructorsNetApi.Data;
using DestructorsNetApi.Endpoints;
using DestructorsNetApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PatchNotesDbContext>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWebsiteToFetch",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "http://localhost:5173") // your Next.js dev URL
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors("AllowWebsiteToFetch");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();


app.UseMiddleware<ApiKeyMiddleware>();
app.MapPatchNotesEndpoints();

app.Run();
