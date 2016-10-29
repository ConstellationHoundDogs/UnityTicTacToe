using UnityEngine;
using System.Collections;

public enum PlayWith {
	Human,
	Computer
}

public class GameController : MonoBehaviour {

	public static GameController instance;

	[HideInInspector]
	public PlayWith opponent;

	void Awake () {
		DontDestroyOnLoad (this);
		instance = this;

		Debug.Log ("I should be called only once!");
	}

}
