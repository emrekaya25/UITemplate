using UITemplate.Model.DTO.Role;
using UITemplate.Model.DTO.User;
using UITemplate.Model.ExceptionHelper;
using UITemplate.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDistributedMemoryCache(); // Session verilerini bellekte saklar
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30);
	options.Cookie.HttpOnly = true; //tarayýcý tarafýndaki JavaScript kodunun çerezlere (cookies) eriþimini engeller.
	options.Cookie.IsEssential = true; //GDPR(General Data Protection Regulation) uyumluluðu
});


builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<BaseService<UserDTO>>();
builder.Services.AddScoped<BaseService<RoleDTO>>();
builder.Services.AddScoped<ResponseChecker>();
builder.Services.AddScoped(typeof(BaseService<>));

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

app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
	  name: "areas",
	  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
	);
});

app.Use(async (context, next) =>
{
	if (!context.User.Identity.IsAuthenticated)
	{
		context.Response.Redirect("/admin/login");
		return;
	}

	await next();
});

app.Run();
