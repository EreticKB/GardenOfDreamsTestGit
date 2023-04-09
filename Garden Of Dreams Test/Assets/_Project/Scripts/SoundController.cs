
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioSource _music;
    [SerializeField] AudioSource _audio;
    [SerializeField] AudioClip[] _sounds;

    internal void ToggleSound(bool state)
    {
        _audio.enabled = state;
        _music.enabled = state;
    }

    public void SpawnAmmoSound()
    {
        _audio.clip = _sounds[0];
        _audio.Play();
    }

    public void SpawnItemSound()
    {
        _audio.clip = _sounds[1];
        _audio.Play();
    }
    public void DeleteItem()
    {
        _audio.clip = _sounds[2];
        _audio.Play();
    }
    public void ShotOneSound()
    {
        _audio.clip = _sounds[3];
        _audio.Play();
    }
    public void ShotTwoSound()
    {
        _audio.clip = _sounds[4];
        _audio.Play();
    }
    public void ShotFailSound()
    {
        _audio.clip = _sounds[5];
        _audio.Play();
    }
    
}
