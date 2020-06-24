using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    public override int[] GetAvailableTiles(Tile[] state) {
        int[] moves = TileUtilities.GetQueenTiles(CalculateIndex(), playerType, state);

        return moves;
    }
}
