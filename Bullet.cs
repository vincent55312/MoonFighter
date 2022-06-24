using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public class Bullet
{
    public Texture2D texture { get; set; }
    public int speed { get; set; }
    public Rectangle element;
    public Map map { get; set; }
    public int sizeX { get; set; }
    public int sizeY { get; set; }
    public List<SizeElement> prefixedSizes { get; set; } = new List<SizeElement>();
    public bool mouvementIsVertial { get; set; }

    void addPrefixedSizes()
    {
        prefixedSizes.Add(new SizeElement(20, 40));
        prefixedSizes.Add(new SizeElement(50, 100));
        prefixedSizes.Add(new SizeElement(60, 120));
        prefixedSizes.Add(new SizeElement(75, 150));
        prefixedSizes.Add(new SizeElement(80, 165));
        prefixedSizes.Add(new SizeElement(90, 180));
        prefixedSizes.Add(new SizeElement(42, 84));
        prefixedSizes.Add(new SizeElement(52, 104));
        prefixedSizes.Add(new SizeElement(62, 124));
        prefixedSizes.Add(new SizeElement(77, 144));
        prefixedSizes.Add(new SizeElement(84, 165));
        prefixedSizes.Add(new SizeElement(86, 180));
        prefixedSizes.Add(new SizeElement(40, 80));
        prefixedSizes.Add(new SizeElement(49, 101));
        prefixedSizes.Add(new SizeElement(60, 124));
        prefixedSizes.Add(new SizeElement(75, 155));
        prefixedSizes.Add(new SizeElement(80, 165));
        prefixedSizes.Add(new SizeElement(90, 180));
        prefixedSizes.Add(new SizeElement(180, 360));
        prefixedSizes.Add(new SizeElement(360, 720));

    }

    public Bullet(int speed, Map map, SizeElement sizeElement, Texture2D texture, bool randomizeSize = false, bool mouvementIsVertial = true)
    {
        Random rnd = new Random();

        this.speed = rnd.Next(1, speed);
        this.mouvementIsVertial = mouvementIsVertial;
        this.texture = texture;
        this.map = map;

        if (randomizeSize)
        {
            int randomYposition = rnd.Next(0, this.map.yPixel);

            addPrefixedSizes();
            SizeElement sizeSelected = prefixedSizes[rnd.Next(0, prefixedSizes.Count)];
            this.element = new Rectangle(randomYposition, 0 - sizeSelected.sizeY, sizeSelected.sizeX, sizeSelected.sizeY);

        } else
        {
            if (this.mouvementIsVertial == false)
            {
                int randomXposition = rnd.Next(0, this.map.xPixel);

                this.element = new Rectangle(0 - sizeElement.sizeX, randomXposition, sizeElement.sizeX, sizeElement.sizeY);
            } else
            {
                int randomYposition = rnd.Next(0, this.map.yPixel);
                this.element = new Rectangle(randomYposition, 0 - sizeElement.sizeY, sizeElement.sizeX, sizeElement.sizeY);
            }
        }
    }

    public int getSpeed()
    {
        Random rnd = new Random();
        return rnd.Next(0, speed);
    }
     
}
