using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioScript : MonoBehaviour
{

    public AudioClip backgroundSoundClip;
    public AudioClip menuMusicClip;
    


    private List<AudioSource> Music = new List<AudioSource>();
    
    public static float musicVolume = 0.8f;

    public float time = 15f;
    // Start is called before the first frame update

    private static GameObject instance;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex > 0 && GetCurrentMusicPlaying().Count == 0 && Music[1] != null)
        {
            Music[1].Play();
        }
        else if(scene.buildIndex == 0 && GetCurrentMusicPlaying().Count == 0 && Music[0] != null)
        {
            Music[0].Play();
        }
        else if(scene.buildIndex == 0 && GetCurrentMusicPlaying().Count > 0 && Music[0] != null)
        {
            Music[1].Stop();
            Music[0].Play();
        }
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = gameObject;
        else
            Destroy(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;


        Music.Add(AddAudio(false, false, musicVolume, menuMusicClip));
        Music.Add(AddAudio(false, false, musicVolume, backgroundSoundClip));
        


        //AudioSources.Add(AddAudio(false, false, 0.5f, mouseDownClip));

        transform.position = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Music[0].Play();
        }
        else
        {
            Music[1].Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        IEnumerable<AudioSource> playingAudio = Music.Where(x => x.isPlaying);
        if (time > 0 && playingAudio.Count() == 0)
        {
            time -= Time.deltaTime;
        }
        if (SceneManager.GetActiveScene().name == "Menu" && playingAudio.Count() == 0 && time <= 0)
        {
            Music[0].Play();
            time = 15f;
        }

        if (SceneManager.GetActiveScene().buildIndex >= 1 && playingAudio.Count() == 0 && time <= 0)
        {
            Music[1].Play();
            time = 15f;
        }
    }


    public AudioSource AddAudio(bool loop, bool playAwake, float vol, AudioClip clip)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol;
        return newAudio;
    }

    public List<AudioSource> GetCurrentMusicPlaying()
    {
        List<AudioSource> audiosPlaying = new List<AudioSource>();
        if(Music.Count != 0)
        {
            foreach (AudioSource audio in Music)
            {
                if(audio != null){
                    if (audio.isPlaying)
                    {
                        audiosPlaying.Add(audio);
                    }
                }
                
            }
        }
        return audiosPlaying;
    }

    public void playGameMusic()
    {
        Music[1].Play();
    }

    public void setMusicVolume(float volume)
    {
        musicVolume = volume;
        foreach (AudioSource music in Music)
        {
            music.volume = volume;
        }
    }
}
