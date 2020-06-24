using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceHolder : MonoBehaviour
{
    private int index = 0;
    private Transform[] slots;

    void Start() {
        slots = GetComponentsInChildren<Transform>();
    }

    public void AddPiece(Piece piece) {
        if (index >= slots.Length) return;

        piece.transform.SetParent(slots[index]);
        piece.Move(slots[index].position, 1f);
        //piece.SetState(Piece.SelectedStates.DESELECTED);
        index++;
    }
}
