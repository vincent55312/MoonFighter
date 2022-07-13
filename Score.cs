using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace MoonFighter
{
    public class Score : GraphicElement
    {
        public Texture2D TextureLossLife { get; set; }
        public Rectangle ElementLossLife { get; set; }
        public float LossLifePercent { get; set; }
        public int scoreNumber { get; set; }
        public Color Color { get; set; }
        public int Nrockets { get; set; }
        public float PercentScoreLeft { get; set; }
        public Texture2D TextureScore { get; set; }
        public Rectangle ElementScore { get; set; }
        private int BaseLife { get; } = 800;

        public static void Save(List<int> memoryScores)
        {
            string fileName = "Scores.json";
            string pathFolder = Path.Combine(Environment.CurrentDirectory, @"prod_data");
            string path = pathFolder + '/' + fileName;
            File.WriteAllText(path, JsonSerializer.Serialize(memoryScores));
        }

        public static List<int> GetAllScores()
        {
            string fileName = "Scores.json";
            string pathFolder = Path.Combine(Environment.CurrentDirectory, @"prod_data");
            string path = pathFolder + '/' + fileName;

            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<int>>(json);
        }


        public string GetScore()
        {
            return scoreNumber.ToString();
        }



        public Score(GraphicsDevice graphicsDevice, int lossLife, int nFrames) : base(graphicsDevice)
        {
            TextureLossLife = new Texture2D(graphicsDevice, 1, 1);
            TextureLossLife.SetData<Color>(new Color[] { Color.White });
            scoreNumber = nFrames / 10;
            PercentScoreLeft = ((BaseLife - (float)lossLife) / BaseLife);
            float sizeHeight = 675 * PercentScoreLeft;
            ElementLossLife = new Rectangle(20, 20, 50, (int)sizeHeight);

            if (PercentScoreLeft > 0.75f)
            {
                Color = Color.GreenYellow;

            }
            else if (PercentScoreLeft > 0.5f)
            {
                Color = Color.LightYellow;
            }
            else if (PercentScoreLeft > 0.25f)
            {
                Color = Color.Orange;
            }
            else
            {
                Color = Color.Red;
            }
            Color = Color * 0.5f;

            TextureScore = new Texture2D(graphicsDevice, 1, 1);
            TextureScore.SetData<Color>(new Color[] { Color.White });
            ElementScore = new Rectangle(1050, 25, 125, 85);
        }
    }
}