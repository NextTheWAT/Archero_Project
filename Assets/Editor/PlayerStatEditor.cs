using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerStat))]
public class PlayerStatEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlayerStat stat = (PlayerStat)target;

        if (GUILayout.Button("currentHp에 1 데미지 (디버그)"))
        {
            stat.DebugDamage1();
        }

        if (GUILayout.Button("currentHp에 999 데미지 (디버그)"))
        {
            stat.DebugDamage999();
        }
    }
}