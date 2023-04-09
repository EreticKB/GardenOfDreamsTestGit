using UnityEngine;

public class GameCoreController : MonoBehaviour
{
    [SerializeField] InventoryController _inventory;
    [SerializeField] InventoryUIController _invUI;
    [SerializeField] UIController _UI;
    [SerializeField] SoundController _sound;
    [SerializeField] bool _addPack;
   
    public void ToggleSound(bool state)
    {
        _sound.ToggleSound(state);
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
        _sound.SpawnItemSound();
        //No clear moment, coz button name means singular, but description - plural.
        //With enabled checkbox in inspector will spawn only one random non-ammo item, without - one random non-ammo item for each type.
        if (!_addPack)
        {
            if (!_inventory.AddItem(Random.Range(1, 4), Random.Range(1, 3), -1)) return false;
        }
        else
        {
            if (!_inventory.AddItem(Random.Range(1, 4), Random.Range(1, 3), -1)) return false;
            if (!_inventory.AddItem(Random.Range(1, 4), Random.Range(1, 3), -1)) return false;
            if (!_inventory.AddItem(Random.Range(1, 4), Random.Range(1, 3), -1)) return false;
        }
        return true;
    }

    internal bool AddAmmo()
    {
        _sound.SpawnAmmoSound();
        if (!_inventory.AddItem(0, 1, -1)) return false;
        if (!_inventory.AddItem(0, 2, -1)) return false;
        return true;
    }

    public bool Shot()
    {
        int ammo = Random.Range(1, 3);
        bool result = _inventory.SpendAmmo(ammo);
        if (!result) _sound.ShotFailSound();
        else if (ammo == 1) _sound.ShotOneSound();
        else if (ammo == 2) _sound.ShotTwoSound();
        return result;
    }
    public bool ClearItemSlot()
    {
        _sound.DeleteItem();
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
