using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class MovimentPlayer : MonoBehaviourPun
{
    public GameObject canvaGameOver;
    public GameObject canvaWinGame;
    public GameObject gun;
    public float rotationSpeed = 100f;
    public float speed = 5f;
    public Transform heathPlayer;
    private Vector3 heathScale;
    private float heathPercent;
    private float heath = 100;
    public TextMeshProUGUI playerNameText;  // Adiciona essa vari�vel para armazenar a refer�ncia ao TextMeshProUGUI

    void Start()
    {
        if (!photonView.IsMine)
        {
            // Se n�o for o jogador local, desativa este script
            gun.GetComponent<Bullet>().enabled = false;
            enabled = false;
        }
        else
        {
            Camera camera = FindObjectOfType<Camera>();
            camera.GetComponent<CameraController>().player = transform;
        }

        // Chama o m�todo para configurar o sistema de vida
        LifeSystem();

        // Obt�m o nome do jogador do Photon
        string playerName = photonView.Owner.NickName;

        // Atualiza o TextMeshProUGUI com o nome do jogador
        if (playerNameText != null)
        {
            playerNameText.text = playerName;
        }
    }
    void Update()
    {
        if (photonView.IsMine)
        {
            // Se for o jogador local, executa a l�gica de movimenta��o
            Moviment();
            Rotation();
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

            PhotonNetwork.Destroy(gameObject);
        }
        else
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                Camera camera = FindObjectOfType<Camera>();
                GameObject winGame = Instantiate(canvaWinGame, camera.GetComponent<CameraController>().cameraPosition, Quaternion.identity);
                winGame.GetComponent<Canvas>().worldCamera = camera;
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            TakeDamage(20);
        }
    }
}
