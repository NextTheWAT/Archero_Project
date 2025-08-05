using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndingEffect : MonoBehaviour
{
    public Image imageToFade;     // EndingImage ����
    public float fadeTime = 5f;

    void Start()
    {
        // ó���� �̹��� �����ϰ�
        Color c = imageToFade.color;
        c.a = 0f;
        imageToFade.color = c;

        // �ڵ����� ������ �����ֱ� ����
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float t = 0f;
        Color c = imageToFade.color;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, t / fadeTime);
            imageToFade.color = c;
            yield return null;
        }

        c.a = 1f;
        imageToFade.color = c;
    }
}
