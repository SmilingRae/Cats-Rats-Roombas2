using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActivated;
    public ItemSlot[] itemSlot;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

 

    public void AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if(itemSlot[i].isFull == false)
            {
                itemSlot[i].AddItem(itemName, quantity, itemSprite);
                return;
            }
        }
    }

    public bool HasItem(string itemName)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull && itemSlot[i].itemName == itemName)
            {
                return true;
            }
        }
        return false;
    }

    public void ClearInventory()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].ClearSlot();
        }
    }
}

