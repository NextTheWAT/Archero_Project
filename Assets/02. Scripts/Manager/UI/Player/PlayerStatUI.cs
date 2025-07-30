using TMPro;
using UnityEngine;

public class PlayerStatUI : MonoBehaviour
{
    public PlayerStat player;

    public TMP_Text attackPowerText;
    public TMP_Text attackSpeedText;
    public TMP_Text moveSpeedText;
    public TMP_Text projectileCountText;
    public TMP_Text hpText;

    private void Awake()
    {
        // �ڵ����� �±װ� "Player"�� ������Ʈ�� PlayerStat ������Ʈ ã��
        if (player == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Player");
            if (obj != null)
                player = obj.GetComponent<PlayerStat>();
        }
    }

    private void Update()
    {
        if (player == null) return;

        attackPowerText.text = $"���ݷ�: {player.attackPower}";
        attackSpeedText.text = $"���ݼӵ�: {player.attackSpeed}";
        moveSpeedText.text = $"�̵��ӵ�: {player.moveSpeed}";
        projectileCountText.text = $"����ü ��: {player.projectileCount}";
        hpText.text = $"ü��: {player.currentHp}";
    }
}
