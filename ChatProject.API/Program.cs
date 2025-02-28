using ChatProject.API.Hubs;
using ChatProject.API.Mappings;
using ChatProject.Domain.Entities;
using ChatProject.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MediatR;
using System.Reflection;
using ChatProject.Application.ChatRooms.Commands;
using ChatProject.API.Services;
using ChatProject.Application.Interfaces;
 
using ChatProject.Infrastructure.Services;
using ChatProject.Domain.Common.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders() ;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

 


// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // output: "JwtBearer"
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // output: "JwtBearer"
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Set to true in production!
    options.SaveToken = true;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };

    // Add this to extract the token from the query string for SignalR
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chathub"))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});

// Register AutoMapper and scan for profiles in the current assembly
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add SignalR service
builder.Services.AddSignalR();

// CORS Configuration
var allowedOrigins = "_allowedOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowedOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "https://localhost:7246") // Allow Next.js frontend
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
                 
        });
});


//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateChatRoomCommandHandler).Assembly));


builder.Services.AddScoped<IChatNotificationService, ChatNotificationService>();

// Get Upload Folder Path from appsettings.json
string uploadFolderPath = Path.Combine(builder.Environment.WebRootPath, "uploads");

// Register File Storage Service with the upload folder path
builder.Services.AddScoped<IFileStorageService>(provider => new LocalFileStorageService(uploadFolderPath));


var app = builder.Build();

// Ensure static files can be served (important for displaying uploaded files)
app.UseStaticFiles();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
// Use CORS
app.UseCors(allowedOrigins);

app.UseAuthentication();
app.UseAuthorization();
app.UseWebSockets();

// Map SignalR Hub endpoint
app.MapHub<ChatHub>("/chathub");

app.MapControllers();

app.Run();
