using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Serialization;


namespace Com.MyCompany.MyGame
{
    public class Launcher : MonoBehaviourPunCallbacks 
    {
        #region Private Serializable Fields

        [SerializeField]
        private byte _maxBoiInRoom = 8;

        #endregion


        #region Private Fields
 
        private string gameVersion = "1";
        bool _isConnecting;


        #endregion


        #region MonoBehaviour CallBacks


        public void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }


        void Start()
        {
            Connect();
        }


        #endregion


        #region Public Methods


        public void Connect()
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                _isConnecting = PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }

        }
        #endregion


 #region MonoBehaviourPunCallbacks Callbacks


    public override void OnConnectedToMaster()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");

        if (_isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
            _isConnecting = false;
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
    // #Critical
    // Load the Room Level.
    PhotonNetwork.LoadLevel("Main");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");
        PhotonNetwork.CreateRoom(null, new RoomOptions(){MaxPlayers = _maxBoiInRoom});
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
    }


#endregion   


    }
}