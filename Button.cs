using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

public class Button
{
    public Button(Rectangle rectangle, int positionX, int positionY, GameState gameState, Color colorBackground, string text, GraphicsDevice graphicsDevice)
    {
        this.positionX = positionX;
        this.positionY = positionY;
        this.gameState = gameState;
        this.text = text;
        this.colorBackground = colorBackground;
        this.rectangle = rectangle;
        this.texture2D = new Texture2D(graphicsDevice, 1, 1);
        this.texture2D.SetData<Color>(new Color[] { Color.White });
    }
    public Texture2D texture2D { get; set; }
    public int positionX { get; set; }
    public int positionY { get; set; }
    public GameState gameState { get; set; }
    public string text { get; set; }
    public Color colorBackground { get; set; }
    public Rectangle rectangle { get; set; }

}
