using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public enum PieceTypes {
        NONE,
        PAWN,
        TOWER,
        KNIGHT,
        BISHOP,
        QUEEN,
        KING
    };

    public Color hoverOutlineColor;
    public Color selectedOutlineColor;

    private Outline outline;
    
    public enum PlayerTypes {
        BLACK,
        WHITE
    };

    public enum SelectedStates {
        HOVERING, SELECTED, DESELECTED
    }

    public PieceTypes pieceType = PieceTypes.NONE;
    public PlayerTypes playerType = PlayerTypes.BLACK;
    public SelectedStates selectedState = SelectedStates.DESELECTED;

    private Action onHover, onSelected;

    void Awake() {
        outline = GetComponent<Outline>();    
    }

    public void Setup(Action onHover, Action onSelected) {
        this.onHover = onHover;
        this.onSelected = onSelected;
    }

    public void SetSelected(bool value) {
        outline.enabled = value;
    }

    public void SetState(SelectedStates selectedState) {
        this.selectedState = selectedState;
        switch(selectedState) {
            case SelectedStates.HOVERING:
                outline.enabled = true;
                outline.OutlineColor = hoverOutlineColor;
                break;
            case SelectedStates.SELECTED:
                outline.enabled = true;
                outline.OutlineColor = selectedOutlineColor;
                break;
            case SelectedStates.DESELECTED:
                outline.enabled = false;
                break;
            default:
                break;
        }
    }

    void OnMouseDown() {
        onSelected();
    }

    void OnMouseOver() {
        if(selectedState == SelectedStates.DESELECTED) {
            selectedState = SelectedStates.HOVERING;
            outline.OutlineColor = hoverOutlineColor;
            outline.enabled = true;
        }
    }

    private void OnMouseExit() {
        if (selectedState == SelectedStates.SELECTED) return;
        selectedState = SelectedStates.DESELECTED;
        outline.enabled = false;
    }
}
