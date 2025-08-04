using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundUI : MonoBehaviour
{
    public GameObject Sound_UI;

    private bool isOnSetting = false;

    private void Start()
    {
        Sound_UI.SetActive(isOnSetting);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenSoundButton();
        }

    }

    public void OpenSoundButton()
    {
        isOnSetting = !isOnSetting;
        Sound_UI.SetActive(isOnSetting);
    }
    public void CloseSoundUI()
    {
        isOnSetting = false;
        Sound_UI.SetActive(isOnSetting);
    }
}
