using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Project.Application;
using Project.Application.Services;
using Project.Domain.Identity;
using Project.Persistence;
using Project.Persistence.Context;
using Project.Persistence.IPersistence;
using Project.Persistence.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme{
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement(){
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});
builder.Services.AddDbContext<DataContext>(options =>
{
    // options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"));
    string connectionString = builder.Configuration.GetConnectionString("MySqlConnection");

//   options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));


    try
    {
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
    catch (Exception ex)
    {
        // Log the exception or handle it in some way
        Console.WriteLine($"Error connecting to the database: {ex.Message}");
        throw; // Rethrow the exception to prevent the application from starting with a misconfigured database
    }
});

// implementado "false" somente para facilitar os testes
builder.Services.AddIdentityCore<User>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;
})
.AddSignInManager<SignInManager<User>>()
.AddEntityFrameworkStores<DataContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    string tokenKey = builder.Configuration["TokenKey"] ?? "chave-padrao-se-nulo";
                    options.TokenValidationParameters = new TokenValidationParameters{
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

builder.Services.AddControllers()
                .AddJsonOptions(
                    options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
                )
                .AddNewtonsoftJson(
                    options => options.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IProjectListService, ProjectListService>();
builder.Services.AddScoped<IListService, ListService>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<IProjectUserService, ProjectUserService>();

builder.Services.AddScoped<IGeralPersistence, GeralPersistence>();
builder.Services.AddScoped<IProjectListPersistence, ProjectListPersistence>();
builder.Services.AddScoped<IListPersistence, ListPersistence>();
builder.Services.AddScoped<ICardPersistence, CardPersistence>();
builder.Services.AddScoped<IUserPersistence, UserPersistence>();
builder.Services.AddScoped<IProjectUserPersistence, ProjectUserPersistence>();


builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: "CorsPolicy",
    policy =>
    {
        policy.WithOrigins("http://localhost").AllowAnyHeader().AllowAnyMethod();
        //  policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseStaticFiles();
    app.UseHttpsRedirection();
    app.UseSwagger();
    app.UseSwaggerUI(c =>{
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project.API");
        c.InjectStylesheet("/swagger-ui/SwaggerDark.css");
    });
} else {
    app.UseStaticFiles();

    app.UseHsts();
    
    app.UseSwagger();
     app.UseSwaggerUI(c =>{
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project.API");
        c.InjectStylesheet("/swagger-ui/SwaggerDark.css");
    });
}

app.UseCors("CorsPolicy");

// app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
