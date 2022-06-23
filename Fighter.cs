using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Fighter
{
    public Texture2D texture;
    public int speed;
    public int jump;

    public Rectangle element;

    public Fighter(int speed, int jump, Rectangle element, Texture2D texture)
    {
        this.texture = texture;
        this.speed = speed;
        this.jump = jump;
        this.element = element;
    }
}
