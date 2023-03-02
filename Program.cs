using Microsoft.AspNetCore.Antiforgery;
using QuizPop;
using QuizPop.DAL;
using QuizPop.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using QuizPop.Constants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Our singletons
builder.Services.AddSingleton<DatabaseManager>();
builder.Services.AddSingleton<QuizService>();

// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidAudience = "quizpop.com",
        ValidateIssuer = true,
        ValidIssuer = "quizpop.com",
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(JwtConstants.JwtToken))
    };
});

// Anti-forgery
builder.Services.AddAntiforgery(options =>
{
    // Options to help protect against XSS attacks
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

var app = builder.Build();
#if DEBUG
Initializer.Initialize(app);
#endif

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();
app.UseStaticFiles();

// Enable Anti-Forgery middleware
app.Use(next => context =>
{
    // Validate Anti-Forgery token
    var tokens = context.RequestServices.GetRequiredService<IAntiforgery>();
    var isRequestValid = tokens.IsRequestValidAsync(context).GetAwaiter().GetResult();
    if (isRequestValid) return next(context);
    context.Response.StatusCode = StatusCodes.Status403Forbidden;
    return Task.CompletedTask;
});

app.MapControllerRoute(
    "api",
    "api/{controller}/{action}/{id?}",
    // ReSharper disable once Mvc.ControllerNotResolved
    constraints: new { controller = "^API.*" });

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();