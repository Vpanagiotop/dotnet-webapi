using System;
using NumSharp;
using System.Collections.Generic;
namespace dotnet_webapi.Controllers
{
    public class Perimeter
    {
        public double? u = null;
        public double? a = null;
        public double? Wx = null;
        public double? Wy = null;
        public double? by = null;
        public double? bx = null;
        public Perimeter(Column column, double? a)
        {
            Dictionary<(SectionType, Position), Func<double?, double?>> actions = new Dictionary<(SectionType, Position), Func<double?, double?>>()
            {
                {(SectionType.Rectangular, Position.Inner), (a) => {return getInnerPerimeter(column.cx, column.cy, a);}},
                {(SectionType.Rectangular, Position.Edge), (a) => {return getEdgePerimeter(column.cx, column.cy, column.dx, column.dy, a);}},
                {(SectionType.Rectangular, Position.Corner), (a) => {return getCornerPerimeter(column.cx, column.cy, column.dy, column.dy, a);}},
            };
            var perimeter = actions[(column.sectionType, column.position)](a);
            this.u = Math.Round(perimeter.GetValueOrDefault(), 3);
            this.a = a;
        }
        public double? getInnerPerimeter(double? cx, double? cy, double? a)
        {
            double? u = 2 * (cx + cy + 2 * np.pi * a);
            return u;
        }
        public double? getEdgePerimeter(double? cx, double? cy, double? dx, double? dy, double? a)
        {
            double? u;
            if (dy != null && dy <= a)
            {
                u = cx + 2 * (cy + dy) + np.pi * a;
            }
            else if (dx != null && dx <= a)
            {
                u = cy + 2 * (cx + dx) + np.pi * a;
            }
            else
            {
                u = getInnerPerimeter(cx, cy, a);
            }
            return u;
        }
        public double? getCornerPerimeter(double? cx, double? cy, double? dx, double? dy, double? a)
        {
            double? u;
            if (dy <= a && dx <= a)
            {
                u = (cx + dx) + (cy + dy) + np.pi * a;
            }
            else if (dy <= a && dx > a)
            {
                u = getEdgePerimeter(cx, cy, dx, dy, a);
            }
            else
            {
                u = getInnerPerimeter(cx, cy, a);
            }
            return u;
        }
    }
}