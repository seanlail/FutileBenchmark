using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * @author Sean Lailvaux
 *
 * Based on BunnyMark by @iainlobb
 * @see https://github.com/iainlobb/DullDude
 */
public class Bunny : FSprite {

	public float speedX = 0.0f;
	public float speedY = 0.0f;
	

	public Bunny() : base( "Bunny" ) {
		speedX = RXRandom.Float() * 10.0f;
		speedY = RXRandom.Float() * 10.0f - 5.0f;
		alpha = 0.3f + RXRandom.Float() * 0.7f;
		rotation = RXRandom.Float() * 360.0f;
	}
}