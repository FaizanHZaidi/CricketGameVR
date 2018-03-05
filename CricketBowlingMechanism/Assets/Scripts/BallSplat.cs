using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSplat : MonoBehaviour {
	private ScoreBoxManager hitWall;
	public AudioManager audio;
	public ScoreManager scoreboard;
	public ParticleSystem splat;
	public ParticelDecalPool pdp;

	// Use this for initialization
	void Start () {
		pdp 		=	GameObject.Find ("PaintDecalManager").GetComponent<ParticelDecalPool> ();
		scoreboard 	= 	GameObject.Find ("ScoreManager").GetComponent<ScoreManager> ();
		audio 		= 	GameObject.Find ("AudioManager").GetComponent<AudioManager> ();
	}

	void OnCollisionEnter(Collision col){
		handleCollision (col);
	}

	public void handleCollision(Collision col){
		Debug.Log ("ball collided");
		if (col.gameObject.tag == "wall") {

			//Get reference to the wall's score object
			hitWall = col.gameObject.GetComponent<ScoreBoxManager> ();

			// only update the multiplier if the wall hasn't had paint on it yet.
			if (!hitWall.isPainted()) {
				scoreboard.addToPainted (1);
				scoreboard.addToMultiplier (hitWall.mult_value);

			}

			//Tell the wall it has been painted, and update the player's score
			hitWall.paintWall();
			scoreboard.addToRuns (hitWall.runs_value);

			// play the appropriate sound
			if (hitWall.runs_value == 4) {
				audio.playSound ("FourRuns");
			} 
			else {
				audio.playSound("SixRuns");
			}

			GameObject.Destroy (gameObject);
			pdp.ParticleHit (col, Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1f, 0.5f));
		}
	}

}
