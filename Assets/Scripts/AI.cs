using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AI {

	public TileState aiTileState;

	private Board board;
	private Game game;
	private Tile[,] boardTiles;
	private TileState opponentTileState;

	public AI(Game _game, Board _board, TileState _aiTileState) {
		board = _board;
		game = _game;
		boardTiles = _board.tiles;
		aiTileState = _aiTileState;
		opponentTileState = (_aiTileState == TileState.O) ? TileState.X : TileState.O;
	}
		
	public void MakeMove() {
		int [] aiMove = Minimax(2, aiTileState);
		game.MakeMove (aiMove[1], aiMove[2]);
	}


	//Minimax algorithm https://www.ntu.edu.sg/home/ehchua/programming/java/JavaGame_TicTacToe_AI.html

	private int[] Minimax(int depth, TileState playerTIleState) {
		List<Tile> nextMoves = GenerateMoves ();

		int bestScore = (playerTIleState == aiTileState) ? Int32.MinValue : Int32.MaxValue;
		int currentScore;
		int bestRow = -1;
		int bestCol = -1;

		if (nextMoves.Count == 0 || depth == 0) {
			// Gameover or depth reached, evaluate score
			bestScore = EvaluateAllLines();
		} else {
			for (int i = 0; i < nextMoves.Count; i++) {
				Tile move = nextMoves [i];
				// Try this move for the current "player"
				boardTiles[move.XIndex, move.YIndex].State = playerTIleState;
				if (playerTIleState == aiTileState) {  // aiTileState (computer) is maximizing player
					currentScore = Minimax(depth - 1, opponentTileState)[0];
					if (currentScore > bestScore) {
						bestScore = currentScore;
						bestRow = move.XIndex;
						bestCol = move.YIndex;
					}
				} else {  // oppSeed is minimizing player
					currentScore = Minimax(depth - 1, aiTileState)[0];
					if (currentScore < bestScore) {
						bestScore = currentScore;
						bestRow = move.XIndex;
						bestCol = move.YIndex;
					}
				}
				// Undo move
				boardTiles[move.XIndex, move.YIndex].State = TileState.Empty;
			}
		}
		return new int[] {bestScore, bestRow, bestCol};

	}

	private List<Tile> GenerateMoves() {
		List<Tile> nextMoves = new List<Tile>(); // allocate List

		// Search for empty cells and add to the List

		for (int row = 0; row < board.dimention; ++row) {
			for (int col = 0; col < board.dimention; ++col) {
				if (boardTiles[row, col].State == TileState.Empty) {
					nextMoves.Add(boardTiles[row, col]);
				}
			}
		}

		return nextMoves;
	}

	private int EvaluateAllLines() {
		int score = 0;

		// Evaluate score for each of the 8 lines (3 rows, 3 columns, 2 diagonals)
		score += EvaluateLine(0, 0, 0, 1, 0, 2);  // col 0
		score += EvaluateLine(1, 0, 1, 1, 1, 2);  // col 1
		score += EvaluateLine(2, 0, 2, 1, 2, 2);  // col 2
		score += EvaluateLine(0, 0, 1, 0, 2, 0);  // row 0
		score += EvaluateLine(0, 1, 1, 1, 2, 1);  // row 1
		score += EvaluateLine(0, 2, 1, 2, 2, 2);  // row 2
		score += EvaluateLine(0, 0, 1, 1, 2, 2);  // diagonal
		score += EvaluateLine(0, 2, 1, 1, 2, 0);  // alternate diagonal

		return score;
	}

	private int EvaluateLine(int row1, int col1, int row2, int col2, int row3, int col3) {
		int score = 0;

		// First cell
		if (boardTiles[row1, col1].State == aiTileState) {
			score = 1;
		} else if (boardTiles[row1, col1].State == opponentTileState) {
			score = -1;
		}

		// Second cell
		if (boardTiles[row2, col2].State == aiTileState) {
			if (score == 1) {   // cell1 is mySeed
				score = 10;
			} else if (score == -1) {  // cell1 is oppSeed
				return 0;
			} else {  // cell1 is empty
				score = 1;
			}
		} else if (boardTiles[row2, col2].State == opponentTileState) {
			if (score == -1) { // cell1 is oppSeed
				score = -10;
			} else if (score == 1) { // cell1 is mySeed
				return 0;
			} else {  // cell1 is empty
				score = -1;
			}
		}

		// Third cell
		if (boardTiles[row3, col3].State == aiTileState) {
			if (score > 0) {  // cell1 and/or cell2 is mySeed
				score *= 10;
			} else if (score < 0) {  // cell1 and/or cell2 is oppSeed
				return 0;
			} else {  // cell1 and cell2 are empty
				score = 1;
			}
		} else if (boardTiles[row3, col3].State == opponentTileState) {
			if (score < 0) {  // cell1 and/or cell2 is oppSeed
				score *= 10;
			} else if (score > 1) {  // cell1 and/or cell2 is mySeed
				return 0;
			} else {  // cell1 and cell2 are empty
				score = -1;
			}
		}
		return score;
	}

}
