using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndingEffect : MonoBehaviour
{
    public Image imageToFade;     // EndingImage 연결
    public float fadeTime = 5f;

    void Start()
    {
        // 처음에 이미지 투명하게
        Color c = imageToFade.color;
        c.a = 0f;
        imageToFade.color = c;

        // 자동으로 서서히 보여주기 시작
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
