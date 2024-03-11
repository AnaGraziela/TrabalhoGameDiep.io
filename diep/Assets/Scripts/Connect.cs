using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class Connect : MonoBehaviourPunCallbacks
{
    public TMP_InputField namePlayer;
    public TMP_InputField nameNewRoom;
    public TMP_InputField nameSpecificRoom;

    private string playerName;
    private string roomName;

    private void Start()
    {
        playerName = "JOGADOR_" + Random.Range(0, 1000);
        namePlayer.text = playerName;
        ConnectToPhoton();
    }

    private void ConnectToPhoton()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.ConnectToRegion("sa");

        PhotonNetwork.NickName = namePlayer.text;
    }

    public void ButtonFindMatch()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void ButtonCreateRoom()
    {
        roomName = (string.IsNullOrEmpty(nameNewRoom.text)) ? "SALA_" + Random.Range(0, 1000) : nameNewRoom.text;

        RoomOptions roomOptions = new RoomOptions() { MaxPlayers = 10 };
        PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default);
        //PhotonNetwork.NickName = namePlayer.text;
        //PhotonNetwork.LoadLevel("Game");
    }

    public void ButtonSpecificRoom()
    {
        roomName = nameSpecificRoom.text;

        if (!string.IsNullOrEmpty(roomName))
        {
            PhotonNetwork.NickName = namePlayer.text;
            PhotonNetwork.JoinRoom(roomName);
        }
        else
        {
            Debug.Log("Nome da sala não informado.");
        }
    }

    public void ButtonExit()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("CONECTOU NO MASTER!");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Entrou no Lobby!");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Falhou ao entrar em uma sala aleatória.");

        // Cria uma sala aleatória se não encontrar
        ButtonCreateRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Entrou na sala " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.NickName = namePlayer.text;
        PhotonNetwork.LoadLevel("Game");
    }
}
