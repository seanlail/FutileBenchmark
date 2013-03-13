using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * @author Sean Lailvaux
 *
 * Based on Unity Wiki: FramesPerSecond
 * @see http://wiki.unity3d.com/index.php?title=FramesPerSecond#CSharp_HUDFPS.cs
 */
public class Stats : FContainer {

	public static float FPS;

	private float updateInterval = 0.5F;
	private float accum   = 0; // FPS accumulated over the interval
	private int frames  = 0; // Frames drawn over the interval
	private float timeleft; // Left time for current interval
	private FSprite background;
	private FLabel label;


	override public void HandleAddedToStage() {
		base.HandleAddedToStage();
		build();
		Futile.instance.SignalUpdate += HandleUpdate;
	}

	override public void HandleRemovedFromStage() {
		Futile.instance.SignalUpdate -= HandleUpdate;
		purge();
		base.HandleRemovedFromStage();
	}



	private void build() {
		
		background = new FSprite( "blank" );
		background.color = Color.black;
		background.isVisible = false;
		AddChild( background );

		label = new FLabel( "Franchise", "FSP: 00.00" );
		//label.scale = 0.3f;
		label.isVisible = false;
		AddChild( label );

    	timeleft = updateInterval;
	}

	private void purge() {
		
		RemoveChild( background );
		RemoveChild( label );

		background = null;
		label = null;
	}



	protected void HandleUpdate() {

		timeleft -= Time.deltaTime;
	    accum += Time.timeScale/Time.deltaTime;
	    ++frames;
	    
	    if( timeleft <= 0.0 ) {

			FPS = accum/frames;
			string format = System.String.Format("FPS: {0:F2}", FPS );
			label.text = format;

			Color c = Color.white;
			if ( FPS < 30 ) c = Color.yellow;
			if ( FPS < 10 ) c = Color.red;
			label.color = c;

	        timeleft = updateInterval;
	        accum = 0.0F;
	        frames = 0;

	        background.isVisible = true;
	        label.isVisible = true;
	    }

	    background.width = label.textRect.width * label.scale + 4.0f;
	    background.height = label.textRect.height * label.scale;
	    background.x = - Futile.screen.halfWidth + ( background.width * 0.5f );
	    background.y = Futile.screen.halfHeight - ( background.height * 0.5f );
	    label.x = background.x;
	    label.y = background.y;
	}
}