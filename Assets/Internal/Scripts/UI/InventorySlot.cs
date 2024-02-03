using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        GameObject target = eventData.pointerDrag;
        if (target.TryGetComponent<InventoryItem>(out var item))
        {
            InventoryItem tempItem = GetItem();
            if (tempItem != null)
            {
                int remain = tempItem.AddItem(item.GetCurrentQuantity());
                if (remain == 0)
                {
                    Destroy(item.gameObject);
                }
                else
                {
                    item.SetCurrentQuantity(remain);
                }
            }
            else
            {
                item.rootParent = transform;
            }
        }
    }
    public InventoryItem GetItem()
    {
        if (transform.childCount > 0)
        {
            return transform.GetChild(0).GetComponent<InventoryItem>();
        }
        return null;
    }
}
