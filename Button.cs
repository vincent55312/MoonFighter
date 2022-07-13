using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MoonFighter
{
    public class Button : GraphicElement
    {
        public Button(Rectangle rectangle, int positionX, int positionY, GameState gameState, Color colorBackground, string text, GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
            PositionX = positionX;
            PositionY = positionY;
            GameState = gameState;
            Text = text;
            ColorBackground = colorBackground;
            Rectangle = rectangle;
            Texture2D = new Texture2D(graphicsDevice, 1, 1);
            Texture2D.SetData<Color>(new Color[] { Color.Gold });
        }
        public Texture2D Texture2D { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public GameState GameState { get; set; }
        public string Text { get; set; }
        public Color ColorBackground { get; set; }
        public Rectangle Rectangle { get; set; }

    }
}