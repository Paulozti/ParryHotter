using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject GameOverPanel;
    public Text JogadorVencedorText;

    void Update()
    {
        InputCheck();
    }

    public void GameOver(PlayerController.Player playerVencedor)
    {
        Time.timeScale = 0;
        JogadorVencedorText.text = "O " + playerVencedor.ToString() + " É O VENCEDOR!";
        GameOverPanel.SetActive(true);
    }

    private void InputCheck()
    {
        if (Input.GetButtonDown("Submit") && Time.timeScale == 0)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(3);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }

    }
}
