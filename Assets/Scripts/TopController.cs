using UnityEngine;
using System;
using System.Collections;
using System.Text;

public class TopController : MonoBehaviour {

	public Sprite tileSpriteEmpty;
	public Sprite tileSpriteX;
	public Sprite tileSpriteO;

	public float spriteXOffset;
	public float spriteYOffset;

	public bool aiOpponent;

	private Game game;
	private Board board;
	private AI ai;

	void GeneratePlayingBoard (Board board) {
		SpriteRenderer spriteRenderer;

		for (int x = 0; x < board.dimention; x++) {
			for (int y = 0; y < board.dimention; y++) {
				Tile boardTile = board.tiles [x, y];
				GameObject tileGo = new GameObject ();

				boardTile.changeTileStateCallback = ((tile) => {OnTileChangedState(tile, tileGo);});
				tileGo.transform.position = new Vector3 (x + spriteXOffset, y + spriteYOffset, 0);

				//I believe that String.Fomat uses string builder internaly so it is ok
				tileGo.name = String.Format("Tile_{0}_{1}", x, y);

				tileGo.AddComponent<BoxCollider2D> ();

				spriteRenderer = tileGo.AddComponent<SpriteRenderer> ();
				spriteRenderer.sprite = tileSpriteEmpty;

				TileController controller = tileGo.AddComponent<TileController> ();
				controller.tile = boardTile;
				controller.game = game;
			}	
		}
	}

	void Awake () {
		game = new Game ();
		board = game.board;
		if(aiOpponent){
			ai = new AI (game, board, game.nextPlayer);
		}

		GeneratePlayingBoard (board);
	}
		
	void GameWon() {
		Debug.Log (game.currentPlayer + " won!");
	}

	void GameDraw() {
		Debug.Log ("Draw");
	}

	void ChangeTileSprite(Tile changedTile, SpriteRenderer spriteRenderer){
		if(changedTile.State == TileState.O) {
			spriteRenderer.sprite = tileSpriteO;
		} else if(changedTile.State == TileState.X) {
			spriteRenderer.sprite = tileSpriteX;
		} else if (changedTile.State == TileState.Empty) {
			spriteRenderer.sprite = tileSpriteEmpty;
		}
	}

	void OnTileChangedState(Tile changedTile, GameObject tileGo) {

		ChangeTileSprite (changedTile, tileGo.GetComponent<SpriteRenderer> ());

		if (game.CheckForWinPosition ()) {
			GameWon ();
			return;
		}

		if(board.emptyTiles <= 0){
			GameDraw ();
			return;
		}

		game.SwitchPlayer ();

		if(aiOpponent && game.currentPlayer == ai.aiTileState){
			ai.MakeMove ();
		}
	}

}
