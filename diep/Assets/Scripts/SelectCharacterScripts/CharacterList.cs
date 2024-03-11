using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterList : MonoBehaviour
{
    public static CharacterList instance = null;

    public List<CharacterStats> characters = new List<CharacterStats>();

    private int selectedCharIndex; //Indice de personagens selecionados

    internal CharacterStats currentCharacter; //Personagem atual
    public int SelectedCharIndex
    {
        get { return selectedCharIndex; }
        set
        {
            if (value < 0) return;
            if (value >= characters.Count) return;
            selectedCharIndex = value;
            currentCharacter = characters[selectedCharIndex];
        }
    }
    private void Awake()
    {
        instance = this;
    }
    public CharacterStats GetPrevious()
    {
        var index = SelectedCharIndex - 1;
        if (index < 0) return null;
        return characters[SelectedCharIndex - 1];
    }
    public CharacterStats GetNext()
    {
        var index = SelectedCharIndex + 1;
        if (index >= characters.Count) return null;
        return characters[SelectedCharIndex + 1];
    }
    public void UpdateSpriteForPlayer(Player player, string characterName)
    {
        int spriteIndex = characters.FindIndex(c => c.characterName == characterName);
        if (spriteIndex != -1)
        {
            Sprite selectedSprite = characters[spriteIndex].face;

            // Encontrar o GameObject do jogador correspondente e atualizar o sprite
            GameObject playerObject = GetPlayerObjectByActorNumber(player.ActorNumber);
            if (playerObject != null)
            {
                playerObject.GetComponent<Image>().sprite = selectedSprite;
            }
        }
    }

    GameObject GetPlayerObjectByActorNumber(int actorNumber)
    {
        foreach (GameObject playerObject in GameObject.FindGameObjectsWithTag("Player"))
        {
            PhotonView photonView = playerObject.GetComponent<PhotonView>();
            if (photonView != null && photonView.Owner.ActorNumber == actorNumber)
            {
                return playerObject;
            }
        }
        return null;
    }
}
