using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Player player;
    public Text coinText;
    public Image[] hearts;
    public Sprite isLife, nonLife;
    public GameObject PauseScreen, WinScreen, LoseScreen, inventoryPanel, buttonPause;
    public SoundEffect soundEffect;
    int allCoins;

    private void Start()
    {
        Application.targetFrameRate = PlayerPrefs.GetInt("fps");
        allCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
    }
    
    public void ReloadLevel()
    {
        PlayerPrefs.SetInt("cafe", 1);
        Time.timeScale = 1f;
        player.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Update()
    {
        coinText.text = player.GetCoins().ToString() + "/" + allCoins.ToString();

        for(int i = 0; i < hearts.Length; i++)
        {
            if (player.GetHP() > i)
                hearts[i].sprite = isLife;
            else
                hearts[i].sprite = nonLife;
        }
    }

    public void PauseOn()
    {
        Time.timeScale = 0f;
        player.enabled = false;
        PauseScreen.SetActive(true);
    }

    public void PauseOff()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        PauseScreen.SetActive(false);
    }

    public void Win()
    {
        soundEffect.PlayWinSound();
        Time.timeScale = 0f;
        player.enabled = false;
        WinScreen.SetActive(true);

        if (!PlayerPrefs.HasKey("Lvl") || PlayerPrefs.GetInt("Lvl") < SceneManager.GetActiveScene().buildIndex)
            PlayerPrefs.SetInt("Lvl", SceneManager.GetActiveScene().buildIndex);

        if (PlayerPrefs.HasKey("coins"))
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + player.GetCoins());
        else
            PlayerPrefs.SetInt("coins", player.GetCoins());

        inventoryPanel.SetActive(false);
        GetComponent<Inventory>().RecountItems();

        buttonPause.SetActive(false);
    }

    public void Lose()
    {
        soundEffect.PlayLoseSound();
        Time.timeScale = 0f;
        player.enabled = false;
        LoseScreen.SetActive(true);

        inventoryPanel.SetActive(false);
        GetComponent<Inventory>().RecountItems();

        buttonPause.SetActive(false);
    }

    public void MenuLevel()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        SceneManager.LoadScene("Cafe");
        PlayerPrefs.SetInt("cafe", 1);
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.SetInt("cafe", 1);
    }

    private void OnApplicationPause(bool pause)
    {
        PlayerPrefs.SetInt("cafe", 0);
    }
}
