using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public TMP_Text timer;
    public Animator winScreen;
    public TMP_Text winnerText;
    public float time = 59;
    bool startTimer;
    public Animator T_Anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(CountDown());
    }

    // Update is called once per frame

    void Update()
    {
        if (startTimer == false) return;
        time -= Time.deltaTime;
        timer.text = time.ToString();
        if(time <= 0f)
        {
            EndGame();
        }
    }

    IEnumerator CountDown()
    {
        Time.timeScale = 0f;
        yield return new WaitForSeconds(2.95f);
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
        yield return new WaitForSeconds(3f);
        T_Anim.SetTrigger("fade");
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
