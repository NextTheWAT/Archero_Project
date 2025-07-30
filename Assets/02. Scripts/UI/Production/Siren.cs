using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Siren : MonoBehaviour
{
    [SerializeField] private Light lightCompoenet;
    private bool isRed = true;
    public bool IsRed // 나중에 꺼줄때 접근
    {
        get { return isRed; }
        set { isRed = value; }
    }

    private void Awake()
    {
        lightCompoenet = GetComponent<Light>();
        StartCoroutine(DelayLight());
    }

    IEnumerator DelayLight()
    {
        while (isRed)
        {
            // 빨간색으로 변경
            lightCompoenet.color = Color.red;
            yield return new WaitForSeconds(0.5f);

            // 파란색으로 변경
            lightCompoenet.color = Color.blue;
            yield return new WaitForSeconds(0.5f);
        }
    }

}
