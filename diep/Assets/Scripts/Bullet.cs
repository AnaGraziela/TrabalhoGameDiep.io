using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviourPun
{
    public GameObject canvasBullets;  // Refer�ncia ao Canvas
    public GameObject projectPrefab;
    public float speedBullet = 10f;

    private void Update()
    {
        if (photonView.IsMine && Input.GetMouseButtonDown(0))
        {
            if (GetComponentInParent<MovimentPlayer>().heath > 0f) 
            {
                photonView.RPC("Shoot", RpcTarget.AllViaServer);
            }
        }
    }

    [PunRPC]
    void Shoot()
    {
        // Instancia o objeto usando PhotonNetwork
        GameObject projectile = PhotonNetwork.Instantiate(projectPrefab.name, transform.position, Quaternion.identity);

        // Define o canvasBullets como o pai do objeto instanciado
        projectile.transform.parent = canvasBullets.transform;

        // Obt�m o componente Rigidbody do proj�til
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // Verifica se o Rigidbody2D do proj�til existe
        if (rb != null)
        {
            // Aplica velocidade ao proj�til na dire��o para a frente do jogador
            rb.velocity = transform.right * speedBullet;
        }
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bullet : MonoBehaviour
{
    public GameObject canvasBullets;  // Refer�ncia ao Canvas
    public GameObject projectPrefab;
    public float speedBullet = 10f;

    private void Start()
    {
       // canvasBullets = GameObject.FindGameObjectWithTag("CanvasBullets");
    }
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
        projectile.transform.parent = canvasBullets.transform;

        // Obt�m o componente Rigidbody do proj�til
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // Verifica se o Rigidbody2D do proj�til existe
        if (rb != null)
        {
            // Aplica velocidade ao proj�til na dire��o para a frente do jogador
            rb.velocity = transform.right * speedBullet;
        }
    }
}
*/

