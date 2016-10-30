using UnityEngine;
using System.Collections;

public class Board {

	public int dimention;
	public Tile[,] tiles;

	public int emptyTiles;

	public Board(int _dimention) {
		dimention = _dimention;
		tiles = new Tile[dimention, dimention];

		for (int i = 0; i < dimention; i++) {
			for(int j = 0; j < dimention; j++){
				tiles[i, j] = new Tile(this, i, j);
			}
		}
		emptyTiles = dimention * dimention;
	}

	public void ClearBoard() {
		for (int i = 0; i < dimention; i++) {
			for (int j = 0; j < dimention; j++) {
				tiles[i, j].SetTile(TileState.Empty);
			}
		}
		emptyTiles = dimention * dimention;
	}

}