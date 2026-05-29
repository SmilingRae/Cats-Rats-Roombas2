using UnityEngine;
using UnityEngine.SceneManagement;

public class Item : MonoBehaviour
{
    [SerializeField]
    private string itemName;

    [SerializeField]
    private int quantity;

    [SerializeField]
    private Sprite sprite;
    
    AudioManager audioManager;
    private InventoryManager inventoryManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            inventoryManager.AddItem(itemName, quantity, sprite);
            audioManager.PlaySFX(audioManager.pickup);

            if(SceneManager.GetActiveScene().name == "Level 2")
            {
                MenuManager menuManager = GameManager.Instance.GetComponent<MenuManager>();
                menuManager.ShowYouWin();
            }
            Destroy(gameObject);
        }
    }


    
  
}
