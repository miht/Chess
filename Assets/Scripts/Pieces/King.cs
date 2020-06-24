using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    public override int[] GetAvailableTiles(Tile[] state) {
        int[] moves = TileUtilities.GetKingTiles(CalculateIndex(), playerType, state);

        return moves;
    }
}
