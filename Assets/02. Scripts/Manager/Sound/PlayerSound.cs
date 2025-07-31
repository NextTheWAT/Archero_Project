using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private void StepSound()
    {
        SoundManager.Instance.PlayerStep_SFX();
    }
}
