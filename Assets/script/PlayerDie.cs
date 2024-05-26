using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDie : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            SceneManager.LoadScene("GameOverScreen");
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}

// public GameObject gameOverPanel;
// gameOverPanel.SetActive(true);
