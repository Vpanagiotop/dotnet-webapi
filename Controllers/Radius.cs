using System;
using NumSharp;
using System.Collections.Generic;
namespace dotnet_webapi.Controllers
{
    public class Radius
    {
        public double? radius { get; set; }
        public double? perimeter { get; set; }
        public Radius(Column column, double? perimeter)
        {
            Dictionary<(SectionType, Position), Func<double?, double?>> actions = new Dictionary<(SectionType, Position), Func<double?, double?>>()
            {
                {(SectionType.Rectangular, Position.Inner), (a) => {return getInnerRadius(perimeter, column.cx, column.cy);}},
                {(SectionType.Rectangular, Position.Edge), (a) => {return getEdgeRadius(perimeter, column.cx, column.cy, column.dx, column.dy);}},
                // {(SectionType.Rectangular, Position.Corner), (a) => {return getCornerPerimeter(column.cx, column.cy, column.dy, column.dy, a);}},
            };
            var radius = actions[(column.sectionType, column.position)](perimeter);
            this.radius = Math.Round(radius.GetValueOrDefault(), 3);
            this.perimeter = perimeter;
        }
        public double? getInnerRadius(double? perimeter, double? cx, double? cy)
        {
            double? d = (0.5 * perimeter - cx - cy) / np.pi;
            return d;
        }
        public double? getEdgeRadius(double? perimeter, double? cx, double? cy, double? dx, double? dy)
        {
            double? d;
            d = getInnerRadius(perimeter, cx, cy);
            if (dy != null && dy <= d)
            {
                d = (perimeter - cx - 2 * (cy + dy)) / np.pi;
            }
            else if (dx != null && dx <= d)
            {
                d = (perimeter - cx - 2 * (cy + dx)) / np.pi;
            }
            return d;
        }
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