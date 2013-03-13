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
public class PirateMark : Page {
	
	private FContainer holder;
	private Counter counter;

	private List<Pirate> pirates;
	private float gravity = 0.75f;
	private float maxX = 0;
	private float minX = 0;
	private float maxY = 0;
	private float minY = 0;
	private int startCount = 2;
	private bool isAdding = false;
	private int count = 0;
	private int amount = 2;


	public override void build() {

		arrange();

		holder = new FContainer();
		AddChild( holder );
		
		counter = new Counter();
		counter.title = "COUNT";
		AddChild( counter );
		
		pirates = new List<Pirate>();
		count = startCount;
		counter.count = startCount;

		int i = 0;
		for ( i=0; i<startCount; ++i ) {
			addPirate();
		}
	}

	public override void purge() {
		
		RemoveChild( counter );
		counter = null;

		holder.RemoveAllChildren();
		pirates.Clear();

		RemoveChild( holder );
		holder = null;
		pirates = null;
	}

	public override void arrange() {
		maxX = Futile.screen.halfWidth;
		minX = - Futile.screen.halfWidth;
		maxY = Futile.screen.halfHeight;
		minY = - Futile.screen.halfHeight;
	}

	public override void update() {
		
		int i = 0;

		if ( isAdding ) {
			
			for ( i=0; i<amount; ++i ) {
				if ( count < Main.LIMIT ) {
					addPirate();
					count++;
				}
			}
			
			if ( count >= Main.LIMIT ) amount = 0;
			counter.count = count;
		}
		
		for ( i=0; i<pirates.Count; ++i ) {
			
			Pirate pirate = pirates[i];
			
			pirate.x += pirate.speedX;
			pirate.y -= pirate.speedY;
			pirate.speedY += gravity;
			
			if ( pirate.x > maxX ) {
				pirate.speedX *= -1;
				pirate.x = maxX;
			}
			else if ( pirate.x < minX ) {
				pirate.speedX *= -1;
				pirate.x = minX;
			}

			if ( pirate.y < minY ) {
				pirate.speedY *= -0.8f;
				pirate.y = minY;
				pirate.rotation = RXRandom.Float() * 360.0f;
				if ( RXRandom.Float() > 0.5f ) {
					pirate.speedY -= RXRandom.Float() * 12.0f;
				}
			}
			else if ( pirate.y > maxY ) {
				pirate.speedY = 0;
				pirate.y = maxY;
			}
		}
	}

	
	
	override public void HandleSingleTouchMoved(FTouch touch) {
		isAdding = true;
	}

	override public void HandleSingleTouchEnded(FTouch touch) {
		isAdding = false;
	}

	override public void HandleSingleTouchCanceled(FTouch touch) {
		isAdding = false;
	}



	private void addPirate() {
		Pirate pirate = new Pirate();
		pirate.x = minX;
		pirate.y = maxY;
		holder.AddChild(pirate);
		pirates.Add(pirate);
	}
}
