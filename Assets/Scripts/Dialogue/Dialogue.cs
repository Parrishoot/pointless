using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dialogue
{

    public static float DEFAULT_SPEED = -1f; 

    [SerializeField]
    private float speed = DEFAULT_SPEED;

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
