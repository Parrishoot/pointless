using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dialogue
{

    public static float DEFAULT_SPEED = -1f; 

    private float speed;
    private string text;

    public Dialogue(string text, float speed = -1)
    {
        this.speed = speed;
        this.text = text;
    }

    public float Speed { get => speed; set => speed = value; }
    public string Text { get => text; set => text = value; }

    
}
