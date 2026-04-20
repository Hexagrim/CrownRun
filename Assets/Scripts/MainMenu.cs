using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator T_Anim;

    public string[] playTwoScenes, playThreeScenes, playFourScenes;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void PlayTwo()
    {
        StartCoroutine(LoadScene(playTwoScenes[Random.Range(0, playTwoScenes.Length)]));
    }
    public void PlayThree()
    {
        StartCoroutine(LoadScene(playThreeScenes[Random.Range(0, playThreeScenes.Length)]));
    }
    public void PlayFour()
    {
        StartCoroutine(LoadScene(playFourScenes[Random.Range(0, playFourScenes.Length)]));
    }

    IEnumerator LoadScene(string s)
    {
        T_Anim.SetTrigger("fade");
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadSceneAsync(s);
    }
}
