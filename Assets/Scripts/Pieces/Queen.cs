using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    public override int[] GetAvailableTiles(Tile[] state) {
        int[] moves = new int[64];
        int index = CalculateIndex();

        return moves;
    }
}
