using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndStateTransition : Transition
{
    public void OnEndState()
    {
        NeedTransit = true;
    }
}
