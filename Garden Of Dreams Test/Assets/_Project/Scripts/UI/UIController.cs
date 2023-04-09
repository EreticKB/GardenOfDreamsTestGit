using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject _errorPanel;
    [SerializeField] GameCoreController _game;
    

    public void Error(string message)
    {
        _errorPanel.SetActive(true);
        _errorPanel.GetComponent<ErrorMessageController>().SetMessage(message);
    }   

    public void AddAmmoButtonHandler()
    {
        if (!_game.AddAmmo()) Error("No empty space.");
    }

    public void AddItemButtonHandler()
    {
        if (!_game.AddItem()) Error("No empty space.");
    }
    public void ShotButtonHandler()
    {
        _game.Shot();
    }
    public void ClearButtonHandler()
    {
        if (!_game.ClearItemSlot()) Error("All slots is empty.");
    }
}
