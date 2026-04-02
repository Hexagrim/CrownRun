using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectionHandler : MonoBehaviour
{
    private void Awake()
    {
        // Only server/host should handle approval
        if (NetworkManager.Singleton.IsServer)
        {
            NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
        }
    }

    private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request,
                               NetworkManager.ConnectionApprovalResponse response)
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName != "Lobby")
        {
            response.Approved = false;
            response.Reason = "Match already started";
            return;
        }

        response.Approved = true;
        response.CreatePlayerObject = true;
    }
}
