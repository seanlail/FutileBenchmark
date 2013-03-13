using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * @author Sean Lailvaux
 */
public class Menu : FContainer, FSingleTouchableInterface {
	
	private FSprite background;
	private FButton bunnyButton;
	private FButton benchButton;
	private FButton pirateButton;

	
	override public void HandleAddedToStage() {
		base.HandleAddedToStage();
		build();
		Futile.screen.SignalResize += HandleResize;
		HandleResize( false );
	}

	override public void HandleRemovedFromStage() {
		Futile.screen.SignalResize -= HandleResize;
		purge();
		base.HandleRemovedFromStage();
	}



	public void build() {

		Futile.touchManager.AddSingleTouchTarget( this );
		
		background = new FSprite( "blank" );
		background.color = Color.white;
		AddChild( background );

		bunnyButton = new FButton( "YellowButton_normal", "YellowButton_down", "YellowButton_over" );
		bunnyButton.AddLabel( "Franchise", "TEST 1", Color.white );
		bunnyButton.SignalRelease += onBunnyClicked;
		AddChild( bunnyButton );

		benchButton = new FButton( "YellowButton_normal", "YellowButton_down", "YellowButton_over" );
		benchButton.AddLabel( "Franchise", "TEST 2", Color.white );
		benchButton.SignalRelease += onBenchClicked;
		AddChild( benchButton );

		pirateButton = new FButton( "YellowButton_normal", "YellowButton_down", "YellowButton_over" );
		pirateButton.AddLabel( "Franchise", "TEST 3", Color.white );
		pirateButton.SignalRelease += onPirateClicked;
		AddChild( pirateButton );
	}

	public void purge() {
		
		Futile.touchManager.RemoveSingleTouchTarget( this );

		bunnyButton.SignalRelease -= onBunnyClicked;
		benchButton.SignalRelease -= onBenchClicked;
		pirateButton.SignalRelease -= onPirateClicked;

		RemoveChild( background );
		RemoveChild( bunnyButton );
		RemoveChild( benchButton );
		RemoveChild( pirateButton );

		background = null;
		bunnyButton = null;
		benchButton = null;
		pirateButton = null;
	}



	protected void HandleResize(bool wasOrientationChange) {

		background.width = Futile.screen.width;
		background.height = Futile.screen.height;

		float h = bunnyButton.sprite.height + benchButton.sprite.height + pirateButton.sprite.height + 20.0f;

		bunnyButton.y = ( h - bunnyButton.sprite.height ) * 0.5f;
		benchButton.y = bunnyButton.y - bunnyButton.sprite.height * 0.5f - 10.0f - benchButton.sprite.height * 0.5f;
		pirateButton.y = benchButton.y - benchButton.sprite.height * 0.5f - 10.0f - pirateButton.sprite.height * 0.5f;
	}

	private void onBunnyClicked( FButton button ) {
		Main.instance.showBunnyMark();
	}

	private void onBenchClicked( FButton button ) {
		Main.instance.showBenchMark();
	}

	private void onPirateClicked( FButton button ) {
		Main.instance.showPirateMark();
	}



	public bool HandleSingleTouchBegan(FTouch touch) {
		return true;
	}
	
	public void HandleSingleTouchMoved(FTouch touch) {
		//
	}

	public void HandleSingleTouchEnded(FTouch touch) {
		//
	}

	public void HandleSingleTouchCanceled(FTouch touch) {
		//
	}
}
