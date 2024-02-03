using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static InventoryController instance;
    public InventorySlot inventorySlot;
    public InventoryItem inventoryItem;
    [Tooltip("Big inventory")]
    public GameObject inventoryContainer;
    public GameObject inventoryStore;

    [SerializeField] private int slot = 16;
    public Transform canvasUI;
    private Dictionary<string, int> stock = new();
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void Start()
    {
        foreach (Transform child in inventoryStore.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < slot; i++)
        {
            Instantiate(inventorySlot, inventoryStore.transform);
        }

        Reload();
    }
    public bool AddItem(Item item, int quantity = 1)
    {
        for (int i = 0; i < inventoryStore.transform.childCount; i++)
        {
            Transform transform = inventoryStore.transform.GetChild(i);
            if (transform.TryGetComponent<InventorySlot>(out var slot))
            {
                InventoryItem tempInventoryItem = slot.GetItem();
                if (tempInventoryItem == null)
                {
                    InventoryItem tempItem = Instantiate(inventoryItem, slot.transform);
                    tempItem.ItemInit(item);
                    Reload();
                    return true;
                }
                else
                {
                    int remain = tempInventoryItem.AddItem(item.GetName(), quantity);
                    if (remain == 0)
                    {
                        Reload();
                        return true;
                    }
                }
            }
        }
        return false;
    }


    private void Reload()
    {
        stock?.Clear();
        foreach (Transform child in inventoryStore.transform)
        {
            if (child.TryGetComponent<InventorySlot>(out var slot))
            {
                InventoryItem tempInventoryItem = slot.GetItem();
                if (tempInventoryItem != null)
                {
                    int inStock = stock.ContainsKey(tempInventoryItem.GetItemName().ToString()) ? stock[tempInventoryItem.GetItemName().ToString()] : 0;
                    int nextStock = inStock + tempInventoryItem.GetCurrentQuantity();
                    stock[tempInventoryItem.GetItemName().ToString()] = nextStock;
                }
            }
        }
    }
}
