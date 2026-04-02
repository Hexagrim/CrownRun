using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectionHandler : MonoBehaviour
{
    public bool canJoin = true; 

    private void Awake()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
    }
    private void Update()
    {
        if (!NetworkManager.Singleton.IsServer) return;

        if(SceneManager.GetActiveScene().name != "Lobby" )
        {
            canJoin = false;
        }
        else
        {
            canJoin = true;
        }
    }
    private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request,
                               NetworkManager.ConnectionApprovalResponse response)
    {
        if (!NetworkManager.Singleton.IsServer)
            return;

        if (!canJoin)
        {
            response.Approved = false;
            response.Reason = "Server is not accepting players right now.";
            return;
        }


        // APPROVE
        response.Approved = true;
        response.CreatePlayerObject = true;
        response.Position = Vector3.zero;
        response.Rotation = Quaternion.identity;
    }
}
