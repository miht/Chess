
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
    
    public enum PlayerTypes {
        BLACK,
        WHITE
    };

    public PieceTypes pieceType = PieceTypes.NONE;
    public PlayerTypes playerType = PlayerTypes.BLACK;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
