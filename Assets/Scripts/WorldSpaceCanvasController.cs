using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceCanvasController : MonoBehaviour
{
    public Transform anchor;

    public void Start() {
        transform.position = anchor.position;
    }
}
