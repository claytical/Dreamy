﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CupidTalk : MonoBehaviour {
	public string[] introDialogue;
	public string[] endDialogue;
	public Text dialog;
	public GameObject startButton;
	public GameObject continueButton;
	public Text heartsHealed;
	public int finalTally = 0;
	private bool hasPlayedBefore = false;

	private int dialogIndex = 0;
	private int selectedDialog = 0;

	// Use this for initialization
	void Start () {
		int playedBefore = PlayerPrefs.GetInt("has played loveQ",0);
		if (playedBefore == 1) {
			hasPlayedBefore = true;
		}

		if (hasPlayedBefore) {
			continueButton.SetActive (false);
			startButton.SetActive (true);
			dialog.text = "Welcome back! Are you ready to play again?";
		}
		else {
			dialog.text = introDialogue [0];
		}

	}

	public void changeDialog(string dialogName) {
		dialogIndex = 0;

		if (dialogName == "intro") {
			selectedDialog = 0;
			dialog.text = introDialogue [dialogIndex];
		}
		if (dialogName == "end") {
			selectedDialog = 1;
			PlayerPrefs.SetInt("has played loveQ",1);

//			dialog.text = endDialogue [dialogIndex];
			if(finalTally > 0) {
				dialog.text = "You healed " + finalTally.ToString() + " hearts, you're more loveable already!";

			}
			else {
				dialog.text = "Oh no! You didn't heal any hearts. There's always next time.";
			}
			Debug.Log("HEAL COUNT: " + finalTally);

			//TODO: Convert string to int
			/*			if (gameObject.GetComponent<CupidStats>().healed == 1) {
				dialog.text = "You healed " + heartsHealed.text + " heart! Maybe you'll do better next time.";
			}
			if (gameObject.GetComponent<CupidStats>().healed == 0) {
				dialog.text = "Oh no, you didn't heal any hearts! Remember, you can shoot an arrow with the spacebar.";
			}
			*/

		}
	}

	public void advanceDialog() {
		dialogIndex++;

		switch(selectedDialog) {
			case 0:
				if (introDialogue.Length > dialogIndex) {
			
					dialog.text = introDialogue [dialogIndex];
				}
				if (introDialogue.Length - 1 == dialogIndex) {
					continueButton.SetActive (false);
					startButton.SetActive (true);
				}

				break;
			case 1:

				if (endDialogue.Length > dialogIndex) {
					dialog.text = endDialogue[dialogIndex];
				}
				if (endDialogue.Length - 1 == dialogIndex) {
					//				startButton.interactable = true;
				}
				break;
			}

	}

	public void cueEndTalk() {
		dialogIndex = 0;

	}
}
