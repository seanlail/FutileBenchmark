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
public class Pirate : FSprite {

	public float speedX = 0.0f;
	public float speedY = 0.0f;
	

	public Pirate() : base( "Pirate" ) {
		speedX = RXRandom.Float() * 10.0f;
		speedY = RXRandom.Float() * 10.0f - 5.0f;
		rotation = RXRandom.Float() * 360.0f;
		scale = 0.2f + RXRandom.Float() * 0.3f;
		
		// commented this out, as the Web Player version does some funky stuff
		// when using the shaders :S
		//if ( RXRandom.Float() > 0.5f ) {
		//	shader = FShader.Additive;
		//}
	}
}