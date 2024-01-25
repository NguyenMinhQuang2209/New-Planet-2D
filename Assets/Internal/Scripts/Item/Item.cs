using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Inventory setup")]
    public Sprite itemImage;
    public Vector2 imgSize = new(70f,70f);
    [SerializeField] private int maxQuantity = 1;
    [SerializeField] private bool useStack = false;
    [SerializeField] private ItemName itemName;
    private int currentQuantity = 1;
    public int GetCurrentQuantity()
    {
        return currentQuantity;
    }
    public int GetMaxQuantity()
    {
        return maxQuantity;
    }
    public bool UseStack()
    {
        return useStack;
    }
    public ItemName GetName()
    {
        return itemName;
    }

    public string GetQuantityShowTxt()
    {
        return useStack ? GetCurrentQuantity().ToString() : "";
    }
}
