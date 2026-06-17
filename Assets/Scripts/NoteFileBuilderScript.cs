using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;


[System.Serializable]
public class NoteProperties 
{
    public float beatPos;
    public int beatLine;
    public int type;
    public List<float> timeData;
    public List<int> lineData;
}

public class NoteFileBuilderScript : MonoBehaviour
{
    public string songName;
    public List<NoteProperties> notes = new List<NoteProperties>();

#if UNITY_EDITOR
    [CustomEditor(typeof(NoteFileBuilderScript))]
    public class NoteFileBuilderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            NoteFileBuilderScript builder = (NoteFileBuilderScript)target;

            if (GUILayout.Button("Add These Notes!"))
            {
                builder.AddNotesToFile();
            }
        }
    }
#endif

    public void AddNotesToFile() 
    {
        string filePath = "Assets/StreamingAssets/LevelData/" + songName + ".txt";

        StreamWriter writer = new StreamWriter(filePath, true);

        foreach (NoteProperties note in notes) {
            string data = "";
            for (int i = 0; i < note.timeData.Count; i++)
            {
                if (note.timeData[i] != null) {
                    data += " " + note.timeData[i];
                    if (note.lineData != null)
                    {
                        data += " " + note.lineData[i];
                    }
                }
                
            }
            writer.WriteLine(note.beatPos + " " + note.beatLine + " " + note.type + data);
        }

        writer.Close();

        notes.Clear();

        Debug.Log("Notes added!");
    }
}