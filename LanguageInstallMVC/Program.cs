using LanguageInstall.Data.Data;
using LanguageInstall.Service.Service;
using LanguageInstall.Service.Service.MultiTable;
using LanguageInstall.Service.SignalR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); // MVC controllers with views
builder.Services.AddHttpContextAccessor();

// Configure the database context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("con")));

// Dependency Injection for custom services
builder.Services.AddScoped<ITranslationService, TranslationService>();
builder.Services.AddScoped<ILocalizationService, LocalizationService>();
builder.Services.AddScoped<ILanguageTableService, LanguageTableService>();

builder.Services.AddScoped<IUserService, UserService>();

// Add SignalR
builder.Services.AddSignalR();

// Add session management
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Ensure session is essential for GDPR compliance
});

// Add authentication and authorization
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/User/Login"; // Redirect to login page if unauthenticated
        options.AccessDeniedPath = "/User/AccessDenied";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    });

// Add HTTP client support
builder.Services.AddHttpClient<TranslationService>();

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Detailed error page in development
   
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Generic error page in production
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Middleware to handle localization via cookies
app.Use(async (context, next) =>
{
    var language = context.Request.Cookies["Language"] ?? "en"; // Default to English
    context.Items["Language"] = language;

    if (context.Request.Cookies["Language"] == null)
    {
        context.Response.Cookies.Append("Language", "en", new CookieOptions
        {
            HttpOnly = true,
            Secure = !app.Environment.IsDevelopment(), // Secure in production
            SameSite = SameSiteMode.Lax
        });
    }

    await next();
});

// Add authentication, authorization, and session middleware
app.UseRouting();
app.UseSession();
app.UseAuthentication(); // Ensure authentication comes before authorization
app.UseAuthorization();

// Map SignalR hubs
app.MapHub<ProgressHub>("/progressHub");

// Map MVC routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
