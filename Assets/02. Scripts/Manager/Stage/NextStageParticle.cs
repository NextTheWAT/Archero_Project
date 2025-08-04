using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageParticle : MonoBehaviour
{

    [SerializeField] private ParticleSystem particle;

    private void Update()
    {
        ClearColorChange();
    }

    private void ClearColorChange()
    {
        if (GameManager.Instance.isCleared == true)
        {
            var main = particle.main;

            main.startColor = Color.blue;
        }
    }
}
