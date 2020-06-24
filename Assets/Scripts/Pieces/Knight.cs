using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    public override int[] GetAvailableTiles(Tile[] state) {
        int[] moves = new int[64];
        int index = CalculateIndex();

        Func<int, bool> isFree = (int i) => state[i].Piece == null;
        Func<int, bool> isHostile = (int i) => !isFree(i) && state[i].Piece.playerType != playerType;

        if (index % 8 > 1 && index + 6 < 64) // OK
            moves[index + 6] = isFree(index + 6) ? 1 : (isHostile(index + 6) ? 2 : 0);
        if (index % 8 > 0 && index + 15 < 64) // OK
            moves[index + 15] = isFree(index + 15) ? 1 : (isHostile(index + 15) ? 2 : 0);
        if (index % 8 <= 7 && index + 17 < 64) // OK
            moves[index + 17] = isFree(index + 17) ? 1 : (isHostile(index + 17) ? 2 : 0);
        if (index % 8 < 6 && index + 10 < 64) // OK
            moves[index + 10] = isFree(index + 10) ? 1 : (isHostile(index + 10) ? 2 : 0);
        if (index % 8 < 6 && index - 6 >= 0) // OK
            moves[index - 6] = isFree(index - 6) ? 1 : (isHostile(index - 6) ? 2 : 0);
        if (index % 8 <= 6 && index - 15 >= 0) // OK
            moves[index - 15] = isFree(index - 15) ? 1 : (isHostile(index - 15) ? 2 : 0);
        if (index % 8 > 0 && index - 17 >= 0) //OK
            moves[index - 17] = isFree(index - 17) ? 1 : (isHostile(index - 17) ? 2 : 0);
        if (index % 8 > 1 && index - 10 >= 0) // OK
            moves[index - 10] = isFree(index - 10) ? 1 : (isHostile(index - 10) ? 2 : 0);
        return moves;
    }
}
