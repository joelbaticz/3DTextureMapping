using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextureMapping
{
    public partial class Form1 : Form
    {
        public struct Vector3D
        {
            public double x, y, z;
        }

        public struct Vector2D
        {
            public double x, y;
        }

        public struct face
        {
            public int a, b, c;
        }

        public struct BoundingBox
        {
            public Vector3D[] vertex;
            public face[] face;
        }

        private struct VoxelObject
        {





        }

        int frameBufferWidth = 1024;
        int frameBufferHeight = 768;
        Bitmap bitmapFrameBuffer;
        Graphics gFrame;

        double centerX;
        double centerY;
        

        Pen penFace;
        Pen penVertex;
        Brush brushVertex;

        BoundingBox bb;
        

        double perspective = 640;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Magnify(ref BoundingBox bb, double magLevel)
        {
            for (int i = 0; i < bb.vertex.Length; i++)
            {
                bb.vertex[i].x = bb.vertex[i].x * magLevel;
                bb.vertex[i].y = bb.vertex[i].y * magLevel;
                bb.vertex[i].z = bb.vertex[i].z * magLevel;
            }
        }

        private void RotateX(ref BoundingBox bb, double angle)
        {
            double alphaInRads = (double)(angle * Math.PI / 180);
            double cosAngle = (double)Math.Cos(alphaInRads);
            double sinAngle = (double)Math.Sin(alphaInRads);


            for (int i = 0; i < bb.vertex.Length; i++)
            {
                var v = bb.vertex[i];

                double newY = cosAngle * v.y + sinAngle * v.z;
                double newZ = cosAngle * v.z - sinAngle * v.y;

                v.y = newY;
                v.z = newZ;

                bb.vertex[i] = v;
            }
        }

        private void RotateY(ref BoundingBox bb, double angle)
        {
            double alphaInRads = (double)(angle * Math.PI / 180);
            double cosAngle = (double)Math.Cos(alphaInRads);
            double sinAngle = (double)Math.Sin(alphaInRads);


            for (int i = 0; i < bb.vertex.Length; i++)
            {
                var v = bb.vertex[i];

                double newX = cosAngle * v.x + sinAngle * v.z;
                double newZ = cosAngle * v.z - sinAngle * v.x;

                v.x = newX;
                v.z = newZ;

                bb.vertex[i] = v;
            }
        }

        private void RotateZ(ref BoundingBox bb, double angle)
        {
            double alphaInRads = (double)(angle * Math.PI / 180);
            double cosAngle = (double)Math.Cos(alphaInRads);
            double sinAngle = (double)Math.Sin(alphaInRads);


            for (int i = 0; i < bb.vertex.Length; i++)
            {
                var v = bb.vertex[i];

                double newX = cosAngle * v.x + sinAngle * v.y;
                double newY = cosAngle * v.y - sinAngle * v.x;

                v.x = newX;
                v.y = newY;

                bb.vertex[i] = v;
            }
        }

        private void ClearFrameBuffer()
        {
            var bitmapData = bitmapFrameBuffer.LockBits(new Rectangle(0, 0, frameBufferWidth, frameBufferHeight), ImageLockMode.ReadOnly, bitmapFrameBuffer.PixelFormat);
            var length = bitmapData.Width * bitmapData.Height; //It was Stride instead Width

            unsafe
            {
                byte* pointer = (byte*)bitmapData.Scan0;

                for (int y = 0; y < frameBufferHeight; y++)
                {
                    for (int x = 0; x < frameBufferWidth; x++)
                    {
                        pointer[0] = 0;
                        pointer[1] = 0;
                        pointer[2] = 0;
                        pointer[3] = 255;

                        pointer += 4;
                    }
                    pointer += -(frameBufferWidth * 4) + (bitmapData.Stride);// -4;


                }
            }
            bitmapFrameBuffer.UnlockBits(bitmapData);
        }

        private void CreateFrameBuffer()
        {
            bitmapFrameBuffer = new Bitmap(frameBufferWidth, frameBufferHeight);
            pbFrameBuffer.Width = frameBufferWidth;
            pbFrameBuffer.Height = frameBufferHeight;
            pbFrameBuffer.Image = bitmapFrameBuffer;

            ClearFrameBuffer();
        }

        

        private void CreateBoundingBox(ref BoundingBox bb)
        {
            bb.vertex = new Vector3D[8];
            bb.face = new face[12];
        }

        private void ResetVertices(ref BoundingBox bb)
        {
            bb.vertex[0].x = -1;
            bb.vertex[0].y = -1;
            bb.vertex[0].z = -1;

            bb.vertex[1].x = 1;
            bb.vertex[1].y = -1;
            bb.vertex[1].z = -1;

            bb.vertex[2].x = 1;
            bb.vertex[2].y = 1;
            bb.vertex[2].z = -1;

            bb.vertex[3].x = -1;
            bb.vertex[3].y = 1;
            bb.vertex[3].z = -1;

            bb.vertex[4].x = -1;
            bb.vertex[4].y = -1;
            bb.vertex[4].z = 1;

            bb.vertex[5].x = 1;
            bb.vertex[5].y = -1;
            bb.vertex[5].z = 1;

            bb.vertex[6].x = 1;
            bb.vertex[6].y = 1;
            bb.vertex[6].z = 1;

            bb.vertex[7].x = -1;
            bb.vertex[7].y = 1;
            bb.vertex[7].z = 1;
        }

        private void ResetFaces(ref BoundingBox bb)
        {
            bb.face[0].a = 0;
            bb.face[0].b = 1;
            bb.face[0].c = 2;

            bb.face[1].a = 0;
            bb.face[1].b = 2;
            bb.face[1].c = 3;

            bb.face[2].a = 0;
            bb.face[2].b = 4;
            bb.face[2].c = 7;

            bb.face[3].a = 0;
            bb.face[3].b = 7;
            bb.face[3].c = 3;

            bb.face[4].a = 1;
            bb.face[4].b = 5;
            bb.face[4].c = 6;

            bb.face[5].a = 1;
            bb.face[5].b = 6;
            bb.face[5].c = 2;

            bb.face[6].a = 4;
            bb.face[6].b = 5;
            bb.face[6].c = 6;

            bb.face[7].a = 4;
            bb.face[7].b = 6;
            bb.face[7].c = 7;

            bb.face[8].a = 0;// 0;
            bb.face[8].b = 0;// 4;
            bb.face[8].c = 0;// 5;

            bb.face[9].a = 0;// 0;
            bb.face[9].b = 0;// 1;
            bb.face[9].c = 0;// 5;

            bb.face[10].a = 0;//3;
            bb.face[10].b = 0;//7;
            bb.face[10].c = 0;//6;

            bb.face[11].a = 0;//3;
            bb.face[11].b = 0;//6;
            bb.face[11].c = 0;//2;

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            //Set up Frame Buffer
            CreateFrameBuffer();
            centerX = frameBufferWidth / 2;
            centerY = frameBufferHeight / 2;
            gFrame = Graphics.FromImage(bitmapFrameBuffer);

            penFace = new Pen(Color.DodgerBlue, 1);
            penVertex = new Pen(Color.Gold, 4);
            brushVertex = new SolidBrush(Color.Gold);

            bb = new BoundingBox();
            CreateBoundingBox(ref bb);
            ResetVertices(ref bb);
            ResetFaces(ref bb);        

            btnDraw.Enabled = true;

            /*
            using (gFrame)
            {
                gFrame.DrawLine(penFace, 50, 50, 100, 100);
            }
            */

            pbFrameBuffer.Refresh();

        }

        void Swap(ref Vector2D v0, ref Vector2D v1)
        {
            Vector2D temp;

            temp = v0;
            v0 = v1;
            v1 = temp;
        }

        void DrawFlatBottom(Vector2D v0, Vector2D v1, Vector2D v2)
        {
            
            double m0 = (v1.x - v0.x) / (v1.y - v0.y);
            double m1 = (v2.x - v0.x) / (v2.y - v0.y);

            //Calculate scanline Start and End
            int yStart = (int)v0.y;
            int yEnd = (int)v2.y;

            for (int y = yStart; y < yEnd; y++)
            {
                //Calculate horizontal Start and End
                double x0 = m0 * ((double)(y) - yStart) + v0.x;
                double x1 = m1 * ((double)(y) - yStart) + v0.x;

                int xStart = (int)x0;
                int xEnd = (int)x1;

                for (int x = xStart; x < xEnd; x++)
                {
                    bitmapFrameBuffer.SetPixel(x, y, Color.DarkMagenta); //Color.AliceBlue
                }
            }

        }

        void DrawFlatTop(Vector2D v0, Vector2D v1, Vector2D v2)
        {
            double m0 = (v2.x - v0.x) / (v2.y - v0.y);
            double m1 = (v2.x - v1.x) / (v2.y - v0.y);

            //Calculate scanline Start and End
            int yStart = (int)v0.y;
            int yEnd = (int)v2.y;

            for (int y = yStart; y < yEnd; y++)
            {
                //Calculate horizontal Start and End
                double x0 = m0 * ((double)(y) - v0.y) + v0.x;
                double x1 = m1 * ((double)(y) - v0.y) + v1.x;

                int xStart = (int)x0;
                int xEnd = (int)x1;

                for (int x = xStart; x < xEnd; x++)
                {
                    bitmapFrameBuffer.SetPixel(x, y, Color.DarkMagenta); //Color.DarkGray
                }
            }

        }

        void DrawTriangle(Vector2D v0, Vector2D v1, Vector2D v2)
        {
            //Sorting vertices by y
            if (v1.y < v0.y) Swap(ref v0, ref v1);
            if (v2.y < v1.y) Swap(ref v1, ref v2);
            if (v1.y < v0.y) Swap(ref v0, ref v1);
            //if (v2.y < v1.y) Swap(v1, v2);

            //Check flatness

            /*
            if (v0.y == v1.y) //Natural Flat Top
            {
                //Sort vertices by x
                if (v1.x < v0.x) Swap(v0, v1);
                //DrawFlatTop(v0, v1, v2);
            }
            else if (v1.y == v2.y) //Natural Flat Bottom
            {
                //Sort vertices by x
                if (v2.x < v1.x) Swap(v1, v2);
                //DrawFlatBottom(v0, v1, v2);
            }
            else // General Triangle
            */
            {
                double alpha = Math.Abs(v1.y - v0.y) / Math.Abs(v2.y - v0.y);

                Vector2D vi;

                //vi.x = v0.x + (v2.x - v0.x) * alpha;
                vi.x = v0.x + ((v2.x - v0.x) / (v2.y - v0.y) * (v1.y - v0.y));
                vi.y = v1.y;// v0.y + (v2.y - v0.y) * alpha;

                if (vi.x < v1.x)
                {
                    Swap(ref v1, ref vi);
                    //Vector2D temp;
                    //temp = vi;
                    //vi = v1;
                    //v1 = temp;
                }

                DrawFlatBottom(v0, v1, vi);
                DrawFlatTop(v1, vi, v2);

                /*
                if (v1.y < vi.y) //Major right
                {
                    //DrawFlatBottom(v0, vi, v1);
                    //DrawFlatTop(vi, v1, v2);

                }
                else //Major left
                {
                    DrawFlatBottom(v0, v1, vi);
                  //  DrawFlatTop(v1, vi, v2);

                }
                */
            }

        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            ClearFrameBuffer();
            ResetVertices(ref bb);
            Magnify(ref bb, 100.0);
            RotateX(ref bb, trackBar1.Value);
            RotateY(ref bb, trackBar2.Value);
            RotateZ(ref bb, trackBar3.Value);

            /* Draw faces */
            double x;
            double y;
            double z;

            Vector2D v0, v1, v2;
            
            //DRAW FACES            
            for (int i = 0; i < 12; i++)// bb.face.Length; i++)
            {
                //Face A
                x = bb.vertex[bb.face[i].a].x;
                y = bb.vertex[bb.face[i].a].y;
                z = bb.vertex[bb.face[i].a].z;

                v0.x = centerX + x + (z / perspective * (x));
                v0.y = centerY + y + (z / perspective * (y));
                
                //Face B
                x = bb.vertex[bb.face[i].b].x;
                y = bb.vertex[bb.face[i].b].y;
                z = bb.vertex[bb.face[i].b].z;

                v1.x = centerX + x + (z / perspective * (x));
                v1.y = centerY + y + (z / perspective * (y));

                //Face C
                x = bb.vertex[bb.face[i].c].x;
                y = bb.vertex[bb.face[i].c].y;
                z = bb.vertex[bb.face[i].c].z;

                v2.x = centerX + x + (z / perspective * (x));
                v2.y = centerY + y + (z / perspective * (y));

                using (gFrame = Graphics.FromImage(bitmapFrameBuffer))
                {
                    DrawTriangle(v0, v1, v2);
                }
            }

            //DRAW LINES
            for (int i = 0; i < 12; i++)// bb.face.Length; i++)
            {
                //Face A
                x = bb.vertex[bb.face[i].a].x;
                y = bb.vertex[bb.face[i].a].y;
                z = bb.vertex[bb.face[i].a].z;

                v0.x = centerX + x + (z / perspective * (x));
                v0.y = centerY + y + (z / perspective * (y));

                //Face B
                x = bb.vertex[bb.face[i].b].x;
                y = bb.vertex[bb.face[i].b].y;
                z = bb.vertex[bb.face[i].b].z;

                v1.x = centerX + x + (z / perspective * (x));
                v1.y = centerY + y + (z / perspective * (y));

                //Face C
                x = bb.vertex[bb.face[i].c].x;
                y = bb.vertex[bb.face[i].c].y;
                z = bb.vertex[bb.face[i].c].z;

                v2.x = centerX + x + (z / perspective * (x));
                v2.y = centerY + y + (z / perspective * (y));

                using (gFrame = Graphics.FromImage(bitmapFrameBuffer))
                {
                    gFrame.DrawLine(penFace, (int)v0.x, (int)v0.y, (int)v1.x, (int)v1.y);
                    gFrame.DrawLine(penFace, (int)v1.x, (int)v1.y, (int)v2.x, (int)v2.y);
                    gFrame.DrawLine(penFace, (int)v2.x, (int)v2.y, (int)v0.x, (int)v0.y);
                }
            }

            //DRAW VERTICES
            for (int i = 0; i < bb.vertex.Length; i++)// bb.face.Length; i++)
            {
                x = bb.vertex[i].x;
                y = bb.vertex[i].y;
                z = bb.vertex[i].z;

                v0.x = centerX + x + (z / perspective * (x));
                v0.y = centerY + y + (z / perspective * (y));

                using (gFrame = Graphics.FromImage(bitmapFrameBuffer))
                {
                    gFrame.FillEllipse(brushVertex, new Rectangle((int)v0.x - 2, (int)v0.y - 2, 4, 4));
                }
            }

                /* Draw vertices */

                /*
                for (int i = 0; i < bb.vertex.Length; i++)
                {
                    bb.vertex[i].z += 100;
                }
                //If you used Z offset you need to use this formula:
                //xPrime = perspective * (x) / (-z);
                //yPrime = perspective * (y) / (-z);
                //perspective = 6f;// 640f;

                */

                /*
                for (int i = 0; i < bb.vertex.Length; i++)
                {
                    x = bb.vertex[i].x;
                    y = bb.vertex[i].y;
                    z = bb.vertex[i].z;

                    xPrime = x + (z / perspective * (x));
                    yPrime = y + (z / perspective * (y));

                    intX0 = centerX + (int)xPrime;
                    intY0 = centerY + (int)yPrime;

                    //if (intX > 0 && intX < frameBufferWidth && intY > 0 && intY < frameBufferHeight)
                    //    bitmapFrameBuffer.SetPixel(intX, intY, Color.FromArgb(255, 255, 255, 255));

                    using (gFrame = Graphics.FromImage(bitmapFrameBuffer))
                    {
                        gFrame.DrawEllipse(penVertex, new Rectangle(intX0 - 2, intY0 - 2, 4, 4));
                    }
                }
                */

                //Vector2D p0, p1, p2;
                //p0.x = 20;
                //p0.y = 20;
                //p1.x = 100;
                //p1.y = 100;
                //p2.x = 50;
                //p2.y = 200;
                //
                //p0.x = 50;
                //p0.y = 50;
                //p1.x = 100;
                //p1.y = 50;
                //p2.x = 75;
                //p2.y = 200;
                //DrawTriangle(p0, p1, p2);

                pbFrameBuffer.Refresh();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            btnDraw_Click(sender, e);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {

            //RotateY(ref bb, trackBar2.Value);
            btnDraw_Click(sender, e);
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            //RotateZ(ref bb, trackBar3.Value);
            btnDraw_Click(sender, e);
        }
    }
}
