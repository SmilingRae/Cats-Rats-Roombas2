using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject youWinUI;
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void gameOver()
    {
        youWinUI.SetActive(false);
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void restart()
    {
        Debug.Log("Restart clicked!");
        Time.timeScale = 1f;
        gameOverUI.SetActive(false);
        GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>().ClearInventory();
        PlayerMovement.Instance.GetComponent<PlayerHealth>().ResetPlayer();
        SceneManager.LoadScene("Level 1");
    }

    public void mainMenu()
    {
        Time.timeScale = 1f;
        gameOverUI.SetActive(false);
        youWinUI.SetActive(false);
        GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>().ClearInventory();
        SceneManager.LoadScene("Main Menu");
    }

    public void ShowYouWin()
    {
        Time.timeScale = 0f;
        gameOverUI.SetActive(false);
        youWinUI.SetActive(true);
    }
}
