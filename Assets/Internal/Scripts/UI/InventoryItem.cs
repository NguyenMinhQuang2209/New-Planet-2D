
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Image img;
    public TextMeshProUGUI txt;
    private int currentQuantity = 0;
    private int maxQuantity = 1;
    private bool useStack = false;
    private ItemName itemName;
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(InventoryController.instance.canvasUI, false);
        transform.SetAsLastSibling();
        img.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(transform);
        img.raycastTarget = true;
    }
    public void ItemInit(Item newItem)
    {
        img.sprite = newItem.itemImage;
        txt.text = newItem.GetQuantityShowTxt();
        currentQuantity = newItem.GetCurrentQuantity();
        maxQuantity = newItem.GetMaxQuantity();
        useStack = newItem.UseStack();
        itemName = newItem.GetName();
    }

    public int AddItem(int amount = 1)
    {
        if (!useStack)
        {
            return amount;
        }
        int nextQuantity = currentQuantity + amount;
        if (nextQuantity > maxQuantity)
        {
            currentQuantity = maxQuantity;
            return nextQuantity - maxQuantity;
        }
        currentQuantity = nextQuantity;
        return 0;
    }
}
