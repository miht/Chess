using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Piece
{
    public override int[] GetAvailableTiles(Tile[] state) {
        int[] moves = TileUtilities.GetTowerTiles(CalculateIndex(), playerType, state);

        return moves;
    }
}
