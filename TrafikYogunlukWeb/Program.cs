var builder = WebApplication.CreateBuilder(args);

// MVC + HttpClient servisi ekleniyor
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<TrafikYogunlukWeb.Services.TomTomService>();

var app = builder.Build();

// Üretim ortamýysa hata yönlendirmesi yap
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Anasayfa}/{id?}");


app.Run();
