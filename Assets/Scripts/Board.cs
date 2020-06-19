﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{
    public MeshRenderer bounds;
    public Piece piecePrefab;
    public Piece[] pieces = new Piece[64];
    public BoardState boardState;

    public Material blackPieceMaterial;
    public Material whitePieceMaterial;

    public Mesh knightMesh;
    public Mesh towerMesh;
    public Mesh bishopMesh;
    public Mesh pawnMesh;
    public Mesh queenMesh;
    public Mesh KingMesh;

    private Piece selectedPiece = null;

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                PieceState pieceState =  boardState.GetPieceState(i * 8 + j);
                if (pieceState.pieceType == Piece.PieceTypes.NONE) continue;

                // Vector3 position = new Vector3(i * dimensions.x / 8, j * dimensions.z / 8);
                float x = bounds.bounds.center.x - bounds.bounds.extents.x + bounds.bounds.extents.x * 2f * (i / 8f) + bounds.bounds.extents.x / 8f;
                float y = bounds.bounds.max.y; 
                float z = bounds.bounds.center.z - bounds.bounds.extents.z + bounds.bounds.extents.z * 2f * (j / 8f) + bounds.bounds.extents.z / 8f;

                Vector3 position = new Vector3(x, y, z);
                Piece piece = Instantiate(piecePrefab, position, Quaternion.identity, transform);
                piece.Setup(() => {

                }, () => {
                    if (selectedPiece != null) selectedPiece.SetState(Piece.SelectedStates.DESELECTED);
                    selectedPiece = piece;
                    piece.SetState(Piece.SelectedStates.SELECTED);
                });
                SetupPiece(piece, pieceState.playerType, pieceState.pieceType);
                pieces[i * 8 + j] = piece;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedPiece == null) return;

        if(Input.GetMouseButton(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out hit, LayerMask.GetMask("Piece"))) {
                selectedPiece.SetState(Piece.SelectedStates.DESELECTED);
                selectedPiece = null;
                // Do something with the object that was hit by the raycast.
            }
        }
    }

    void SetupPiece(Piece piece, Piece.PlayerTypes playerType, Piece.PieceTypes pieceType) {

        piece.GetComponent<MeshRenderer>().material = playerType == Piece.PlayerTypes.BLACK ? blackPieceMaterial : whitePieceMaterial;

        Mesh mesh = null;
        switch (pieceType) {
            case Piece.PieceTypes.TOWER:
                mesh = towerMesh;
                break;
            case Piece.PieceTypes.KNIGHT:
                mesh = knightMesh;
                break;
            case Piece.PieceTypes.BISHOP:
                mesh = bishopMesh;
                break;
            case Piece.PieceTypes.QUEEN:
                mesh = queenMesh;
                break;
            case Piece.PieceTypes.KING:
                mesh = KingMesh;
                break;
            case Piece.PieceTypes.PAWN:
                mesh = pawnMesh;
                break;
            default:
                break;
        }

        piece.transform.Rotate(Vector3.up, playerType == Piece.PlayerTypes.BLACK ? 90f : -90f);

        piece.GetComponent<MeshFilter>().mesh = mesh;
        BoxCollider bColl = piece.GetComponent<BoxCollider>();
        bColl.size = mesh.bounds.size;
        bColl.center = mesh.bounds.center;

        piece.playerType = playerType;
        piece.pieceType = pieceType;
    }
}