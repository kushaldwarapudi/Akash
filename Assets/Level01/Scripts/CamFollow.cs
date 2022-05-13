using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamFollow : MonoBehaviour
{
    public Vector3 offset;
    public GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        offset = this.transform.position - ball.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = ball.transform.position + offset;
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(1);
        ball.GetComponent<Ball>().GameOverMenu.SetActive(false);
        ball.GetComponent<Ball>().ISGameOver = false;
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
