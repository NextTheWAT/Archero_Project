using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCleared : MonoBehaviour
{
    bool isClear = false; // ���ӸŴ������� ������ �ð�
    [Header("�� ���������� �ִ� IsClearParicle")]
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
