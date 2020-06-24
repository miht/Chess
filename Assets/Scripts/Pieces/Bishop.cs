using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    public override int[] GetAvailableTiles(Tile[] state) {
        int[] moves = TileUtilities.GetBishopTiles(CalculateIndex(), playerType, state);

        return moves;
    }
}
