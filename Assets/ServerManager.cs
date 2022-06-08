using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ServerManager : MonoBehaviourPunCallbacks
{
    private readonly string GameVersion = "1";

    public Text StateText;
    public Button JoinButton;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.GameVersion = GameVersion;
        PhotonNetwork.ConnectUsingSettings();

        JoinButton.interactable = false;
        StateText.text = "Connecting...";
    }

    public override void OnConnectedToMaster()
    {
        JoinButton.interactable = true;
        StateText.text = "Online";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        JoinButton.interactable = false;
        StateText.text = $"ERROR : {cause.ToString()}";

        PhotonNetwork.ConnectUsingSettings(); 
    }

    public void Connect()
    {
        JoinButton.interactable = false;

        if(PhotonNetwork.IsConnected)
        {
            StateText.text = "Connecting to Room...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            StateText.text = "ERROR";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnJoinRandomFailed(short returnCode,string message)
    {
        StateText.text = "Create New Room";
        PhotonNetwork.CreateRoom(null,new RoomOptions {MaxPlayers = 4});
    }

    public override void OnJoinedRoom()
    {
        StateText.text = "JoinRoom";
        PhotonNetwork.LoadLevel("Main");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
