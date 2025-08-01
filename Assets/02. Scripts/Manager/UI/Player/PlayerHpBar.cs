using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    private PlayerStat playerStat;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            playerStat = player.GetComponent<PlayerStat>();
        }

        // 슬라이더 초기 설정
        hpSlider.minValue = 0;
        hpSlider.maxValue = playerStat.maxHp;
        hpSlider.value = playerStat.currentHp;
    }

    private void Update()
    {
        if (playerStat != null && hpSlider != null)
        {
            hpSlider.value = playerStat.currentHp;
        }
    }
}
