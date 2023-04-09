using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuyingSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int[] _price = new int[16];//pointer как сиблинг индекс минус 15.
    [SerializeField] GameObject _activeSlot;
    [SerializeField] InventoryUIController _invUI;
    [SerializeField] GraphicRaycaster _raycaster;
    [SerializeField] EventSystem _eventSystem;
    [SerializeField] TextMeshProUGUI _text;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        _raycaster.Raycast(eventData, results);
        if (results[0].gameObject.name.Equals(gameObject.name))
        {
            _invUI.OpenSlot();
        }
    }

    internal void OpenUISlot()
    {
        Instantiate(_activeSlot, transform.parent);
        transform.SetSiblingIndex(transform.GetSiblingIndex() + 1);
        if (transform.GetSiblingIndex() > 29) Destroy(gameObject);
        if (transform.GetSiblingIndex() > 14) _text.text = _price[transform.GetSiblingIndex() - 15].ToString();
    }
}
