using OAuth_Presentation.Configurations;
using OAuth_Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Host.AddAutofac();

builder.Services.AddSession();

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
//app.UseExceptionMiddleware();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
