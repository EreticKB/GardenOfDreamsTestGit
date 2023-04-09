using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour, IPointerMoveHandler
{
    [SerializeField] GameObject[] _prefabs;
    [SerializeField] BuyingSlot _slotBuyer;
    [SerializeField] string _targetName;
    [SerializeField] GraphicRaycaster _raycaster;
    [SerializeField] EventSystem _eventSystem;
    [SerializeField] ScrollRect _scroll;
    [SerializeField] GameCoreController _game;
    [SerializeField] Transform _gridBox;
    PointerEventData _pointerEventData;
    Transform _transmitter;
    Transform _receiver;
    Transform _draggedObject;
    bool _stopCoroutine;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            List<RaycastResult> results = new List<RaycastResult>();
            _pointerEventData = new PointerEventData(_eventSystem);
            _pointerEventData.position = Input.mousePosition;
            _raycaster.Raycast(_pointerEventData, results);
            if (results[0].gameObject.name.Equals(_targetName))
            {
                try
                {
                    _transmitter = results[0].gameObject.transform.parent;
                    _draggedObject = _transmitter.GetChild(1);
                    StartCoroutine("Timer");
                }
                catch { }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (_draggedObject == null) return;
            StopCoroutine("Timer");
            _scroll.vertical = true;
            _draggedObject.SetParent(_transmitter);
            _draggedObject.localPosition = Vector2.zero;
            _draggedObject = null;
            List<RaycastResult> results = new List<RaycastResult>();
            _pointerEventData = new PointerEventData(_eventSystem);
            _pointerEventData.position = Input.mousePosition;
            _raycaster.Raycast(_pointerEventData, results);
            if (!results[0].gameObject.name.Equals(_targetName)) return;
            _receiver = results[0].gameObject.transform.parent;
            if (_transmitter == _receiver) return;
            _game.UpdateInventory(_transmitter.GetSiblingIndex(), _receiver.GetSiblingIndex());
        }
    }

    public void OnPointerMove(PointerEventData eventData) //возможно понадобится другой перехватчик, скажем Move
    {
        _stopCoroutine = true;
    }

    internal void UpdateUI(Slot slotData, int slotId)
    {
        try { Destroy(_gridBox.GetChild(slotId).GetChild(1).gameObject); } catch { }
        Debug.Log("Item Slot ID" + slotId);
        if (slotData.ID > 0)
        {
            GameObject gameObject = Instantiate(_prefabs[slotData.ID], _gridBox.GetChild(slotId));
            gameObject.GetComponent<UIItemCounterController>().ChangeItemQuantity(slotData.Quantity);
        }
    }

    internal void OpenSlot()
    {
        _game.OpenSlot();
    }
    internal void OpenUISlot()
    {
        _slotBuyer.OpenUISlot();
    }

    IEnumerator Timer()
    {
        _stopCoroutine = false;
        yield return new WaitForSeconds(0.5f);
        if (_stopCoroutine)
        {
            _draggedObject = null;
            yield break;
        }
        _draggedObject.SetParent(transform);
        _scroll.vertical = false;
        while (true)
        {
            yield return new WaitForEndOfFrame();
            _draggedObject.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _draggedObject.position.z);
        }
    }


}
