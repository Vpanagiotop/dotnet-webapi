using System;
using NumSharp;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace dotnet_webapi.Controllers
{
    public class Check
    {
        public Perimeter controlPerimeter;
        public double ved { get; set; }
        public Radius outerPerimeter { get; set; }
        public Resistance resistance;
        public List<ReinforcedPerimeter> reinforcedPerimeters;
        public Check(Column column, Load load, Slab slab, Material material)
        {
            this.controlPerimeter = new Perimeter(column, slab.a);
            this.resistance = new Resistance(material, slab);
            this.ved = (double)(1.4 * load.Ved / this.controlPerimeter.u / slab.dm);
            var uout = 1.4 * load.Ved / this.resistance.Vrdc / slab.dm;
            this.outerPerimeter = new Radius(column, Math.Round(uout.GetValueOrDefault(), 3));
            var rules = new Rules(slab, material);
            var d1 = Math.Floor((0.5 * slab.dm * 100).GetValueOrDefault()) * 10;
            List<ReinforcedPerimeter> perimeters = new List<ReinforcedPerimeter>();
            for (double d = d1; d <= (this.outerPerimeter.radius - 1.5 * slab.dm) * 1000 + rules.Sr; d = d + rules.Sr)
            {
                perimeters.Add(new ReinforcedPerimeter(column, slab, material, rules, resistance.Vrdc, this.ved, d));
            }
            this.reinforcedPerimeters = perimeters;
        }
    }
}