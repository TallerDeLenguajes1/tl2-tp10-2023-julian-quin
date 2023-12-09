using tl2_tp10_2023_julian_quin;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
var CadenaDeConexion = builder.Configuration.GetConnectionString("SqliteConexion")!.ToString();
builder.Services.AddSingleton<string>(CadenaDeConexion); //inyecto la cadena de conexion
builder.Services.AddScoped<ITareasRepository,TareasRepository>();
builder.Services.AddScoped<IUsuarioRepository,UsuarioRepository>();
builder.Services.AddScoped<ITableroRepository,TableroRepository>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10000);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});;

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
    pattern: "{controller=Login}/{action=Index}/{id?}");
app.Run();
