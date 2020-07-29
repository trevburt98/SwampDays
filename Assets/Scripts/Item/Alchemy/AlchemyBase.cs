using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AlchemyBase
{
    GameObject ResultPrefab
    {
        get;
    }

    string BaseName
    {
        get;
    }

    Sprite BaseImage
    {
        get;
    }
}
