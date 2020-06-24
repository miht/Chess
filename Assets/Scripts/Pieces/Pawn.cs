using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public override int[] GetAvailableTiles(Tile[] state) {
        int index = CalculateIndex();

        int[] moves = TileUtilities.GetPawnTiles(CalculateIndex(), playerType, state, MoveNumber);
        return moves;
    }
}
