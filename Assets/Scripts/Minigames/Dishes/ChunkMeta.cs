using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkMeta
{
    public ChunkMeta(int id, Color color)
    {
        this.id = id;
        this.Color = color;
    }
    
    private int id;
    private Color color;

    public int Id { get => id; set => id = value; }
    public Color Color { get => color; set => color = value; }
}
