using UnityEngine;
using System.Collections;
using System;
using System.Text;

public enum TileState {
	Empty,
	X,
	O
}

public class Game {

	public TileState currentPlayer = TileState.O;
	public TileState nextPlayer = TileState.X;
	public bool inProgress = false;
	public Board board;

	private string[] winConditions = {
		"111000000", "000111000", "000000111", // rows
		"100100100", "010010010", "001001001", // cols
		"100010001", "001010100"               // diagonals
	};

	public Game(int boardDimention = 3)	{
		board = new Board(boardDimention);
		inProgress = true;
	}

	public void MakeMove(int x, int y) {
		if (x < 0 && x >= board.dimention) {
			Debug.LogError ("Game:MakeMove X is out of range");
			return;
		}
		if (y < 0 && y >= board.dimention) {
			Debug.LogError ("Game:MakeMove Y is out of range");
			return;
		}
		board.tiles [x, y].SetTile (currentPlayer);
	}

	public void SwitchPlayer() {
		if (currentPlayer == TileState.O) {
			currentPlayer = TileState.X;
			nextPlayer = TileState.O;
		} else {
			currentPlayer = TileState.O;
			nextPlayer = TileState.X;
		}
	}

	private bool DetectLine(StringBuilder pattern) {
		for (int i = 0; i < winConditions.Length; i++) {
			int counter = 0;
			for (int j = 0; j < winConditions[i].Length; j++) {
				if (winConditions[i][j] == pattern[j] && pattern[j] == '1') {
					counter++;
					if(counter >= board.dimention){
						return true;
					}
				}
			}
		}

		return false;
	}

	public bool CheckForWinPosition() {

		StringBuilder pattern = new StringBuilder("000000000");

		for (int row = 0; row < board.dimention; ++row) {
			for (int col = 0; col < board.dimention; ++col) {
				if (board.tiles[row, col].State == currentPlayer) {
					pattern[row * board.dimention + col] = '1';
				}
			}
		}

		return DetectLine (pattern);
	}

}