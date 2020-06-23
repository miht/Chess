using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public override int[] GetAvailableTiles(Tile[] state) {
        int[] moves = new int[64];
        int index = CalculateIndex();

        Func<int, bool> isFree = (int i) =>
            state[i].Piece == null;
        Func<int, bool> isOccupied = (int i) => !isFree(i) && state[i].Piece.playerType != playerType;

        if ((index + 8 > 64 && playerType == PlayerTypes.BLACK) || (index - 8 < 0 && playerType == PlayerTypes.WHITE)) return moves;

        //Pawn test
        if (playerType == PlayerTypes.BLACK) {
            moves[index + (8 - 1)] = isOccupied(index + 7) ? 2 : 0;
            moves[index + 8] = isFree(index + 8) ? 1 : 0;
            moves[index + (8 + 1)] = isOccupied(index + 9) ? 2 : 0;
        } else {
            moves[index - 7] = isOccupied(index - 7) ? 2 : 0;
            moves[index - 8] = isFree(index - 8) ? 1 : 0;
            moves[index - 9] = isOccupied(index - 9) ? 2 : 0;
        }

        return moves;
    }
}
