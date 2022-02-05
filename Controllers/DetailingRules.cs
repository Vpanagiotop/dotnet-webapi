using System;
using NumSharp;
namespace dotnet_webapi.Controllers
{
    public class Rules
    {
        public double St;
        public double Sr;
        public double Asw;
        public double dbmin;
        public Rules(Slab slab, Material material)
        {
            this.Sr = Convert.ToInt32(Math.Floor((0.75 * slab.dm).GetValueOrDefault() * 100)) * 10;
            this.St = Convert.ToInt32(Math.Floor((1.5 * slab.dm).GetValueOrDefault() * 100)) * 10;
            var db = new double[] { 8, 10, 12, 14, 16, 20 };
            int i = 0;
            while (Math.Pow(db[i], 2) * np.pi / (4 * this.Sr * this.St) < 0.08 * Math.Sqrt(material.fck) / material.fyk)
            {
                i++;
            }
            this.Asw = Math.Floor(Math.Pow(db[i], 2) * np.pi / 4);
            this.dbmin = db[i];
        }
    }
}