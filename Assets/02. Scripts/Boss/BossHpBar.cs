using UnityEngine;
using UnityEngine.UI;
using static MonsterFSM;

public class BossHpBar : MonoBehaviour
{
    [SerializeField] Slider hpSlider;
    [SerializeField] BossController bossStat;

    private void Start()
    {
        if (bossStat != null && hpSlider != null)
        {
            hpSlider.minValue = 0;
            hpSlider.maxValue = bossStat.maxHp;
            hpSlider.value = bossStat.currentHp;
        }
    }

    private void Update()
    {
        if (bossStat != null && hpSlider != null)
        {
            hpSlider.value = bossStat.currentHp;
        }
    }
}
