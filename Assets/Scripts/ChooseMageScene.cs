using UnityEngine.SceneManagement;
using UnityEngine;

public class ChooseMageScene : MonoBehaviour
{
    public int player1_character, player2_character;
    public void StartGame()
    {
        StaticGameController.player1 = player1_character;
        StaticGameController.player2 = player2_character;
        SceneManager.LoadScene(3);
    }

    public void setPlayer1(int character)
    {
        player1_character = character;
    }
    public void setPlayer2(int character)
    {
        player2_character = character;
    }
}
