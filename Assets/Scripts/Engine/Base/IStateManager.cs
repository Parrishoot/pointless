using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMinigameController
{
    bool IsFinished();

    void Teardown();
}
