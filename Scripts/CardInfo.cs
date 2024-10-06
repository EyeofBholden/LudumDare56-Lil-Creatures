using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Card", menuName = "Cards")]
public class CardInfo : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite artwork;
    public int fierceMod;
    public int cuteMod;
    public int durpyMod;
    public int coolMod;
    public int priority;
    

}
