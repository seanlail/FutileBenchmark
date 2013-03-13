using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * @author Sean Lailvaux
 */
public class Main : MonoBehaviour {

	public static Main instance;
	public static int LIMIT = 16000;
	public static int TARGET_FPS = 60;
	
	private FStage stage;
	private Stats stats;
	private BunnyMark bunny;
	private BenchMark bench;
	private PirateMark pirate;
	private Menu menu;


	/**
	 * Awake is called when the script instance is being loaded.
	 * Awake is used to initialize any variables or game state before the game starts.
	 * Awake is always called before any Start functions.
	 * This allows you to order initialization of scripts.
	 * Awake can not act as a coroutine.
	 * Use Awake instead of the constructor for initialization, 
	 * as the serialized state of the component is undefined at construction time.
	 * Awake is called once, just like the constructor.
	*/
	private void Awake() {

		// target 30 fps on mobile (setting framerate is ignored in Editor)
		#if UNITY_IPHONE
			Application.targetFrameRate = 30;
			TARGET_FPS = 30;
		#endif

		#if UNITY_ANDROID
			Application.targetFrameRate = 30;
			TARGET_FPS = 30;
		#endif
	}

	private void Start() {
		
		instance = this;

		bool isIPad = SystemInfo.deviceModel.Contains("iPad");
		bool shouldSupportPortraitUpsideDown = isIPad; //only support portrait upside-down on iPad
		
		FutileParams fparams = new FutileParams(true,true,true,shouldSupportPortraitUpsideDown);
		
		fparams.AddResolutionLevel(480.0f,	1.0f,	1.0f,	"_Scale1"); //iPhone
		fparams.AddResolutionLevel(960.0f,	2.0f,	2.0f,	"_Scale2"); //iPhone 4 retina
		fparams.AddResolutionLevel(1024.0f,	2.0f,	2.0f,	"_Scale2"); //iPad and iPad Mini
		fparams.AddResolutionLevel(1136.0f,	2.0f,	2.0f,	"_Scale2"); //iPhone 5 retina
		fparams.AddResolutionLevel(1280.0f,	2.0f,	2.0f,	"_Scale2"); //Nexus 7
		fparams.AddResolutionLevel(2048.0f,	4.0f,	4.0f,	"_Scale4"); //iPad Retina
		
		fparams.origin = new Vector2(0.5f,0.5f);
		
		Futile.instance.Init (fparams);
		
		Futile.atlasManager.LoadAtlas("Atlases/Atlas");
		Futile.atlasManager.LoadFont("Franchise","FranchiseFont" + Futile.resourceSuffix, "Atlases/FranchiseFont" + Futile.resourceSuffix, -2.0f,-5.0f);
		
		stage = Futile.stage;

		showMenu();

		stats = new Stats();
		stage.AddChild( stats );
	}

	public void showMenu() {
		purgeAll();
		menu = new Menu();
		stage.AddChild( menu );
		if ( stats != null ) stage.AddChild( stats );
	}

	public void showBunnyMark() {
		purgeAll();
		bunny = new BunnyMark();
		stage.AddChild( bunny );
		if ( stats != null ) stage.AddChild( stats );
	}

	public void showBenchMark() {
		purgeAll();
		bench = new BenchMark();
		stage.AddChild( bench );
		if ( stats != null ) stage.AddChild( stats );
	}

	public void showPirateMark() {
		purgeAll();
		pirate = new PirateMark();
		stage.AddChild( pirate );
		if ( stats != null ) stage.AddChild( stats );
	}

	private void purgeAll() {

		if ( menu != null ) {
			stage.RemoveChild( menu );
			menu = null;
		}

		if ( bunny != null ) {
			stage.RemoveChild( bunny );
			bunny = null;
		}

		if ( bench != null ) {
			stage.RemoveChild( bench );
			bench = null;
		}

		if ( pirate != null ) {
			stage.RemoveChild( pirate );
			pirate = null;
		}
	}

	/**
	 * Sent to all game objects when the player pauses.
	 */
	private void OnApplicationPause() {
		//Debug.Log( "OnApplicationPause" );
	}

	/**
	 * Sent to all game objects when the player gets or looses focus.
	 */
	private void OnApplicationFocus() {
		//Debug.Log( "OnApplicationFocus" );
	}

	/**
	 * Sent to all game objects before the application is quit.
	 */
	private void OnApplicationQuit() {
		//Debug.Log( "OnApplicationQuit" );
	}

	/**
	 * This function is called when the object becomes enabled and active.
	 */
	private void OnEnable() {
		//Debug.Log( "OnEnable" );
	}

	/**
	 * This function is called when the behaviour becomes disabled () or inactive.
	 */
	private void OnDisable() {
		//Debug.Log( "OnDisable" );
	}

	/**
	 * This function is called when the MonoBehaviour will be destroyed.
	 */
	private void OnDestroy() {
		//Debug.Log( "OnDestroy" );
	}
}