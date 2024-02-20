using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GameObject projectile = Instantiate(projectPrefab, transform.position, transform.rotation, canvasBullets.transform);

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


