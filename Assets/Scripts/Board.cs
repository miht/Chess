﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{
    public MeshRenderer bounds;
    public GameObject piecePrefab;

    public PieceHolder blackPieceHolder;
    public PieceHolder whitePieceHolder;

    public Piece[] pieces = new Piece[64];
    public Tile[] tiles = new Tile[64];
    public BoardState boardState;

    public Material blackPieceMaterial;
    public Material whitePieceMaterial;
    public Material pieceHighlightMaterial;

    public Mesh knightMesh;
    public Mesh towerMesh;
    public Mesh bishopMesh;
    public Mesh pawnMesh;
    public Mesh queenMesh;
    public Mesh KingMesh;

    public AnimationCurve pieceMovementCurve;

    private Tile selectedTile = null;

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        tiles = GetComponentsInChildren<Tile>();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                PieceState pieceState =  boardState.GetPieceState(i * 8 + j);
                if (pieceState.pieceType == Piece.PieceTypes.NONE) continue;

                Piece piece = GetPiece(pieceState.playerType, pieceState.pieceType);
                tiles[i * 8 + j].piece = piece;
                piece.transform.SetParent(tiles[i * 8 + j].transform, false);
                pieces[i * 8 + j] = piece;
            }
        }
    }

    void TakeTile(Tile player, Tile opponent) {
        Piece playerPiece = player.Piece;
        Piece opponentPiece = opponent.Piece;
        if (opponentPiece) {
            if(opponentPiece.playerType == Piece.PlayerTypes.BLACK) {
                blackPieceHolder.AddPiece(opponentPiece);
            }
            else {
                whitePieceHolder.AddPiece(opponentPiece);
            }
        }
        player.Piece = null;
        opponent.Piece = playerPiece;

        selectedTile = null;
        foreach (Tile t2 in tiles)
            t2.SetHighlighted(Tile.TileModes.DEFAULT);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 99f, LayerMask.GetMask("Tile"))) {
                Tile t = hit.transform.GetComponent<Tile>();
                if(selectedTile) {
                    switch(t.TileMode) {
                        case Tile.TileModes.HOSTILE:
                            //Take
                            TakeTile(selectedTile, t);
                            break;
                        case Tile.TileModes.TAKEABLE:
                            TakeTile(selectedTile, t);
                            //Regular move
                            break;
                        case Tile.TileModes.SELECTED:
                            break;
                        default:
                            SelectTile(t);
                            break;
                    }
                }
                else {
                    if (!t.Piece) {
                        foreach (Tile t2 in tiles)
                            t2.SetHighlighted(Tile.TileModes.DEFAULT);
                        selectedTile = null;
                        return;
                    }

                    SelectTile(t);

                }
            } else {
                selectedTile = null;
                foreach (Tile t2 in tiles)
                    t2.SetHighlighted(Tile.TileModes.DEFAULT);
            }
        }
    }

    void SelectTile(Tile tile) {
        if (tile.Piece) {
            selectedTile = tile;
            int[] tileStates = selectedTile.Piece.GetAvailableTiles(tiles);
            for (int i = 0; i < tileStates.Length; i++) {
                switch (tileStates[i]) {
                    case 1:
                        tiles[i].SetHighlighted(Tile.TileModes.TAKEABLE);
                        break;
                    case 2:
                        Debug.Log(i);
                        tiles[i].SetHighlighted(Tile.TileModes.HOSTILE);
                        break;
                    default:
                        tiles[i].SetHighlighted(Tile.TileModes.DEFAULT);
                        break;
                }
            }
            tile.SetHighlighted(Tile.TileModes.SELECTED);
        }
        else {
            selectedTile = null;
            foreach (Tile t2 in tiles)
                t2.SetHighlighted(Tile.TileModes.DEFAULT);
        }
        
    }

    Piece GetPiece(Piece.PlayerTypes playerType, Piece.PieceTypes pieceType) {
        GameObject go = Instantiate(piecePrefab).gameObject;
        go.GetComponentInChildren<MeshRenderer>().material = playerType == Piece.PlayerTypes.BLACK ? blackPieceMaterial : whitePieceMaterial;

        Mesh mesh = null;
        Piece piece = null;
        switch (pieceType) {
            case Piece.PieceTypes.TOWER:
                mesh = towerMesh;
                piece = go.AddComponent<Tower>();
                break;
            case Piece.PieceTypes.KNIGHT:
                mesh = knightMesh;
                piece = go.AddComponent<Knight>();
                break;
            case Piece.PieceTypes.BISHOP:
                mesh = bishopMesh;
                piece = go.AddComponent<Bishop>();
                break;
            case Piece.PieceTypes.QUEEN:
                mesh = queenMesh;
                piece = go.AddComponent<Queen>();
                break;
            case Piece.PieceTypes.KING:
                mesh = KingMesh;
                piece = go.AddComponent<King>();
                break;
            case Piece.PieceTypes.PAWN:
                mesh = pawnMesh;
                piece = go.AddComponent<Pawn>();
                break;
            default:
                break;
        }
        piece.Setup(pieceHighlightMaterial);

        piece.movementCurve = pieceMovementCurve;

        go.transform.Rotate(Vector3.up, playerType == Piece.PlayerTypes.BLACK ? 0f: 180f);

        go.GetComponentInChildren<MeshFilter>().mesh = mesh;
        //BoxCollider bColl = go.GetComponent<BoxCollider>();
        //bColl.size = mesh.bounds.size;
        //bColl.center = mesh.bounds.center;

        piece.playerType = playerType;
        piece.pieceType = pieceType;
        return piece;
    }
}
