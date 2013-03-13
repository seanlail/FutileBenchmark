using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * @author Sean Lailvaux
 */
public class Page : FContainer, FSingleTouchableInterface {
	
	private FSprite background;
	private FButton closeButton;

	
	override public void HandleAddedToStage() {

		base.HandleAddedToStage();
		Futile.touchManager.AddSingleTouchTarget( this );
		
		background = new FSprite( "blank" );
		background.color = Color.white;
		AddChild( background );
		
		build();

		closeButton = new FButton( "CloseButton_normal", "CloseButton_down", "CloseButton_over" );
		closeButton.SignalRelease += onCloseClicked;
		AddChild( closeButton );
		
		Futile.instance.SignalUpdate += HandleUpdate;
		Futile.screen.SignalResize += HandleResize;
		HandleResize( false );
	}

	override public void HandleRemovedFromStage() {

		Futile.instance.SignalUpdate -= HandleUpdate;
		Futile.screen.SignalResize -= HandleResize;

		Futile.touchManager.RemoveSingleTouchTarget( this );

		closeButton.SignalRelease -= onCloseClicked;
		RemoveChild( closeButton );
		closeButton = null;

		RemoveChild( background );
		background = null;

		purge();

		base.HandleRemovedFromStage();
	}
	
	protected void HandleResize(bool wasOrientationChange) {

		background.width = Futile.screen.width;
		background.height = Futile.screen.height;

		closeButton.x = Futile.screen.halfWidth - closeButton.sprite.width * 0.5f - 10.0f;
		closeButton.y = Futile.screen.halfHeight - closeButton.sprite.height * 0.5f - 10.0f;

		arrange();
	}

	protected void HandleUpdate() {
		update();
	}



	public virtual void build() {
		//
	}

	public virtual void purge() {
		//
	}

	public virtual void arrange() {
		//
	}

	public virtual void update() {
		//
	}

	

	public bool HandleSingleTouchBegan(FTouch touch) {
		return true;
	}
	
	public virtual void HandleSingleTouchMoved(FTouch touch) {
		//
	}

	public virtual void HandleSingleTouchEnded(FTouch touch) {
		//
	}

	public virtual void HandleSingleTouchCanceled(FTouch touch) {
		//
	}


	
	private void onCloseClicked( FButton button ) {
		Main.instance.showMenu();
	}
}
