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

    }

    public void Play(string sfx_name)
    {
        sfx_dictionary[sfx_name].Play();
    }
}
