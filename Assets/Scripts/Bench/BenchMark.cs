using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * @author Sean Lailvaux
 *
 * Based on BunnyMark by @iainlobb
 * @see https://github.com/iainlobb/DullDude
 *
 * Based on Starling benchmark by @PrimaryFeather
 * @see https://github.com/PrimaryFeather/Starling-Framework
 */
public class BenchMark : Page {
	
	private FContainer mContainer;
	private Counter counter;
	private int mFrameCount;
	private bool mStarted;
	private int mFailCount;
	private int mWaitFrames;


	public override void build() {

		mContainer = new FContainer();
		AddChild( mContainer );

		counter = new Counter();
		counter.title = "COUNT";
		AddChild( counter );

		mFrameCount = 0;
		mWaitFrames = 1;
		mStarted = true;
	}

	public override void purge() {
		
		mStarted = false;

		RemoveChild( counter );
		counter = null;

		mContainer.RemoveAllChildren();
		mContainer = null;
	}

	public override void arrange() {
		//
	}

	public override void update() {
		
		if (!mStarted) return;

		mFrameCount++;

		int i = 0;
		int len = mContainer.GetChildCount();

		if ( len < Main.LIMIT ) {

			if ( mFrameCount % mWaitFrames == 0 ) {
				
				float fps = Stats.FPS;

				if ( Mathf.Ceil(fps) >= Main.TARGET_FPS ) {
					mFailCount = 0;
					addTestObjects();
					len = mContainer.GetChildCount();
				}
				else {
					mFailCount++;

					if ( mFailCount > 20 ) mWaitFrames = 5;

					// slow down creation process to be more exact
					if ( mFailCount > 30 ) mWaitFrames = 10;

					// target fps not reached for a while
					if ( mFailCount == 40 ) benchmarkComplete();
				}

				mFrameCount = 0;
			}
		}
		
		counter.count = len;

		for ( i=0; i<len; ++i ) {
			mContainer.GetChildAt(i).rotation += 1; //( Time.timeScale/Time.deltaTime ) / Stats.FPS;
		}
	}



	private void addTestObjects() {
		
		int i = 0;
		int padding = 10;
		int numObjects = mFailCount > 20 ? 2 : 10;
		
		int len = mContainer.GetChildCount();
		if ( len + numObjects > Main.LIMIT ) numObjects = Main.LIMIT - len;

		for ( i=0; i<numObjects; ++i ) {
			Bench s = new Bench();
			s.x = - Futile.screen.halfWidth + padding + RXRandom.Float() * ( Futile.screen.width - 2 * padding );
			s.y = - Futile.screen.halfHeight + padding + RXRandom.Float() * ( Futile.screen.height - 2 * padding );
			mContainer.AddChild( s );
		}
	}

	private void benchmarkComplete() {

		mStarted = false;
		int len = mContainer.GetChildCount();
		Debug.Log( "Benchmark complete!");
		Debug.Log( "FPS: " + Stats.FPS );
		Debug.Log( "Number of objects: " + len );

		counter.count = len;
	}
}