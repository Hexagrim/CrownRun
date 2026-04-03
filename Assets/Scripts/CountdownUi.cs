using UnityEngine;
using TMPro;

public class CountdownUi : MonoBehaviour
{
    public TextMeshProUGUI text;
    private CountdownServer countdown;

    void Start()
    {
        countdown = FindFirstObjectByType<CountdownServer>();
    }

    void Update()
    {
        if (countdown == null) return;

        float remaining = countdown.GetRemainingTime();

        int minutes = Mathf.FloorToInt(remaining / 60f);
        int seconds = Mathf.FloorToInt(remaining % 60f);
        if (countdown.GetRemainingTime() <= 0)
        {
            text.text = "";
        }
        else
        {
            text.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }
}
