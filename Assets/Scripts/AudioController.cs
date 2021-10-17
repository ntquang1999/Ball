using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField ]public static AudioClip bounce, win;
    [SerializeField] public static AudioSource audiosource;
    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        bounce = Resources.Load<AudioClip>("Sounds/smb_jump-small");
        win = Resources.Load<AudioClip>("Sounds/smb_world_clear");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlaySound(string name)
    {

        switch (name)
        {
            case "bounce":
                audiosource.clip = bounce;
                audiosource.Play();
                //audiosource.loop = true;
                break;
            case "win":
                audiosource.clip = win;
                audiosource.Play();
                //audiosource.loop = true;
                break;
        }
    }

    public void adjustVolume(float volume)
    {
        audiosource.volume = volume;
    }

}
