using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAlchemyIngredient
{
    int OptimalTemp
    {
        get;
    }

    int MinTemp
    {
        get;
    }

    int MaxTemp
    {
        get;
    }
}
