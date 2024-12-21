using System.Security.Claims;
using PickleballCourtBookingSystem.Core.Interfaces.DBContext;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;
using PickleballCourtBookingSystem.Core.Services;
using PickleballCourtBookingSystem.Infrastructure.Repository;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotenv.net;
using Microsoft.Extensions.Options;
using PickleballCourtBookingSystem.Core.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Console.OutputEncoding = Encoding.UTF8;
builder.Services.AddControllers();
//Khiến controller chuyển đổi string thành enum nếu truyền vào là string
builder.Services.AddControllers()
    .AddJsonOptions(option =>
    {
        option.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        // ValidIssuer = builder.Configuration["Jwt:Issuer"],
        // ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:secretKey"]))
    };
});

// builder.Services.AddAuthorization(options =>
// {
//     options.AddPolicy("Admin", policy =>
//         policy.RequireClaim(ClaimTypes.Role, "1"));
//     options.AddPolicy("CourtOwner", policy =>
//         policy.RequireClaim(ClaimTypes.Role, "2"));
//     options.AddPolicy("Customer", policy =>
//         policy.RequireClaim(ClaimTypes.Role, "3"));
//     options.FallbackPolicy = options.DefaultPolicy;
// });

//Cấu hình allow origin
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

//Cau hinh cloudinary
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("Cloudinary"));
builder.Services.AddSingleton(provider =>
{
    var cloudinarySettings = provider.GetRequiredService<IOptions<CloudinarySettings>>().Value;

    return new Cloudinary(new Account(
        cloudinarySettings.CloudName,
        cloudinarySettings.ApiKey,
        cloudinarySettings.ApiKeySecret
    ))
    {
        Api = { Secure = true } // Kích hoạt kết nối HTTPS
    };
});
// Set Cloudinary credentials
// DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
// Cloudinary cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
// cloudinary.Api.Secure = true;

//Cấu hình Dependency Injection(DI) cho project
builder.Services.AddScoped<IDbContext, MySqlDbContext>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICourtOwnerRepository, CourtOwnerRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICourtRepository, CourtRepository>();
builder.Services.AddScoped<ICourtClusterRepository, CourtClusterRepository>();
builder.Services.AddScoped<IImageCourtUrlRepository, ImageCourtUrlRepository>();
builder.Services.AddScoped<ICourtTimeSlotRepository, CourtTimeSlotRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<ICourtTimeBookingRepository, CourtTimeBookingRepository>();
builder.Services.AddScoped<ICancellationRepository, CancellationRepository>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped<ICourtPriceRepository, CourtPriceRepository>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICourtOwnerService, CourtOwnerService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICourtService, CourtService>();
builder.Services.AddScoped<ICourtClusterService, CourtClusterService>();
builder.Services.AddScoped<IImageCourtUrlService, ImageCourtUrlService>();
builder.Services.AddScoped<ICourtTimeSlotService, CourtTimeSlotService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ICourtTimeBookingService, CourtTimeBookingService>();
builder.Services.AddScoped<ICancellationService, CancellationService>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();
builder.Services.AddScoped<ITimeService, TimeService>();
builder.Services.AddScoped<ICourtPriceService, CourtPriceService>();
builder.Services.AddScoped<IGetListTimeService, GetListTimeService>();
builder.Services.AddScoped<ILocationsService, LocationsService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
