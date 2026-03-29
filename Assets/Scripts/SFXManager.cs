using UnityEngine;
using System.Collections.Generic;



public class SFXManager : MonoBehaviour
{
    [SerializeField] List<string> sfx_name_list;
    Dictionary<string, AudioSource> sfx_dictionary = new Dictionary<string, AudioSource>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < sfx_name_list.Count; i++)
        {
            sfx_dictionary[sfx_name_list[i]] = transform.GetChild(i).GetComponentInChildren<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update SFX volume based on player preferences
        for (int i = 0; i < sfx_name_list.Count; i++)
        {
            sfx_dictionary[sfx_name_list[i]].volume = PlayerPrefs.GetFloat("sfx_volume", 1f);
        }
    }

    public void Play(string sfx_name)
    {
        if (sfx_dictionary.ContainsKey(sfx_name))
        {
            sfx_dictionary[sfx_name].Play();
        }
    }

    public void Pause(string sfx_name)
    {
        if (sfx_dictionary.ContainsKey(sfx_name))
        {
            sfx_dictionary[sfx_name].Pause();
        }
    }

    public void Stop(string sfx_name)
    {
        if (sfx_dictionary.ContainsKey(sfx_name))
        {
            sfx_dictionary[sfx_name].Stop();
        }
    }

    public void PausePlaying()
    {
        for (int i = 0; i < sfx_name_list.Count; i++)
        {
            if (sfx_dictionary[sfx_name_list[i]].isPlaying == true)
            {
                sfx_dictionary[sfx_name_list[i]].Pause();
            }
        }
    }

    public void PlayPaused()
    {
        for (int i = 0; i < sfx_name_list.Count; i++)
        {
            sfx_dictionary[sfx_name_list[i]].UnPause();
        }
    }
}
