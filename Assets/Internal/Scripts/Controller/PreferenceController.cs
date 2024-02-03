using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreferenceController : MonoBehaviour
{
    public static PreferenceController instance;
    public GameObject inventory;

    [Header("Item Name Config")]
    [SerializeField] private List<ItemNameStore> itemNameStores = new();
    private void Start()
    {
        inventory.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CursorController.instance.ChangeCursor("Inventory", new() { inventory });
        }
    }
    public GameObject GetItemByName(ItemName name)
    {
        for (int i = 0; i < itemNameStores.Count; i++)
        {
            ItemNameStore tempItem = itemNameStores[i];
            if (tempItem != null && tempItem.itemName == name)
            {
                return tempItem.item;
            }
        }
        return null;
    }
}
[System.Serializable]
public class ItemNameStore
{
    public GameObject item;
    public ItemName itemName;
}