using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public Camera camera;
    public GameObject playerPrefab; // Prefab do jogador

    private GameObject localPlayer;

    void Start()
    {
        SpawnPlayer();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Novo jogador entrou na sala: " + newPlayer.NickName);
        if (localPlayer == null)
        {
            SpawnPlayer();
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Jogador saiu da sala: " + otherPlayer.NickName);

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            // Se o único jogador sair, destruir a sala
            PhotonNetwork.LeaveRoom();
        }
    }

    private void SpawnPlayer()
    {
        // Instancia o jogador na posição inicial desejada
        localPlayer = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        localPlayer.GetComponent<Canvas>().worldCamera = camera;
    }

    public void ExitGame()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Menu");
    }
}
