using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;


// �÷��̾��� �ɷ�ġ�� ����(ü��, ���ݷ� ��) ����
// ������ ó��, ȸ��, ���, ���� �ʱ�ȭ

public class PlayerStat : MonoBehaviour
{
    // �÷��̾��� ���ݷ�
    public float attackPower = 10f;
    // �̵� �ӵ�
    public float moveSpeed = 5f;
    // ȸ�� �ӵ�
    public float rotationSpeed = 10f;
    // ���� �ӵ�(���� �� ������)
    public float attackSpeed = 1f;
    // �ִ� ü��
    public int maxHp = 100;
    // ���� ü��
    public int currentHp = 100;
    // �߻�ü ����(��Ƽ�� ��)
    public int projectileCount = 1;

    // �÷��̾��� ���� ��Ÿ�(�⺻�� 20)
    public float attackRange = 20f;

    // ��� ���� �÷���
    private bool isDead = false;
    // �÷��̾� �� �ڽ� ������Ʈ�� Renderer ������Ʈ �迭
    private Renderer[] renderers;
    // �� Renderer�� ���� ���� ����� �迭
    private Color[] originalColors;

    private void Start()
    {
        // �ڽ� ���� ��� Renderer ������Ʈ ��������
        renderers = GetComponentsInChildren<Renderer>();
        // �� Renderer�� ���� ���� ����
        originalColors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i].material.HasProperty("_Color"))
                originalColors[i] = renderers[i].material.color;
        }

        // ��ų UI �Ŵ����� �����ϸ� UI ǥ��(���� �ּ� ó��)
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
        if (isDead) return; // �̹� ��� �� ����

        currentHp -= Mathf.RoundToInt(amount);
        SoundManager.Instance.Player_TakeDamage(); // ������ ���� ���
        if (currentHp < 0) currentHp = 0;
        Debug.Log($"���ظ� ����! ���� ü��: {currentHp}");

        StartCoroutine(FlashRed()); // �ǰ� �� �Ӱ� ����

        if (currentHp <= 0)
        {
            Die(); // ü���� 0 �����̸� ��� ó��
        }
    }

    
    // �ǰ� �� ��� ���������� �����ϴ� �ڷ�ƾ
    private System.Collections.IEnumerator FlashRed()
    {
        if (renderers == null || renderers.Length == 0)
            yield break;

        // ��� Renderer�� ���������� ����
        foreach (var r in renderers)
        {
            if (r.material.HasProperty("_Color"))
                r.material.color = Color.red;
        }

        yield return new WaitForSeconds(0.2f);

        // ���� �������� ����
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

        // �̵� �� �Է� ��Ȱ��ȭ
        var movement = GetComponent<PlayerMovement>();
        if (movement != null)
            movement.enabled = false;

        // ��� �ִϸ��̼� Ʈ����
        var animator = GetComponent<Animator>();
        if (animator != null)
            animator.SetTrigger("Die");

        // ���� ���� UI ǥ��
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

  
    // �÷��̾� ���� �� �ð� ȿ���� �ʱ�ȭ
    public void ResetStat()
    {
        isDead = false;                     
        currentHp = maxHp;

        // �̵�/�Է� Ȱ��ȭ
        var movement = GetComponent<PlayerMovement>();
        if (movement != null)
            movement.enabled = true;

        // �ִϸ����� ���� �ʱ�ȭ
        var animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.Rebind();
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


    // �����Ϳ��� �÷��̾��� ���� ������ �ð������� ǥ��
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
