using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject homeMenuPanel;
    /*[SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject resetPlayerPrefs;
    [SerializeField] private GameObject confirmResetPlayerPrefs;*/
    public AudioSource backgroundSound;

    // metodo do botão de jogar 
    public void Play()
    {
        Debug.Log("sound " + backgroundSound.volume.ToString());
        PlayerPrefs.SetString("sound", backgroundSound.volume.ToString());
        PlayerPrefs.Save();
        SceneManager.LoadScene("SelectCharacter");
    }

    //metodo do botão de abrir opções
    /*public void OpenSettings()
    {
        homeMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
        settings.SetActive(false);
    }

    //metodo do botão de fechar opções
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        homeMenuPanel.SetActive(true);
        settings.SetActive(true);

    }
    public void OpenCredits()
    {
        homeMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
        settings.SetActive(false);

    }

    public void CloseCredits()
    {
        homeMenuPanel.SetActive(true);
        creditsPanel.SetActive(false);
        settings.SetActive(true);
    }
    */

    //metodo de sair do jogo
    public void Exit()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }

    /*public void OnpenResetPlayerPrefs()
    {
        resetPlayerPrefs.SetActive(true);
        homeMenuPanel.SetActive(false);
        settings.SetActive(false);
    }

    public void CloseResetPlayerPrefs()
    {
        resetPlayerPrefs.SetActive(false);
        homeMenuPanel.SetActive(true);
        settings.SetActive(true);
    }

    public void OpenConfirmResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        resetPlayerPrefs.SetActive(false);
        homeMenuPanel.SetActive(false);
        settings.SetActive(false);
        confirmResetPlayerPrefs.SetActive(true);

    }
    
    public void CloseConfirmResetPlayerPrefs()
    {
        homeMenuPanel.SetActive(true);
        settings.SetActive(true);
        confirmResetPlayerPrefs.SetActive(false);
    }*/

}
