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

    private bool isDead = false;

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
        if (isDead) return;

        currentHp -= Mathf.RoundToInt(amount);
        if (currentHp < 0) currentHp = 0;
        Debug.Log($"���ظ� ����! ���� ü��: {currentHp}");

        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("�÷��̾� ���!");

        // ����: �̵�/�Է�/�ִϸ��̼� ��Ȱ��ȭ
        var movement = GetComponent<PlayerMovement>();
        if (movement != null)
            movement.enabled = false;

        var animator = GetComponent<Animator>();
        if (animator != null)
            animator.SetTrigger("Die");

        // �ʿ�� UIManager ��� ���ӿ��� ó�� ȣ��
        // UIManager.Instance.ShowGameOver();
    }

    // �����: currentHp���� 999��ŭ �������� �ִ� �޼���
    public void DebugDamage999()
    {
        Damage(999);
        Debug.Log("�����: currentHp�� 999 ������ ����");
    }

    public bool IsDead => isDead;
}
