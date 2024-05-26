using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    void start()
    {

    }

    void Update()
    {

    }

    public void LoadGame()
    {
        SceneManager.LoadScene("lobby");
    }
}
