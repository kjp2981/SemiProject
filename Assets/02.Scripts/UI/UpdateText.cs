using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateText : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void OnEnable()
    {
        StartCoroutine(DestroyText());
    }

    private void OnDisable()
    {
        text.color = new Color(1, 1, 1, 1);
    }

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private IEnumerator DestroyText()
    {
        yield return new WaitForSeconds(1f);

        Sequence seq = DOTween.Sequence();
        seq.Append(text.DOFade(0, 1f));
        seq.AppendCallback(() => text.gameObject.SetActive(false));
    }
}
