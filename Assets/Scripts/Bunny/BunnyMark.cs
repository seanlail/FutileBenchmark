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
public class BunnyMark : Page {
	
	private FContainer holder;
	private Counter counter;

	private List<Bunny> bunnys;
	private float gravity = 0.75f;
	private float maxX = 0;
	private float minX = 0;
	private float maxY = 0;
	private float minY = 0;
	private int startCount = 10;
	private bool isAdding = false;
	private int count = 0;
	private int amount = 10;


	public override void build() {

		arrange();

		holder = new FContainer();
		AddChild( holder );
		
		counter = new Counter();
		counter.title = "COUNT";
		AddChild( counter );
		
		bunnys = new List<Bunny>();
		count = startCount;
		counter.count = startCount;

		int i = 0;
		for ( i=0; i<startCount; ++i ) {
			addBunny();
		}
	}

	public override void purge() {
		
		RemoveChild( counter );
		counter = null;

		holder.RemoveAllChildren();
		bunnys.Clear();

		RemoveChild( holder );
		holder = null;
		bunnys = null;
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
					addBunny();
					count++;
				}
			}
			
			if ( count >= Main.LIMIT ) amount = 0;
			counter.count = count;
		}
		
		for ( i=0; i<bunnys.Count; ++i ) {
			
			Bunny bunny = bunnys[i];
			
			bunny.x += bunny.speedX;
			bunny.y -= bunny.speedY;
			bunny.speedY += gravity;
			
			if ( bunny.x > maxX ) {
				bunny.speedX *= -1;
				bunny.x = maxX;
			}
			else if ( bunny.x < minX ) {
				bunny.speedX *= -1;
				bunny.x = minX;
			}

			if ( bunny.y < minY ) {
				bunny.speedY *= -0.8f;
				bunny.y = minY;
				bunny.rotation = RXRandom.Float() * 360.0f;
				if ( RXRandom.Float() > 0.5f ) {
					bunny.speedY -= RXRandom.Float() * 12.0f;
				}
			}
			else if ( bunny.y > maxY ) {
				bunny.speedY = 0;
				bunny.y = maxY;
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



	private void addBunny() {
		Bunny bunny = new Bunny();
		bunny.x = minX;
		bunny.y = maxY;
		holder.AddChild(bunny);
		bunnys.Add(bunny);
	}
}
