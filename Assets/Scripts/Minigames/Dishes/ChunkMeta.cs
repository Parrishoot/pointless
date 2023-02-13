using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkMeta
{
    public ChunkMeta(int id)
    {
        this.id = id;
    }
    
    private int id;

    public int Id { get => id; set => id = value; }
}
