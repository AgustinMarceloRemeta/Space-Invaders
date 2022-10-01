using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.GetInt("Score", 0);
        PlayerPrefs.SetInt("Score", 0);
    }
    public void NextLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    public void ExitGame() => Application.Quit();
}
