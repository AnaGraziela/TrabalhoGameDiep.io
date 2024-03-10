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
        else
        {
            // Se houver apenas um jogador na sala, mostra o pop-up de aguardando jogadores
            ShowLoadPlayersPopup();
        }
    }

    private void ShowLoadPlayersPopup()
    {
        // Instancia o pop-up de aguardando jogadores
        loadPlayersPopup = Instantiate(loadPlayersPopup, camera.GetComponent<CameraController>().cameraPosition, Quaternion.identity);
        loadPlayersPopup.GetComponent<Canvas>().worldCamera = camera;

        // Inicia a verificação de jogadores
        StartCoroutine(CheckForPlayers());
    }

    private IEnumerator CheckForPlayers()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f); // Verifica a cada 2 segundos (ajuste conforme necessário)

            // Se houver mais de um jogador na sala, remove o pop-up e inicia o jogo
            if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
            {
                Destroy(loadPlayersPopup);
                SpawnPlayer();
                break; // Sai do loop
            }
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
