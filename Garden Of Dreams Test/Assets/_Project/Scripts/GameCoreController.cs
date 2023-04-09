using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCoreController : MonoBehaviour
{
    [SerializeField] InventoryController _inventory;
    [SerializeField] InventoryUIController _invUI;
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
}
