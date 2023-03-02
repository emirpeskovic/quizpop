using QuizPop;
using QuizPop.DAL;
using QuizPop.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using QuizPop.Constants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<DatabaseManager>();
builder.Services.AddSingleton<QuizService>();
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

app.MapControllerRoute(
    "api",
    "api/{controller}/{action}/{id?}",
    // ReSharper disable once Mvc.ControllerNotResolved
    constraints: new { controller = "^API.*" });

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();