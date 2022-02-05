namespace dotnet_webapi.Controllers
{
    public class Load
    {
        public double Ved;
        public double? area = null;
        public double? areaLoads = null;
        public double? pernamentLoads = null;
        public double? variableLoads = null;
        public double? selfWeight = null;
        public double? extraDeadLoads = null;
        public double? pernamentPartialFactor = null;
        public double? variablePartialFactor = null;
        public Load(double Ved)
        {
            this.Ved = Ved;
        }
        public Load(double area, double areaLoads)
        {
            getPunchingShearForce(area, areaLoads);
        }
        public Load(double area, double pernamentLoads, double variableLoads,
            double pernamentPartialFactor = 1.35, double variablePartialFactor = 1.5)
        {
            double areaLoads = calculateAreaLoads(pernamentLoads, variableLoads,
              pernamentPartialFactor, variablePartialFactor);
            getPunchingShearForce(area, areaLoads);
        }
        public Load(double area, double height, double variableLoads, double extraDeadLoads = 0,
           double selfWeight = 25, double pernamentPartialFactor = 1.35, double variablePartialFactor = 1.5)
        {
            double pernamentLoads = calculatePernamentLoads(selfWeight, height, extraDeadLoads);
            double areaLoads = calculateAreaLoads(pernamentLoads, variableLoads,
             pernamentPartialFactor, variablePartialFactor);
            getPunchingShearForce(area, areaLoads);
        }
        public void getPunchingShearForce(double area, double areaLoads)
        {
            Ved = area * areaLoads;
            this.area = area;
            this.areaLoads = areaLoads;
        }
        public double calculateAreaLoads(double pernamentLoads, double variableLoads,
            double pernamentPartialFactor, double variablePartialFactor)
        {
            double areaLoads = pernamentLoads * pernamentPartialFactor + variableLoads * variablePartialFactor;
            this.pernamentLoads = pernamentLoads;
            this.pernamentPartialFactor = pernamentPartialFactor;
            this.variablePartialFactor = variablePartialFactor;
            this.variableLoads = variableLoads;
            return areaLoads;
        }
        public double calculatePernamentLoads(double selfWeight, double height, double extraDeadLoads)
        {
            this.selfWeight = selfWeight;
            this.extraDeadLoads = extraDeadLoads;
            double pernamentLoads = selfWeight * height + extraDeadLoads;
            return pernamentLoads;
        }
    }
}