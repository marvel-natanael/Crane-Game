using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField unInput;
    [SerializeField] private TMP_InputField joinInput;
    [SerializeField] private Button joinButton;
    [SerializeField] private Button createButton;

    private void Start()
    {
        if (PhotonNetwork.IsConnected)
		{
			if (PhotonNetwork.Server == ServerConnection.GameServer)
			{
				this.OnJoinedRoom();

			}
			else if (PhotonNetwork.Server == ServerConnection.MasterServer || PhotonNetwork.Server == ServerConnection.NameServer)
			{

				if (PhotonNetwork.InLobby)
				{
					this.OnJoinedLobby();
				}
				else
				{
					this.OnConnectedToMaster();
				}

			}
		}
	}

    public void setUname()
    {
        PhotonNetwork.NickName = unInput.text;
    }

    public void createGame()
    {
        PhotonNetwork.CreateRoom(joinInput.text, new RoomOptions() { MaxPlayers = 2 }, null);
    }

    public void joinGame()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom(joinInput.text, roomOptions, TypedLobby.Default);
    }

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Test_Map");
    }
}
