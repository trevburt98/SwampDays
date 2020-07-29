﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaBase : AlchemyBase
{
    [SerializeField] private GameObject _resultPrefab;
    public GameObject ResultPrefab
    {
        get => _resultPrefab;
    }

    private string _baseName = "I catch these in animal crossing";
    public string BaseName
    {
        get => _baseName;
    }

    private Sprite _baseImage;
    public Sprite BaseImage
    {
        get => _baseImage;
    }

    public SeaBase()
    {

    }
}
