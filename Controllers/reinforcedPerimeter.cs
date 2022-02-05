using System;
using NumSharp;
namespace dotnet_webapi.Controllers
{
    public class ReinforcedPerimeter
    {
        public double? perimeter;
        public double? radius;
        public double Nmin;
        public double Aswmin;
        public double? AswReq;
        public double? Ntot;
        public double? Aswtot;
        public double db;
        public double? St;
        public ReinforcedPerimeter(Column column, Slab slab, Material material, Rules rules, double Vrdc, double? ved, double radius)
        {
            var countPerimeter = new Perimeter(column, radius / 1000);
            this.perimeter = countPerimeter.u;
            this.radius = countPerimeter.a;
            this.Nmin = Math.Ceiling((this.perimeter * 1000 / rules.St).GetValueOrDefault());
            this.Aswmin = this.Nmin * rules.Asw;
            double? fywd = Math.Min((250 + 0.25 * slab.dm * 1000).GetValueOrDefault(), material.fyd / 1000);
            this.AswReq = this.perimeter * (ved - 0.75 * Vrdc) * rules.Sr / (1.5 * fywd);
            if (this.AswReq <= this.Aswmin)
            {
                this.Ntot = this.Nmin;
                this.Aswtot = this.Aswmin;
                this.db = rules.dbmin;
                this.St = this.perimeter / this.Ntot * 1000;
            }
            else if (this.AswReq > this.Aswmin)
            {
                var db = new double[] { 8, 10, 12, 14, 16, 20 };
                int i = 0;
                while (this.perimeter * (Math.Pow(db[i], 2) * np.pi / 4) / this.AswReq < 0.1)
                {
                    i++;
                }
                this.Ntot = Math.Ceiling((this.AswReq / (Math.Pow(db[i], 2) * np.pi / 4)).GetValueOrDefault());
                this.St = this.perimeter / this.Ntot;
                this.db = db[i];
                this.Aswtot = this.Ntot * Math.Pow(this.db, 2) * np.pi / 4;
            }
        }
    }
}