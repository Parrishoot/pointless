using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dialogue
{
    private float speed = .1f;
    private string text;

    public Dialogue(string text, float speed = .1f)
    {
        this.speed = speed;
        this.text = text;
    }

    public float Speed { get => speed; set => speed = value; }
    public string Text { get => text; set => text = value; }

    
}
