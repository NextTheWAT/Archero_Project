using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundUI : MonoBehaviour
{
    public GameObject Sound_UI;

    private bool isOnSetting = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isOnSetting = !isOnSetting;
            Sound_UI.SetActive(isOnSetting);
        }

    }
}
