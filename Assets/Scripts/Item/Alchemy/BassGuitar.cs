﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BassGuitar : AlchemyBase
{
    [SerializeField] private GameObject _resultPrefab;
    public GameObject ResultPrefab
    {
        get => _resultPrefab;
        set => _resultPrefab = value;
    }

    private string _baseName = "Slappin the bass";
    public string BaseName
    {
        get => _baseName;
    }

    private Sprite _baseImage;
    public Sprite BaseImage
    {
        get => _baseImage;
    }

    public BassGuitar()
    {
        ResultPrefab = Resources.Load("Alchemy/Flask") as GameObject;
    }
}
