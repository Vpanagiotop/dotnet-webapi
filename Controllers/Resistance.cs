using System;
using NumSharp;
namespace dotnet_webapi.Controllers
{
    public class Resistance
    {
        public double Vrdc;
        public double Vrdmax;
        public Resistance(Material material, Slab slab)
        {
            Vrdmax = Math.Round(0.3 * (1 - material.fck / 250000) * material.fcd, 2);
            double k = 1 + np.sqrt(0.2 / slab.dm);
            double vmin = 35 * np.sqrt(k) * Math.Pow(material.fck / 1000, 1 / 6.0);
            Vrdc = Math.Round(Math.Max(180 / 1.5 * Math.Pow(100 * slab.rhoL, 1 / 3.0),
                    vmin) * k * Math.Pow(material.fck / 1000, 1 / 3.0), 2);
        }
    }
}