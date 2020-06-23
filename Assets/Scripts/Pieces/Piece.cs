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

    public Color hoverOutlineColor = Color.yellow;
    public Color selectedOutlineColor = Color.green;

    private MeshRenderer meshRenderer;

    private Outline outline;
   
    
    public enum PlayerTypes {
        BLACK,
        WHITE
    };

    public enum SelectedStates {
        HOVERING, SELECTED, DESELECTED, ENEMY, TAKEABLE
    }

    public PieceTypes pieceType = PieceTypes.NONE;
    public PlayerTypes playerType = PlayerTypes.BLACK;
    public SelectedStates selectedState = SelectedStates.DESELECTED;

    public AnimationCurve movementCurve;

    private Animator anim;
    private Material material;
    private Material highlightMaterial;

    public void Setup(Material highlightMaterial) {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        material = meshRenderer.material;
        anim = GetComponentInChildren<Animator>();
        this.highlightMaterial = Instantiate(highlightMaterial);
    }

    public void SetSelected(bool value) {
        outline.enabled = value;
    }

    public void SetState(SelectedStates selectedState) {
        this.selectedState = selectedState;

        switch(selectedState) {
            case SelectedStates.SELECTED:
                highlightMaterial.SetColor("_Color", Constants.selectedColor);
                break;
            case SelectedStates.ENEMY:
                highlightMaterial.SetColor("_Color", Constants.hostileColor);
                break;
            default:
                break;
        }

        meshRenderer.material = selectedState == SelectedStates.DESELECTED ? material : highlightMaterial;
    }

    public void Move(Vector3 destination, float time) {
        StartCoroutine(MoveToDestination(destination, time));
    }

    IEnumerator MoveToDestination(Vector3 destination, float time) {
        float elapsedTime = 0;
        Vector3 currentPos = transform.position;
        anim.SetBool("moving", true);
        while (elapsedTime < time) {
            transform.position = Vector3.Lerp(currentPos, destination, movementCurve.Evaluate((elapsedTime / time)));
            elapsedTime += Time.deltaTime;
            if (elapsedTime / time >= 0.75f)
                anim.SetBool("moving", false);

            // Yield here
            yield return null;
        }
        // Make sure we got there
        transform.position = destination;
        //anim.SetBool("moving", false);
        yield return null;
    }

    protected int CalculateIndex() {
        return transform.parent.GetSiblingIndex();
    }

    public virtual int[] GetAvailableTiles(Tile[] state) {
        int[] moves = new int[64];
        return moves;
    }
}
