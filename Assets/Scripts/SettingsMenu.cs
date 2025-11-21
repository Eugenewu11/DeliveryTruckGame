using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public void SetVolume(float volume)
    {
        Debug.Log("Volume set to: " + volume);
        AudioListener.volume = volume;
    }
}
