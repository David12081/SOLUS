using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
    public Image image;
    public SpriteRenderer spriteRenderer;

    private void Update()
    {
        image.sprite = spriteRenderer.sprite;
    }
}