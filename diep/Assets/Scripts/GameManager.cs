using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public Camera camera;
    public GameObject playerPrefab; // Prefab do jogador
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

        if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            if (localPlayer == null)
            {
                SpawnPlayer();
            }
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Jogador saiu da sala: " + otherPlayer.NickName);

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            // Se o �nico jogador sair, destruir a sala
            PhotonNetwork.LeaveRoom();
        }
    }

    private void SpawnPlayer()
    {
        // Instancia o jogador na posi��o inicial desejada
        localPlayer = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        localPlayer.GetComponent<Canvas>().worldCamera = camera;
        /*// Desabilita o script de movimenta��o se n�o houver jogadores suficientes
        if (PhotonNetwork.CurrentRoom.PlayerCount <= 1)
        {
            MovimentPlayer movimentPlayer = localPlayer.GetComponent<MovimentPlayer>();
            if (movimentPlayer != null)
            {
                movimentPlayer.enabled = false;
            }
        }*/
    }

    public void ExitGame()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}
