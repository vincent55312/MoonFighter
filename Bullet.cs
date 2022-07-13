using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MoonFighter
{
    public class Bullet
    {
        public Texture2D Texture { get; set; }
        public int Speed { get; set; }
        public Rectangle Element;
        public Chunk Chunk { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public List<SizeElement> PrefixedSizes { get; set; } = new List<SizeElement>();
        public bool MouvementIsVertial { get; set; }

        void AddPrefixedSizes()
        {
            PrefixedSizes.Add(new SizeElement(20, 40));
            PrefixedSizes.Add(new SizeElement(50, 100));
            PrefixedSizes.Add(new SizeElement(60, 120));
            PrefixedSizes.Add(new SizeElement(75, 150));
            PrefixedSizes.Add(new SizeElement(80, 165));
            PrefixedSizes.Add(new SizeElement(90, 180));
            PrefixedSizes.Add(new SizeElement(42, 84));
            PrefixedSizes.Add(new SizeElement(52, 104));
            PrefixedSizes.Add(new SizeElement(62, 124));
            PrefixedSizes.Add(new SizeElement(77, 144));
            PrefixedSizes.Add(new SizeElement(84, 165));
            PrefixedSizes.Add(new SizeElement(86, 180));
            PrefixedSizes.Add(new SizeElement(40, 80));
            PrefixedSizes.Add(new SizeElement(49, 101));
            PrefixedSizes.Add(new SizeElement(60, 124));
            PrefixedSizes.Add(new SizeElement(75, 155));
            PrefixedSizes.Add(new SizeElement(80, 165));
            PrefixedSizes.Add(new SizeElement(90, 180));
            PrefixedSizes.Add(new SizeElement(180, 360));
            PrefixedSizes.Add(new SizeElement(360, 720));

        }

        public Bullet(int speed, Chunk map, SizeElement sizeElement, Texture2D texture, bool randomizeSize = false, bool mouvementIsVertial = true)
        {
            Random rnd = new Random();

            Speed = rnd.Next(1, speed);
            MouvementIsVertial = mouvementIsVertial;
            Texture = texture;
            Chunk = map;

            if (randomizeSize)
            {
                int randomYposition = rnd.Next(0, Chunk.Ypixels);

                AddPrefixedSizes();
                SizeElement sizeSelected = PrefixedSizes[rnd.Next(0, PrefixedSizes.Count)];
                Element = new Rectangle(randomYposition, 0 - sizeSelected.SizeY, sizeSelected.SizeX, sizeSelected.SizeY);

            }
            else
            {
                if (MouvementIsVertial == false)
                {
                    int randomXposition = rnd.Next(0, Chunk.Xpixels);

                    Element = new Rectangle(0 - sizeElement.SizeX, randomXposition, sizeElement.SizeX, sizeElement.SizeY);
                }
                else
                {
                    int randomYposition = rnd.Next(0, Chunk.Ypixels);
                    Element = new Rectangle(randomYposition, 0 - sizeElement.SizeY, sizeElement.SizeX, sizeElement.SizeY);
                }
            }
        }

        public int GetSpeed()
        {
            Random rnd = new Random();
            return rnd.Next(0, Speed);
        }
    }
}