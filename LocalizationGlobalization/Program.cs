using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });
builder.Services.AddMvc().AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();
builder.Services.Configure<RequestLocalizationOptions>(opt =>
{
	var supportedCultures = new List<CultureInfo>
	{
		new CultureInfo("en"),
		new CultureInfo("es"),
		new CultureInfo("fr")
	};
	opt.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en");
	opt.SupportedCultures = supportedCultures;
	opt.SupportedUICultures = supportedCultures;
});
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

var options = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);

//var supportedCultures = new[] { "en", "fr", "es" };
//var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
//	.AddSupportedCultures(supportedCultures)
//	.AddSupportedUICultures(supportedCultures);

//app.UseRequestLocalization(localizationOptions);

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
