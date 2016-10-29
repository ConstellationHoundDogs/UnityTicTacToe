using UnityEngine;
using System;
using System.Collections;

public class Tile
{
	private Board board;
	private int xIndex;
	private int yIndex;
	private TileState state;

	public Action<Tile> changeTileStateCallback;

	public int XIndex {
		get {
			return xIndex;
		}
	}

	public int YIndex {
		get {
			return yIndex;
		}
	}

	public TileState State {
		get {
			return state;
		}
		set {
			state = value;
		}
	}


	public void SetTile(TileState newState)	{
		if(state == TileState.Empty){
			state = newState;
			board.emptyTiles--;
			if (changeTileStateCallback != null) {
				changeTileStateCallback (this);
			}
		}
	}


	public Tile(Board _board, int x, int y)
	{
		board = _board;
		xIndex = x;
		yIndex = y;
		state = TileState.Empty;
	}
}
