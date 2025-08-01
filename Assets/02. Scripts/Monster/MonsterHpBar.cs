using UnityEngine;
using UnityEngine.UI;
using static MonsterFSM;

public class MonsterHpBar : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] MonsterStat monsterStat;

    private void Awake()
    {
        if (monsterStat == null)
            monsterStat = GetComponent<MonsterStat>();
    }

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
