using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Refer�ncia ao objeto do jogador
    public float cameraSpeed = 5f; // Velocidade de suaviza��o da c�mera

    public float maxXLimit; // Limite m�ximo do mapa em X
    public float minXLimit; // Limite m�nimo do mapa em X
    public float maxYLimit; // Limite m�ximo do mapa em Y
    public float minYLimit; // Limite m�nimo do mapa em Y
    public Vector3 offset;  // armazenar a diferen�a entre a posi��o da c�mera e a posi��o do jogador no in�cio do jogo.
    private void Update()
    {
        if (player != null)
            CameraControllerPosition();
    }

    private void CameraControllerPosition()
    {
        CamFollowingPlayer(maxXLimit, minXLimit, maxYLimit, minYLimit);
    }

    public void CamFollowingPlayer(float maxXLimit, float minXLimit, float maxYLimit, float minYLimit)
    {
        // Calcula a posi��o desejada da c�mera com base na posi��o do jogador e o offset
        // Calcula a posi��o desejada da c�mera somando a posi��o atual do jogador (player.position) com o deslocamento (offset) que foi calculado anteriormente
        Vector3 desiredPosition = player.position + offset;

        // Limita a posi��o desejada nos limites definidos
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minXLimit, maxXLimit);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minYLimit, maxYLimit);
        // Debug.Log(maxXLimit + "\n" + minXLimit + "\n" + maxYLimit + "\n" +minYLimit);

        // Suaviza o movimento da c�mera usando Lerp
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, cameraSpeed);

        // Atualiza a posi��o da c�mera
        transform.position = smoothedPosition;
    }
}