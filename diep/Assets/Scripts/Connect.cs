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
    public GameObject loginPanel, findMatchPanel;

    private string namePlayerTemporary;
    private string nameRoomTemporary;

    private void Start()
    {
        namePlayerTemporary = PhotonNetwork.NickName = "JOGADOR_" + Random.Range(0, 1000);
        namePlayer.text = namePlayerTemporary;
    }
    public void Login()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.ConnectToRegion("sa");

        if (namePlayer.text != namePlayerTemporary)
            PhotonNetwork.NickName = namePlayer.text;
        else
            PhotonNetwork.NickName = namePlayerTemporary;

        loginPanel.SetActive(false);
    }

    public void ButtonFindMatch() //Encontrar partida
    {
        PhotonNetwork.JoinLobby();
    }

    public void ButtonCreateRoom() //Criar sala
    {
        string nameR;

        if (nameNewRoom.text != null)
            nameR = nameNewRoom.text;
        else
            nameR = "SALA_" + Random.Range(0, 1000);

        RoomOptions roomOptions = new RoomOptions() { MaxPlayers = 10 };

        PhotonNetwork.CreateRoom(nameR, roomOptions, TypedLobby.Default);
    }
    public void ButtonSpecificRoom() //Buscar sala específica
    {
        string nameR;

        if (nameSpecificRoom.text != nameRoomTemporary)
        {
            nameR = nameSpecificRoom.text;
            PhotonNetwork.JoinRoom(nameR);
        }
        else
        {
            Debug.Log("Nome não informado");
        }

    }

    public override void OnConnected()
    {
        Debug.Log("CONECTOU!");
        Debug.Log($"SERVIDOR: {PhotonNetwork.CloudRegion} PING: {PhotonNetwork.GetPing()}");
        findMatchPanel.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("CONECTOU NO MASTER!");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("ENTROU NO LOBBY!");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("FALHOU!");

        string roomName = "SALA_" + Random.Range(0, 1000);
        PhotonNetwork.CreateRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("ENTROU NA SALA!");
        Debug.Log("NOME DA SALA: " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("NÚMERO DE JOGADORES: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }
}