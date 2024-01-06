using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject Explosion;
    public string SceneName;

    public GameObject Settings;
    public GameObject Main;

    public void SpawnExplosion()
    {
        Instantiate(Explosion, transform);
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ActivateSettings()
    {
        Settings.SetActive(true);
        Main.SetActive(false);
    }

    public void ActivateMain()
    {
        Main.SetActive(true);
        Settings.SetActive(false);
    }

    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }
}
