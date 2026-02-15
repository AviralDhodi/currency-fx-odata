using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using Microsoft.EntityFrameworkCore;
using CurrencyFxOData.Models;
using CurrencyFxOData.Data;
using CurrencyFxOData.Chaos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ChaosConfig>();

builder.Services.AddDbContext<FxDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("FxDb")));

builder.Services.AddControllers()
    .AddOData(opt =>
    {
        opt.EnableQueryFeatures();
        opt.AddRouteComponents("odata", GetEdmModel());
    });

var app = builder.Build();

/* DB migrate + seed ONCE */
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FxDbContext>();
    db.Database.Migrate();
    FxDataSeeder.Seed(db);
}

app.MapControllers();
app.Run();

static Microsoft.OData.Edm.IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();
    builder.EntitySet<CurrencyFXRate>("CurrencyFXRates")
           .EntityType
           .HasKey(x => x.UniqueNameDate);
    return builder.GetEdmModel();
}