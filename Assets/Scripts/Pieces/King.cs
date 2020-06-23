using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    public override int[] GetAvailableTiles(Tile[] state) {
        int[] moves = new int[64];
        int index = CalculateIndex();

        Func<int, int> getPieceState = (int i) => 
            state[i].Piece == null ? 1 : (state[i].Piece.playerType != playerType ? 2 : 0);

        //Pawn test
        if (index + 8 < 64)
            moves[index + 8] = getPieceState(index + 8);
        if (index - 8 >= 0)
            moves[index - 8] = getPieceState(index - 8);
        if (index + 1 < 64 && index % 8 < 7)
            moves[index + 1] = getPieceState(index + 1);
        if (index -1 >= 0 && index % 8 > 0)
            moves[index - 1] = getPieceState(index - 1);
        if (index + 7 < 64 && index % 8 > 0)
            moves[index + 7] = getPieceState(index + 7);
        if (index + 9 < 64 && index % 8 < 7)
            moves[index + 9] = getPieceState(index + 9);
        if (index - 7 >= 0 && index % 8 < 7)
            moves[index - 7] = getPieceState(index - 7);
        if (index - 9 >= 0 && index % 8 > 0)
            moves[index - 9] = getPieceState(index - 9);

        moves[index] = 0;

        return moves;
    }
}
