using System.Drawing;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Text;

namespace MyGIS
{
    public class GISDocument
    {
        public List<GISLayer> layers = new List<GISLayer>();
        public GISExtent extent;
        public void AddLayer(GISLayer layer)
        {
            layers.Add(layer);
            UpdateExtent();
        }
        public void RemoveLayer(GISLayer layer)
        {
            layers.Remove(layer);
            UpdateExtent();
        }
        public void draw(Graphics graphics, GISView view)
        {
            GISVertex v1 = view.ToMapVertex(new Point(0, view.MapWindowSize.Height - 1));
            GISVertex v2 = view.ToMapVertex(new Point(view.MapWindowSize.Width - 1, 0));
            GISExtent displayextent = new GISExtent(v1, v2);

            for (int i = 0; i < layers.Count; i++)
            {
                layers[i].draw(graphics, view, displayextent);
            }
        }
        private void UpdateExtent()
        {
            if (layers.Count == 0)
            {
                extent = null;
                return;
            }
            extent=new GISExtent(new GISVertex(0,0),new GISVertex(1,1));
            extent.CopyFrom(layers[0].Extent);
            for (int i = 1; i < layers.Count; i++)
                extent.Merge(layers[i].Extent);
        }
    }

    public enum MOUSECOMMAND
    {
        Unused, Select, ZoomIn, ZoomOut, Pan,MY,Dis
    };
    public class GISFeature
    {
        public GISSpatial spatialpart;
        public GISAttribute attributepart;
        public bool Selected = false;

        public GISFeature(GISSpatial spatial, GISAttribute attribute)
        {
            spatialpart = spatial;
            attributepart = attribute;
        }
        public GISSpatial getSpatial()
        {
            return spatialpart;
        }
        public GISAttribute getAttribute()
        {
            return attributepart;
        }

        public void draw(Graphics graphics, GISView view, bool DrawAttributeOrNot, int index)
        {
            spatialpart.draw(graphics, view, Selected);
            if (DrawAttributeOrNot)
                attributepart.draw(graphics, view, spatialpart.getCentroid(), index);
        }

        public object getAttributeValue(int index)
        {
            return attributepart.GetValue(index);
        }

    }
    public class GISField
    {
        public Type datatype;
        public string name;
        public GISField(Type _dt, string _name)
        {
            datatype = _dt;
            name = _name;
        }
    }

    public class GISAttribute
    {
        ArrayList values = new ArrayList();
        public void AddValue(object o)
        {
            values.Add(o);
        }
        public object GetValue(int index)
        {
            return values[index];
        }
        public void draw(Graphics graphics, GISView view, GISVertex location, int index)
        {
            Point screenpoint = view.ToScreenPoint(location);
            graphics.DrawString(values[index].ToString(), 
                new Font("宋体", 20), 
                new SolidBrush(Color.Green),
                new PointF(screenpoint.X, screenpoint.Y));
        }
    }
    public class GISVertex
    {
        public double x;
        public double y;

        public GISVertex(double _x, double _y)
        {
            setX(_x);
            setY(_y);
        }

        public void setX(double _x)
        {
            x = _x;
        }

        public void setY(double _y)
        {
            y = _y;
        }
        public double getX()
        {
            return x;
        }

        public double getY()
        {
            return y;
        }

        public double Distance(GISVertex anothervertex)
        {
            return Math.Sqrt((x - anothervertex.x) * (x - anothervertex.x) + (y - anothervertex.y) * (y - anothervertex.y));
        }

        private static double DotProduct(GISVertex A, GISVertex B, GISVertex C)
        {
            GISVertex AB = new GISVertex(B.x - A.x, B.y - C.y);
            GISVertex BC = new GISVertex(C.x - B.x, C.y - B.y);
            return AB.x * BC.x + AB.y * BC.y;
        }

        private static double CrossProduct(GISVertex A, GISVertex B, GISVertex C)
        {
            GISVertex AB = new GISVertex(B.x - A.x, B.y - A.y);
            GISVertex AC = new GISVertex(C.x - A.x, C.y - A.y);
            return AB.x * AC.y - AB.y * AC.x;
        }

        private static double Distance(GISVertex A, GISVertex B)
        {
            double d1 = A.x - B.x;
            double d2 = A.y - B.y;
            return Math.Sqrt(d1 * d1 + d2 * d2);
        }

        public static double PointToSegment(GISVertex A, GISVertex B, GISVertex C)
        {
            double dot1 = DotProduct(A, B, C);
            if (dot1 > 0) return Distance(B, C);
            double dot2 = DotProduct(B, A, C);
            if (dot2 > 0) return Distance(A, C);
            double dist = CrossProduct(A, B, C) / Distance(A, B);
            return Math.Abs(dist);
        }
        // 判断射线与线段的关系一相交返回1，不相交返回0，射线起点在线段上返回-1
        public static int IntersactCount(GISVertex p1, GISVertex p2, GISVertex mp)
        {
            double minX = Math.Min(p1.x, p2.x);
            double minY = Math.Min(p1.y, p2.y);
            double maxX = Math.Max(p1.x, p2.x);
            double maxY = Math.Max(p1.y, p2.y);
            if (mp.x > maxX || mp.y > maxY || mp.y < minY)
                return 0;
            if (p1.y == p2.y)
            {
                if (mp.x > minX && mp.x < maxX)//水平线段，则点在线段上返回一1，否则返回0
                    return -1;
                else
                    return 0;
            }
            double X0 = p1.x + (mp.y - p1.y) * (p2.x - p1.x) / (p2.y - p1.y);
            if (X0 < mp.x)//交点在射线起点左侧(射线与线段不相交) 
                return 0;
            else if (X0 == mp.x)//交点与射线起点相同一点在线段上，返回一1
                return -1;
            else if (mp.y == minY)//如果射线穿过线段下端点则不计数
                return 0;
            else
                return 1;//交点在边的中间点或者上端点，计数
        }
    }

    public abstract class GISSpatial
    {
        public GISVertex centroid;
        public GISExtent extent;

        public abstract void draw(Graphics graphics, GISView view, bool Selected);
        public GISVertex getCentroid()
        {
            return centroid;
        }
        public GISExtent getExtent()
        {
            return extent;
        }

        public GISVertex CalculateCentroid(List<GISVertex> _vertexes)
        {
            double x = 0;
            double y = 0;
            for (int i = 0; i < _vertexes.Count; i++)
            {
                x += _vertexes[i].getX();
                y += _vertexes[i].getY();
            }
            return new GISVertex(x / _vertexes.Count, y / _vertexes.Count);
        }
        public GISExtent CalculateExtent(List<GISVertex> _vertexes)
        {
            double minx = Double.MaxValue;
            double miny = Double.MaxValue;
            double maxx = Double.MinValue;
            double maxy = Double.MinValue;
            for (int i = 0; i < _vertexes.Count; i++)
            {
                if (_vertexes[i].getX() < minx) minx = _vertexes[i].getX();
                if (_vertexes[i].getX() > maxx) maxx = _vertexes[i].getX();
                if (_vertexes[i].getY() < miny) miny = _vertexes[i].getY();
                if (_vertexes[i].getY() > maxy) maxy = _vertexes[i].getY();
            }
            return new GISExtent(new GISVertex(minx, miny), new GISVertex(maxx, maxy));
        }
        public Point[] GetScreenPoints(List<GISVertex> _vertexes, GISView view)
        {
            Point[] points = new Point[_vertexes.Count];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = view.ToScreenPoint(_vertexes[i]);
            }
            return points;
        }

    }

    public class GISExtent
    {
        GISVertex upright;
        GISVertex bottomleft;

        public double MinX;
        public double MaxX;
        public double MinY;
        public double MaxY;
        public double Width;
        public double Height;
        public GISVertex MapCenter;

        public GISExtent(GISVertex _bottomleft, GISVertex _upright)
        {
            SetValue(_bottomleft, _upright);
        }
        public bool Outside(GISExtent extent)
        {
            return MaxX < extent.MinX || MinX > extent.MaxX ||
                MaxY < extent.MinY || MinY > extent.MaxY;
        }
        public void CopyFrom(GISExtent extent)
        {
            GISVertex _bottomleft = new GISVertex(extent.MinX, extent.MinY);
            GISVertex _upright = new GISVertex(extent.MaxX, extent.MaxY);
            SetValue(_bottomleft, _upright);
        }
        public bool Include(GISExtent extent)
        {
            return (MinX < extent.MinX) && (MaxX > extent.MaxX)
                && (MinY < extent.MinY) && (MaxY > extent.MaxY);
        }
        public bool WithInDistance(GISVertex vertex, double dist)
        {
            return (((vertex.x + dist) > MinX) &&
               ((vertex.y + dist) > MinY) &&
               ((vertex.x - dist) < MaxX) &&
               ((vertex.y - dist) < MaxY));
        }
        public void SetMapCenter(GISVertex v)
        {
            SetValue(new GISVertex(v.x - Width / 2, v.y - Height / 2),
                new GISVertex(v.x + Width / 2, v.y + Height / 2));
        }
        public void SetValue(GISVertex _bottomleft, GISVertex _upright)
        {
            upright = _upright;
            bottomleft = _bottomleft;
            MinX = bottomleft.getX();
            MinY = bottomleft.getY();
            MaxX = upright.getX();
            MaxY = upright.getY();
            Width = upright.getX() - bottomleft.getX();
            Height = upright.getY() - bottomleft.getY();
            MapCenter=new GISVertex((MinX + MaxX) / 2, (MinY + MaxY) / 2);
        }
        public void ZoomIn()
        {
            double newminx = MinX + Width / 4;
            double newminy = MinY + Height / 4;
            double newmaxx = MaxX - Width / 4;
            double newmaxy = MaxY - Height / 4;
            SetValue(new GISVertex(newminx, newminy), new GISVertex(newmaxx, newmaxy));
        }
        public void ZoomOut()
        {
            double newminx = MinX - Width / 2;
            double newminy = MinY - Height / 2;
            double newmaxx = MaxX + Width / 2;
            double newmaxy = MaxY + Height / 2;
            SetValue(new GISVertex(newminx, newminy), new GISVertex(newmaxx, newmaxy));
        }
        public void MoveUp()
        {
            double newminy = MinY - 2;
            double newmaxy = MaxY - 2;
            SetValue(new GISVertex(MinX, newminy), new GISVertex(MaxX, newmaxy));
        }
        public void MoveDown()
        {
            double newminy = MinY + 2;
            double newmaxy = MaxY + 2;
            SetValue(new GISVertex(MinX, newminy), new GISVertex(MaxX, newmaxy));
        }
        public void MoveLeft()
        {
            double newminx = MinX +2;
            double newmaxx = MaxX + 2;
            SetValue(new GISVertex(newminx, MinY), new GISVertex(newmaxx, MaxY));
        }
        public void MoveRight()
        {
            double newminx = MinX - 2;
            double newmaxx = MaxX - 2;
            SetValue(new GISVertex(newminx, MinY), new GISVertex(newmaxx, MaxY));
        }
        public void Merge(GISExtent extent)
        {
            double newminx = Math.Min(MinX, extent.MinX);
            double newminy = Math.Min(MinY, extent.MinY);
            double newmaxx = Math.Max(MaxX, extent.MaxX);
            double newmaxy = Math.Max(MaxY, extent.MaxY);
            SetValue(new GISVertex(newminx, newminy), new GISVertex(newmaxx, newmaxy));
        }
    }

    public class GISPoint : GISSpatial
    {
        public GISVertex Location;
        public GISPoint(GISVertex onevertex)
        {
            Location = onevertex;
            centroid = onevertex;
            extent = new GISExtent(onevertex, onevertex);
        }
        public override void draw(Graphics graphics, GISView view, bool Selected)
        {
            Point screenpoint = view.ToScreenPoint(Location);
            graphics.FillEllipse(new SolidBrush(Selected ? Color.Red : Color.Black), 
                new Rectangle(screenpoint.X - 3, screenpoint.Y - 3, 6, 6));
        }
        public double Distance(GISVertex anothervertex)
        {
            return Location.Distance(anothervertex);
        }
    }

    public class GISLine : GISSpatial
    {
        public List<GISVertex> Vertexes;
        public GISLine(List<GISVertex> _vertexes)
        {
            Vertexes = _vertexes;
            centroid = CalculateCentroid(Vertexes);
            extent = CalculateExtent(Vertexes);
        }
        public override void draw(Graphics graphics, GISView view, bool Selected)
        {
            Point[] points=GetScreenPoints(Vertexes,view);
            graphics.DrawCurve(new Pen(Selected ? Color.Red : Color.Green, 2), points);
        }
        public double Distance(GISVertex vertex)
        {
            double distance = Double.MaxValue;
            for (int i = 0; i < Vertexes.Count - 1; i++)
            {
                distance = Math.Min(GISVertex.PointToSegment(Vertexes[i], Vertexes[i + 1], vertex), distance);
            }
            return distance;
        }
        public double Length()
        {
            double length = 0;
            for (int i = 0; i < Vertexes.Count-1; i++)
            {
                length += Vertexes[i].Distance(Vertexes[i + 1]);
            }
            return length;
        }
    }

    public class GISPolygon : GISSpatial
    {
        public List<GISVertex> Vertexes;
        public GISPolygon(List<GISVertex> _vertexes)
        {
            Vertexes = _vertexes;
            centroid = CalculateCentroid(_vertexes);
            extent = CalculateExtent(_vertexes);
        }
        public override void draw(Graphics graphics, GISView view, bool Selected)
        {
            Point[] points = GetScreenPoints(Vertexes, view);
            graphics.FillPolygon(new SolidBrush(Selected ? Color.Red : Color.Yellow), points);
            graphics.DrawPolygon(new Pen(Color.Black, 2), points);
        }
        public bool Include(GISVertex vertex)
        {
            int count = 0;
            int sum = 0;
            for (int i = 0; i < Vertexes.Count - 1; i++)
            {
                count = GISVertex.IntersactCount(Vertexes[i], Vertexes[i + 1], vertex);
                if (count < 0) return true;
                sum += count;
            }
            count = GISVertex.IntersactCount(Vertexes[Vertexes.Count - 1], Vertexes[0], vertex);
            if (count < 0) return true;
            sum += count;
            return sum%2!=0;
        }
        public double Area()
        {
            double area = 0;
            for (int i = 0; i < Vertexes.Count - 1; i++)
            {
                area += Vertexes[i].getX() * Vertexes[i + 1].getY() - Vertexes[i].getY() * Vertexes[i + 1].getX();
            }
            area += Vertexes[Vertexes.Count - 1].getX() * Vertexes[0].getY() - Vertexes[Vertexes.Count - 1].getY() * Vertexes[0].getX();
            return Math.Abs(area / 2);
        }
    }

    public class GISView
    {
        public GISExtent CurrentMapExtent;
        public Rectangle MapWindowSize;
        double ScaleX;
        double ScaleY;

        public GISView(GISExtent _extent, Rectangle _rectangle)
        {
            SetValue(_extent, _rectangle);
        }
        public GISExtent RectToExtent(Rectangle rect)
        {
            GISVertex bottomleft = ToMapVertex(new Point(rect.X, rect.Y + rect.Height));
            GISVertex upright = ToMapVertex(new Point(rect.X + rect.Width, rect.Y));
            return new GISExtent(bottomleft, upright);
        }
        public double ToMapDistance(int screendistance)
        {
            return screendistance * (ScaleX + ScaleY) / 2;
        }

        public void SetValue(GISExtent _extent, Rectangle _rectangle)
        {
            CurrentMapExtent = _extent;
            MapWindowSize = _rectangle;
            ScaleX = CurrentMapExtent.Width / MapWindowSize.Width;
            ScaleY = CurrentMapExtent.Height / MapWindowSize.Height;
            ScaleX = Math.Max(ScaleX, ScaleY);
            ScaleY = ScaleX;
        }

        public Point ToScreenPoint(GISVertex onevertex)
        {
            double ScreenX = MapWindowSize.Width / 2 - (CurrentMapExtent.MapCenter.x - onevertex.x) / ScaleX;
            double ScreenY = MapWindowSize.Height / 2 - (onevertex.y - CurrentMapExtent.MapCenter.y) / ScaleY;
            return new Point((int)ScreenX, (int)ScreenY);
        }

        public GISVertex ToMapVertex(Point point)
        {
            double MapX = CurrentMapExtent.MapCenter.x - (MapWindowSize.Width / 2 - point.X) * ScaleX;
            double MapY = CurrentMapExtent.MapCenter.y + (MapWindowSize.Height / 2 - point.Y) * ScaleY;
            return new GISVertex(MapX, MapY);
        }
    }
    
    public enum SHAPETYPE
    {
        POINT, LINE, POLYGON
    };

    public class GISMyFile
    {
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct MyFileHeader
        {
            public double MinX, MinY, MaxX, MaxY;
            public int FeatureCount, DrawAttributeOrNot, ShapeType, FieldCount;
        };

        static byte[] ToBytes(object c)
        {
            byte[] bytes = new byte[Marshal.SizeOf(c.GetType())];
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            Marshal.StructureToPtr(c, handle.AddrOfPinnedObject(), false);
            handle.Free();
            return bytes;
        }

        static int ShapeTypeToInt(SHAPETYPE st)
        {
            if (st == SHAPETYPE.POINT) return 1;
            else if (st == SHAPETYPE.LINE) return 2;
            else if (st == SHAPETYPE.POLYGON) return 3;
            else return 0;
        }

        static void WriteFileHeader(GISLayer layer, BinaryWriter bw)
        {
            MyFileHeader mfh = new MyFileHeader();
            mfh.MinX = layer.Extent.MinX;
            mfh.MinY = layer.Extent.MinY;
            mfh.MaxX = layer.Extent.MaxX;
            mfh.MaxY = layer.Extent.MaxY;
            mfh.FeatureCount = layer.Features.Count;
            mfh.DrawAttributeOrNot = layer.DrawAttributeOrNot ? 1 : 0;
            mfh.ShapeType = ShapeTypeToInt(layer.ShapeType);
            mfh.FieldCount = layer.Fields.Count;
            bw.Write(ToBytes(mfh));
        }
 
        static void WriteString(string s, BinaryWriter bw)
        {
            bw.Write(s.Length);
            byte[] sbytes = Encoding.Default.GetBytes(s);
            bw.Write(sbytes);
        }

        static int TypeToInt(Type type)
        {
            if (type == typeof(Int32) || type == typeof(int)) return 1;
            else if (type == typeof(Decimal) || type == typeof(decimal) || type == typeof(double) || type == typeof(Double)) return 2;
            else if (type == typeof(String) || type == typeof(string)) return 3;
            else return 0;
        }

        static void WriteFields(List<GISField> fields, BinaryWriter bw)
        {
            for (int fieldindex = 0; fieldindex < fields.Count; fieldindex++)
            {
                GISField field = fields[fieldindex];
                bw.Write(TypeToInt(field.datatype));
                WriteString(field.name, bw);
            }
        }

        static void WriteVertex(GISVertex v, BinaryWriter bw)
        {
            bw.Write(v.getX());
            bw.Write(v.getY());
        }

        static void WriteFeatures(GISLayer layer, BinaryWriter bw)
        {
            for (int featureindex = 0; featureindex < layer.Features.Count; featureindex++)
            {
                GISFeature feature = layer.Features[featureindex];
                if (layer.ShapeType == SHAPETYPE.POINT)
                {
                    WriteVertex(((GISPoint)feature.spatialpart).Location, bw);
                }
                else if (layer.ShapeType == SHAPETYPE.LINE)
                {
                    GISLine line = (GISLine)(feature.spatialpart);
                    bw.Write(line.Vertexes.Count);
                    for (int vc = 0; vc < line.Vertexes.Count; vc++)
                        WriteVertex(line.Vertexes[vc],bw);
                }
                else if (layer.ShapeType == SHAPETYPE.POLYGON)
                {
                    GISPolygon polygon = (GISPolygon)(feature.spatialpart);
                    bw.Write(polygon.Vertexes.Count);
                    for (int vc = 0; vc < polygon.Vertexes.Count; vc++)
                        WriteVertex(polygon.Vertexes[vc], bw);
                }

                for (int fieldindex = 0; fieldindex < layer.Fields.Count; fieldindex++)
                {
                    GISField field = layer.Fields[fieldindex];
                    int tint = TypeToInt(field.datatype);
                    if (tint == 1) bw.Write((int)(feature.getAttributeValue(fieldindex)));
                    else if (tint == 2) bw.Write(Convert.ToDouble(feature.getAttributeValue(fieldindex)));
                    else if (tint == 3) WriteString(feature.getAttributeValue(fieldindex).ToString(), bw);
                }
            }
        }
        public static void WriteFile(GISLayer layer, string filename)
        {
            FileStream fsr = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fsr);
            WriteFileHeader(layer,bw);
            WriteString(layer.Name,bw);
            WriteFields(layer.Fields, bw);
            WriteFeatures(layer, bw);
            //其它写入内容
            bw.Close();
            fsr.Close();
        }

//  IntToShapeType：用于把读到的整数转成特定的空间实体类型。
//	ReadFileHeader：用于读我们自定义文件的第一部分，文件头。
//	ReadString：从文件中读一个字符串。
//	IntToType：用于把读到的整数转成特定的数据类型。
//	ReadFields：用于从文件中读取字段信息。
//	ReadVertex：用于专门从文件中读一个GISVertex实例。
//	ReadFeatures：用于读文件中所有GISFeatures的空间及属性值。
        static SHAPETYPE IntToShapeType(int st)
        {
            if (st == 1) return SHAPETYPE.POINT;
            else if (st == 2) return SHAPETYPE.LINE;
            else if (st == 3) return SHAPETYPE.POLYGON;
            else return SHAPETYPE.POINT;
        }

        static Object FromBytes(BinaryReader br, Type type)
        {
            byte[] buff = br.ReadBytes(Marshal.SizeOf(type));
            GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);
            Object result = Marshal.PtrToStructure(handle.AddrOfPinnedObject(), type);
            handle.Free();
            return result;
        }

        static string ReadString(BinaryReader br)
        {
            int length = br.ReadInt32();
            byte[] sbytes = br.ReadBytes(length);
            return Encoding.Default.GetString(sbytes);
        }

        static Type IntToType(int oneint)
        {
            if (oneint == 1) return typeof(int);
            else if (oneint == 2) return typeof(double);
            else if (oneint == 3) return typeof(string);
            else return null;
        }

        static List<GISField> ReadFields(BinaryReader br, int FieldCount)
        {
            List<GISField> fields = new List<GISField>();
            for (int fieldindex = 0; fieldindex < FieldCount; fieldindex++)
            {
                Type fieldtype = IntToType(br.ReadInt32());
                string fieldname = ReadString(br);
                fields.Add(new GISField(fieldtype, fieldname));
            }
            return fields;
        }

        static GISVertex ReadVertex(BinaryReader br)
        {
            return new GISVertex(br.ReadDouble(), br.ReadDouble());
        }

        static List<GISFeature> ReadFeatures(BinaryReader br, SHAPETYPE ShapeType, List<GISField> Fields, int FeatureCount)
        {
            List<GISFeature> Features = new List<GISFeature>();
            for (int featureindex = 0; featureindex < FeatureCount; featureindex++)
            {
                GISFeature feature = new GISFeature(null,null);
                if (ShapeType == SHAPETYPE.POINT)
                {
                    GISVertex _location = ReadVertex(br);
                    feature.spatialpart = new GISPoint(_location);
                }
                else if (ShapeType == SHAPETYPE.LINE)
                {
                    List<GISVertex> vs = new List<GISVertex>();
                    int vcount = br.ReadInt32();
                    for (int vc = 0; vc < vcount; vc++)
                        vs.Add(ReadVertex(br));
                    feature.spatialpart = new GISLine(vs);
                }
                else if (ShapeType == SHAPETYPE.POLYGON)
                {
                    List<GISVertex> vs = new List<GISVertex>();
                    int vcount = br.ReadInt32();
                    for (int vc = 0; vc < vcount; vc++)
                        vs.Add(ReadVertex(br));
                    feature.spatialpart = new GISPolygon(vs);
                }
                GISAttribute ga = new GISAttribute();
                for (int fieldindex = 0; fieldindex < Fields.Count; fieldindex++)
                {
                    GISField field = Fields[fieldindex];
                    if (field.datatype == typeof(int))
                        ga.AddValue(br.ReadInt32());
                    else if (field.datatype == typeof(double))
                        ga.AddValue(br.ReadDouble());
                    else if (field.datatype == typeof(string))
                        ga.AddValue(ReadString(br));
                }
                feature.attributepart = ga;
                Features.Add(feature);
            }
            return Features;
        }

        public static GISLayer ReadFile(string filename)
        {
            FileStream fsr = new FileStream(filename, FileMode.Open);
            BinaryReader br = new BinaryReader(fsr);
            MyFileHeader mfh = (MyFileHeader)(FromBytes(br, typeof(MyFileHeader)));
            SHAPETYPE ShapeType=IntToShapeType(mfh.ShapeType);
            GISExtent Extent=new GISExtent(new GISVertex(mfh.MinX,mfh.MinY),new GISVertex(mfh.MaxX,mfh.MaxY));
            string layername = ReadString(br);
            List<GISField> Fields = ReadFields(br,mfh.FieldCount);
            GISLayer layer = new GISLayer(layername, ShapeType, Extent, Fields);
            layer.DrawAttributeOrNot = (mfh.DrawAttributeOrNot == 1);
            layer.Features = ReadFeatures(br, ShapeType, Fields, mfh.FeatureCount);
            br.Close();
            fsr.Close();
            return layer;
        }
    }

    public class GISLayer
    {
        public string Name;
        public GISExtent Extent;
        public List<GISFeature> Features = new List<GISFeature>();
        public bool DrawAttributeOrNot=false;
        public SHAPETYPE ShapeType;
        public List<GISField> Fields;
        public static int MINIMUMSCREENDISTANCE = 3;

        public GISLayer(string _name, SHAPETYPE _shapetype, GISExtent _extent,List<GISField> _fields)
        {
            Name = _name;
            ShapeType = _shapetype;
            Extent = _extent;
            Fields = _fields;
        }
        public void ClearSelection()
        {
            for (int i = 0; i < Features.Count; i++)
                Features[i].Selected = false;
        }
        public void draw(Graphics graphics, GISView view, GISExtent extent)
        {
            for (int i = 0; i < Features.Count; i++)
            {
                if (Features[i].spatialpart.extent.Outside(extent) == false)
                    Features[i].draw(graphics, view, DrawAttributeOrNot, 0);
            }
        }
        public GISFeature SelectPointByVertex(GISVertex vertex, double mindist)
        {
            Double distance = Double.MaxValue;
            int id = -1;
            for (int i = 0; i < Features.Count; i++)
            {
                if (Features[i].spatialpart.extent.WithInDistance(vertex, mindist) == false) continue;
                GISPoint point = (GISPoint)(Features[i].spatialpart);
                double dist = point.Distance(vertex);
                if (dist < distance)
                {
                    distance = dist;
                    id = i;
                }
            }
            return (distance < mindist) ? Features[id] : null;
        }

        public GISFeature SelectLineByVertex(GISVertex vertex, double mindist)
        {
            Double distance = Double.MaxValue;
            int id = -1;
            for (int i = 0; i < Features.Count; i++)
            {
                if (Features[i].spatialpart.extent.WithInDistance(vertex, mindist) == false) continue;
                GISLine line = (GISLine)(Features[i].spatialpart);
                double dist = line.Distance(vertex);
                if (dist < distance)
                {
                    distance = dist;
                    id = i;
                }
            }
            return (distance < mindist) ? Features[id] : null;
        }
        public GISFeature SelectPolygonByVertex(GISVertex vertex)
        {
            for (int i = 0; i < Features.Count; i++)
            {
                if (Features[i].spatialpart.extent.WithInDistance(vertex, 0) == false) continue;
                GISPolygon polygon = (GISPolygon)(Features[i].spatialpart);
                if (polygon.Include(vertex)) return Features[i];
            }
            return null;
        }


        public GISFeature SelectByClick(Point mousepoint, GISView view)
        {
            GISFeature feature = null;
            GISVertex vertex = view.ToMapVertex(mousepoint);
            double mindist = view.ToMapDistance(MINIMUMSCREENDISTANCE);
            if (ShapeType == SHAPETYPE.POINT)
                feature=SelectPointByVertex(vertex, mindist);
            if (ShapeType == SHAPETYPE.LINE)
                feature =SelectLineByVertex(vertex, mindist);
            if (ShapeType == SHAPETYPE.POLYGON)
                feature =SelectPolygonByVertex(vertex);
            return feature;
        }
        public List<GISFeature> SelectByExtent(GISExtent extent)
        {
            List<GISFeature> features = new List<GISFeature>();
            for (int i = 0; i < Features.Count; i++)
                if (extent.Include(Features[i].spatialpart.extent)) 
                    features.Add(Features[i]);
            return features;
        }
    }

    public class GISShapefile
    {
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        struct ShapeFileHeader
        {
            public int Unused1, Unused2, Unused3, Unused4;
            public int Unused5, Unused6, Unused7, Unused8;
            public int ShapeType;
            public double Xmin;
            public double Ymin;
            public double Xmax;
            public double Ymax;
            public double Unused9, Unused10, Unused11, Unused12;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        struct RecordHeader
        {
            public int RecordNumber;
            public int RecordLength;
            public int ShapeType;
        };

        static ShapeFileHeader ReadFileHeader(BinaryReader br)
        {
            byte[] buff = br.ReadBytes(Marshal.SizeOf(typeof(ShapeFileHeader)));
            GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);
            ShapeFileHeader header = (ShapeFileHeader)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(ShapeFileHeader));
            handle.Free();
            return header;
        }

        static RecordHeader ReadRecordHeader(BinaryReader br)
        {
            byte[] buff = br.ReadBytes(Marshal.SizeOf(typeof(RecordHeader)));
            GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);
            RecordHeader header = (RecordHeader)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(RecordHeader));
            handle.Free();
            return header;
        }

        static DataTable ReadDBF(string dbffilename)
        {
            OdbcConnection conn = new OdbcConnection(@"Driver={Microsoft Visual FoxPro Driver};SourceType=DBF;SourceDB=" +
                              dbffilename + ";Exclusive=No;NULL=NO; Collate=Machine; BACKGROUNDFETCH=NO; DELETED=NO");
            conn.Open();
            OdbcCommand command = new OdbcCommand("select * from " + dbffilename, conn);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            conn.Close();
            return table;
        }
        static List<GISField> ReadFields(DataTable table)
        {
            List<GISField> fields=new List<GISField>();
            foreach (DataColumn column in table.Columns)
            {
                fields.Add(new GISField(column.DataType, column.ColumnName));
            }
            return fields;
        }
        static GISAttribute ReadAttribute(DataTable table, int RowIndex)
        {
            GISAttribute attribute = new GISAttribute();
            DataRow row = table.Rows[RowIndex];
            for (int i = 0; i < table.Columns.Count; i++)
            {
                attribute.AddValue(row[i]);
            }
            return attribute;
        }
        public static GISLayer ReadShapeFile(string shpfilename)
        {
            FileStream fsr = new FileStream(shpfilename, FileMode.Open);
            BinaryReader br = new BinaryReader(fsr);
            ShapeFileHeader sfh = ReadFileHeader(br);
            SHAPETYPE ShapeType = SHAPETYPE.POINT;
            if (sfh.ShapeType == 1) ShapeType = SHAPETYPE.POINT;
            else if (sfh.ShapeType == 3) ShapeType = SHAPETYPE.LINE;
            else if (sfh.ShapeType == 5) ShapeType = SHAPETYPE.POLYGON;
            else return null;
            GISExtent extent = new GISExtent(new GISVertex(sfh.Xmin, sfh.Ymin), new GISVertex(sfh.Xmax, sfh.Ymax));

            string dbffilename = shpfilename.Replace(".shp", ".dbf");
            DataTable table = ReadDBF(dbffilename);
            GISLayer layer = new GISLayer(shpfilename, ShapeType, extent,ReadFields(table));
            int rowindex = 0;
            while (br.PeekChar() != -1)
            {
                RecordHeader rh = ReadRecordHeader(br);
                int RecordLength = FromBigToLittle(rh.RecordLength);
                byte[] RecordContent = br.ReadBytes(RecordLength*2-4);
                if (ShapeType == SHAPETYPE.POINT)
                {
                    GISPoint onepoint = ReadPoint(RecordContent);
                    GISFeature onefeature = new GISFeature(onepoint, ReadAttribute(table,rowindex));
                    layer.Features.Add(onefeature);
                }
                if (ShapeType == SHAPETYPE.LINE)
                {
                    List<GISLine> lines = ReadLines(RecordContent);
                    for (int i = 0; i < lines.Count; i++)
                    {
                        GISFeature onefeature = new GISFeature(lines[i], ReadAttribute(table, rowindex));
                        layer.Features.Add(onefeature);
                    }
                }
                if (ShapeType == SHAPETYPE.POLYGON)
                {
                    List<GISPolygon> polygons = ReadPolygons(RecordContent);
                    for (int i = 0; i < polygons.Count; i++)
                    {
                        GISFeature onefeature = new GISFeature(polygons[i], ReadAttribute(table, rowindex));
                        layer.Features.Add(onefeature);
                    }
                }
                rowindex++;

                //其它代码,用于处理RecordContent
            }
            br.Close();
            fsr.Close();
            return layer;
        }

        static GISPoint ReadPoint(byte[] RecordContent)
        {
            double x = BitConverter.ToDouble(RecordContent, 0);
            double y = BitConverter.ToDouble(RecordContent, 8);
            return new GISPoint(new GISVertex(x, y));
        }

        static List<GISPolygon> ReadPolygons(byte[] RecordContent)
        {
            int N = BitConverter.ToInt32(RecordContent, 32);
            int M = BitConverter.ToInt32(RecordContent, 36);
            int[] parts = new int[N + 1];

            for (int i = 0; i < N; i++)
            {
                parts[i] = BitConverter.ToInt32(RecordContent, 40 + i * 4);
            }
            parts[N] = M;
            List<GISPolygon> polygons = new List<GISPolygon>();
            for (int i = 0; i < N; i++)
            {
                List<GISVertex> vertexs = new List<GISVertex>();
                for (int j = parts[i]; j < parts[i + 1]; j++)
                {
                    double x = BitConverter.ToDouble(RecordContent, 40 + N * 4 + j * 16);
                    double y = BitConverter.ToDouble(RecordContent, 40 + N * 4 + j * 16 + 8);
                    vertexs.Add(new GISVertex(x, y));
                }
                polygons.Add(new GISPolygon(vertexs));
            }
            return polygons;
        }

        static List<GISLine> ReadLines(byte[] RecordContent)
        {
            int N = BitConverter.ToInt32(RecordContent, 32);
            int M = BitConverter.ToInt32(RecordContent, 36);
            int[] parts = new int[N+1];
            
            for (int i = 0; i < N; i++)
            {
                parts[i] = BitConverter.ToInt32(RecordContent, 40 + i * 4);
            }    
            parts[N]=M;
            List<GISLine> lines = new List<GISLine>();
            for (int i=0;i<N;i++)
            {
                List<GISVertex> vertexs=new List<GISVertex>();
                for (int j=parts[i];j<parts[i+1];j++)
                {
                    double x=BitConverter.ToDouble(RecordContent,40+N*4+j*16);
                    double y=BitConverter.ToDouble(RecordContent,40+N*4+j*16+8);
                    vertexs.Add(new GISVertex(x,y));
                }
                lines.Add(new GISLine(vertexs));
            }
            return lines;
        }

        static int FromBigToLittle(int bigvalue)
        {
            byte[] bigbytes = new byte[4];
            GCHandle handle = GCHandle.Alloc(bigbytes, GCHandleType.Pinned);
            Marshal.StructureToPtr(bigvalue, handle.AddrOfPinnedObject(), false);
            handle.Free();
            byte b2 = bigbytes[2];
            byte b3 = bigbytes[3];
            bigbytes[3] = bigbytes[0];
            bigbytes[2] = bigbytes[1];
            bigbytes[1] = b2;
            bigbytes[0] = b3;
            return BitConverter.ToInt32(bigbytes, 0);
        }
    }
    public class Line
    {
        public PointF StartNode;
        public PointF EndNode;
        public Boolean drawornot;
        public Line()
        {
            this.StartNode = new PointF(0, 0);
            this.EndNode = new PointF(0, 0);
            this.drawornot = false;
        }
        public void draw(Graphics g, Color cl)
        {
            g.DrawLine(new Pen(cl), this.StartNode, this.EndNode);
        }
        public Boolean equals(Line l)
        {
            if ((this.StartNode == l.StartNode && this.EndNode == l.EndNode) || (this.StartNode == l.EndNode && this.EndNode == l.StartNode))
                return true;
            else
                return false;
        }
    }
    public class Delaunay3
    {
        public PointF[] pt = new PointF[3];
        public PointF center;
        private PointF p;
        public float r;
        private int k, n, m;
        public Line[] sjb = new Line[3];
        public Delaunay3(PointF p1, PointF p2, PointF p3)
        {
            this.pt[0] = p1;
            this.pt[1] = p2;
            this.pt[2] = p3;
            PointF[] _pt = new PointF[3];
            _pt = pt;
            center.X = ((pt[2].Y - pt[1].Y) / 2 + (pt[0].X - pt[1].X) * (pt[0].X + pt[1].X) / (pt[1].Y - pt[0].Y) * 0.5F - (pt[0].X - pt[2].X) * (pt[0].X + pt[2].X) / (pt[2].Y - pt[0].Y) * 0.5F) / ((pt[0].X - pt[1].X) / (pt[1].Y - pt[0].Y) - (pt[0].X - pt[2].X) / (pt[2].Y - pt[0].Y));
            center.Y = (pt[0].X - pt[1].X) / (pt[1].Y - pt[0].Y) * (center.X - (pt[0].X + pt[1].X) / 2) + (pt[0].Y + pt[1].Y) / 2;
            r = Convert.ToSingle(distance(center, 1));
            for (int i = 0; i < 2; i++)
            {
                for (int j = i + 1; j < 3; j++)
                {
                    if (_pt[i].Y < _pt[j].Y)
                    {
                        p = _pt[i];
                        _pt[i] = _pt[j];
                        _pt[j] = p;
                    }
                }
            }
            sjb[0] = new Line();
            sjb[1] = new Line();
            sjb[2] = new Line();
            sjb[0].StartNode = _pt[0];
            sjb[0].EndNode = _pt[1];
            sjb[1].StartNode = _pt[1];
            sjb[1].EndNode = _pt[2];
            sjb[2].StartNode = _pt[0];
            sjb[2].EndNode = _pt[2];
        }

        public double distance(PointF v, int i)
        {
            return Math.Sqrt((double)(v.X - pt[i - 1].X) * (v.X - pt[i - 1].X) + (v.Y - pt[i - 1].Y) * (v.Y - pt[i - 1].Y));
        }

        public void droutcir(Graphics g)
        {

            g.FillEllipse(new SolidBrush(Color.Yellow), center.X - 3, center.Y - 3, 7, 7);
            g.DrawEllipse(new Pen(Color.Red, 2), center.X - r, center.Y - r, 2 * r, 2 * r);
        }
        public float DisToCen(PointF v)
        {
            return Convert.ToSingle(Math.Sqrt((double)(v.X - center.X) * (v.X - center.X) + (v.Y - center.Y) * (v.Y - center.Y)));
        }
        public void drawsjx(Graphics g, Color cl)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = i + 1; j < 3; j++)
                {
                    g.DrawLine(new Pen(cl), pt[i], pt[j]);
                }
            }
        }
        public Line Findggb(Delaunay3 de)
        {
            Boolean cz = false;
            Line l = new Line();
            for (k = 0; k < 3; k++)
            {
                if (cz == true)
                    break;
                for (n = 0; n < 3; n++)
                {
                    if (this.sjb[k].StartNode == de.sjb[n].StartNode && this.sjb[k].EndNode == de.sjb[n].EndNode)
                    {
                        cz = true;
                        l = this.sjb[k];
                        break;
                    }
                }
            }
            return l;
        }
        public Boolean Ifggb(Delaunay3 de)
        {
            Boolean cz = false;
            for (k = 0; k < 3; k++)
            {
                if (cz == true)
                    break;
                for (n = 0; n < 3; n++)
                {
                    if (this.sjb[k].StartNode == de.sjb[n].StartNode && this.sjb[k].EndNode == de.sjb[n].EndNode)
                    {
                        cz = true;
                        break;
                    }
                }
            }
            return cz;
        }
        public Boolean equals(Delaunay3 sj)
        {
            m = 0;
            for (k = 0; k < 3; k++)
            {
                for (n = 0; n < 3; n++)
                {
                    if (this.pt[k] == sj.pt[n])
                        m++;
                }
            }
            if (m == 3)
                return true;//代表两个三角形一样
            else
                return false;
        }
        public Boolean HaveLine(Line l)
        {
            for (k = 0; k < 3; k++)
            {
                if (this.pt[k] == l.StartNode)
                {
                    for (n = 0; n < 3; n++)
                        if (this.pt[n] == l.EndNode)
                            return true;
                }
            }
            return false;
        }
        public Boolean contain(PointF v)
        {
            double dis1, dis2, dis3;
            double jd1, jd2, jd3;
            dis1 = Math.Sqrt((this.pt[0].X - v.X) * (this.pt[0].X - v.X) + (this.pt[0].Y - v.Y) * (this.pt[0].Y - v.Y));
            dis2 = Math.Sqrt((this.pt[1].X - v.X) * (this.pt[1].X - v.X) + (this.pt[1].Y - v.Y) * (this.pt[1].Y - v.Y));
            dis3 = Math.Sqrt((this.pt[2].X - v.X) * (this.pt[2].X - v.X) + (this.pt[2].Y - v.Y) * (this.pt[2].Y - v.Y));
            jd1 = Math.Acos((dis1 * dis1 + dis2 * dis2 - ((this.pt[0].X - this.pt[1].X) * (this.pt[0].X - this.pt[1].X) + (this.pt[0].Y - this.pt[1].Y) * (this.pt[0].Y - this.pt[1].Y))) / (2 * dis1 * dis2)) * 180 / Math.PI;
            jd2 = Math.Acos((dis1 * dis1 + dis3 * dis3 - ((pt[0].X - pt[2].X) * (pt[0].X - pt[2].X) + (pt[0].Y - pt[2].Y) * (pt[0].Y - pt[2].Y))) / (2 * dis1 * dis3)) * 180 / Math.PI;
            jd3 = Math.Acos((dis3 * dis3 + dis2 * dis2 - ((pt[2].X - pt[1].X) * (pt[2].X - pt[1].X) + (pt[2].Y - pt[1].Y) * (pt[2].Y - pt[1].Y))) / (2 * dis3 * dis2)) * 180 / Math.PI;
            double jd = jd1 + jd2 + jd3;
            if (jd3 + jd2 + jd1 >= 359.555555)
                return true;
            else
                return false;
        }

    }
}