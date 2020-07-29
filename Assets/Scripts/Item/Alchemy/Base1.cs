using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base1 : AlchemyBase
{
    [SerializeField] private GameObject _resultPrefab;
    public GameObject ResultPrefab
    {
        get => _resultPrefab;
    }

    private string _baseName = "Basey boy";
    public string BaseName
    {
        get => _baseName;
    }

    private Sprite _baseImage;
    public Sprite BaseImage
    {
        get => _baseImage;
    }

    public Base1()
    {

    }
}
