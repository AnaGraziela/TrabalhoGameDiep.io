using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bullet : MonoBehaviour
{
    public Canvas canvasBullets;  // Refer�ncia ao Canvas
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

        // Obt�m o componente Rigidbody do proj�til
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // Verifica se o Rigidbody2D do proj�til existe
        if (rb != null)
        {
            // Aplica velocidade ao proj�til na dire��o para a frente do jogador
            rb.velocity = transform.right * speedBullet;
        }
        else
        {
            Debug.LogWarning("Rigidbody2D n�o encontrado no prefab do proj�til.");
        }
    }
}


