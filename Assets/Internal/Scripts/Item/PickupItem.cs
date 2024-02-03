using UnityEngine;

public class PickupItem : Item
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemInteract();
    }
    public override void ItemInteract()
    {
        bool canAdd = InventoryController.instance.AddItem(this, 1);
        if (canAdd)
        {
            Destroy(gameObject);
        }
        else
        {
            LogController.instance.Log(LanguageOptions.Error_Inventory_Full);
        }
    }
}
