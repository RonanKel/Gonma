using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Levels", menuName = "Level")]
public class Level : ScriptableObject
{
    public new string name;

    public Sprite fishSprite;

    public AudioClip song;

    public string GetLevelData () {
        return "Assets/LevelData/" + name + ".txt";
    }

}
