using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    string[][] _itemNames;
    [SerializeField] string[] _ammoNames;
    [SerializeField] string[] _weaponNames;
    [SerializeField] string[] _bodyNames;
    [SerializeField] string[] _headNames;

    [SerializeField] GameCoreController _game;
    Slot[] _slots = new Slot[30];

    public int ActiveSlotNumber { get; private set; } = 0;
    const string _filename = "/inventory.dat";

    private void Start()
    {
        _itemNames = new string[4][];
        _itemNames[0] = _ammoNames;
        _itemNames[1] = _weaponNames;
        _itemNames[2] = _bodyNames;
        _itemNames[3] = _headNames;
        if (SaveHandler.Load(Application.persistentDataPath + _filename, out InventoryDataCollector save))
        {
            _slots = save.GetSlotData();
            ActiveSlotNumber = save.GetSlotNumber();
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
        //No && to prevent excess copmaration in cause of type already mismatch.
        if (_slots[transmitter].TypeID == _slots[receiver].TypeID)
        {
            if (_slots[transmitter].ID == _slots[receiver].ID)
            {
                slotTransfer(transmitter, receiver);
                return;
            }
        }
        slotSwap(transmitter, receiver);
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

    private bool AddItem(int typeId, int id, string itemName, int quanity, int limit, float extra1, float extra2, float extra3)
    {
        //Preferables:
        //Extra1 - mass.
        //Extra2 - damage/damage negation.
        //Extra3 - free, m.b. durability
        if (quanity == -1) quanity = limit;
        for (int i = 0; i < ActiveSlotNumber; i++)
        {
            if (_slots[i].ID == 0)
            {
                _slots[i] = new Slot(typeId, id, itemName, quanity, limit, extra1, extra2, extra3);
                _game.UpdateInvUI(_slots[i], i);
                return true;
            }
        }
        return false;
    }

    //если успею, пропишу вытаскивание значений из настраиваемой через инспектор базы предметов. Чтобы геймдизам не приходилось залезать в код.
    public bool AddItem(int typeId, int id, int quantity = -1)
    {
        if (typeId == 0)
        {
            if (id == 1) return AddItem(typeId, id, _itemNames[typeId][id], quantity, 100, 0.01f, 0f, 0f);
            if (id == 2) return AddItem(typeId, id, _itemNames[typeId][id], quantity, 300, 0.01f, 0f, 0f);
        }
        if (typeId == 1)
        {
            if (id == 1) return AddItem(typeId, id, _itemNames[typeId][id], quantity, 2, 1f, 10f, 0f);
            if (id == 2) return AddItem(typeId, id, _itemNames[typeId][id], quantity, 1, 5f, 20f, 0f);
        }
        if (typeId == 2)
        {
            if (id == 1) return AddItem(typeId, id, _itemNames[typeId][id], quantity, 1, 10f, 3f, 0f);
            if (id == 2) return AddItem(typeId, id, _itemNames[typeId][id], quantity, 1, 10f, 10f, 0f);
        }
        if (typeId == 3)
        {
            if (id == 1) return AddItem(typeId, id, _itemNames[typeId][id], quantity, 1, 0.01f, 3f, 0f);
            if (id == 2) return AddItem(typeId, id, _itemNames[typeId][id], quantity, 1, 1f, 10f, 0f);
        }
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
        for (int i = ActiveSlotNumber - 1; i >= 0; i--)
        {
            if (_slots[i].TypeID == 0)
            {
                if (_slots[i].ID == type)
                {
                    _slots[i].DecreaseQuantity(1);
                    _game.UpdateInvUI(_slots[i], i);
                    return true;
                }
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
