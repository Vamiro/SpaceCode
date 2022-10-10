using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsData : StorageData<SettingsData>
{
    public float generalVolume = 1f;
    public float musicVolume = 1f;
    public float SFXlVolume = 1f;

    public void UpdateAudioVolume()
    {
        var audioMixer = StartUp.Instance.AudioMixer;
        audioMixer.SetFloat("Master", LogarithmicValue(generalVolume));
        audioMixer.SetFloat("Music", LogarithmicValue(musicVolume));
        audioMixer.SetFloat("SFX", LogarithmicValue(SFXlVolume));
    }
    private static float CorrectValue(float rawValue) => Mathf.Max(rawValue, 0.0001f);
    private static float LogarithmicValue(float rawValue) => Mathf.Log(CorrectValue(rawValue)) * 20f;

}
