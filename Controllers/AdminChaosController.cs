using Microsoft.AspNetCore.Mvc;
using CurrencyFxOData.Chaos;

namespace CurrencyFxOData.Controllers;

[ApiController]
[Route("admin/chaos")]
public class AdminChaosController : ControllerBase
{
    private readonly ChaosConfig _cfg;

    public AdminChaosController(ChaosConfig cfg) => _cfg = cfg;

    [HttpGet]
    public IActionResult Get() => Ok(_cfg);

    [HttpPost]
    public IActionResult Set(ChaosConfig input)
    {
        _cfg.DelaySeconds = input.DelaySeconds;
        _cfg.ErrorMode = input.ErrorMode;
        _cfg.ForceRecordCount = input.ForceRecordCount;
        return Ok(_cfg);
    }

    [HttpPost("reset")]
    public IActionResult Reset()
    {
        _cfg.DelaySeconds = 0;
        _cfg.ErrorMode = null;
        _cfg.ForceRecordCount = null;
        return Ok(_cfg);
    }
}