using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using CurrencyFxOData.Data;
using CurrencyFxOData.Chaos;

namespace CurrencyFxOData.Controllers;

public class CurrencyFXRatesController : ODataController
{
    private readonly FxDbContext _db;
    private readonly ChaosConfig _chaos;

    public CurrencyFXRatesController(FxDbContext db, ChaosConfig chaos)
    {
        _db = db;
        _chaos = chaos;
    }

    [EnableQuery(PageSize = 1000)]
    public async Task<IActionResult> Get()
    {
        if (_chaos.DelaySeconds > 0)
            await Task.Delay(TimeSpan.FromSeconds(_chaos.DelaySeconds));

        if (_chaos.ErrorMode != null)
        {
            if (_chaos.ErrorMode == "random")
            {
                int[] codes = { 500, 502, 503, 504, 429 };
                return StatusCode(codes[Random.Shared.Next(codes.Length)]);
            }
            return StatusCode(int.Parse(_chaos.ErrorMode));
        }

        var q = _db.CurrencyFXRates.AsQueryable();

        if (_chaos.ForceRecordCount.HasValue)
            q = q.Take(_chaos.ForceRecordCount.Value);

        return Ok(q);
    }
}