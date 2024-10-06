using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PartsCode", menuName = "Scriptable Objects/PartsCode")]
public class PartsCode : ScriptableObject
{
    public List<CardInfo> baseDeckChunk = new List<CardInfo>();
    public new string name;
    public string description;
    public Sprite artwork;
    public int fierceMod;
    public int cuteMod;
    public int durpyMod;
    public int coolMod;


}