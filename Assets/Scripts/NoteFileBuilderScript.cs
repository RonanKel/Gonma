using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;


[System.Serializable]
public class NoteProperties 
{
    public float beatPos;
    public string color;
    public string type;
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
        string filePath = "Assets/LevelData/" + songName + ".txt";

        StreamWriter writer = new StreamWriter(filePath, true);

        foreach (NoteProperties note in notes) {
            writer.WriteLine(note.beatPos + " " + note.color + " " + note.type);
        }

        writer.Close();

        notes.Clear();

        Debug.Log("Notes added!");
    }
}