﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AlchemyBase
{
    GameObject ResultPrefab
    {
        get;
        set;
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
