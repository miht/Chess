
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum TileModes {
        DEFAULT,
        SELECTED,
        TAKEABLE,
        HOVERING,
        HOSTILE,
    }
    private string name;

    public int index = 0;
    public Piece piece = null;

    private Outline outline;
    private TileModes tileMode = TileModes.DEFAULT;
    public TileModes TileMode {
        get { return tileMode; }
        set { tileMode = value; }
    }
    private TileModes prevTileMode = TileModes.DEFAULT;

    private MeshRenderer glowMeshRenderer;
    private ParticleSystem particleSystem;
    private ParticleSystem.MainModule mainParticleModule;
    private Renderer particleSystemRenderer;

    private MaterialPropertyBlock block;
    private MaterialPropertyBlock particleBlock;

    public string Name {
        get {
            return name;
        }
        set {
            name = value;
        }
    }

    void OnMouseOver() {
        //if (tileMode != TileModes.SELECTED || tileMode != TileModes.TAKEABLE || tileMode != TileModes.HOSTILE) {
        //    SetHighlighted(TileModes.HOVERING);
        //}
    }

    private void OnMouseExit() {
        //if(tileMode == TileModes.HOSTILE || prevTileMode == TileModes.HOSTILE) {
        //    return;
        //}
        //if(tileMode == TileModes.TAKEABLE || prevTileMode == TileModes.TAKEABLE) {
        //    return;
        //}
        //if (tileMode == TileModes.SELECTED || prevTileMode == TileModes.SELECTED) {
        //    return;
        //}

        //SetHighlighted(TileModes.DEFAULT);
    }

    protected int CalculateIndex() {
        return transform.parent.GetSiblingIndex();
    }

    public Piece Piece {
        get { return piece; }
        set {
            piece = value;
            if (piece == null) return;
            piece.transform.parent = transform;
            piece.Move(transform.position, .5f);
        }
    }

    public void SetHighlighted(TileModes tileMode) {
        Piece.SelectedStates pieceState = Piece.SelectedStates.DESELECTED;
        switch(tileMode) {
            case TileModes.HOSTILE:
                mainParticleModule.startColor = Constants.hostileColor;
                //particleBlock.SetColor("_Color", Constants.hostileColor);
                block.SetColor("_Color", Constants.hostileColor);
                pieceState = Piece.SelectedStates.ENEMY;
                break;
            case TileModes.DEFAULT:
                pieceState = Piece.SelectedStates.DESELECTED;
                mainParticleModule.startColor = Constants.defaultColor;
                block.SetColor("_Color", Constants.defaultColor);
                break;
            case TileModes.SELECTED:
                //mainParticleModule.startColor = Color.blue;
                mainParticleModule.startColor = Constants.selectedColor;
                block.SetColor("_Color", Constants.selectedColor);
                pieceState = Piece.SelectedStates.SELECTED;
                break;
            case TileModes.TAKEABLE:
                mainParticleModule.startColor = Constants.takeableColor;
                //particleBlock.SetColor("_Color", Constants.takeableColor);
                block.SetColor("_Color", Constants.takeableColor);
                //mainParticleModule.startColor = Color.green;
                pieceState = Piece.SelectedStates.TAKEABLE;
                break;
            default:
                break;
        }

        glowMeshRenderer.SetPropertyBlock(block);
        //particleSystemRenderer.SetPropertyBlock(particleBlock);
        //glowMeshRenderer.enabled = tileMode != TileModes.DEFAULT;


        if (tileMode != TileModes.DEFAULT) {
            particleSystem.Play();
        } else {
            particleSystem.Stop();
        }
        prevTileMode = this.tileMode;
        this.tileMode = tileMode;

        if (piece != null) piece.SetState(pieceState);
    }

    void Awake() {
        glowMeshRenderer = GetComponent<MeshRenderer>();
        block = new MaterialPropertyBlock();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        //particleSystemRenderer = particleSystem.GetComponent<Renderer>();
        //particleBlock = new MaterialPropertyBlock();
        mainParticleModule = particleSystem.main;
    }

    void Start() {
        index = transform.GetSiblingIndex();
    }
}
