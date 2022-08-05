using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TMP_FontAsset Normal;
    [SerializeField] private TMP_FontAsset HighLight;
    [SerializeField] private TMP_Text Text;

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip hover, click;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Text.font = HighLight;
        //backGround.color = highlightColor;
        source.PlayOneShot(hover);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Text.font = Normal;
        //backGround.color = normalColor;
    }

    public void playClickSound()
    {
        source.PlayOneShot(click);
    }
}

