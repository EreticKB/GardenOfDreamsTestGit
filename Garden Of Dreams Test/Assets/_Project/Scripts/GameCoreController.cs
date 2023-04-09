using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCoreController : MonoBehaviour
{
    [SerializeField] InventoryController _inventory;
    [SerializeField] InventoryUIController _invUI;
    [SerializeField] UIController _UI;
    [SerializeField] bool _addPack;
    private void Start()
    {
       
    }
    public void UpdateInventory(int transmitter, int receiver)
    {
        _inventory.SlotInteraction(transmitter, receiver);
    }

    public void UpdateInvUI(Slot slotData, int slotId)
    {
        _invUI.UpdateUI(slotData, slotId);
    }
    public bool AddItem()
    {
        if (!_addPack)
        {
            int type = Random.Range(1, 4);
            if (type == 1) return _inventory.AddWeapon(Random.Range(1, 3));
            else if (type == 2) return _inventory.AddBodyArmor(Random.Range(1, 3));
            else if (type == 3) return _inventory.AddHeadArmor(Random.Range(1, 3));
            else return false;
        }
        else
        {
            if (!_inventory.AddWeapon(Random.Range(1, 3))) return false;
            if (!_inventory.AddBodyArmor(Random.Range(1, 3))) return false;
            if (!_inventory.AddHeadArmor(Random.Range(1, 3))) return false;
            return true;
        }
    }

    internal bool AddAmmo()
    {
        if (!_inventory.AddAmmo(1)) return false;
        if (!_inventory.AddAmmo(2)) return false;
        return true;
    }

    public bool Shot()
    {
        return _inventory.SpendAmmo(Random.Range(1, 3));
    }

    public bool ClearItemSlot()
    {
        return _inventory.ClearRandomItemSlot();
    }

    internal void OpenSlot()
    {
        _inventory.OpenSlot();
    }
    internal void OpenUISlot()
    {
        _invUI.OpenUISlot();
    }
}
