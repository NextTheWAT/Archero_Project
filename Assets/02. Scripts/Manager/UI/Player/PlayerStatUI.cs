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
        // 자동으로 태그가 "Player"인 오브젝트의 PlayerStat 컴포넌트 찾기
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

        attackPowerText.text = $"공격력: {player.attackPower}";
        attackSpeedText.text = $"공격속도: {player.attackSpeed}";
        moveSpeedText.text = $"이동속도: {player.moveSpeed}";
        projectileCountText.text = $"투사체 수: {player.projectileCount}";
        hpText.text = $"체력: {player.currentHp}";
    }
}
