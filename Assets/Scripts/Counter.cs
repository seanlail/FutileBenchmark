using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * @author Sean Lailvaux
 */
public class Counter : FContainer {

	public int count = 0;
	public string title = "";

	private int countUsed = -1;

	private FSprite background;
	private FLabel label;


	override public void HandleAddedToStage() {

		base.HandleAddedToStage();

		background = new FSprite( "blank" );
		background.color = Color.black;
		background.isVisible = false;
		AddChild( background );

		label = new FLabel( "Franchise", title + ": 0" );
		//label.scale = 0.3f;
		label.isVisible = false;
		label.color = Color.white;
		AddChild( label );

		Futile.instance.SignalUpdate += HandleUpdate;
	}

	override public void HandleRemovedFromStage() {
		
		Futile.instance.SignalUpdate -= HandleUpdate;
		
		RemoveChild( background );
		RemoveChild( label );

		background = null;
		label = null;

		base.HandleRemovedFromStage();
	}



	protected void HandleUpdate() {

	    if ( countUsed != count ) {
	    	countUsed = count;
			label.text = title + ": " + count;
	        background.isVisible = true;
	        label.isVisible = true;
	    }

	    background.width = label.textRect.width * label.scale + 4.0f;
	    background.height = label.textRect.height * label.scale;
	    background.x = - Futile.screen.halfWidth + ( background.width * 0.5f );
	    background.y = - Futile.screen.halfHeight + ( background.height * 0.5f );
	    label.x = background.x;
	    label.y = background.y;
	}
}