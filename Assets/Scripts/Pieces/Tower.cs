using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Piece
{
    public override int[] GetAvailableTiles(Tile[] state) {
        int[] moves = new int[64];
        int index = CalculateIndex();
        int line = (int) Mathf.Floor(index / 8);
        int column = index % 8;

        Debug.Log(column);

        Func<int, bool> isFree = (int i) => state[i].Piece == null;
        Func<int, bool> isHostile = (int i) => !isFree(i) && state[i].Piece.playerType != playerType;

        for(int i = column + 1; i < 8; i++) { //Horizontal right
            if(isFree(i)) {
                moves[i] = 1;
            }
            if(isHostile(i)) {
                moves[i] = 2;
                break;
            }
        }

        for(int i = column - 1; i >= 0; i--) { //Horizontal left
            if (!isFree(i)) {
                break;
            }
            if (isHostile(i)) {
                moves[i] = 1;
            } else {
                moves[i] = 2;
            }
        }

        for (int i = index - 1; i > 0; i--) {
            if (!isFree(i)) {
                break;
            }
            if (isHostile(i)) {
                moves[i] = 1;
            } else {
                moves[i] = 2;
            }
        }

        //index = i * 8 + j
        //i = (index - j) / 8

        return moves;
    }
}
