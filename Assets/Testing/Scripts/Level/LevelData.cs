using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public int levelNum;
    public int starsNeeded;
    [Range(0, 3)] public int stars;
    public string sceneName;
}
