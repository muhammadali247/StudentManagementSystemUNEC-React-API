using Microsoft.OpenApi.Models;
using StudentManagementSystemUNEC.API.Extensions;
using StudentManagementSystemUNEC.API.Middleware;
using StudentManagementSystemUNEC.Business;
using StudentManagementSystemUNEC.DataAccess;
using StudentManagementSystemUNEC.DataAccess.Contexts;

var builder = WebApplication.CreateBuilder(args);
//var allowedOrigins = builder.Configuration["AllowedOrigins"].Split(',');
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();    

builder.Services.AddDataAccessServices(builder.Configuration);
builder.Services.AddBuinessServices(); 

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

builder.Services.AddIdentityService();

builder.Services.AddJwtServiceExtension(builder.Configuration);

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAllOrigins", builder =>
//    {
//        builder.AllowAnyOrigin()
//               .AllowAnyHeader()
//               .AllowAnyMethod();
//    });
//});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
    builder =>
    {
        builder.WithOrigins(allowedOrigins)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
    });
});

builder.Services.AddEndpointsApiExplorer();


//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});


var app = builder.Build();

app.UseMiddleware<JwtCookieMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
    await initializer.InitializeAsync();
    await initializer.UserSeedAsync();

};

app.UseHttpsRedirection();

app.UseDefaultFiles();

app.UseStaticFiles();

app.AddExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

//app.UseCors("AllowAllOrigins");
app.UseCors();

app.MapControllers();

app.Run();