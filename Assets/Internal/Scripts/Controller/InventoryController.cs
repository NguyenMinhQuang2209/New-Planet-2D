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
    }
}
