using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource musica;
    public AudioClip menu;
    public AudioClip[] randomMusic;

    public static MusicManager instance = null;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        OnLevelWasLoaded(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        if (!musica.isPlaying)
        {
            OnLevelWasLoaded(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            musica.clip = menu;
            musica.Play();

        }else if (level == 1)
        {
            musica.clip = randomMusic[Random.Range(0, randomMusic.Length)];
            musica.Play();
        }

    }

    // Update is called once per frame

}
