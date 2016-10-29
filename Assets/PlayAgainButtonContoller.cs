using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayAgainButtonContoller : MonoBehaviour {

	public void OnClick() {
		SceneManager.LoadScene ("Menu");
	}
}
