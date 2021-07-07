using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : Photon.PunBehaviour
{
    public Text logText;
    private void Awake()
    {
        Cursor.visible = true;
    }
    private void Start()
    {
        PhotonNetwork.playerName = DataBaseManager.Instance.Name;
        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.gameVersion = "1";
        PhotonNetwork.ConnectUsingSettings(PhotonNetwork.gameVersion);
    }

    public override void OnConnectedToMaster()
    {
        Log("Connected to the Internet");
    }

    //Методы для создания комнаты и подключения к ней
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        Log("Joined the Room");

        PhotonNetwork.LoadLevel("TestCharacter");
    }

    private void Log(string message)
    {
        Debug.Log(message);
        logText.text += "\n";

        logText.text += message;
    }

}
