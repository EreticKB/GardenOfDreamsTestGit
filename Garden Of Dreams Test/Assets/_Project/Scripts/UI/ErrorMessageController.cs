using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ErrorMessageController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] TextMeshProUGUI _text;

    public void SetMessage(string message)
    {
        _text.text = message;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.SetActive(false);
    }
}
