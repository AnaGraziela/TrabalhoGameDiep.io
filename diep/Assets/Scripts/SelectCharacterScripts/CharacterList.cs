using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterList : MonoBehaviour
{
    public static CharacterList instance = null;    
    
    public List<CharacterStats> characters = new List<CharacterStats>();

    private int selectedCharIndex; //Indice de personagens selecionados
    
    public int SelectedCharIndex
    {
        get { return selectedCharIndex; }   
        set { 
            if(value < 0) return;
            if (value >= characters.Count) return;
            selectedCharIndex = value;
            currentCharacter = characters[selectedCharIndex];
        }
    }
    internal CharacterStats currentCharacter; //Personagem atual
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
}
