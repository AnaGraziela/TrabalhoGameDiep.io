using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    public Canvas canvasBullets;  // Referência ao Canvas
    public GameObject projectPrefab;
    public float speedBullet = 10f;

    void Update()
    {
        // Verifica se o jogador local está pressionando o botão de tiro
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            // Instancia o projétil localmente
            GameObject projectile = Instantiate(projectPrefab, transform.position, transform.rotation, canvasBullets.transform);

            // Obtém o componente Rigidbody2D do projétil
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            // Verifica se o Rigidbody2D do projétil existe
            if (rb != null)
            {
                // Aplica velocidade ao projétil na direção para a frente do jogador
                rb.velocity = transform.right * speedBullet;
            }
            else
            {
                Debug.LogWarning("Rigidbody2D não encontrado no prefab do projétil.");
            }
        }
    }
}
