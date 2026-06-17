using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HookNoteScript : NoteScript
{
    
    public Dictionary<float, Line> hookData = new Dictionary<float, Line>();
    public Line newLine;
    int i = 0;

    private void Start()
    {
        base.Start();
    }

    private void Update()
    {
        base.Update();
        // If the float in the dictionary is greater than the current song beat (because the time is in beats), then it will jump the note to the line
        float timeInBeats = ((float)mmScript.GetCurrentSongTime() / mmScript.bps);
        List<float> keys = hookData.Keys.ToList<float>();
        if (i < keys.Count)
        {
            float nextJumpTime = keys[i];
            Debug.Log(timeInBeats);
            Debug.Log(nextJumpTime);
            if (timeInBeats > nextJumpTime)
            {
                Debug.Log("JUMPING NOW");
                Line line = hookData[nextJumpTime];
                JumpNote(line.noteSpawner.transform.position, line.beatLine.transform.position);
                i++;
            }
        }
    }
}
