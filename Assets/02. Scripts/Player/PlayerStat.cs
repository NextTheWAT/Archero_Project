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


    private void Start()
    {
        if (SkillUIManager.Instance != null)
        {
            //SkillUIManager.Instance.ShowSkillUI();
        }
        else
        {
            Debug.LogWarning("SkillUIManager.Instance is null. ShowSkillUI() ȣ�� ����");
        }
    }
    public void Heal(float amount)
    {
        currentHp += Mathf.RoundToInt(amount);
        if (currentHp > maxHp) currentHp = maxHp;
        Debug.Log($"ü�� ȸ����! ���� ü��: {currentHp}");
    }

    public void Damage(float amount)
    {
        currentHp -= Mathf.RoundToInt(amount);
        if (currentHp < 0) currentHp = 0;
        Debug.Log($"���ظ� ����! ���� ü��: {currentHp}");
    }

}
