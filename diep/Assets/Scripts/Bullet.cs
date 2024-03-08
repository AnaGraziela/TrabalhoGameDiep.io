using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bullet : MonoBehaviour
{
    public Canvas canvasBullets;  // Referência ao Canvas
    public GameObject projectPrefab;
    public float speedBullet = 10f;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        // Instancia o objeto usando PhotonNetwork
        GameObject projectile = PhotonNetwork.Instantiate(projectPrefab.name, transform.position, Quaternion.identity);

        // Define o canvasBullets como o pai do objeto instanciado
        projectile.transform.SetParent(canvasBullets.transform);

        // Obtém o componente Rigidbody do projétil
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


