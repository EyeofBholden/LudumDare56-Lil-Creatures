using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Target", menuName = "Targets")]
public class TargetInfo : ScriptableObject
{
    public new string name;
    public string description;
    public int targetFierce;
    public int targetCute;
    public int targetDurpy;
    public int targetCool;


}