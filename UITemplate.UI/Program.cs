using UITemplate.Model.DTO.Role;
using UITemplate.Model.DTO.User;
using UITemplate.Model.ExceptionHelper;
using UITemplate.UI.Controllers;
using UITemplate.UI.Middleware;
using UITemplate.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDistributedMemoryCache(); // Session verilerini bellekte saklar
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30);
	options.Cookie.HttpOnly = true; //tarayıcı tarafındaki JavaScript kodunun çerezlere (cookies) erişimini engeller.
	options.Cookie.IsEssential = true; //GDPR(General Data Protection Regulation) uyumluluğu
});


builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<BaseService<UserDTO>>();
builder.Services.AddScoped<BaseService<RoleDTO>>();
builder.Services.AddScoped<ResponseChecker>();
builder.Services.AddScoped(typeof(BaseService<>));
builder.Services.AddScoped<SessionHelper>();

var app = builder.Build();

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

app.UseApiResponseMiddleware();

app.UseSession();

app.UseSessionNullCheckMiddleware();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors(opt => { opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });


app.UseAuthorization();



app.Use(async (context, next) =>
{
	await next();

	if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
	{
		// 404 sayfasýný göster
		//context.Request.Path = "/Admin/ExtraPages/ErrorPages/NotFound.html"; // 404 sayfasýnýn yolunu güncelleyin
		context.Response.Redirect("/Admin/ExtraPages/ErrorPages/NotFound.html");
	}
});

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

	// Kök URL'ye gelen talepleri Login'e yönlendir
	endpoints.MapGet("/", context =>
	{
		context.Response.Redirect("/admin/login");
		return Task.CompletedTask;
	});
});


app.Run();
