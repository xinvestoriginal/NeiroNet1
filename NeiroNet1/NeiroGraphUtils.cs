using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeiroNet1
{
    class NeiroGraphUtils
    {

        public static void ClearImage(PictureBox pictureBox)
        {
            pictureBox.Image = (Image)new Bitmap(pictureBox.Width, pictureBox.Height);
        }

        public static int[,] GetArrayFromBitmap(Bitmap image, bool message = false)
        {
            int[,] res = new int[image.Width, image.Height];
            for (int n = 0; n < res.GetLength(0); n++)
                for (int m = 0; m < res.GetLength(1); m++)
                {
                    int color = (image.GetPixel(n, m).R + image.GetPixel(n, m).G + image.GetPixel(n, m).B) / 3;
                    res[n, m] = color > 0 ? 1 : 0;
                }
            return res;
        }

        public static Image DrawLitera(Image bmp, string l)
        {
            Font myFont = new Font("Arial", 40f);           
            using (Graphics g = Graphics.FromImage(bmp))
            {
                SizeF size = g.MeasureString(l, myFont);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawString(l, myFont, new SolidBrush(Color.Black), Point.Empty);
            }
            return bmp;
        }

        public static Bitmap GetBitmapFromArr(int[,] array)
        {
            Bitmap bitmap = new Bitmap(array.GetLength(0), array.GetLength(1));
            for (int x = 0; x < array.GetLength(0); x++)
                for (int y = 0; y < array.GetLength(1); y++)
                    if (array[x, y] == 0)
                        bitmap.SetPixel(x, y, Color.White);
                    else
                        bitmap.SetPixel(x, y, Color.Black);
            return bitmap;
        }

        public static int[,] CutImageToArray(Bitmap b, Point max)
        {
            int x1 = 0;
            int y1 = 0;
            int x2 = max.X;
            int y2 = max.Y;
            
            for (int y = 0; y < b.Height && y1 == 0; y++)
                for (int x = 0; x < b.Width && y1 == 0; x++)
                    if (b.GetPixel(x, y).ToArgb() != 0) y1 = y;
            for (int y = b.Height - 1; y >= 0 && y2 == max.Y; y--)
                for (int x = 0; x < b.Width && y2 == max.Y; x++)
                    if (b.GetPixel(x, y).ToArgb() != 0) y2 = y;
            for (int x = 0; x < b.Width && x1 == 0; x++)
                for (int y = 0; y < b.Height && x1 == 0; y++)
                    if (b.GetPixel(x, y).ToArgb() != 0) x1 = x;
            for (int x = b.Width - 1; x >= 0 && x2 == max.X; x--)
                for (int y = 0; y < b.Height && x2 == max.X; y++)
                    if (b.GetPixel(x, y).ToArgb() != 0) x2 = x;

            if (x1 == 0 && y1 == 0 && x2 == max.X && y2 == max.Y) return null;            

            int size = x2 - x1 > y2 - y1 ? x2 - x1 + 1 : y2 - y1 + 1;
            int dx = y2 - y1 > x2 - x1 ? ((y2 - y1) - (x2 - x1)) / 2 : 0;
            int dy = y2 - y1 < x2 - x1 ? ((x2 - x1) - (y2 - y1)) / 2 : 0;            

            int[,] res = new int[size, size];
            for (int x = 0; x < res.GetLength(0); x++)
                for (int y = 0; y < res.GetLength(1); y++)
                {
                    int pX = x + x1 - dx;
                    int pY = y + y1 - dy;
                    if (pX < 0 || pX >= max.X || pY < 0 || pY >= max.Y)
                        res[x, y] = 0;
                    else
                        res[x, y] = b.GetPixel(x + x1 - dx, y + y1 - dy).ToArgb() == 0 ? 0 : 1;
                }
            return res;
        }

        public static int[,] LeadArray(int[,] source, int[,] res)
        {
            for (int n = 0; n < res.GetLength(0); n++)
                for (int m = 0; m < res.GetLength(1); m++) res[n, m] = 0;

            double pX = (double)res.GetLength(0) / (double)source.GetLength(0);
            double pY = (double)res.GetLength(1) / (double)source.GetLength(1);

            for (int n = 0; n < source.GetLength(0); n++)
                for (int m = 0; m < source.GetLength(1); m++)
                {
                    int posX = (int)(n * pX);
                    int posY = (int)(m * pY);
                    if (res[posX, posY] == 0) res[posX, posY] = source[n, m];
                }
            return res;
        }
    }
}
