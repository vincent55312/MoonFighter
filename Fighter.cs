using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Fighter
{
    public Texture2D texture { get; set; }
    public int health { get; set; }
    public int speed { get; set; }
    public int jump { get; set; }

    public Rectangle element;

    public Fighter(int health, int speed, int jump, Rectangle element, Texture2D texture)
    {
        this.health = health;
        this.texture = texture;
        this.speed = speed;
        this.jump = jump;
        this.element = element;
    }
}
