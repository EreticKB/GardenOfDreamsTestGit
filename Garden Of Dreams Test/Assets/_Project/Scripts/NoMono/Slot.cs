[System.Serializable]
public struct Slot
{
    public int Quantity { get; private set; }
    public string ItemName { get; private set; }
    public int StackLimit { get; private set; }
    public int ID { get; private set; }
    public float ExtraParam1 { get; private set; }
    public float ExtraParam2 { get; private set; }
    public float ExtraParam3 { get; private set; }

    
    public Slot(int quanity, string itemName, int limit, int id, float extra1, float extra2, float extra3)
    {
        if (quanity < 1) Quantity = 1;
        else if (quanity > limit) Quantity = limit;
        else Quantity = quanity;
        ItemName = itemName;
        StackLimit = limit;
        ID = id;
        ExtraParam1 = extra1;
        ExtraParam2 = extra2;
        ExtraParam3 = extra3;
    }
    /// <summary>
    /// Enter any int for init with default params.
    /// </summary>
    /// <param name="id"></param>
    public Slot(int id)
    {
        Quantity = 0;
        ItemName = "Empty";
        StackLimit = 0;
        ID = 0;
        ExtraParam1 = 0f;
        ExtraParam2 = 0f;
        ExtraParam3 = 0f;
    }
    public int IncreaseQuantity(int quantity)
    {
        int excess = 0;
        Quantity += quantity;
        if (Quantity > StackLimit)
        {
            excess = Quantity - StackLimit;
            Quantity = StackLimit;
        }
        return excess;
    }
    public bool DecreaseQuantity(int quantity)
    {
        Quantity -= quantity;
        if (Quantity > 0) return true;
        ItemName = "Empty";
        ID = 0;
        return false;
    }
}
