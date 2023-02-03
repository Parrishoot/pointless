using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dialogue
{

    public static float DEFAULT_SPEED = 0; 

    [SerializeField]
    private float speed = 0;

    [SerializeField]
    private string text;

    public Dialogue(string text, float speed)
    {
        this.speed = speed;
        this.text = text;
    }

    public float Speed { get => speed; set => speed = value; }
    public string Text { get => text; set => text = value; }

    
}
