var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Lägger till en rad för att sessions ska fungera
builder.Services.AddDistributedMemoryCache();

//Lägg till ett block för att sessions ska fungera
builder.Services.AddSession(opttions =>
{
    opttions.IdleTimeout = TimeSpan.FromSeconds(100);
    opttions.Cookie.HttpOnly = true;
    opttions.Cookie.IsEssential = true;
});

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
    pattern: "{controller=GameLibrary}/{action=WelcomePage}/{id?}");

app.Run();
