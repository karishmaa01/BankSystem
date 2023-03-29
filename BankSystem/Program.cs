using BankSystem.Domain.Data;
using BankSystem.Domain.Interface;
using BankSystem.Domain.Jwt;
using BankSystem.Domain.Repository;
using BankSystem.MapperProfile;
using BankSystem.Service.Interface;
using BankSystem.Service.Mapper;
using BankSystem.Service.Repository;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using System.Diagnostics.CodeAnalysis;
using System.Text;

public class Program
{
    [ExcludeFromCodeCoverage]
    private static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        //database
        builder.Services.AddDbContext<BankDb>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnect")));

        //repository
        builder.Services.AddScoped<ICustomerRepository, CustomerService>();
        builder.Services.AddScoped<ITransactionDetRepository, TransactionDetService>();
        builder.Services.AddScoped<IAmountTransferRepository, AmountTransferService>();
        builder.Services.AddScoped<ICustomer, CustomerRepository>();
        builder.Services.AddScoped<ITransactionDet, TransactionDetailRepository>();
        builder.Services.AddScoped<IAmountTransfer, AmountTransferRepository>();

        //auto mapper
        builder.Services.AddAutoMapper(typeof(MapperProfile));
        //builder.Services.AddAutoMapper(typeof(CustomerDtoProfile));
        builder.Services.AddAutoMapper(typeof(ATDtoProfile));

        //jwt
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "BankProject",
                Version = "v1"
            });

            //swagger documentation
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "BankSystem.xml"));

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
            });
        });  


        var _jwtsetting = builder.Configuration.GetSection("JWTSettings");
        builder.Services.Configure<JWTSettings>(_jwtsetting);

        var authkey = builder.Configuration.GetValue<string>("JWTSettings:SecretKey");
        builder.Services.AddAuthentication(item =>
        {
            item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(item =>
        {
            item.RequireHttpsMetadata = true;
            item.RequireHttpsMetadata = true;
            item.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authkey)),
                ValidateIssuer = false,
                ValidateAudience = false,
            };
        });

        //paymenttypeenum
        builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                })
                .AddFluentValidation();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v3", new OpenApiInfo { Title = "BankSystem", Version = "v3" });
        }).AddSwaggerGenNewtonsoftSupport();

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

        //jwt
        app.UseAuthentication();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}