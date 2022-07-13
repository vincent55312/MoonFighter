using Microsoft.Xna.Framework.Graphics;

namespace MoonFighter
{
    public abstract class GraphicElement
    {
        public GraphicsDevice GraphicsDevice { get; set; }

        public GraphicElement(GraphicsDevice graphicsDevice)
        {
            this.GraphicsDevice = graphicsDevice;
        }
    }
}