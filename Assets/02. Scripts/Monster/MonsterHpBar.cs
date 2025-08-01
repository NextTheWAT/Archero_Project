using UnityEngine;
using UnityEngine.UI;
using static MonsterFSM;

public class MonsterHpBar : MonoBehaviour
{
    [SerializeField] Slider hpSlider;
    [SerializeField] MonsterStat monsterStat;

    private void Start()
    {
        if (monsterStat != null && hpSlider != null)
        {
            hpSlider.minValue = 0;
            hpSlider.maxValue = monsterStat.maxHp;
            hpSlider.value = monsterStat.currentHp;
        }
    }

    private void Update()
    {
        if (monsterStat != null && hpSlider != null)
        {
            hpSlider.value = monsterStat.currentHp;
        }
    }
}
