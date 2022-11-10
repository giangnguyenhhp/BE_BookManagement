using System.Text;
using BookManagement.Models;
using BookManagement.Models.Auth;
using BookManagement.Service.IAuth;
using BookManagement.Service.IAuthor;
using BookManagement.Service.IBook;
using BookManagement.Service.ICatalog;
using BookManagement.Service.Impl.Auth;
using BookManagement.Service.Impl.Author;
using BookManagement.Service.Impl.Book;
using BookManagement.Service.Impl.Catalog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);
const string policyName = "_CORS";

// Connect to PostgresSQL Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//For Entity FrameWork
builder.Services.AddDbContext<MasterDbContext>(option
=> option.UseNpgsql(connectionString));

//For Identity
builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<MasterDbContext>().AddDefaultTokenProviders();

//Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

//Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidAudience = builder.Configuration["JWT:ValidAudience"],
    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

//CORS COnfig
var cors = builder.Configuration.GetSection("CORS").GetChildren().Select(x => x.Value).ToArray();
builder.Services.AddCors(options =>
{
    options.AddPolicy(policyName,
    corsPolicyBuilder => { corsPolicyBuilder.WithOrigins(cors).AllowAnyHeader().AllowAnyMethod().AllowCredentials(); });
});

//Registration Service
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IAuthenticateRepository, AuthenticateRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
    In = ParameterLocation.Header,
    Description = "Please enter a valid token",
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
    Type = ReferenceType.SecurityScheme,
    Id = "Bearer"
    }
    },
    new string[] {}
    }
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Fix loi CORS
app.UseCors(policyName);

app.UseHttpsRedirection();

//Authentication và Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();