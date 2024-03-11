using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public Camera camera;
    public GameObject playerPrefab; // Prefab do jogador
    public GameObject loadPlayersPopup; // Pop-up de aguardando jogadores

    private GameObject localPlayer;

    private void Start()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            SpawnPlayer();
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Novo jogador entrou na sala: " + newPlayer.NickName);

        if (PhotonNetwork.CurrentRoom.PlayerCount <= 1)
        {
            loadPlayersPopup = Instantiate(loadPlayersPopup, camera.GetComponent<CameraController>().cameraPosition, Quaternion.identity);
            loadPlayersPopup.GetComponent<Canvas>().worldCamera = camera;
        }
       else
        {
            if (localPlayer == null)
            {
                SpawnPlayer();
                PhotonNetwork.Destroy(loadPlayersPopup); // Remove o pop-up quando outro jogador entra
            }
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
