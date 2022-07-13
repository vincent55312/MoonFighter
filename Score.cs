using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

public class Score: GraphicElement
{
    public Texture2D textureLossLife { get; set; }
    public Rectangle elementLossLife { get; set; }
    public float lossLifePercent { get; set; }
    public int score { get; set; }
    public Color color { get; set; }
    public int nRockets { get; set; }
    public float percentScoreLeft { get; set; }

    public Texture2D textureScore { get; set; }
    public Rectangle elementScore { get; set; }
    private int baseLife { get; } = 800;

    public string getScore()
    {
        return this.score.ToString();
    }

    public Score(GraphicsDevice graphicsDevice, int lossLife, int nFrames): base(graphicsDevice)
    {   
        textureLossLife = new Texture2D(graphicsDevice, 1, 1);
        textureLossLife.SetData<Color>(new Color[] { Color.White });
        score = nFrames / 10;
        percentScoreLeft = ((baseLife - (float)lossLife) / baseLife);
        float sizeHeight = 675 * percentScoreLeft;
        elementLossLife = new Rectangle(20, 20, 50, (int)sizeHeight);

        if (percentScoreLeft > 0.75f)
        {
            color = Color.GreenYellow;

        }
        else if (percentScoreLeft > 0.5f)
        {
            color = Color.LightYellow;
        }
        else if (percentScoreLeft > 0.25f)
        {
            color = Color.Orange;
        }
        else
        {
            color = Color.Red;
        }
        color = color * 0.5f;

        textureScore = new Texture2D(graphicsDevice, 1, 1);
        textureScore.SetData<Color>(new Color[] { Color.White });
        elementScore = new Rectangle(1050, 25, 125, 85);
    }
}
