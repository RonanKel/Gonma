using UnityEngine;

public class SFXWrapperScript : MonoBehaviour
{
    private AudioSource audio = null;
    private float initValue = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        audio = GetComponent<AudioSource>();
        initValue = audio.volume;
        if (PlayerPrefs.HasKey("sfx_volume")) {
            SetVolume(PlayerPrefs.GetFloat("sfx_volume"));
        }
    }

    public void SetVolume(float value)
    {
        if (audio != null) {
            audio.volume = initValue * value;
        }
    }
}
