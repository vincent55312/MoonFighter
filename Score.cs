using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

public class Score
{
    public Texture2D textureLossLife;
    public Rectangle elementLossLife;
    public float lossLifePercent;
    public int score;
    public Color color;
    public int nRockets;
    public float percentScoreLeft;

    public Texture2D textureScore;
    public Rectangle elementScore;

    public Score(GraphicsDevice graphicsDevice, int lossLife, int nRockets)
    {   
        this.textureLossLife = new Texture2D(graphicsDevice, 1, 1);
        this.textureLossLife.SetData<Color>(new Color[] { Color.White });
        this.nRockets = nRockets;
        this.score = nRockets - lossLife;
        percentScoreLeft = ((500 - (float)lossLife) / 500);
        float sizeHeight = 675 * percentScoreLeft;
        this.elementLossLife = new Rectangle(20, 20, 50, (int)sizeHeight);

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



        this.textureScore = new Texture2D(graphicsDevice, 1, 1);
        this.textureScore.SetData<Color>(new Color[] { Color.White });
        this.elementScore = new Rectangle(1050, 25, 125, 85);
    }
}
