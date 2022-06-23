using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class GraphicElement
{
    public Texture2D texture { get; set; }
    public Rectangle element { get; set; }

    public GraphicElement(Rectangle element, Texture2D texture)
    {
        this.texture = texture;
        this.element = element;
    }
}