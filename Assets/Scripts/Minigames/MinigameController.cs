using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameController : MonoBehaviour
{
    protected MinigameState currentState;

    public virtual void Start()
    {
        currentState.Setup();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        currentState.Process();
    }

    public virtual void FixedUpdate()
    {
        currentState.FixedProcess();
    }

    public virtual void SetState(MinigameState state)
    {
        currentState.Teardown();
        currentState = state;
        state.Setup();
    }
}
