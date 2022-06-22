using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPopup : PoolableMono
{
    private TextMeshProUGUI text;

    Sequence seq;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void OnDisable()
    {
        seq?.Kill();
    }

    public void Popup(string text, Color color)
    {
        this.text.SetText(text);
        this.text.color = color;

        seq = DOTween.Sequence();
        seq.Append(transform.DOMove(transform.position + Vector3.up, .5f));
        seq.AppendInterval(.3f);
        seq.Join(this.text.DOFade(0, .2f));
        seq.AppendCallback(() =>
        {
            transform.SetParent(GameManager.Instance.transform);
            PoolManager.Instance.Push(this);
        });
    }

    public override void Reset()
    {
        
    }
}
