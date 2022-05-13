using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Map {
    public int xPixel { get; set; }
    public int yPixel { get; set; }
    public int sizePixel { get; set; }
    public int gravity { get; set; }
    public Vector2 pixel { get; set; }
    public Texture2D background { get; set; }

    public Map(int xPixel, int yPixel, int gravity, int sizePixel)
    {
        this.yPixel = xPixel;
        this.xPixel = yPixel;
        this.gravity = gravity;
        this.sizePixel = sizePixel;

        pixel = new Vector2(sizePixel, sizePixel);
    }
}
