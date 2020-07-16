using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStackable
{
    int MaxInStack 
    {
        get;
    }

    int NumInStack
    {
        get;
        set;
    }

    int ChangeNumInStack(int numToChange);
}
