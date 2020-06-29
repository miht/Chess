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

    private int moveCounter;
   
    
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
    private MaterialPropertyBlock block;

    public int MoveNumber {
        get { return moveCounter; }
        set { moveCounter = value; }
    }

    public void Setup() {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        anim = GetComponentInChildren<Animator>();

        block = new MaterialPropertyBlock();
    }

    public void SetSelected(bool value) {
        outline.enabled = value;
    }

    public void SetState(SelectedStates selectedState) {
        this.selectedState = selectedState;

        switch(selectedState) {
            case SelectedStates.SELECTED:
                block.SetColor("_Color", Constants.selectedColor);
                break;
            case SelectedStates.ENEMY:
                block.SetColor("_Color", Constants.hostileColor);
                break;
            default:
                block.SetColor("_Color", Constants.defaultColor);
                break;
        }

        block.SetFloat("_IsActive", selectedState == SelectedStates.DESELECTED ? 0f : 1f);
        meshRenderer.SetPropertyBlock(block);
        //meshRenderer.material = selectedState == SelectedStates.DESELECTED ? material : highlightMaterial;
    }

    public void Move(Vector3 destination, float time, Action onComplete = null) {
        StartCoroutine(MoveToDestination(destination, time, onComplete));
    }

    IEnumerator MoveToDestination(Vector3 destination, float time, Action onComplete) {
        float halfTime = time / 2f;
        float elapsedTime = 0;
        Vector3 currentPos = transform.position;
        block.SetFloat("_Dissolve", 0f);
        while (elapsedTime < halfTime) {
            //Dissolve
            block.SetFloat("_Dissolve", elapsedTime / halfTime);
            meshRenderer.SetPropertyBlock(block);
            elapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }
        transform.position = destination;
        elapsedTime = 0f;
        while (elapsedTime < halfTime) {
            //Materialize
            block.SetFloat("_Dissolve", 1 - (elapsedTime / halfTime));
            meshRenderer.SetPropertyBlock(block);
            elapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }
        block.SetFloat("_Dissolve", 0f);
        meshRenderer.SetPropertyBlock(block);

        // Make sure we got there
        transform.position = destination;
        onComplete();
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
