using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMatchBehaviour : NetworkBehaviour
{
    public Button StartBtn;
    bool started = false;
    private void Start()
    {
        
    }
    public override void OnNetworkSpawn()
    {
        if (!IsServer && StartBtn != null)
        {

            StartBtn.gameObject.SetActive(false);
            
        }
        if(IsServer) StartBtn.onClick.AddListener(StartMatch);



    }

    
    void Update()
    {
        
    }
    void StartMatch()
    {
        if (started || !IsServer) 
        { 
            StartBtn.gameObject.SetActive(false);
            return;
        }

        StartCoroutine(MatchStart(3));
    }
    IEnumerator MatchStart(float t)
    {
        
        started = true;
        yield return new WaitForSeconds(t);
        NetworkManager.Singleton.SceneManager.LoadScene("Map1", LoadSceneMode.Single);
    }
}
