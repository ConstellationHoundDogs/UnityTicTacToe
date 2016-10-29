using UnityEngine;
using System.Collections;

public class TileController : MonoBehaviour {

	public Tile tile;
	public Game game;

	void OnMouseDown() {
		game.MakeMove (tile.XIndex, tile.YIndex);
	}
}
