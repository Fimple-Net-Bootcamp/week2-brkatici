using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SpaceWeatherForecast;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSwaggerGen(options =>
//{
//    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
//    {
//        Description = "Please provide:  Bearer [space] your token here ",
//        Name = "Authorization",
//        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
//        Scheme = "Bearer"
//    });
//    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
//    {
//        {
//            new Microsoft.OpenApi.Models.OpenApiSecurityScheme{
//                Reference=new Microsoft.OpenApi.Models.OpenApiReference{
//                    Type= Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
//                    Id="Bearer"
//                },
//                Scheme="oauth2",
//                Name="Bearer",
//                In=Microsoft.OpenApi.Models.ParameterLocation.Header
//            },
//            new List<String>()
//        }
//    });
//});
//var key = "kygmtest12345678kygmtest12345678kygmtest12345678";

//builder.Services.AddAuthentication(x =>
//{
//    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(x =>
//{
//    x.RequireHttpsMetadata = false;
//    x.SaveToken = true;
//    x.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
//        ValidateIssuer = false,
//        ValidateAudience = false
//    };
//});

//builder.Services.AddScoped<JwtAuthenticationManager>(t => new JwtAuthenticationManager(key, t.GetRequiredService<AppDbContext>()));

var weatherDataSeeder = new WeatherDataSeeder();
var weatherDataList = weatherDataSeeder.SeedWeatherData();

builder.Services.AddSingleton(weatherDataList);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
