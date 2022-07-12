using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class GraphicElement
{
    public GraphicsDevice graphicsDevice { get; set; }

    public GraphicElement(GraphicsDevice graphicsDevice)
    {
        this.graphicsDevice = graphicsDevice;
    }
}