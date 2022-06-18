using AgendaWeb.Infra.Data.Interfaces;
using AgendaWeb.Infra.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("BDAgendaWeb");

builder.Services.AddTransient<ITarefaRepository>(map => new TarefaRepository(connectionString));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}"
);

app.Run();
