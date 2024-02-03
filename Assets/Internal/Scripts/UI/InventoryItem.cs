
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Image img;
    public TextMeshProUGUI txt;
    private int currentQuantity = 1;
    private int maxQuantity = 1;
    private bool useStack = false;
    private ItemName itemName;
    public Transform rootParent = null;
    public void OnBeginDrag(PointerEventData eventData)
    {
        rootParent = transform.parent;
        transform.SetParent(InventoryController.instance.canvasUI, false);
        transform.SetAsLastSibling();
        img.raycastTarget = false;
        txt.text = string.Empty;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(rootParent);
        img.raycastTarget = true;
        txt.text = currentQuantity.ToString();
    }
    public void ItemInit(Item newItem)
    {
        if (img.TryGetComponent<RectTransform>(out var recTranform))
        {
            recTranform.sizeDelta = new(newItem.imgSize.x, newItem.imgSize.y);
        }

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
            ReloadQuantity();
            return nextQuantity - maxQuantity;
        }
        currentQuantity = nextQuantity;
        ReloadQuantity();
        return 0;
    }
    public int AddItem(ItemName itemName, int amount = 1)
    {
        if (itemName != GetItemName())
        {
            return amount;
        }
        if (!useStack)
        {
            return amount;
        }
        int nextQuantity = currentQuantity + amount;
        if (nextQuantity > maxQuantity)
        {
            currentQuantity = maxQuantity;
            ReloadQuantity();
            return nextQuantity - maxQuantity;
        }
        currentQuantity = nextQuantity;
        ReloadQuantity();
        return 0;
    }
    private void ReloadQuantity()
    {
        txt.text = currentQuantity.ToString();
    }

    public int GetCurrentQuantity()
    {
        return currentQuantity;
    }
    public ItemName GetItemName()
    {
        return itemName;
    }
    public void SetCurrentQuantity(int v)
    {
        ReloadQuantity();
        SetCurrentQuantity(v);
    }
}
