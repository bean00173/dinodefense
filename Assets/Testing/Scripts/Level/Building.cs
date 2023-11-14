using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Building // class that stores building prefab stats
{
    public string name;
    public string material;
    public int endurance;
    public int cost;
    public float fallDamage;
    public Sprite icon;
    public Sprite damagedState;
}
