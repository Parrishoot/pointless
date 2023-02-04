using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayMaskAnimationController : MonoBehaviour
{

    public Animator animator;

    public void Show() {
        animator.Play("In");
    }

    public void Hide() {
        animator.Play("Out");
    }

}
