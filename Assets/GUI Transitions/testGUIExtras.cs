using UnityEngine;
using System.Collections;

public class testGUIExtras : MonoBehaviour {

	GUIArea hat,topHat,batman,bottomBar,topBar,altTop;
	
	string[] transitionNames = {"linear","exponential","square root","slow fast slow","fast slow fast"};
	int selectedTransition = 0;
	float transitionSpeed = 0.25f;
	
	// Use this for initialization
	void Start () {
		hat = new GUIArea(hatFunction,new Vector2(0.25f,1f),"left");
		topHat = new GUIArea(topHatFunction,new Vector2(0.25f,1f),"right");
		batman = new GUIArea(batmanFunction,0.25f);
		bottomBar = new GUIArea(bottomFunction,new Vector2(0.5f,0.25f),"bottom");
		topBar = new GUIArea(topFunction,new Vector2(0.5f,0.25f),"top");
		altTop = new GUIArea(altTopFunction,new Vector2(0.5f,0.25f),"top");
		hat.enterLeft(0.25f);
		topHat.exitRight(0.25f);
		batman.enterSpinScale(1f);
		topBar.enterTop(0.25f);
		altTop.exitTop(0.25f);
	}
	
	// Update is called once per frame
	void OnGUI(){
		hat.displayGUI();
		topHat.displayGUI();
		batman.displayGUI();
		bottomBar.displayGUI();
		topBar.displayGUI();
		altTop.displayGUI();
	}
	
	void hatFunction(){
		GUILayout.BeginVertical("box");
		if(GUILayout.Button("exit left")){
			hat.exitLeft(transitionSpeed);
			topHat.enterRight(transitionSpeed);
		}
		GUILayout.FlexibleSpace();
		transitionMenu();
		GUILayout.FlexibleSpace();
		GUILayout.Label("Left Bar");
		GUILayout.EndVertical();
	}

	void topHatFunction(){
		GUILayout.BeginVertical("box");
		if(GUILayout.Button("exit right")){
			hat.enterLeft(transitionSpeed);
			topHat.exitRight(transitionSpeed);
		}
		GUILayout.FlexibleSpace();
		transitionMenu();
		GUILayout.FlexibleSpace();
		GUILayout.Label("Right Bar");
		GUILayout.EndVertical();
	}
	
	void transitionMenu(){
		GUILayout.Label("Transition Speed: "+transitionSpeed.ToString());
		transitionSpeed = GUILayout.HorizontalSlider(transitionSpeed,0.001f,10f);
		selectedTransition = GUILayout.SelectionGrid(selectedTransition,transitionNames,1);
		hat.setTimeTransitionFunction(selectedTransition);
		topHat.setTimeTransitionFunction(selectedTransition);
	}
	
	void batmanFunction(){
		GUILayout.BeginVertical("box");
		GUILayout.Label("Spin Me");
		GUILayout.FlexibleSpace();
		if(GUILayout.Button("Exit Shrink Fade")){
			batman.exitShrinkFade(0.25f);
		}
		if(GUILayout.Button("Exit Spin Scale")){
			batman.exitSpinScale(1f);
		}
		GUILayout.EndVertical();
	}
	
	void bottomFunction(){
		GUILayout.BeginVertical("box");
		
		GUILayout.Label("Bottom Bar");
		
		GUILayout.BeginHorizontal("box");
		GUILayout.FlexibleSpace();
		if(GUILayout.Button("Enter Spin Scale")){
			batman.enterSpinScale(1f);
		}
		GUILayout.FlexibleSpace();
		if(GUILayout.Button("Enter Expand Fade")){
			batman.enterExpandFade(0.25f);
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal("box");
		GUILayout.FlexibleSpace();
		if(GUILayout.Button("Sides In")){
			hat.enterLeft(0.25f);
			topHat.enterRight(0.25f);
		}
		GUILayout.FlexibleSpace();
		if(GUILayout.Button("Sides Output")){
			hat.exitLeft(0.25f);
			topHat.exitRight(0.25f);
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal("box");
		GUILayout.FlexibleSpace();
		if(GUILayout.Button("small")){
			bottomBar.scale(new Vector2(0.5f,0.5f),1f);
		}
		GUILayout.FlexibleSpace();
		if(GUILayout.Button("normal")){
			bottomBar.scale(Vector2.one,1f);
			// or bottomBar.returnScaleRot(1f); to reset the scale and rotation
		}
		GUILayout.FlexibleSpace();
		if(GUILayout.Button("large")){
			bottomBar.scale(new Vector2(1.5f,1.5f),1f);
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal("box");
		GUILayout.FlexibleSpace();
		
		if(GUILayout.Button("Alpha 0")){
			batman.changeAlpha(0f,0.25f);
		}
		GUILayout.FlexibleSpace();
		if(GUILayout.Button("Alpha 0.5")){
			batman.changeAlpha(0.5f,0.25f);
		}
		GUILayout.FlexibleSpace();
		if(GUILayout.Button("Alpha 1")){
			batman.changeAlpha(1f,0.25f);
		}
		
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		
		GUILayout.FlexibleSpace();
		GUILayout.EndVertical();
	}
	
	void topFunction(){
		GUI.backgroundColor = Color.red;
		GUILayout.BeginVertical("box");
		GUILayout.Label("Top Bar");
		GUILayout.FlexibleSpace();
		if(GUILayout.Button("Fade Switch")){
			altTop.resetRect();
			GUIArea.fadeOutIn(0.25f,ref topBar,ref altTop);
		}
		GUILayout.FlexibleSpace();
		if(GUILayout.Button("Slide Up Switch")){
			altTop.setAlpha(1f);
			GUIArea.slideUpOutIn(0.25f,ref topBar,ref altTop);
		}
		GUILayout.FlexibleSpace();
		if(GUILayout.Button("Slide Up Switch")){
			altTop.setAlpha(1f);
			GUIArea.slideTopBottom(0.25f,ref topBar,ref altTop);
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndVertical();
		GUI.backgroundColor = Color.white;
	}
	
	void altTopFunction(){
		GUI.backgroundColor = Color.green;
		GUILayout.BeginVertical("box");
		GUILayout.Label("Alternate Top Bar");
		GUILayout.FlexibleSpace();
		if(GUILayout.Button("Fade Switch")){
			topBar.resetRect();
			GUIArea.fadeOutIn(0.25f,ref altTop,ref topBar);
		}
		GUILayout.FlexibleSpace();
		if(GUILayout.Button("Slide Up Switch")){
			topBar.setAlpha(1f);
			GUIArea.slideUpOutIn(0.25f,ref altTop,ref topBar);
		}
		GUILayout.FlexibleSpace();
		if(GUILayout.Button("Slide Up Rotate")){
			topBar.setAlpha(1f);
			GUIArea.slideTopBottom(0.25f,ref altTop,ref topBar);
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndVertical();
		GUI.backgroundColor = Color.white;
	}
}
