using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBoard : MonoBehaviour
{
    private UIPiece[] pieces;


    [SerializeField] Sprite none;

    [SerializeField] Sprite pawn_w;
    [SerializeField] Sprite knight_w;
    [SerializeField] Sprite tower_w;
    [SerializeField] Sprite king_w;
    [SerializeField] Sprite bishop_w;
    [SerializeField] Sprite queen_w;

    [SerializeField] Sprite pawn_b;
    [SerializeField] Sprite knight_b;
    [SerializeField] Sprite tower_b;
    [SerializeField] Sprite king_b;
    [SerializeField] Sprite bishop_b;
    [SerializeField] Sprite queen_b;

    void Awake() {
    }

    public void Setup() {
        pieces = GetComponentsInChildren<UIPiece>();
        foreach(UIPiece piece in pieces) {
            piece.Setup();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTiles(BoardState state) {
        for(int i = 0; i < 64; i++) {
            UIPiece piece = pieces[i];

            PieceState pieceState = state.GetPieceState(i);
            Piece.PlayerTypes playerType = pieceState.playerType;
            Sprite sprite;
            switch(pieceState.pieceType) {
                case Piece.PieceTypes.BISHOP:
                    sprite = playerType == Piece.PlayerTypes.BLACK ? bishop_b : bishop_w;
                    break;
                case Piece.PieceTypes.PAWN:
                    sprite = playerType == Piece.PlayerTypes.BLACK ? pawn_b : pawn_w;
                    break;
                case Piece.PieceTypes.KING:
                    sprite = playerType == Piece.PlayerTypes.BLACK ? king_b : king_w;
                    break;
                case Piece.PieceTypes.QUEEN:
                    sprite = playerType == Piece.PlayerTypes.BLACK ? queen_b : queen_w;
                    break;
                case Piece.PieceTypes.TOWER:
                    sprite = playerType == Piece.PlayerTypes.BLACK ? tower_b : tower_w;
                    break;
                case Piece.PieceTypes.KNIGHT:
                    sprite = playerType == Piece.PlayerTypes.BLACK ? knight_b : knight_w;
                    break;
                default:
                    sprite = none;
                    break;
            }

            piece.SetSprite(sprite);
        }

    }
}
