using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public string requiredItem;
    public string nextSceneName;

    private InventoryManager inventory;

    void Start()
    {
        inventory = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (inventory.HasItem(requiredItem))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.Log("You need " + requiredItem + " to continue!");
        }
    }
}