using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentPlayer : MonoBehaviour
{
    float speed = 5f;
    float rotationSpeed = 100f;
    public Transform heathPlayer;
    public GameObject heathObject;
    private Vector3 heathScale;
    private float heathPercent;
    private float heath = 100;




    void Start()
    {
        LifeSystem();
    }

    // Update is called once per frame
    void Update()
    {
        Moviment();
        Rotation();
        

    }
    void Moviment()
    {
        Vector3 movementDirection;
        float rotationInput = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            movementDirection = new Vector3(0, 1, 0f).normalized;

            Vector3 currentPosition = transform.position;
            Vector3 newPosition = currentPosition + movementDirection * speed * Time.deltaTime;


            transform.position = newPosition;
        }

        if (Input.GetKey(KeyCode.S))
        {
            movementDirection = new Vector3(0, -1, 0f).normalized;

            Vector3 currentPosition = transform.position;
            Vector3 newPosition = currentPosition + movementDirection * speed * Time.deltaTime;


            transform.position = newPosition;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movementDirection = new Vector3(1, 0, 0f).normalized;

            Vector3 currentPosition = transform.position;
            Vector3 newPosition = currentPosition + movementDirection * speed * Time.deltaTime;


            transform.position = newPosition;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movementDirection = new Vector3(-1, 0, 0f).normalized;

            Vector3 currentPosition = transform.position;
            Vector3 newPosition = currentPosition + movementDirection * speed * Time.deltaTime;


            transform.position = newPosition;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotationInput = -1;
            float rotation = rotationInput * speed * Time.deltaTime;


            transform.Rotate(Vector3.forward, rotation);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotationInput = 1;
            float rotation = rotationInput * speed * Time.deltaTime;


            transform.Rotate(Vector3.forward, rotation);
        }







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
        heathPlayer.localScale= heathScale;
    }
    public void TakeDamage(float damage)
    {
        heath -= damage;
        UpdateHealth();
        if(heath <= 0f)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Voce morreu");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemyBullet"))
        {
            TakeDamage(20);
        }
    }
}


