using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TravelAgencyApplication.Domain.Identity;
using TravelAgencyApplication.Repository.Implementation;
using TravelAgencyApplication.Repository.Interface;
using TravelAgencyApplication.Service.Implementation;
using TravelAgencyApplication.Service.Interface;
using TravelAgencyApplication.Web.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<TAUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
builder.Services.AddScoped(typeof(IDepartureLocationRepository), typeof(DepartureLocationRepository));
builder.Services.AddScoped(typeof(IDestinationRepository), typeof(DestinationRepository));
builder.Services.AddScoped(typeof(IReservationRepository), typeof(ReservationRepository));
builder.Services.AddScoped(typeof(ITagRepository), typeof(TagRepository));
builder.Services.AddScoped(typeof(ITravelPackageRepository), typeof(TravelPackageRepository));
builder.Services.AddScoped(typeof(IItineraryRepository), typeof(ItineraryRepository));

builder.Services.AddTransient<ITravelPackageService, TravelPackageService>();
builder.Services.AddTransient<IItineraryService, ItineraryService>();
builder.Services.AddTransient<IDepartureLocationService, DepartureLocationService>();
builder.Services.AddTransient<IDestinationService, DestinationService>();
builder.Services.AddTransient<IReservationService, ReservationService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITagService, TagService>();
builder.Services.AddTransient<ITravelPackageItineraryService, TravelPackageItineraryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
