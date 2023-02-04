using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CutoutMaskUIImage : Image
{
    public override Material materialForRendering {

        get {
            Material material = new Material(base.materialForRendering);
            material.SetInt("_StencilComp", (int) CompareFunction.NotEqual);
            return material;
        }

    }
}
