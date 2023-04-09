[System.Serializable]
public struct InventoryDataCollector
{
    private Slot[] _slots;
    private int _activeSlotNumber;

    public InventoryDataCollector(Slot[] slots, int activeSlotNumber)
    {
        _slots = slots;
        _activeSlotNumber = activeSlotNumber;
    }
    public Slot[] GetSlotData()
    {
        return _slots;
    }
    public void SetSlotData(Slot[] slots)
    {
        _slots = slots;
    }
    public int GetSlotNumber()
    {
        return _activeSlotNumber;
    }
    public void SetSlotNumber(int activeSlotNumber)
    {
        _activeSlotNumber = activeSlotNumber;
    }
}
