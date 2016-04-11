﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using SimpleJSON;
using System;
using System.Collections.Generic;

public class IncomingMessage : MonoBehaviour {
	public string character;
	public Text subject;
	public ViewMessage expandedMessageTemplate;
	public Character avatar;
	public Image overrideImage;

	private Message message;

	public void setMessage(Message msg) {
		message = msg;
		character = message.sender;
		avatar.gameObject.SetActive(false);
		overrideImage.enabled = true;
		switch(character) {
			case "tanning":
				overrideImage.sprite = Resources.Load<Sprite>("Salon/ray");
				break;
			case "love":
				overrideImage.sprite = Resources.Load<Sprite>("LoveQ/cupid");
				break;
			case "exam":
				overrideImage.sprite = Resources.Load<Sprite>("Dermatologist/dermatologist");
				break;
			case "piercing":
				overrideImage.sprite = Resources.Load<Sprite>("Home/pierced_larry");
				break;
			case "haircut":
				overrideImage.sprite = Resources.Load<Sprite>("Home/hairstylist_emerald");
				break;
		default:
				overrideImage.enabled = false;
				avatar.gameObject.SetActive(true);
				break;
		}
		message.alias = PlayerPrefs.GetString(character);
		avatar.assign(character);
		subject.text = message.subject;
	}

	public void show() {
		//TODO: TRACK VIEWING OF MESSAGE
		//CURRENTLY USING SAMPLE


		GameObject msg = Instantiate(expandedMessageTemplate.gameObject);
		msg.GetComponent<ViewMessage>().body.text = message.body;
		msg.GetComponent<ViewMessage>().profilePic.enabled = true;
		msg.GetComponent<ViewMessage>().character.gameObject.SetActive(false);
		switch (message.sender) {
		case "tanning":
			GetComponent<PlayerBehavior>().trackEvent(3, StringArrayFunctions.getMessage(message.path)[1], message.belief, "");
			msg.GetComponent<ViewMessage>().alias.text = "Rays Tanning Salon";
			msg.GetComponent<ViewMessage>().profilePic.sprite = Resources.Load<Sprite>("Salon/ray");

			break;
		case "love":
			GetComponent<PlayerBehavior>().trackEvent(3, "LOVE", "", "");

			msg.GetComponent<ViewMessage>().alias.text = "LoveQ";
			msg.GetComponent<ViewMessage>().profilePic.sprite = Resources.Load<Sprite>("LoveQ/cupid");

			break;
		case "exam":
			message.belief = "EE";

			GetComponent<PlayerBehavior>().trackEvent(3, "EXAM", message.belief, "");

			msg.GetComponent<ViewMessage>().alias.text = "Dermafreeze";
			msg.GetComponent<ViewMessage>().profilePic.sprite = Resources.Load<Sprite>("Dermatologist/dermatologist");
			break;
		case "piercing":
			GetComponent<PlayerBehavior>().trackEvent(3, "PIERCING", "", "");
			msg.GetComponent<ViewMessage>().alias.text = "Larry's Piercing Parlor";
			msg.GetComponent<ViewMessage>().profilePic.sprite = Resources.Load<Sprite>("Home/pierced_larry");
			break;
		case "haircut":
			GetComponent<PlayerBehavior>().trackEvent(3, "HAIRCUT","", "");
			msg.GetComponent<ViewMessage>().alias.text = "Emerald's Salon";
			msg.GetComponent<ViewMessage>().profilePic.sprite = Resources.Load<Sprite>("Home/hairstylist_emerald");
			break;
		default:
			GetComponent<PlayerBehavior>().trackEvent(1, "DLG", message.belief, message.sender);
			msg.GetComponent<ViewMessage>().character.assign(message.sender);
			msg.GetComponent<ViewMessage>().character.assign(character);
			msg.GetComponent<ViewMessage>().alias.text = msg.GetComponent<ViewMessage>().character.name;
			msg.GetComponent<ViewMessage>().profilePic.enabled = false;
			msg.GetComponent<ViewMessage>().character.gameObject.SetActive(true);

			break;

		}
		msg.transform.SetParent(this.gameObject.transform.parent.parent.parent.parent, false);
		this.transform.parent.gameObject.SetActive(false);

		for (int i = 0; i < message.responses.Count; i++) {
			//ADDS PARENT BELIEF FOR RESPONSE IF IT ISN'T MARKED AS ANOTHER BELIEF ID
			if (message.responses[i].belief != null) {
				if(message.responses[i].belief.Contains("none")) {
						message.responses[i].belief = message.belief;
					}
			}
				msg.GetComponent<ViewMessage>().addResponse(message.responses[i]);
		}
	
	}
	/*
	private string[] getPath(string message) {
		return message.Split (new string[] {"/"}, System.StringSplitOptions.None);
	}
*/
}
