using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterPanel : MonoBehaviour //Painel de Personagem
{
    [SerializeField] GameObject transparentPanel; //painel transparente
    [SerializeField] bool showTranparentPanel; //mostrar painel transparente
    [SerializeField] Image charImage; //mostrar painel transparente   
    internal void UpdateCharacterPanel(CharacterStats characterStats)
    {
        if (characterStats == null)
        {
            charImage.sprite = null;
        }
        else
        {
            //Atualizar sprite
            charImage.sprite = characterStats.face;
        }
    }

    void Start()
    {
        transparentPanel.SetActive(showTranparentPanel);
    }
}
