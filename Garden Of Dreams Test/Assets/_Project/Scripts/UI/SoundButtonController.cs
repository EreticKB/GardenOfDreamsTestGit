using UnityEngine;

public class SoundButtonController : MonoBehaviour
{
    //Yes, PlayerPrefs coz it is for simple, fun and temporary addition.
    [SerializeField] GameObject _cross;
    [SerializeField] GameCoreController _game;
    bool _active;
    const string _soundPrefKey = "soundEnabled";
    private void Awake()
    {
        if (PlayerPrefs.GetInt(_soundPrefKey) == 0) _active = false;
        else _active = true;
        _cross.SetActive(!_active);
        _game.ToggleSound(_active);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus) return;
        if (_active) PlayerPrefs.SetInt(_soundPrefKey,1);
        else PlayerPrefs.SetInt(_soundPrefKey, 0);
        PlayerPrefs.Save();
    }

    public void Click()
    {
        _active=!_active;
        _cross.SetActive(!_active);
        _game.ToggleSound(_active);
    }
}
