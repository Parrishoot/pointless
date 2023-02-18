using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameController<T> : MonoBehaviour, ITeardownable
where T: MinigameController<T>
{
    protected MinigameState<T> currentState;

    public virtual void Start()
    {
        currentState.Setup((T) this);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        currentState.Process((T) this);
    }

    public virtual void FixedUpdate()
    {
        currentState.FixedProcess((T) this);
    }

    public virtual void SetState(MinigameState<T> state)
    {
        currentState.Teardown((T) this);
        currentState = state;
        state.Setup((T) this);
    }

    public virtual void Teardown() {
        currentState.Teardown((T) this);
    }
}
