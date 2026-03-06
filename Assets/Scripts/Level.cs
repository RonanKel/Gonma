using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Levels", menuName = "Level")]
public class Level : ScriptableObject
{

    // Song info
    public new string name;

    public Sprite fishSprite;

    public AudioClip song;

    public string GetLevelData () {
        return "LevelData/" + name + ".txt";
    }


    // Journal info
    public new string fishname;

    public new string scientificFishname;

    public new string meetingThoughts;

    public Sprite journalFishSprite;

    public new string description;

    public Sprite trinketSprite;

    public Sprite award1;

    public Sprite award2;

    public Sprite award3;

    public new string extra1;

    public new string extra2;

    

}
