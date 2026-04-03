using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MatchManager : NetworkBehaviour
{
    public float MatchDuration = 60;
    public float MaxPlayers = 2;
    CountdownServer CountdownSrv;
    public GameObject WinnerDisplay;
    public float winnerDisplayTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CountdownSrv = FindFirstObjectByType<CountdownServer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CountdownSrv.IsFinished())
        {
            EndMatch();
        }
    }
    public void StartMatch()
    {
        if (!IsServer) return;
        CountdownSrv.StartCountdownRpc(MatchDuration);

    }
    public void EndMatch()
    {
        if (!IsServer) return;
        CountdownSrv.StopCountdownRpc();
        
    }

    IEnumerator MatchEndSequence()
    {
        showWinner();
        yield return new WaitForSeconds(winnerDisplayTime);
        NetworkManager.Singleton.SceneManager.LoadScene("Map1", LoadSceneMode.Single);

    }
    void showWinner()
    {
        WinnerDisplay.SetActive(true);
    }


}
