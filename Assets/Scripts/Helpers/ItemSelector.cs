﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemSelector : MonoBehaviour {
	public bool inUse;
	public Button removeButton;
	public Button selectButton;
	public string path;
	public AudioClip selected;
	public AudioClip discarded;
	// Use this for initialization
	void Start () {
			inUse = false;
			removeButton.onClick.AddListener (() => {
				Camera.main.gameObject.GetComponent<AudioSource> ().PlayOneShot (discarded);
			});
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void use() {
		Camera.main.gameObject.GetComponent<AudioSource> ().PlayOneShot (selected);
		for(int i = 0; i < gameObject.transform.parent.GetComponentsInChildren<ItemSelector>().Length; i++){
			ItemSelector item = gameObject.transform.parent.GetComponentsInChildren<ItemSelector>()[i];
//			if (item.inUse) {
				item.remove();
//			}
		}
		
		inUse = true;
		Color c = new Color(0.2F, 0.3F, 0.4F, 0.5F);
		selectButton.targetGraphic.color = c;
		removeButton.gameObject.SetActive(true);
		gameObject.GetComponentInParent<ItemFolderSelector> ().assignImage (path + "/" + selectButton.image.sprite.name);
		saveChanges ();
	}

	private void saveChanges() {
		Debug.Log("Saving Changes");
		Character character = gameObject.GetComponentInParent<ItemFolderSelector> ().avatarImage.transform.parent.GetComponent<Character> ();
		// COULD REMOVE THESE AND MOVE TO ALL IN ONE SAVE
/*		character.setPaths ();
		character.setOptions ();
		character.saveCharacter ();
*/
	}

	public void remove() {
		inUse = false;
		Color c = new Color(1F, 1F, 1F, 1F);
		selectButton.targetGraphic.color = c;
		removeButton.gameObject.SetActive(false);
		gameObject.GetComponentInParent<ItemFolderSelector> ().removeImage();
		saveChanges ();
	}
}
