using System;
using NumSharp;
using System.Collections.Generic;
namespace dotnet_webapi.Controllers
{
    public class EccentricityFactor
    {
        public Perimeter controlPerimeter;
        public double? Wx;
        public double? Wy;
        public double? bx;
        public double? by;
        public double? kx;
        public double? ky;
        public EccentricityFactor(double? cx, double? cy, double? a)
        {
            this.Wx = 0.5 * cx * cx + cx * cy + 2 * cy * a + 4 * a * a + Math.PI * a * cx;
            this.Wy = 0.5 * cy * cy + cy * cx + 2 * cx * a + 4 * a * a + Math.PI * a * cy;
            this.bx = cx + 2 * a;
            this.by = cy + 2 * a;
            this.kx = getFactorK(cx, cy, cx / cy);
            this.ky = getFactorK(cx, cy, cy / cx);
        }
        public EccentricityFactor(double? cx, double? cy, double? dx, double? dy, double? a)
        {

        }

        public double? getFactorK(double? cx, double? cy, double? lenRatio)
        {
            double? k = null;
            var X = new double[] { 0.5, 1.0, 2.0, 3.0 };
            var K = new double[] { 0.45, 0.6, 0.7, 0.80 };
            var i = Array.FindIndex(X, X => lenRatio <= X);
            if (i <= 0.5) { k = 0.45; }
            else if (i >= 3.0) { k = 0.80; }
            else if (i > 0.5 && i < 3.0)
            {
                k = K[i - 1] + (lenRatio - X[i - 1]) * (K[i] - K[i - 1]) / (X[i] - X[i - 1]);
            }
            return k;
        }
        // public double? getEdgePerimeter(double? cx, double? cy, double? dx, double? dy, double? a)
        // {
        //     double? u;
        //     if (dy != null && dy <= a)
        //     {
        //         u =  cx + 2 * (cy + dy) + np.pi * a;
        //     }
        //     else if (dx != null && dx <= a)
        //     {
        //         u =  cy + 2 * (cx + dx) + np.pi * a;
        //     }
        //     else
        //     {
        //         u =  getInnerPerimeter(cx, cy, a);
        //     }
        //     return u;
        // }
        // public double? getCornerPerimeter(double? cx, double? cy, double? dx, double? dy, double? a)
        // {
        //     double? u;
        //     if (dy <= a && dx <=a)
        //     {
        //         u =  (cx + dx) + (cy + dy) + np.pi * a;
        //     }
        //     else if (dy <= a && dx > a)
        //     {
        //         u =  getEdgePerimeter(cx, cy, dx, dy, a);
        //     }
        //     else
        //     {
        //         u =  getInnerPerimeter(cx, cy, a);
        //     }
        //     return u;
        // }
    }
}