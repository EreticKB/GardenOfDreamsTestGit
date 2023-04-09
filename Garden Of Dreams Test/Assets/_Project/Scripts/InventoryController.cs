using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] string[] _itemnames;
    [SerializeField] GameCoreController _game;
    Slot[] _slots = new Slot[30];

    public int ActiveSlotNumber { get; private set; } = 0;
    const string _filename = "/inventory.dat";

    private void Awake()
    {
        if (SaveHandler.Load(Application.persistentDataPath + _filename, out InventoryDataCollector save))
        {
            _slots = save.GetSlotData();
            ActiveSlotNumber = save.GetSlotNumber();
            Debug.Log("Number "+ActiveSlotNumber);
            for (int i = 0; i < ActiveSlotNumber; i++)
            {
                _game.OpenUISlot();
                _game.UpdateInvUI(_slots[i], i);
            }
        }
        else for (int i = 0; i < 15; i++) OpenSlot();
    }
    
    public void SlotInteraction(int transmitter, int receiver)
    {
        if (_slots[transmitter].ID == _slots[receiver].ID) slotTransfer(transmitter, receiver);
        else slotSwap(transmitter, receiver);
    }

    private void slotSwap(int transmitter, int receiver)
    {
        Slot slot = _slots[receiver];
        _slots[receiver] = _slots[transmitter];
        _slots[transmitter] = slot;
        UIUpdate(transmitter, receiver);
    }
    private void slotTransfer(int transmitter, int receiver)
    {
        int excess = _slots[receiver].IncreaseQuantity(_slots[transmitter].Quantity);
        _slots[transmitter].DecreaseQuantity(_slots[transmitter].Quantity - excess);
        UIUpdate(transmitter, receiver);
    }


    private void UIUpdate(int transmitter, int receiver)
    {
        _game.UpdateInvUI(_slots[transmitter], transmitter);
        _game.UpdateInvUI(_slots[receiver], receiver);
    }

    public void OpenSlot()
    {
        ActiveSlotNumber++;
        _slots[ActiveSlotNumber - 1] = new Slot(0);
        _game.OpenUISlot();
        _game.UpdateInvUI(_slots[ActiveSlotNumber - 1], ActiveSlotNumber - 1);
    }

    //========================================================
    private bool AddItem(int quanity, string itemName, int limit, int id, float extra1, float extra2, float extra3)
    {
        for (int i = 0; i < ActiveSlotNumber; i++)
        {
            if (_slots[i].ID == 0)
            {
                Debug.Log("Active Slot Number" + ActiveSlotNumber);
                _slots[i] = new Slot(quanity, itemName, limit, id, extra1, extra2, extra3);
                _game.UpdateInvUI(_slots[i], i);
                return true;
            }
        }
        return false;
    }

    //Переделать систему располжения предметов, разбить на отдельные массивы по типам и запихнуть их все в массив типов.
    public bool AddAmmo(int ammoType)
    {
        if (ammoType == 1) return AddItem(100, _itemnames[1], 100, 1, 0.01f, 0f, 0f);
        else if (ammoType == 2) return AddItem(300, _itemnames[2], 300, 2, 0.01f, 0f, 0f);
        return false;
    }
    public bool AddWeapon(int weaponType)
    {
        if (weaponType == 1) return AddItem(1, _itemnames[3], 1, 3, 1f, 10f, 0f);
        else if (weaponType == 2) return AddItem(1, _itemnames[4], 1, 4, 5f, 20f, 0f);
        return false;
    }
    public bool AddBodyArmor(int armorType)
    {
        if (armorType == 1) return AddItem(1, _itemnames[5], 1, 5, 10f, 3f, 0f);
        else if (armorType == 2) return AddItem(1, _itemnames[6], 1, 6, 10f, 10f, 0f);
        return false;
    }
    public bool AddHeadArmor(int armorType)
    {
        if (armorType == 1) return AddItem(1, _itemnames[7], 1, 7, 0.01f, 3f, 0f);
        else if (armorType == 2) return AddItem(1, _itemnames[8], 1, 8, 1f, 10f, 0f);
        return false;
    }

    //========================================================
    public bool ClearItemSlot(int number)
    {
        if (_slots[number].ID == 0) return false;
        _slots[number] = new Slot(0);
        _game.UpdateInvUI(_slots[number], number);
        return true;
    }

    public bool ClearRandomItemSlot()
    {
        List<int> slotIDs = new List<int>();
        for (int i = 0; i < ActiveSlotNumber; i++)
        {
            if (_slots[i].ID != 0) slotIDs.Add(i);
        }
        if (slotIDs.Count == 0) return false;
        return ClearItemSlot(slotIDs[Random.Range(0, slotIDs.Count)]);
    }

    //========================================================
    public bool SpendAmmo(int type)
    {
        //в силу совпадения данных излишне, но остается на случай изменения слотов, чтобы помнить про необходимость поддерживать соответствие.
        if (type == 1) type = 1;
        else if (type == 2) type = 2;
        for (int i = ActiveSlotNumber - 1; i >= 0; i--)
        {
            if (_slots[i].ID == type)
            {
                _slots[i].DecreaseQuantity(1);
                _game.UpdateInvUI(_slots[i], i);
                return true;
            }
        }
        return false;
    }

    //========================================================
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            InventoryDataCollector save = new InventoryDataCollector(_slots, ActiveSlotNumber);
            SaveHandler.Save(save, Application.persistentDataPath + _filename);
        }
    }
}
