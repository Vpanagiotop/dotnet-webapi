using System;
using NumSharp;
namespace dotnet_webapi.Controllers
{
    public class Slab
    {
        public double height;
        public double cover;
        public double dbLx;
        public double dbLy;
        public double? dx = null;
        public double? dy = null;
        public double? dm = null;
        public double rhoLmin;
        public double? rhoLx = null;
        public double? rhoLy = null;
        public double rhoL;
        public double a;
        public Slab(Material material, double height,
                    double cover, double dbLx, double dbLy)
        {
            this.height = height;
            this.cover = cover;
            this.dbLx = dbLx;
            this.dbLy = dbLy;
            getEffectiveDepth(height, cover, dbLx, dbLy);
            rhoLmin = Math.Round(Math.Max(0.26 * material.fctm / material.fyk, 0.0013), 4);
            rhoL = rhoLmin;

        }

        public Slab(Material material, double height,
                    double cover, double dbLx, double dbLy, double sx, double sy)
        {
            this.height = height;
            this.cover = cover;
            this.dbLx = dbLx;
            this.dbLy = dbLy;
            var di = getEffectiveDepth(height, cover, dbLx, dbLy);
            rhoLmin = Math.Round(Math.Max(0.26 * material.fctm / material.fyk, 0.0013), 4);
            rhoLx = Math.Round((Math.Pow(dbLx, 2) * np.pi / 4) / (sx * di[0]), 4);
            rhoLy = Math.Round((Math.Pow(dbLy, 2) * np.pi / 4) / (sy * di[1]), 4);
            rhoL = np.sqrt(rhoLx * rhoLy);
            rhoL = Math.Round(rhoL, 4);
        }
        public double[] getEffectiveDepth(double height, double cover,
                                      double dbLx, double dbLy)
        {
            double dx = height - cover - dbLx / 2;
            this.dx = dx;
            double dy = dx - dbLx / 2 - dbLy / 2;
            this.dy = dy;
            double dm = (dx + dy) / 2;
            this.dm = dm;
            this.a = 2 * dm;
            return new double[] { dx, dy, dm };
        }
    }
}