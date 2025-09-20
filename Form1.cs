namespace Isometric
{
    public class Tile 
    {
        public int pos = 0;
        public Point vIsoWorld;
    }
    public partial class Form1 : Form
    {
        Bitmap off;
        Point vWorldSize = new Point(14,10);
        Point vTileSize = new Point(80,40);
        Point vOrigin = new Point(10,5);
        List <Bitmap> sprites = new List <Bitmap>();
        Tile[,] World;
        Point vCell = new Point();
        Point vOset = new Point();
        Point vSelectedCell = new Point();
        string cell;
        string offset;
        string SelectedCell;
        public Form1()
        {
            WindowState = FormWindowState.Maximized;   
            Load += Form1_Load;
            Paint += Form1_Paint;
            MouseMove += Form1_MouseMove;
            MouseDown += Form1_MouseDown;
        }

        private void Form1_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) 
            {
                if (vSelectedCell.X > -1 && vSelectedCell.Y > -1 && vSelectedCell.X < vWorldSize.X && vSelectedCell.Y < vWorldSize.Y)
                {
                    World[vSelectedCell.X, vSelectedCell.Y].pos = (World[vSelectedCell.X, vSelectedCell.Y].pos + 1) % 6;
                }
            }
            DrawDubb(this.CreateGraphics());
        }

        private void Form1_MouseMove(object? sender, MouseEventArgs e)
        {
            vCell.X = e.X / vTileSize.X; vCell.Y = e.Y / vTileSize.Y;
            cell = "Cell: " + vCell.X + "," + vCell.Y;

            vOset.X = e.X % vTileSize.X;
            vOset.Y = e.Y % vTileSize.Y;
            offset = "Offset: " + vOset.X + "," + vOset.Y;

            vSelectedCell.X = (vCell.Y - vOrigin.Y) + (vCell.X - vOrigin.X);
            vSelectedCell.Y = (vCell.Y - vOrigin.Y) - (vCell.X - vOrigin.X);
            SelectedCell = "Selected Cell: " + vSelectedCell.X + "," + vSelectedCell.Y;

            Bitmap musk = new Bitmap(new Bitmap("isometric_musk.png"), new Size(80, 40));
            Color Pixel = musk.GetPixel(vOset.X, vOset.Y);
            if (Pixel.ToArgb() == Color.Blue.ToArgb())
            {
                vSelectedCell.Y += -1;
            }
            if (Pixel.ToArgb() == Color.Red.ToArgb()) 
            {
                vSelectedCell.X += -1;
            }
            if (Pixel == Color.FromArgb(255,0,255,0))
            {
                
                vSelectedCell.Y += +1;
            }
            if (Pixel.ToArgb() == Color.Yellow.ToArgb())
            {
                vSelectedCell.X += +1;
            }
            //this.Text = "" + Pixel + "," + Color.Green;
            DrawDubb(this.CreateGraphics());
        }

        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            DrawDubb(e.Graphics);
        }
        Point ScreenTransformation(int x, int y) 
        {
            return new Point(
               (vOrigin.X* vTileSize.X) + (x-y)*(vTileSize.X / 2),
                (vOrigin.Y* vTileSize.Y) + (x+y) *(vTileSize.Y / 2));
        }
        private void Form1_Load(object? sender, EventArgs e)
        {
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            World = new Tile[vWorldSize.X, vWorldSize.Y];
            for (int i  = 1; i < 7; i++) 
            {
                Bitmap sprite = new Bitmap(new Bitmap("isometric_" + i +".png"), new Size(80, 40));
                sprites.Add(sprite);
            }
            for (int x = 0; x < vWorldSize.X; x++) 
            {
                for (int y = 0; y < vWorldSize.Y; y++) 
                {
                    World[x, y] = new Tile();
                    World[x,y].vIsoWorld = ScreenTransformation(x, y);
                }
            }
        }

        void DrawScene(Graphics g) 
        {
            g.Clear(Color.White);
            for (int x = 0; x < vWorldSize.X; x++)
            {
                for (int y = 0; y < vWorldSize.Y; y++)
                {
                    int idx = World[x, y].pos;
                    Bitmap sprite = sprites[idx];
                    if (idx == 3 || idx == 2)
                    {
                        g.DrawImage(sprite, World[x, y].vIsoWorld.X, World[x, y].vIsoWorld.Y-40, vTileSize.X, vTileSize.Y + 40);
                    }
                    else 
                    {
                        g.DrawImage(sprite, World[x, y].vIsoWorld.X, World[x, y].vIsoWorld.Y, vTileSize.X, vTileSize.Y);
                    }
                }
            }

            if (vSelectedCell.X > -1 && vSelectedCell.Y > -1 && vSelectedCell.X < vWorldSize.X && vSelectedCell.Y < vWorldSize.Y)
            {
                int idx = World[vSelectedCell.X, vSelectedCell.Y].pos;
                Bitmap sprite = new Bitmap(new Bitmap("isometric_selected.png"), new Size(80, 40));
                g.DrawImage(sprite, World[vSelectedCell.X, vSelectedCell.Y].vIsoWorld.X, World[vSelectedCell.X, vSelectedCell.Y].vIsoWorld.Y, vTileSize.X, vTileSize.Y);
                g.DrawRectangle(Pens.Green, vCell.X * vTileSize.X, vCell.Y * vTileSize.Y, vTileSize.X, vTileSize.Y);
            }
            else 
            {
                g.DrawRectangle(Pens.Red, vCell.X * vTileSize.X, vCell.Y * vTileSize.Y, vTileSize.X, vTileSize.Y);
            }
            Font drawFont = new Font("Arial", 12);
            g.DrawString(cell, drawFont, Brushes.Black, 10, 10);
            g.DrawString(offset, drawFont, Brushes.Black, 10, 40);
            g.DrawString(SelectedCell, drawFont, Brushes.Black, 10, 70);
            

        }
        void DrawDubb(Graphics g) 
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off,0,0);
        }
    }
}
