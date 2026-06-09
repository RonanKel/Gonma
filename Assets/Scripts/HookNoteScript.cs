using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HookNoteScript : NoteScript
{
    
    [SerializeField] Dictionary<float, Line> hookData = new Dictionary<float, Line>();
    public Line newLine;
    int i = 0;

    private void Start()
    {
        base.Start();
        hookData[1.0f] = newLine;
    }

    private void Update()
    {
        base.Update();
        // If the float in the dictionary is greater than the current song beat (because the time is in beats), then it will jump the note to the line
        float timeInBeats = (((float)mmScript.GetCurrentSongTime() -  (float) spawnTime) / mmScript.bps);
        float nextJumpTime = hookData.Keys.ToList<float>()[i];
        if (timeInBeats > nextJumpTime)
        {
            Line line = hookData[nextJumpTime];
            JumpNote(line.noteSpawner.transform.position, line.beatLine.transform.position);
        }

    }
}
