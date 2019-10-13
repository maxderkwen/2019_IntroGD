using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField]
    private int health=5;
    public int Health { get { return health; }set { health = value; } }

    [SerializeField]
    private int score = 0;
    public int Score { get { return score; } set { score = value; } }

    [SerializeField]
    private Text HealthText;
    [SerializeField]
    private BeansManager beansManager;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject MainCamera;
    [SerializeField]
    private GameObject losingUI;
    [SerializeField]
    private GameObject winingUI;
    [SerializeField]
    private Text ScoreUI;
    private void Start()
    {
        Time.timeScale = 1;

        score=PlayerPrefs.GetInt("WinningScore");

    }

    private void FixedUpdate()
    {
        checkHealth();
        checkScore();
        Losing();
        Winning();
        
    }
    private void checkHealth()
    {
        HealthText.text = "x"+ health;
    }

    private void checkScore()
    {
        ScoreUI.text = ""+score;
    }
    private void Losing()
    {
        if (health < 0) {
            PlayerPrefs.SetInt("WinningScore", 0);
            PlayerPrefs.Save();
            health = 0;
            HealthText.text = "x" + health;
            player.transform.GetComponent<AudioSource>().Stop();
            MainCamera.GetComponent<AudioSource>().Stop();
            losingUI.SetActive(true);
            Time.timeScale = 0;
        }
    }
    private void Winning()
    {
        if (beansManager.CheckBeans() == true)
        {
            PlayerPrefs.SetInt("WinningScore",score);
            PlayerPrefs.Save();
            player.transform.GetComponent<AudioSource>().Stop();
            MainCamera.GetComponent<AudioSource>().Stop();
            winingUI.SetActive(true);
            Time.timeScale=0;
        }
    }


}
