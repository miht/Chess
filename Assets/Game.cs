using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] Board _board;
    [SerializeField] UIBoard _uiBoard;
    [SerializeField] PlayerTurnIndicator _turnIndicator;

    private void Awake() {
        _board.enabled = true;
        _board.Setup(BoardUpdate);
        _uiBoard.Setup();
        _turnIndicator.Setup();
    }

    //TODO: Add eventual parameters to this function
    private void BoardUpdate(BoardAction boardAction) {
        _uiBoard.UpdateTiles(_board.GetState());
        _turnIndicator.SetTurn(boardAction.nextPlayer);
    }
}
