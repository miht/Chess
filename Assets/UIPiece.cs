using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPiece : MonoBehaviour
{
    private Image image;

    private void Awake() {
    }

    public void Setup() {
        image = GetComponent<Image>();
        image.sprite = null;
    }

    public void SetSprite(Sprite sprite) {
        image.sprite = sprite;
    }
}
