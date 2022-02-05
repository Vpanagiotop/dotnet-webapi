using System;
namespace dotnet_webapi.Controllers
{
    public class Material
    {
        public double fck;
        public double fctm;
        public double fcd;
        public double concretePartialFactor = 1.5;
        public double fyk;
        public double fyd;
        public double steelPartialFactor = 1.15;
        public Material(double fck, double fyk)
        {
            this.fck = fck;
            fcd = fck / concretePartialFactor;
            fctm = 0.3 * Math.Pow(fck / 1000, 2 / 3.0) * 1000;
            this.fyk = fyk;
            fyd = fyk / steelPartialFactor;
        }
        public Material(double fck, double concretePartialFactor,
                        double fyk, double steelPartialFactor)
        {
            this.fck = fck;
            this.concretePartialFactor = concretePartialFactor;
            fcd = fck / concretePartialFactor;
            fctm = 0.3 * Math.Pow(fck, 2 / 3.0);
            this.fyk = fyk;
            this.steelPartialFactor = steelPartialFactor;
            fyd = fyk / steelPartialFactor;
        }
    }
}