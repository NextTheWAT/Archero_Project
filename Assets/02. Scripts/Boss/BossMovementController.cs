using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ������ ���� Ŭ���� 
public class BossMovementController : MonoBehaviour
{
    /*
     
    ���� ��� : ������ �÷��̾� ���󰡱� 

    �ʿ��� �͵� 
    - �÷��̾� ���󰡱�
        - ������ �÷��̾� ��ǥ�� ��� üũ�Ѵ�. 
        - �÷��̾� ������ ���� �� �ִ� �Ÿ�(distance)�� ������ ������ �÷��̾� ��ġ���� ��¦ ������ �Ÿ����� �̵��Ѵ� (���� �ٴ� ����)
        - 
    - �÷��̾� ��ǥ (O)
    - ���� �̵��ӵ� (O)
    - ���� �̵� �ִϸ��̼� ���� (�߿�X)

    ���� ���
    - �÷��̾� ��ǥ�� �޾ƿ´�
    - ������ �÷��̾ �ν��ؼ� ���� �Ÿ��� ���صд�.
    - ������ �÷��̾��� �Ÿ��� ����Ѵ�. 
    - ������ �÷��̾ �ٶ󺸴� ������ ����Ѵ�. 
    - ����� �Ÿ��� ������ �Ÿ��� ���ؼ� �Ÿ� ���� ������ ������ �̵���Ų��. 
     
     */
    public Transform player; // �÷��̾� ��ġ 

    public float moveSpeed; // ���� �̵��ӵ� 
    public float distance; // ���� �÷��̾ ���� �� �ִ� �Ÿ� (���� ���ϴ� ��)
    public Vector3 directionToTarget; // ������ �÷��̾ �ٶ󺸴� ����

    private void Update()
    {
        distance = Vector3.Distance(transform.position, player.position);
        directionToTarget = MathF.Abs()
    }
}
