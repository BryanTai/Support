using UnityEngine;
using System.Collections;

public class BasicMinionController : AiActorController {

	public enum AiState { STANDBY, MOVE, ATTACK }

	public AiState state = AiState.STANDBY;

	GameObject commandBubble;

	// Use this for initialization
	void Start () {
		speed = 2;

		commandBubble = GameObject.Find ("CommandBubble");
	}
	
	// Update is called once per frame
	void Update () {
		LookForCommandBubble ();

		if (state == AiState.ATTACK) {
			// TODO: ...
		} else if (state == AiState.MOVE) {
			MoveToCommandBubble();
		} else if (state == AiState.STANDBY) {
			// TODO: Idle animation?
		}
	}

	void LookForCommandBubble() {
		CommandBubbleController cbc = (CommandBubbleController) commandBubble.GetComponent ("CommandBubbleController");

		bool isInsideBubble = IsInsideCommandBubble ();

		if (cbc.isActive && isInsideBubble) {
				state = AiState.ATTACK;
		} else if (cbc.isActive && !isInsideBubble) {
			state = AiState.MOVE;
		} else {
			state = AiState.STANDBY;
		}

	}

	bool IsInsideCommandBubble () {
		// TODO: Implment this
		return false;
	}


	void MoveToCommandBubble() {
		Move (commandBubble.transform.position);
	}

}
