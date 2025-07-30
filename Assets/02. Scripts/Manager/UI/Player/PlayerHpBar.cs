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

        if (playerStat == null)
        {
            Debug.LogError("PlayerStat ������Ʈ�� ã�� �� �����ϴ�.");
        }

        if (hpSlider == null)
        {
            Debug.LogError("hpFillImage�� ������� �ʾҽ��ϴ�.");
        }

        // �����̴� �ʱ� ����
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
