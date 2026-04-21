using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEditor;
using Unity.VisualScripting;
public class GameManager : MonoBehaviour
{
    public TMP_Text timer;
    public Animator winScreen;
    public TMP_Text winnerText;
    public float time = 59;
    bool startTimer;
    public Animator T_Anim;
    bool menu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created 
    private void Start()
    {
        Time.timeScale = 0f;
    }
    void Awake()
    {
        StartCoroutine(CountDown());
    }

    // Update is called once per frame

    void Update()
    {
        if (startTimer == false) return;
        time -= Time.deltaTime;

        int minutes = ((int)time) / 60;
        int seconds = ((int)time) % 60;

        timer.text = minutes.ToString("0") + ":" + seconds.ToString("00");


        if (time <= 0f)
        {
            EndGame();
        }
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        yield return new WaitForSecondsRealtime(4f);
        Time.timeScale = 1.0f;
        startTimer = true;
    }
    void EndGame()
    {
        StartCoroutine(LerpTimeScale(0f,0.2f));
        KingManager km = FindFirstObjectByType<KingManager>();
        if(km.king == km.Red)
        {
            winnerText.text = "RED";
            winnerText.color = Color.red;
        }
        else if (km.king == km.Green)
        {
            winnerText.text = "GREEN";
            winnerText.color = Color.green;
        }
        else if (km.king == km.Blue)
        {
            winnerText.text = "BLUE";
            winnerText.color = Color.blue;
        }
        else
        {
            winnerText.text = "YELLOW";
            winnerText.color = Color.yellow;
        }

        winnerText.enabled = true;
        winScreen.SetTrigger("win");
        StartCoroutine(MainMenu());



    }
    public IEnumerator LerpTimeScale(float targetTimeScale, float duration)
    {
        float startTimeScale = Time.timeScale;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / duration;

            Time.timeScale = Mathf.Lerp(startTimeScale, targetTimeScale, t);
            yield return null;
        }

        Time.timeScale = targetTimeScale;
    }

    IEnumerator MainMenu()
    {
        if (!menu)
        {
            menu = true;
            yield return new WaitForSecondsRealtime(3f);
            T_Anim.SetTrigger("fade");
            yield return new WaitForSecondsRealtime(0.25f);
            SceneManager.LoadSceneAsync("MainMenu");
            Time.timeScale = 1.0f;
        }
    }

}
