using CurrencyFxOData.Models;

namespace CurrencyFxOData.Data;

public static class FxDataSeeder
{
    public static void Seed(FxDbContext db)
    {
        if (db.CurrencyFXRates.Any()) return;

        var currencies = new[]
        {
            "USD","EUR","GBP","INR","JPY","AUD","CAD","CHF","SGD","NZD",
            "MXN","BRL","ZAR","SEK","NOK","DKK","PLN","HUF","CZK","ILS"
        };

        var start = DateTime.SpecifyKind(new DateTime(2024, 1, 1), DateTimeKind.Utc);
        var end   = DateTime.SpecifyKind(new DateTime(2026, 12, 31), DateTimeKind.Utc);

        var rows = new List<CurrencyFXRate>(120_000);

        foreach (var d in EachDay(start, end))
        {
            foreach (var from in currencies)
            {
                foreach (var to in currencies.Where(c => c != from).Take(5))
                {
                    var rate = Random.Shared.Next(50,150) / 100m;

                    rows.Add(new CurrencyFXRate
                    {
                        CurrencyFrom = from,
                        CurrencyTo = to,
                        RateDate = d,
                        ExchangeRate = rate,
                        CorporateDailyInvoiceRate = rate,
                        CorporateDailyInvoiceInverseRate = 1 / rate,
                        CorporateMonthEndRate = rate,
                        CorporateMonthEndInverseRate = 1 / rate,
                        UniqueName = $"{from}:{to}",
                        UniqueNameDate = $"{from}{to}{d:yyyyMMdd}",
                        Name = $"{from}:{to}"
                    });
                }
            }
        }

        db.CurrencyFXRates.AddRange(rows);
        db.SaveChanges();
    }

    private static IEnumerable<DateTime> EachDay(DateTime s, DateTime e)
    {
        for (var d = s; d <= e; d = d.AddDays(1))
            yield return DateTime.SpecifyKind(d, DateTimeKind.Utc);
    }
}