using TMPro;
using UnityEngine;

public class UIItemCounterController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _counter;
    
    public void ChangeItemQuantity(int quantity)
    {
        if (quantity <= 1) _counter.text = "";
        else _counter.text = quantity.ToString();
    }
}
