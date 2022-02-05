using System;
using NumSharp;
namespace dotnet_webapi.Controllers
{
    public enum SectionType { Rectangular, Circular }
    public enum Position { Inner, Edge, Corner }
    public class Column
    {
        public double? cx = null;
        public double? cy = null;
        public double? D = null;
        public double? dx = null;
        public double? dy = null;
        public double perimeter;
        public SectionType sectionType;
        public Position position;
        public Column(double cx, double cy)
        {
            this.cx = cx;
            this.cy = cy;
            this.sectionType = SectionType.Rectangular;
            this.position = Position.Inner;
        }
        public Column(double cx, double cy, double? dx = null, double? dy = null)
        {
            this.cx = cx;
            this.cy = cy;
            this.dx = dx;
            this.dy = dy;
            this.sectionType = SectionType.Rectangular;
            if (dx != null && dy != null)
            {
                this.position = Position.Corner;
            }
            else
            {
                this.position = Position.Edge;
            }
        }
        public Column(double D)
        {
            this.D = D;
            this.sectionType = SectionType.Circular;
            this.position = Position.Inner;
        }
        public Column(double D, double? dx = null, double? dy = null)
        {
            this.D = D;
            this.dx = dx;
            this.dy = dy;
            this.sectionType = SectionType.Circular;
            if (dx != null && dy != null)
            {
                this.position = Position.Corner;
            }
            else
            {
                this.position = Position.Edge;
            }
        }
    }
}