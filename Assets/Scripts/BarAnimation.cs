using UnityEngine;
using System.Collections;

public class BarAnimation : MonoBehaviour{

	WizardController wizard;
	int maxHealth;

	// Use this for initialization
	void Start () {
		wizard = (WizardController) GameObject.Find("Wizard").GetComponent("WizardController");
		maxHealth = WizardController.MAX_HEALTH;
	}
	
	// Update is called once per frame
	void Update () {
		//stupid update method sucks
	}

	public void UpdateBar() {
		int currentHealth = wizard.health;
		this.transform.animation["healthbar_animation"].speed = 0.0000f;
		if ( currentHealth <= 0 )  {  
			this.transform.animation["healthbar_animation"].time = this.transform.animation["healthbar_animation"].clip.length;
		} else {
			this.transform.animation["healthbar_animation"].time = this.transform.animation["healthbar_animation"].clip.length * (1.0f - ((float)currentHealth)/maxHealth);
		}
		this.transform.animation.Play("healthbar_animation");
	}
}
