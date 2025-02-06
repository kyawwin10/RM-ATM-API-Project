using Asp.Versioning;
using Azure.Storage.Blobs;
using BAL.Common;
using BAL.IServices;
using BAL.Services;
using BAL.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using MODEL.ApplicationConfig;
using RetailManagementApi;
using System.Text;

//var builder = WebApplication.CreateBuilder(args);
var builder = WebApplication.CreateBuilder(args);

var appSettings = new AppSettings();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Configuration.GetSection("AppSettings").Bind(appSettings);
ServiceManager.SetServiceInfo(builder.Services, appSettings);

//Add Swagger Authentication
builder.Services.AddSwaggerGenWithAuth();

//Add Version
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "Version1",
        Title = "API V1 Title",
        Description = "API V1 Description"
    });

    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "Version2",
        Title = "API V2 Title",
        Description = "API V2 Description"
    });

    options.SwaggerDoc("v3", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "Version3",
        Title = "API V3 Title",
        Description = "API V3 Description"
    });
    //options.ResolveConflictingActions(a => a.First());
});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version")
    );
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});


//Add Authentication
builder.Services.AddSingleton<TokenProvider>();
//builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.RequireHttpsMetadata = false;
        option.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ClockSkew = TimeSpan.Zero
        };
    });

//Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddEndpointsApiExplorer();


builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowSpecificOrigins",
        builder => builder.WithOrigins("http://localhost:5173")
        .WithMethods("GET", "POST", "PATCH", "PUT", "DELETE", "OPTIONS")
        .WithHeaders(HeaderNames.Accept, HeaderNames.ContentType, HeaderNames.Authorization)
        .AllowCredentials());
});

builder.Services.AddSingleton(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("AzureBlobStorage");
    var containerName = configuration["BlobContainerName"];

    return new BlobContainerClient(connectionString, containerName);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint($"/swagger/v1/swagger.json", "API V1");
        options.SwaggerEndpoint($"/swagger/v2/swagger.json", "API V2");
        options.SwaggerEndpoint($"/swagger/v3/swagger.json", "API V3");
    });
}



app.UseHttpsRedirection();
app.UseCors("MyAllowSpecificOrigins");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();