using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace dotnet_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        public class Body
        {
            public int x { get; set; }
            public double y { get; set; }
        }

        [HttpPost]
        [Route("/vasilis")]
        public string Vasilis(Body body)
        {
            Console.WriteLine("body.x: " + body.x);
            return JsonSerializer.Serialize(body);
        }

        [HttpGet]
        [Route("/kosmas")]
        public string Kosmas()
        {
            var material = new Material(fck: 20000, fyk: 500000) { };

            var slab = new Slab
            (
                material: material,
                height: 0.2,
                cover: 0.025,
                dbLx: 0.01,
                dbLy: 0.01
            )
            { };
            var loads = new Load
            (
                area: 18,
                extraDeadLoads: 1.2,
                height: 0.2,
                variableLoads: 3
            )
            { };
            var column = new Column(cx: 0.55, cy: 0.55, dx: 0) { };
            return JsonSerializer.Serialize(new Check(column, loads, slab, material));
        }

        [HttpGet]
        [Route("/kosmas/kati/allo")]
        public dynamic KosmasKatiAllo()
        {
            return new Dictionary<string, string> {
                {"x", "1"},
                {"y", "2"}
            };
        }
    }
}
