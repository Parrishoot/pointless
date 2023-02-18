using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameState<T>
    where T : MinigameController<T>
{
   public virtual void Setup(T controller)
    {

    }

    public virtual void Process(T controller)
    {

    }

    public virtual void FixedProcess(T controller)
    {

    }

    public virtual void Teardown(T controller)
    {

    }
}
