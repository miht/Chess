using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileUtilities {
    public static int[] GetPawnTiles(int index, Piece.PlayerTypes playerType, Tile[] state, int moveNumber) {
        int[] moves = new int[64];

        Func<int, bool> isFree = (int i) =>
            state[i].Piece == null;
        Func<int, bool> isOccupied = (int i) => !isFree(i) && state[i].Piece.playerType != playerType;

        if ((index + 8 > 64 && playerType == Piece.PlayerTypes.BLACK) || (index - 8 < 0 && playerType == Piece.PlayerTypes.WHITE)) return moves;

        //Pawn test
        if (playerType == Piece.PlayerTypes.BLACK) {
            if (index % 8 > 0)
                moves[index + (8 - 1)] = isOccupied(index + 7) ? 2 : 0;
            if (index + 8 < 64) {
                int frontPiece = isFree(index + 8) ? 1 : 0;
                moves[index + 8] = frontPiece;
                if (frontPiece == 1 && index + 16 < 64 && moveNumber == 0)
                    moves[index + 16] = isFree(index + 16) ? 1 : 0;
            }
            if (index % 8 < 7)
                moves[index + (8 + 1)] = isOccupied(index + 9) ? 2 : 0;
        } else {
            if (index % 8 < 7)
                moves[index - 7] = isOccupied(index - 7) ? 2 : 0;
            if (index - 8 >= 0) {
                int frontPiece = isFree(index - 8) ? 1 : 0;
                moves[index - 8] = frontPiece;
                if (frontPiece == 1 && index - 16 >= 0 && moveNumber == 0)
                    moves[index - 16] = isFree(index - 16) ? 1 : 0;
            }
            if (index % 8 > 0)
                moves[index - 9] = isOccupied(index - 9) ? 2 : 0;

        }

        return moves;
    }

    public static int[] GetKnightTiles(int index, Piece.PlayerTypes playerType, Tile[] state) {
        int[] moves = new int[64];

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

    public static int[] GetBishopTiles(int index, Piece.PlayerTypes playerType, Tile[] state) {
        int[] moves = new int[64];
        int line = (int)Mathf.Floor(index / 8);
        int column = index % 8;

        Func<int, bool> isFree = (int i) => state[i].Piece == null;
        Func<int, bool> isHostile = (int i) => !isFree(i) && state[i].Piece.playerType != playerType;

        int leftCount = Mathf.Min(7 - line, column);
        for (int i = 0; i < leftCount; i++) {
            int dIndex = index + (i + 1) * 7;
            if (!isFree(dIndex) && !isHostile(dIndex)) {
                break;
            }
            if (isHostile(dIndex)) {
                moves[dIndex] = 2;
                break;
            } else {
                moves[dIndex] = 1;
            }
        }

        int rightCount = Mathf.Min(7 - line, 7 - column);
        for (int i = 0; i < rightCount; i++) {
            int dIndex = index + (i + 1) * 9;
            if (!isFree(dIndex) && !isHostile(dIndex)) {
                break;
            }
            if (isHostile(dIndex)) {
                moves[dIndex] = 2;
                break;
            } else {
                moves[dIndex] = 1;
            }
        }

        int bottomRightCount = Mathf.Min(line, 7 - column);
        for (int i = 0; i < bottomRightCount; i++) {
            int dIndex = index - (i + 1) * 7;
            if (!isFree(dIndex) && !isHostile(dIndex)) {
                break;
            }
            if (isHostile(dIndex)) {
                moves[dIndex] = 2;
                break;
            } else {
                moves[dIndex] = 1;
            }
        }

        int bottomLeftCount = Mathf.Min(line, column);
        for (int i = 0; i < bottomLeftCount; i++) {
            int dIndex = index - (i + 1) * 9;
            if (!isFree(dIndex) && !isHostile(dIndex)) {
                break;
            }
            if (isHostile(dIndex)) {
                moves[dIndex] = 2;
                break;
            } else {
                moves[dIndex] = 1;
            }
        }

        return moves;
    }

    public static int[] GetKingTiles(int index, Piece.PlayerTypes playerType, Tile[] state) {
        int[] moves = new int[64];

        Func<int, int> getPieceState = (int i) =>
            state[i].Piece == null ? 1 : (state[i].Piece.playerType != playerType ? 2 : 0);

        //Pawn test
        if (index + 8 < 64)
            moves[index + 8] = getPieceState(index + 8);
        if (index - 8 >= 0)
            moves[index - 8] = getPieceState(index - 8);
        if (index + 1 < 64 && index % 8 < 7)
            moves[index + 1] = getPieceState(index + 1);
        if (index - 1 >= 0 && index % 8 > 0)
            moves[index - 1] = getPieceState(index - 1);
        if (index + 7 < 64 && index % 8 > 0)
            moves[index + 7] = getPieceState(index + 7);
        if (index + 9 < 64 && index % 8 < 7)
            moves[index + 9] = getPieceState(index + 9);
        if (index - 7 >= 0 && index % 8 < 7)
            moves[index - 7] = getPieceState(index - 7);
        if (index - 9 >= 0 && index % 8 > 0)
            moves[index - 9] = getPieceState(index - 9);

        return moves;
    }

    public static int[] GetTowerTiles(int index, Piece.PlayerTypes playerType, Tile[] state) {
        int[] moves = new int[64];
        int line = (int)Mathf.Floor(index / 8);
        int column = index % 8;

        Func<int, bool> isFree = (int i) => state[i].Piece == null;
        Func<int, bool> isHostile = (int i) => !isFree(i) && state[i].Piece.playerType != playerType;

        for (int i = index - 1; i >= 8 * line; i--) { //Horizontal left, OK
            if (!isFree(i) && !isHostile(i)) {
                break;
            }
            if (isHostile(i)) {
                moves[i] = 2;
                break;
            } else {
                moves[i] = 1;
            }
        }

        for (int i = index + 1; i < 8 * (line + 1); i++) { //Horizontal right, OK
            if (!isFree(i) && !isHostile(i)) {
                break;
            }
            if (isHostile(i)) {
                moves[i] = 2;
                break;
            } else {
                moves[i] = 1;
            }
        }

        for (int i = index + 8; i < 64; i += 8) { //Vertical forward, OK
            if (!isFree(i) && !isHostile(i)) {
                break;
            }
            if (isHostile(i)) {
                moves[i] = 2;
                break;
            } else {
                moves[i] = 1;
            }
        }

        for (int i = index - 8; i >= 0; i -= 8) { //Vertical backwards, OK
            if (!isFree(i) && !isHostile(i)) {
                break;
            }
            if (isHostile(i)) {
                moves[i] = 2;
                break;
            } else {
                moves[i] = 1;
            }
        }

        return moves;
    }

    public static int[] GetQueenTiles(int index, Piece.PlayerTypes playerType, Tile[] state) {
        int[] moves = new int[64];

        int[] knightTiles = GetTowerTiles(index, playerType, state);
        int[] bishopTiles = GetBishopTiles(index, playerType, state);

        for(int i = 0; i < 64; i++) {
            moves[i] = Mathf.Max(knightTiles[i], bishopTiles[i]);
        }


        return moves;
    }
}
