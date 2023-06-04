using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pay.Billeads.Data;
using Pay.Billeads.Services;
using Pay.Billeads.Services.Abstraction;
using Pay.Billeads.Data.Abstract;
using Pay.Billeads.Data.Repositories;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("devConnection") ?? throw new InvalidOperationException("Connection string 'devConnection' not found."); ;

builder.Services.AddDbContext<PayBilleadsContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllers();

// In production, the React files will be served from this directory
builder.Services.AddSpaStaticFiles(configuration =>
{
  configuration.RootPath = "ClientApp/build";
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
  options.AddPolicy("CorsPolicy",
      builder => builder
      .AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader()
          //.AllowCredentials()
          );
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
  options.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,

    IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JWTSecretKey"))
                )
  };
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IAuthService>(
                new AuthService(
                    builder.Configuration.GetValue<string>("JWTSecretKey"),
                    builder.Configuration.GetValue<int>("JWTLifespan")
                )
            );

builder.Services.AddMvc().AddNewtonsoftJson(options =>
{
  options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
  options.SerializerSettings.Converters.Add(new StringEnumConverter());
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseDeveloperExceptionPage();
}
else
{
  app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseSpaStaticFiles();

app.UseSpa(spa =>
{
  spa.Options.SourcePath = "ClientApp";

  if (app.Environment.IsDevelopment())
  {
    spa.UseReactDevelopmentServer(npmScript: "start");
  }
});

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

