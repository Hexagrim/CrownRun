using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
public class ConnectUiScript : MonoBehaviour
{
    [SerializeField] Button hostButton;

    [SerializeField] Button clientButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hostButton.onClick.AddListener(HostButtonClick);
        clientButton.onClick.AddListener(ClientButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void HostButtonClick()
    {
        NetworkManager.Singleton.StartHost();
    }
    void ClientButtonClick()
    {
        NetworkManager.Singleton.StartClient();
    }
}
