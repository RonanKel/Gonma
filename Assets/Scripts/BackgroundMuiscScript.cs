using UnityEngine;
using System.Collections.Generic;



public class BackgroundMuiscScript : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    [SerializeField] List<AudioClip> backgroundMusicTracks;
    [SerializeField] float timeBeforeMusicPlays = 30.0f;
    bool timerGoing = true;
    float timer = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timerGoing && !audio.isPlaying)
        {
            timer += Time.deltaTime;
            if (timer >= timeBeforeMusicPlays) {
                PlayRandomSong();
                StopTimer();
            }
        }
    }

    public void StartTimer()
    {
        timerGoing = true;
    }

    public void StopTimer()
    {
        timerGoing = false;
        timer = 0.0f;
    }

    void PlayRandomSong()
    {
        Debug.Log("Playing BKGD Music");
        int songIndex = Random.Range(0, backgroundMusicTracks.Count - 1);
        audio.clip = backgroundMusicTracks[songIndex];
        audio.Play();
    }

    public void StopBackgroundMusic()
    {
        audio.Stop();
    }
}
