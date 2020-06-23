using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoardState", menuName = "Board/Board State")]
public class BoardState : ScriptableObject
{
    [SerializeField] private PieceState[] pieces = new PieceState[64];
    [SerializeField] public int turn = 0;

    public PieceState GetPieceState(int index) {
        return pieces[index];
    }
}

[System.Serializable]
public struct PieceState {
    public Piece.PieceTypes pieceType;
    public Piece.PlayerTypes playerType;
}
