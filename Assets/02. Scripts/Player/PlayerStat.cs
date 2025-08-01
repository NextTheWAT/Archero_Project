using Unity.VisualScripting.Antlr3.Runtime.Misc;
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

    public float attackRange = 20f; //�÷��̾� ��Ÿ� �⺻20

    private bool isDead = false;
    private Renderer[] renderers;
    private Color[] originalColors; // ���� ���� ����

    private void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        // ���� ���� ����
        originalColors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i].material.HasProperty("_Color"))
                originalColors[i] = renderers[i].material.color;
        }

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
        SoundManager.Instance.Player_TakeDamage();
        if (currentHp < 0) currentHp = 0;
        Debug.Log($"���ظ� ����! ���� ü��: {currentHp}");

        StartCoroutine(FlashRed());

        if (currentHp <= 0)
        {
            Die();
        }
    }

    private System.Collections.IEnumerator FlashRed()
    {
        if (renderers == null || renderers.Length == 0)
            yield break;

        // ���������� ����
        foreach (var r in renderers)
        {
            if (r.material.HasProperty("_Color"))
                r.material.color = Color.red;
        }

        yield return new WaitForSeconds(0.2f);

        // ���� ���� ����
        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i].material.HasProperty("_Color"))
                renderers[i].material.color = originalColors[i];
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("�÷��̾� ���!");

        // �̵�/�Է�/�ִϸ��̼� ��Ȱ��ȭ
        var movement = GetComponent<PlayerMovement>();
        if (movement != null)
            movement.enabled = false;

        var animator = GetComponent<Animator>();
        if (animator != null)
            animator.SetTrigger("Die");

        // GameOver UI ǥ��
        UIManager.Instance.UpdateUI(UIManager.GameState.GameOver);
    }

    
    public void DebugDamage999()
    {
        Damage(999);
        Debug.Log("�����: currentHp�� 999 ������ ����");
    }
    public void DebugDamage1()
    {
        Damage(1);
        Debug.Log("�����: currentHp�� 1 ������ ����");
    }
    public bool IsDead => isDead;

    //�÷��̾� ���� �ʱ�ȭ �޼���
    public void ResetStat()
    {
        isDead = false;
        currentHp = maxHp;

        // �̵�/�Է�/�ִϸ��̼� Ȱ��ȭ
        var movement = GetComponent<PlayerMovement>();
        if (movement != null)
            movement.enabled = true;

        var animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.Rebind(); // �ִϸ����� ���� �ʱ�ȭ
            animator.Update(0f);
        }

        // ��Ƽ���� ���� ������� ����
        if (renderers != null && originalColors != null)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                if (renderers[i].material.HasProperty("_Color"))
                    renderers[i].material.color = originalColors[i];
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        //�÷��̾��� ���� ������ �ð������� ǥ��
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
