using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgMusic;
    public static AudioManager Instance;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bgMusic = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            bgMusic.volume = 0.4f;
        }
        else
        {
            bgMusic.volume = 0.3f;
        }
    }
}
