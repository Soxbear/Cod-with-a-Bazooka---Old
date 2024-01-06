using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject Music;
    public Player Player;

    public AudioClip Fail;
    public AudioClip SucceedBuy;

    AudioSource Audio;

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void ToggleBBS()
    {
        if (!transform.GetChild(2).gameObject.activeInHierarchy)
        {
            transform.GetChild(2).gameObject.SetActive(true);
            Time.timeScale = 0.25f;
            Player.CanShoot = false;
        }
        else
        {
            transform.GetChild(2).gameObject.SetActive(false);
            Time.timeScale = 1f;
            Player.CanShoot = true;
        }
    }

    public void BBUpgrade(int Upgrade)
    {
        if (PlayerPrefs.GetInt("Bazooka Badges") > 0)
        {
            PlayerPrefs.SetInt("Bazooka Badges", PlayerPrefs.GetInt("Bazooka Badges") - 1);
            PlayerPrefs.SetInt("Upgrade " + Upgrade.ToString(), PlayerPrefs.GetInt("Upgrade " + Upgrade.ToString()) + 1);
            Audio.clip = SucceedBuy;
            Audio.Play();
            transform.GetChild(2).GetChild(Upgrade).GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = (PlayerPrefs.GetInt("Upgrade " + Upgrade.ToString()) + 1).ToString();
            if (PlayerPrefs.GetInt("Upgrade " + Upgrade.ToString()) == 4)
            {
                transform.GetChild(2).GetChild(Upgrade).GetChild(0).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "MAX";
                transform.GetChild(2).GetChild(Upgrade).GetChild(0).GetComponent<UnityEngine.UI.Button>().interactable = false;
            }
            if (Upgrade == 1)
            {
                Player.MaxSpeed = 3 + (0.5f * PlayerPrefs.GetInt("Upgrade 1"));
                Player.Acceleration = 0.2f + (0.04f * PlayerPrefs.GetInt("Upgrade 1"));
            }
            else if (Upgrade == 2)
            {
                Player.MaxHealth = 50 + (10 * PlayerPrefs.GetInt("Upgrade 2"));
                Player.TakeDamage(-10);
            }
            else if (Upgrade == 3)
                Player.ShootSpeed = 0.7f - (0.1f * PlayerPrefs.GetInt("Upgrade 3"));
            else if (Upgrade == 4)
                Player.Damage = 15 + (5 * PlayerPrefs.GetInt("Upgrade 4"));
        }
        else
        {
            Audio.clip = Fail;
            Audio.Play();
        }
    }

    void Start()
    {
        Audio = GetComponent<AudioSource>();
        Player.MaxSpeed = 3 + (0.5f * PlayerPrefs.GetInt("Upgrade 1"));
        Player.Acceleration = 0.2f + (0.04f * PlayerPrefs.GetInt("Upgrade 1"));
        Player.MaxHealth = 50 + (10 * PlayerPrefs.GetInt("Upgrade 2"));
        Player.TakeDamage(-10);
        Player.ShootSpeed = 0.7f - (0.1f * PlayerPrefs.GetInt("Upgrade 3"));
        Player.Damage = 15 + (5 * PlayerPrefs.GetInt("Upgrade 4"));
        if (PlayerPrefs.GetInt("Upgrade 1") == 4)
        {
            transform.GetChild(2).GetChild(1).GetChild(0).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "MAX";
            transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
        if (PlayerPrefs.GetInt("Upgrade 2") == 4)
        {
            transform.GetChild(2).GetChild(2).GetChild(0).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "MAX";
            transform.GetChild(2).GetChild(2).GetChild(0).GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
        if (PlayerPrefs.GetInt("Upgrade 3") == 4)
        {
            transform.GetChild(2).GetChild(3).GetChild(0).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "MAX";
            transform.GetChild(2).GetChild(3).GetChild(0).GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
        if (PlayerPrefs.GetInt("Upgrade 4") == 4)
        {
            transform.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "MAX";
            transform.GetChild(2).GetChild(4).GetChild(0).GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        if (PlayerPrefs.GetInt("BBUnlocked") == 1)
            transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
        else
            transform.GetChild(1).GetChild(1).gameObject.SetActive(false);

        transform.GetChild(2).GetChild(1).GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = (PlayerPrefs.GetInt("Upgrade 1") + 1).ToString();
        transform.GetChild(2).GetChild(2).GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = (PlayerPrefs.GetInt("Upgrade 2") + 1).ToString();
        transform.GetChild(2).GetChild(3).GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = (PlayerPrefs.GetInt("Upgrade 3") + 1).ToString();
        transform.GetChild(2).GetChild(4).GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = (PlayerPrefs.GetInt("Upgrade 4") + 1).ToString();
    }

    void FixedUpdate()
    {
        transform.GetChild(2).GetChild(5).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = PlayerPrefs.GetInt("Bazooka Badges").ToString();
            if (PlayerPrefs.GetInt("BBUnlocked") == 1)
                transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
    }

    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
        Player.MaxSpeed = 3;
        Player.Acceleration = 0.2f;
        Player.MaxHealth = 50;
        Player.ShootSpeed = 0.7f;
        Player.Damage = 15;
    }
}
