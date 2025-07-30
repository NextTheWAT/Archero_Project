using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCleared : MonoBehaviour
{
    bool isClear = false; // 게임매니저에서 가지고 올것
    [Header("각 스테이지에 있는 IsClearParicle")]
    [SerializeField] private ParticleSystem particle;

    private void Start()
    {
        isClear = true;
        ChangeColor(isClear);
    }

    void ChangeColor(bool isClear)
    {
        var color = particle.main;
        if(isClear)
        {
            color.startColor = Color.blue;
        }
    }
}
