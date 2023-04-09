[System.Serializable]
public struct Slot
{
    public int Quantity { get; private set; }
    public string ItemName { get; private set; }
    public int StackLimit { get; private set; }
    public int ID { get; private set; }
    public Slot(int quanity, string itemName, int limit, int id)
    {
        if (quanity < 1) Quantity = 1;
        else if (quanity > limit) Quantity = limit;
        else Quantity = quanity;
        ItemName = itemName;
        StackLimit = limit;
        ID = id;
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
