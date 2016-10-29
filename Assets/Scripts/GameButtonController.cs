using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameButtonController : MonoBehaviour {

	public PlayWith playWith;

	public void OnClick() {
		GameController.instance.opponent = playWith;
		SceneManager.LoadScene ("Game");
	}
}
