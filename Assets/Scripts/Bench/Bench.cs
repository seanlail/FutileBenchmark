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
public class Bench : FSprite {

	public float speedX = 0.0f;
	public float speedY = 0.0f;
	

	public Bench() : base( "Bunny" ) {
		speedX = RXRandom.Float() * 10.0f;
		speedY = RXRandom.Float() * 10.0f - 5.0f;
		rotation = RXRandom.Float() * 360.0f;
	}
}