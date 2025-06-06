using OAuth_Presentation.Configurations;
using OAuth_Presentation.Configurations.Google;
using OAuth_Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var baseAddress = builder.Configuration["ApiSettings:BaseUrl"];
// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddHttpClient();

builder.Host.AddAutofac(baseAddress!);

builder.Services.AddSession();

builder.Services.AddGoogleAuth(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

var a = app.Environment.IsDevelopment();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseFrontExceptionHandler();
//app.UseExceptionMiddleware();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
