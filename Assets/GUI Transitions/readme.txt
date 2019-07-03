Instructions for use.
Created By: Aaron Johnson
Date:		12-12-2013 updated 06-02-2014
email:		johnso4f@students.rowan.edu
website:	sustaincity.rowan.edu
Note: These instructions assume some familiarity with GUILayout in Unity.

The file guiExtras.cs contains the "GUIArea" class, this is what adds the transition functionality. In order to create a new variable of type GUIArea you simply need the have the "guiExtras.cs" file in your assets directory.

Take the following example

//create a Rect which is in the center of the screen and takes up half of the width and half of the height.
Rect areaRect = new Rect(Screen.width/4,Screen.height/4,Screen.width/2,Screen.height/2);

void OnGUI(){
	//initialize the layout area
	GUILayout.BeginArea(areaRect);
	//Format the area vertically and use the same style as the GUI skin box
	GUILayout.BeginVertical("box");
	//use the FlexibleSpace to evenly space the content
	GUILayout.FlexibleSpace();
	GUILayout.Label("Test Area");//test label
	GUILayout.FlexibleSpace();
	if(GUILayout.Button("Test Button")){//test button
		Debug.Log("Click Button");
	}
	GUILayout.FlexibleSpace();
	GUILayout.EndVertical();
	
	GUILayout.EndArea();
}

The previous example could also be written as, which allows for a function call, and can clean up the code in the OnGUI call, if you are performing other operations.

//create a Rect which is in the center of the screen and takes up half of the width and half of the height.
Rect areaRect = new Rect(Screen.width/4,Screen.height/4,Screen.width/2,Screen.height/2);

void OnGUI(){
	testGUIFunction();
}

void testGUIFunction(){
	//initialize the layout area
	GUILayout.BeginArea(areaRect);
	//Format the area vertically and use the same style as the GUI skin box
	GUILayout.BeginVertical("box");
	//use the FlexibleSpace to evenly space the content
	GUILayout.FlexibleSpace();
	GUILayout.Label("Test Area");//test label
	GUILayout.FlexibleSpace();
	if(GUILayout.Button("Test Button")){//test button
		Debug.Log("Click Button");
	}
	GUILayout.FlexibleSpace();
	GUILayout.EndVertical();
	
	GUILayout.EndArea();
}

To get the same result with the GUIArea class you would could..


GUIArea testArea; //initialize new GUIArea
//create a Rect which is in the center of the screen and takes up half of the width and half of the height.
Rect areaRect = new Rect(Screen.width/4,Screen.height/4,Screen.width/2,Screen.height/2);

void Start(){
	testArea = new GUIArea(testGUIFunction,areaRect);
}

void OnGUI(){
	testArea.displayGUI();
}

void testGUIFunction(){
	//Format the area vertically and use the same style as the GUI skin box
	GUILayout.BeginVertical("box");
	//use the FlexibleSpace to evenly space the content
	GUILayout.FlexibleSpace();
	GUILayout.Label("Test Area");//test label
	GUILayout.FlexibleSpace();
	if(GUILayout.Button("Test Button")){//test button
		Debug.Log("Click Button");
	}
	GUILayout.FlexibleSpace();
	GUILayout.EndVertical();
}

This would not be all that useful, as it produces the same result with about the same amount of effort. However, we can start simplifying things.

Instead of defining the area as "testArea = new GUIArea(testGUIFunction,areaRect);"  we could use "testArea = new GUIArea(testGUIFunction,new Vector2(0.5f,0.5f));" to automatically create a rect at the center of the screen with the width and height take up 50% of the screen size.  Because the size of the width and height are the same relative to the total size of the screen we could also use "testArea = new GUIArea(testGUIFunction,0.5f);".  To change the orientation of the created Rect simply add a third parameter "alignment", Which can be any of the following: "center","top","bottom","left","right","upperRight","upperLeft","lowerRight","lowerLeft";

This functionality can be used any time you would like to create a rect, even when not using it with a GUIArea, by calling the function "GUIArea.createRect()" which will return a Rect base on the inputs of (Vector2 size),(float size),(Vector2/float size,string alignment) to create the rect;

Now our code would look like this...

GUIArea testArea; //initialize new GUIArea

void Start(){
	testArea = new GUIArea(testGUIFunction,0.5f);
}

void OnGUI(){
	testArea.displayGUI();
}

void testGUIFunction(){
	//Format the area vertically and use the same style as the GUI skin box
	GUILayout.BeginVertical("box");
	//use the FlexibleSpace to evenly space the content
	GUILayout.FlexibleSpace();
	GUILayout.Label("Test Area");//test label
	GUILayout.FlexibleSpace();
	if(GUILayout.Button("Test Button")){//test button
		Debug.Log("Click Button");
	}
	GUILayout.FlexibleSpace();
	GUILayout.EndVertical();
}

Now we can call transitions on the testArea.  There are several built in transitions used for adding or removing the area from the screen.  These include...
exitLeft(float transitionTime);
exitRight(...
exitTop(...
exitBottom(...

enterLeft(...
enterRight(...
enterTop(...
enterBottom(...

enterExpand(...
exitShrink(...

exitBatman(... //combination of spinning and scaling
enterBatman(...

all of these transitions can be called like functions on the GUIArea.
"testArea.exitLeft(1f);" is an example.

You can test these by replacing the line "Debug.Log("Click Button");" in our code with "testArea.exitBatman(1f);"
Note: this means that the area will no longer be displayed at the end of the transition, so add some way to return the area, by calling "testArea.enterBatman(1f);".

If you would like the transition to happen as soon as the object is displayed simply call the enter transition function when the object is created, like...

void Start(){
	testArea = new GUIArea(testGUIFunction,0.5f);
	testArea.enterLeft(1f);
}

All of the previous enter and exit transition functions were created using the "changeRect(Rect newRect,float transitionTime);","offsetRect(Rect offset,float transitionTime);","spin(float degree,float transitionTime);" and/or "scale(Vector2 newScale,float transitionTime);" functions;  These can be called on the GUIArea just like the enter and exit functions, and give you more control on the end result of the transition.

If at any point you would like to check if the area is performing a transition simply call the "inTransitions();" function, to return a boolean.

Please feel free to use and modify the code however you see fit for your own use.

Please contact me if you are using the code for professional product or have suggestions for it, Thank You.

Updated 06-02-2014
Transition timing functions have been added, previously only linear transitions were supported, now linear,exponential,square root,slow fast slow, and fast slow fast are supported, and accessible in that order by using the function setTimeTransitionFunction(int type) where the type variable is an int from 0-4.
Example:

void Start(){
	testArea = new GUIArea(testGUIFunction,areaRect);
	testArea.setTimeTransitionFunction(1); //sets an transition function of speed exponential
}