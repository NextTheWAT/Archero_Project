using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public float attackPower = 10f;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float attackSpeed = 1f;
    public int maxHp = 100;
    public int currentHp = 100;
    public int projectileCount = 1;

    public void Heal(float amount)
    {
        currentHp += Mathf.RoundToInt(amount);
        if (currentHp > maxHp) currentHp = maxHp;
        Debug.Log($"체력 회복됨! 현재 체력: {currentHp}");
    }

    private void Start()
    {
        SkillUIManager.Instance.ShowSkillUI();
    }
}
