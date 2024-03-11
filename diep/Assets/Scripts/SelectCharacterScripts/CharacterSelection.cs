using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviourPunCallbacks
{
    [SerializeField] CharacterPanel characterPanelMiddle;
    void Start()
    {
        CharacterList.instance.SelectedCharIndex = 0;
        UpdateCharacterPanels();
    }
    private void UpdateCharacterPanels()
    {
        characterPanelMiddle.UpdateCharacterPanel(CharacterList.instance.currentCharacter);
    }
    public void LeftButton()
    {
        if (CharacterList.instance.SelectedCharIndex > 0)
        {
            CharacterList.instance.SelectedCharIndex--;
        }
        else
        {
            CharacterList.instance.SelectedCharIndex = CharacterList.instance.characters.Count - 1;
        }
        Debug.Log("left");
        UpdateCharacterPanels();
    }

    public void RightButton()
    {
        if (CharacterList.instance.SelectedCharIndex < CharacterList.instance.characters.Count - 1)
        {
            CharacterList.instance.SelectedCharIndex++;
        }
        else
        {
            CharacterList.instance.SelectedCharIndex = 0;
        }
        Debug.Log("right");
        UpdateCharacterPanels();
    }
    public void Continue()
    {
        string characterName = CharacterList.instance.currentCharacter.characterName;

        // Sincroniza a escolha de sprite entre todos os jogadores
        photonView.RPC("SyncSpriteChoice", RpcTarget.AllBuffered, characterName);
        // Salva a escolha do personagem no PlayerPrefs
        PlayerPrefs.SetString("characterName", characterName);
        PlayerPrefs.Save();
    }

    [PunRPC]
    void SyncSpriteChoice(string characterName)
    {
        CharacterList.instance.UpdateSpriteForPlayer(photonView.Owner, characterName);
    }
}
