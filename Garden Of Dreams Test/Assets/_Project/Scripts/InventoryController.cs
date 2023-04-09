using System.IO;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameCoreController _game;
    Slot[] _slots = new Slot[30];
    public int ActiveSlotNumber { get; private set; } = 0;
    const string _filename = "/inventory.dat";

    private void Awake()
    {
        Debug.Log(Application.persistentDataPath);
        if (SaveHandler.Load(Application.persistentDataPath + _filename, out InventoryDataCollector save))
        {
            _slots = save.GetSlotData();
            ActiveSlotNumber = save.GetSlotNumber();
            for (int i = 0; i < ActiveSlotNumber; i++)
            {
                _game.UpdateInvUI(_slots[i], i);
            }
        }
        else
        {
            for (int i = 0; i < 14; i++)
            {
                OpenSlot();
            }
            _slots[0] = new Slot(1000, "AR Ammo", 300, 1);
            _game.UpdateInvUI(_slots[0], 0);
        }
    }
    public void SlotInteraction(int transmitter, int receiver)
    {
        if (_slots[transmitter].ItemName.Equals(_slots[receiver].ItemName)) slotTransfer(transmitter, receiver);
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
        _slots[ActiveSlotNumber-1] = new Slot(0, "Empty", 0, 0);
        _game.UpdateInvUI(_slots[ActiveSlotNumber - 1], ActiveSlotNumber - 1);
    }

    /*private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            InventoryDataCollector save = new InventoryDataCollector(_slots, ActiveSlotNumber);
            SaveHandler.Save(save, Application.persistentDataPath + _filename);
        }
    }*/
    public void SaveToFile()//костыль для тестов, потом стереть.
    {
        Debug.Log("SaveToFile");
        InventoryDataCollector save = new InventoryDataCollector(_slots, ActiveSlotNumber);
        Debug.Log(save.GetSlotNumber());
        SaveHandler.Save(save, Application.persistentDataPath + _filename);
    }
}
