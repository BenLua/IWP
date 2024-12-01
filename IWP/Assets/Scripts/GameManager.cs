using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("GAMEMANAGER INSTANCE IS REPEATED");
            return;
        }
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            PlayerStats.Lives--;
        }

        if (PlayerStats.Lives <= 0)
        {
            GameManager.instance.GameOver();
            Destroy(gameObject);
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void GameWin()
    {
        SceneManager.LoadScene("GameWin");
    }
}
