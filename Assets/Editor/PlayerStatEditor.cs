using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerStat))]
public class PlayerStatEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlayerStat stat = (PlayerStat)target;
        if (GUILayout.Button("currentHp�� 999 ������ (�����)"))
        {
            stat.DebugDamage999();
        }
    }
}