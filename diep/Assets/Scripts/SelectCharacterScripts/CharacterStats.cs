using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Ao contrário de MonoBehaviour, que é usado para criar comportamentos, 
ScriptableObject é usado especificamente para armazenar dados e não requer
um objeto de jogo anexado para funcionar.*/    

[CreateAssetMenu(fileName = "Character_", menuName = "ScriptableObject/Character")]

public class CharacterStats : ScriptableObject
{
    public string characterName;
    public Sprite face;
}
