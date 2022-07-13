using Microsoft.Xna.Framework.Graphics;

namespace MoonFighter
{
    public class Chunk
    {
        public int Xpixels { get; set; }
        public int Ypixels { get; set; }
        public int SizePixel { get; set; }
        public int Gravity { get; set; }
        public Texture2D Texture { get; set; }

        public Chunk(int xPixel, int yPixel, int gravity, Texture2D texture)
        {
            Ypixels = xPixel;
            Xpixels = yPixel;
            Gravity = gravity;
            Texture = texture;
        }
    }
}
