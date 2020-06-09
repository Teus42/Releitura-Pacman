using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{    
    public Sound[] sounds;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.velocidade;
            s.source.mute = s.mute;
            s.source.loop= s.loop;
        }
    }

    void Start()
    {
        Play("Theme");
    }

    public void Play(string nome)
    {
        Sound s  = Array.Find(sounds, sound => sound.nome == nome);        

        if(s == null)
        {
            Debug.LogWarning("Som de nome: " + nome + " não encontrado :/");
            return;
        }
        s.source.Play();
    }

    public void Mute()
    {
        Sound s = Array.Find(sounds, sound => sound.mute == false);

        if(s.mute == false)
        {
            AudioListener.volume = 0f;
            s.mute = true;            
        }

        //Mutado e Pausado a musica
        if(s.mute == true)
        {            
            s.mute = false;
            AudioListener.volume = 1f;
        }
    }

   


}
