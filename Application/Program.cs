using Models;
using Services;
using Configuration;
using System.Runtime.InteropServices;
using DbRepos;

var builder = WebApplication.CreateBuilder(args);

// global cors policy
builder.Services.AddCors();

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Dependency Inject Custom logger
#endregion

#region Dependency Inject
builder.Services.AddScoped<ISeedRepo, csSeedRepo>();
builder.Services.AddScoped<IAttractionRepo, csAttractionRepo>();
builder.Services.AddScoped<ICommentRepo, csCommentRepo>();
builder.Services.AddScoped<IUserRepo, csUserRepo>();

builder.Services.AddScoped<IAttractionService, csAttractionServiceDb>();
builder.Services.AddScoped<ISeedService, csSeedServiceDb>();
builder.Services.AddScoped<ICommentService, csCommentServiceDb>();
builder.Services.AddScoped<IUserService, csUserServiceDb>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// global cors policy - the call to UseCors() must be done here
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials


app.MapControllers();
app.Run();

