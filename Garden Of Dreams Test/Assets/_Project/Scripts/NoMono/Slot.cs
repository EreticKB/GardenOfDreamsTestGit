[System.Serializable]
public struct Slot
    //I prefer NoMono struct over Mono-Gameobject because resource preservation.
    //If project will changed enough for this solution become insufficient, it means that changes so massive, that it much simpler to rebuild whole project from scratch.
{
    public int TypeID { get; private set; }
    public int ID { get; private set; }
    public int Quantity { get; private set; }
    public string ItemName { get; private set; }
    public int StackLimit { get; private set; }
    
    public float ExtraParam1 { get; private set; }
    public float ExtraParam2 { get; private set; }
    public float ExtraParam3 { get; private set; }

    
    public Slot(int typeId, int id, string itemName, int quanity, int limit, float extra1, float extra2, float extra3)
    {
        TypeID = typeId;
        ID = id;
        ItemName = itemName;
        if (quanity < 1) Quantity = 1;
        else if (quanity > limit) Quantity = limit;
        else Quantity = quanity;
        StackLimit = limit;
        
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
        TypeID = 0;
        ID = 0;
        ItemName = "Empty";
        Quantity = 0;
        StackLimit = 0;
        
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
