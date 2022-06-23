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
    public int ratioFrequencyGeneration { get; set; }
    public List<SizeElement> prefixedSizes { get; set; } = new List<SizeElement>();

    void addPrefixedSizes()
    {
        prefixedSizes.Add(new SizeElement(40, 80));
        prefixedSizes.Add(new SizeElement(50, 100));
        prefixedSizes.Add(new SizeElement(60, 120));
        prefixedSizes.Add(new SizeElement(75, 150));
        prefixedSizes.Add(new SizeElement(80, 165));
        prefixedSizes.Add(new SizeElement(90, 180));
        prefixedSizes.Add(new SizeElement(180, 360));
    }

    public Bullet(int speed, Map map, SizeElement sizeElement, Texture2D texture, bool randomizeSize = false)
    {

        this.speed = speed;
        this.texture = texture;
        this.map = map;
        Random rnd = new Random();
        int randomYposition = rnd.Next(0, this.map.yPixel);

        if (randomizeSize)
        {
            addPrefixedSizes();
            SizeElement sizeSelected = prefixedSizes[rnd.Next(0, prefixedSizes.Count)];
            this.element = new Rectangle(randomYposition, 0 - sizeSelected.sizeY, sizeSelected.sizeX, sizeSelected.sizeY);

        } else
        {
            this.element = new Rectangle(randomYposition, 0 - sizeElement.sizeY, sizeElement.sizeX, sizeElement.sizeY);
        }
    }
}
