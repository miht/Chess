using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnIndicator : MonoBehaviour
{
    private Image image;

    [SerializeField] Sprite blackSprite;
    [SerializeField] Sprite whiteSprite;
    
    public void Setup() {
        image = GetComponent<Image>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTurn(Piece.PlayerTypes playerType) {
        image.sprite = playerType == Piece.PlayerTypes.BLACK ? blackSprite : whiteSprite;
    }

}
