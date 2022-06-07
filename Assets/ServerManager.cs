
using UnityEngine;
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
        StateText.Text = "Connecting...";
    }

    public override void OnConnectedToMaster()
    {
        JoinButton.interactable = true;
        StateText.Text = "Online";
    }

    public override OnDisconnected(OnDisconnectCause cause)
    {
        JoinButton.interactable = false;
        StateText.Text = $"ERROR : {cause.ToString()}";

        PhotonNetwork.ConnectUsingSettings(); 
    }

    public void Connect()
    {
        JoinButton.interactable = false;

        if(PhotonNetwork.IsConnected)
        {
            StateText.Text = "Connecting to Room...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            StateText.Text = "ERROR";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnJoinRamdomFailed(short returnCode,string message)
    {
        StateText.Text = "Create New Room";
        PhotonNetwork.CreateRoom(null,new RoomOptions {MaxPlayers = 4});
    }

    public override void OnJoinedRoom()
    {
        StateText.Text = "JoinRoom";
        PhotonNetwork.LoadLevel("Main");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
