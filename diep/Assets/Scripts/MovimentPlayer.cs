using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class MovimentPlayer : MonoBehaviourPunCallbacks
{
    public GameObject canvaGameOver;
    public GameObject canvaWinGame;
    public GameObject canvaLoadPlayers;

    public GameObject player;
    public GameObject gun;
    public float rotationSpeed = 100f;
    public float speed = 5f;
    public Transform heathPlayer;
    public float heath = 100;
    public TextMeshProUGUI playerNameText;  // Adiciona essa variável para armazenar a referência ao TextMeshProUGUI
    public List<Sprite> listClothesPlayer;
    private GameObject loadPlayers;
    private float heathPercent;
    private Vector3 heathScale;
    private bool onePlayer;

    void Start()
    {
        UpdateClothesPlayer();

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1 && photonView.IsMine)
        {
            Camera camera = FindObjectOfType<Camera>();
            loadPlayers = Instantiate(canvaLoadPlayers, camera.GetComponent<CameraController>().cameraPosition, Quaternion.identity);
            loadPlayers.GetComponent<Canvas>().worldCamera = camera;
            gun.GetComponent<Bullet>().enabled = false;

            onePlayer = true;
        }
        else
        {
            onePlayer = false;
        }
        if (!photonView.IsMine)
        {
            // Se não for o jogador local, desativa este script
            gun.GetComponent<Bullet>().enabled = false;
            enabled = false;
        }
        else
        {
            Camera camera = FindObjectOfType<Camera>();
            camera.GetComponent<CameraController>().player = transform;
        }

        // Chama o método para configurar o sistema de vida
        LifeSystem();

        // Obtém o nome do jogador do Photon
        string playerName = photonView.Owner.NickName;

        // Atualiza o TextMeshProUGUI com o nome do jogador
        if (playerNameText != null)
        {
            playerNameText.text = playerName;
        }
    }
    private void UpdateClothesPlayer()
    {
        if (PlayerPrefs.HasKey("characterName"))
        {
            string spriteName = PlayerPrefs.GetString("characterName");
            photonView.RPC("UpdateSprite", RpcTarget.AllBuffered, spriteName);
        }
    }

    [PunRPC]
    void UpdateSprite(string spriteName)
    {
        int spriteIndex = CharacterList.instance.characters.FindIndex(c => c.characterName == spriteName);
        if (spriteIndex != -1)
        {
            GetComponent<Image>().sprite = CharacterList.instance.characters[spriteIndex].face;
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            onePlayer = false;
            if (loadPlayers != null)
            {
                Destroy(loadPlayers);
            }
            if (photonView.IsMine)
                gun.GetComponent<Bullet>().enabled = true;

        }
    }
    void Update()
    {
        if (photonView.IsMine)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1 && !onePlayer)
            {
                Camera camera = FindObjectOfType<Camera>();
                GameObject winGame = Instantiate(canvaWinGame, camera.GetComponent<CameraController>().cameraPosition, Quaternion.identity);
                winGame.GetComponent<Canvas>().worldCamera = camera;
            }

            if (heath > 0f) // Check if the player is alive
            {
                // If the player is alive, execute the logic for movement and rotation
                Moviment();
                Rotation();
            }
            else
            {
                // If the player is dead, disable movement and rotation
                enabled = false;
            }
        }
    }
    void Moviment()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, verticalInput, 0f).normalized;
        Vector3 currentPosition = transform.position;
        Vector3 newPosition = currentPosition + movementDirection * speed * Time.deltaTime;
        transform.position = newPosition;
    }

    void Rotation()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.z - transform.position.z));
        Vector3 direction = mouseWorldPosition - transform.position;
        direction.z = 0f;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    void LifeSystem()
    {
        heathScale = heathPlayer.localScale;
        heathPercent = heathScale.x / heath;
    }

    void UpdateHealth()
    {
        heathScale.x = heathPercent * heath;
        heathPlayer.localScale = heathScale;
    }

    public void TakeDamage(float damage)
    {
        heath -= damage;
        UpdateHealth();
        if (heath <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        if (photonView.IsMine)
        {
            Camera camera = FindObjectOfType<Camera>();
            GameObject gameOver = Instantiate(canvaGameOver, camera.GetComponent<CameraController>().cameraPosition, Quaternion.identity);
            gameOver.GetComponent<Canvas>().worldCamera = camera;

            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Destroy(player);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            //Destroy(collision.gameObject);
            TakeDamage(20);
        }

        if (collision.gameObject.CompareTag("Kit"))
        {
            Destroy(collision.gameObject);
            heath += 10;
        }
    }

}
