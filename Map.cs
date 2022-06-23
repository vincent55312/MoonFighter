using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Map {
    public int xPixel;
    public int yPixel;
    public int sizePixel;
    public int gravity;
    public Texture2D texture;

    public Map(int xPixel, int yPixel, int gravity, Texture2D texture)
    {
        this.yPixel = xPixel;
        this.xPixel = yPixel;
        this.gravity = gravity;
        this.texture = texture;
    }
}
