using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
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
        PlayerPrefs.SetString("characterName", CharacterList.instance.currentCharacter.characterName);
        PlayerPrefs.Save();
        SceneManager.LoadScene("SelectLevel");
    }
}
