#if DEBUG
using QuizPop;
#endif
using QuizPop.DAL;
using QuizPop.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<DatabaseManager>();
builder.Services.AddSingleton<QuizService>();

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

app.MapControllerRoute(
    "api",
    "api/{controller}/{action}/{id?}",
    // ReSharper disable once Mvc.ControllerNotResolved
    constraints: new { controller = "^API.*" });

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();