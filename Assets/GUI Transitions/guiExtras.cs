/*
*	Created By: Aaron Johnson
*	Date:		12-12-2013
*/
using UnityEngine;
using System.Collections.Generic;

public class guiExtras : MonoBehaviour {
	
}

public class GUIArea{
	//public variables
	public System.Action function;
	public Rect myRect;
	public bool enable;
	//transition variables
	protected Rect wantRect,currentRect,originalRect;
	protected float transitionTime,startTransitionTime;
	protected bool inTrans = false,rexTrans = false,matTrans = false,rotTrans = false,scaleTrans = false,alphaTrans = false,noRect = false;
	protected float rat = 0f;
	protected Matrix4x4 saveMat = Matrix4x4.identity,wantMat = Matrix4x4.identity,currentMat = Matrix4x4.identity,myMat = Matrix4x4.identity;
	protected float myAngle = 0f,currentAngle = 0f,wantAngle = 0f;
	protected Vector2 myScale = Vector2.one,currentScale = Vector2.one,wantScale = Vector2.one;
	protected float wantAlpha = 1f;
	protected float myAlpha = 1f;
	protected float currentAlpha = 1f;
	protected Color tempColor,savedColor;
	//timing function variables
	protected int ttf = 0; //the timing function selection
	//cubic bezier curves
	protected Vector4 slowFastSlow = new Vector4(0.9f,0.01f,0.07f,1);
	protected Vector4 fastSlowFast = new Vector4(0,0.97f,1,0.3f);
	protected Vector4 customCurve = new Vector4(0,2,1,-1);
	
	//Constructor functions
	public GUIArea(Rect r){myRect = r;	currentRect = r;originalRect=myRect;}
	public GUIArea(Vector2 size):this(createRect(size)){}
	public GUIArea(Vector2 size,string alignment):this(createRect(size,alignment)){}
	public GUIArea(System.Action f){function = f;enable=true;noRect = true;}
	public GUIArea(System.Action f,Rect r):this(r){function = f; enable = true;}
	public GUIArea(System.Action f,Vector2 size):this(f,createRect(size)){}
	public GUIArea(System.Action f,float size):this(f,createRect(size)){}
	public GUIArea(System.Action f,float size,string alignment):this(f,createRect(size,alignment)){}
	public GUIArea(System.Action f,Vector2 size,string alignment):this(f,createRect(size,alignment)){}
	public GUIArea(System.Action f,Rect r,bool e):this(f,r){enable = e;}
	public GUIArea():this(new Rect(0,0,Screen.width,Screen.height)){}
	
	//set functions
	public void setFunction(System.Action f){function=f;}
	public void setFunction(System.Action f,bool e){function=f;enable=e;}
	public void setRect(Rect r){myRect = r;currentRect=myRect;originalRect=myRect;noRect = false;}
	public void setRect(float size){setRect(createRect(size));}
	public void setRect(float size,string alignment){setRect(createRect(size,alignment));}
	public void setRect(Vector2 size,string alignment){setRect(createRect(size,alignment));}
	public void setRect(Vector2 v){myRect.x=v.x;myRect.y=v.y;currentRect=myRect;originalRect=myRect;noRect = false;}
	public void setRect(Vector3 v){myRect.x=v.x;myRect.y=v.y;currentRect=myRect;originalRect=myRect;noRect = false;}
	public void setTimeTransitionFunction(int tf){ttf=tf;} //linear,exponential,square root,slow fast slow, and fast slow fast
	
	
	public void disable(){enable = false;}
	public void disableRect(){noRect=true;}
	public void setEnable(bool b){enable = b;}
	public bool inTransition(){return inTrans;}
	
	//create a new rect
	public static Rect createRect(Vector2 size){return createRect(size,"center");}
	public static Rect createRect(float size){return createRect(new Vector2(size,size),"center");}
	public static Rect createRect(float size,string alignment){return createRect(new Vector2(size,size),alignment);}
	public static Rect createRect(Vector2 size,string alignment){
		Rect output = new Rect(0,0,size.x*Screen.width,size.y*Screen.height);
		alignment = alignment.ToLower();
		switch(alignment){
		case "center":
			output.y = (Screen.height-output.height)/2;
			output.x = (Screen.width-output.width)/2;
			break;
		case "left":
			output.y = (Screen.height-output.height)/2;
			break;
		case "right":
			output.y = (Screen.height-output.height)/2;
			output.x = Screen.width-output.width;
			break;
		case "top":
			output.x = (Screen.width-output.width)/2;
			break;
		case "bottom":
			output.y = Screen.height-output.height;
			output.x = (Screen.width-output.width)/2;
			break;
		case "upperright":
			output.x = Screen.width-output.width;
			break;
		case "upperleft":
			
			break;
		case "lowerright":
			output.x = Screen.width-output.width;
			output.y = Screen.height-output.height;
			break;
		case "lowerleft":
			output.y = Screen.height-output.height;
			break;
		default: //center
			Debug.LogWarning("Alignment '"+alignment+"' not recognized");
			output.y = (Screen.height-output.height)/2;
			output.x = (Screen.width-output.width)/2;
			break;
		}
		return output;
	}
	//create a rect that is next to another rect
	public static Rect createRect(Vector2 size,Rect other,string alignment){
		Rect output = createRect(size);
		return moveRectAround(output,other,alignment);
	}
	public static Rect createRect(float size,Rect other,string alignment){return createRect(new Vector2(size,size),other,alignment);}
	public static Rect createRect(float size,Rect other){return createRect(size,other,"bottom");}
	public static Rect createRect(Vector2 size,Rect other){return createRect(size,other,"bottom");}
	//move a rect next to another rect
	public static Rect moveRectAround(Rect toMove,Rect moveAround,string alignment){
		Rect output = toMove;
		alignment = alignment.ToLower();
		switch(alignment){
			case "top":
				output.y = moveAround.y-output.height;
				output.x = moveAround.x;
				break;
			case "bottom":
				output.y = moveAround.y+output.height;
				output.x = moveAround.x;
				break;
			case "left":
				output.x = moveAround.x-output.width;
				output.y = moveAround.y;
				break;
			case "right":
				output.x = moveAround.x+output.width;
				output.y = moveAround.y;
				break;
			case "topright":
				output.x = moveAround.x+output.width;
				output.y = moveAround.y-output.height;
				break;
			case "topleft":
				output.x = moveAround.x-output.width;
				output.y = moveAround.y-output.height;
				break;
			case "bottomright":
				output.x = moveAround.x+output.width;
				output.y = moveAround.y+output.height;
				break;
			case "bottomleft":
				output.x = moveAround.x-output.width;
				output.y = moveAround.y+output.height;
				break;
			default:
				Debug.LogWarning("The alignment '"+alignment+"' was not found, using 'bottom'");
				output.y = moveAround.y-output.height;
				break;
		}
		return output;
	}
	
	//calculate the transition variables
	protected bool setTransitionVariables(){
		rat = (Time.time-startTransitionTime)/transitionTime;
		if(rat >= 1){
			if(rexTrans){
				currentRect = wantRect;
				myRect = currentRect;
				rexTrans = false;
			}
			if(matTrans){
				currentMat = wantMat;
				myMat = currentMat;
				matTrans = false;
			}
			if(rotTrans){
				currentAngle = wantAngle;
				myAngle = currentAngle;
				rotTrans = false;
			}
			if(scaleTrans){
				currentScale = wantScale;
				myScale = currentScale;
				scaleTrans = false;
			}
			if(alphaTrans){
				currentAlpha = wantAlpha;
				myAlpha = currentAlpha;
				alphaTrans = false;
			}
			return false;
		}
		else{
			rat = transitionTimingFunction(rat);
			if(rexTrans)
				currentRect = recLerp(myRect,wantRect,rat);
			if(matTrans)
				currentMat = matLerp(myMat,wantMat,rat);
			if(rotTrans)
				currentAngle = Mathf.Lerp(myAngle,wantAngle,rat);
			if(scaleTrans)
				currentScale = Vector2.Lerp(myScale,wantScale,rat);
			if(alphaTrans)
				currentAlpha = Mathf.Lerp(myAlpha,wantAlpha,rat);
		}
		return true;
	}
	
	//return the adjusted progress value
	private float transitionTimingFunction(float t){
		switch(ttf){
			case 0: //linear or nothing
				return t;
			case 1: //exponential / slow to fast movement
				return Mathf.Pow(t,3);
			case 2: //square root / fast to slow movement
				return Mathf.Sqrt(t);
			case 3: // slow to fast to slow movement
				//requires if statement to calculate the cube root for negative numbers
				return (t<0.5)?-Mathf.Pow(-((t-0.5f)*2),1/3f)/2 +0.5f:Mathf.Pow(((t-0.5f)*2),1/3f)/2 +0.5f;
			case 4: // fast to slow to fast movement
				return Mathf.Pow(((t-0.5f)*2),3)/2 +0.5f;
			default: //linear or nothing
				return t;
		}
	}
	
	//function that is called inside of a GUI to display
	public virtual void displayGUI(){
		if(enable){
			saveMat = GUI.matrix;
			savedColor = GUI.color;
			if(inTrans)
				inTrans = setTransitionVariables();
			GUI.matrix = currentMat;
			tempColor = GUI.color;
			tempColor.a = currentAlpha;
			GUI.color = tempColor;
			GUIUtility.ScaleAroundPivot(currentScale,currentRect.center);
			GUIUtility.RotateAroundPivot(currentAngle,currentRect.center);
			if(!noRect)
				GUILayout.BeginArea(currentRect);
			GUI.enabled = !(inTrans || currentAlpha <= 0);
			function();
			if(!noRect)
				GUILayout.EndArea();
			GUI.color = savedColor;
			GUI.matrix = saveMat;
			GUI.enabled = true;
		}
	}
	
	//reset the gui rect
	public void resetRect(){
		currentRect = originalRect;
		myRect = originalRect;
		wantRect = originalRect;
	}
	
	//set the alpha value
	public void setAlpha(float alpha){
		wantAlpha = alpha;
		currentAlpha = wantAlpha;
		myAlpha = currentAlpha;
		alphaTrans = false;
	}
	//change the alpha over time
	public void changeAlpha(float newAlpha, float transTime){
		transitionTime = transTime;
		wantAlpha = newAlpha;
		startTransitionTime = Time.time;
		inTrans = true;
		alphaTrans = true;
	}
	//change the rect over time
	public void changeRect(Rect newRect,float transTime){
		transitionTime = transTime;
		wantRect = newRect;
		startTransitionTime = Time.time;
		inTrans = true;
		rexTrans = true;
	}
	//offset the rect over time
	public void offsetRect(Rect offRect,float transTime){
		changeRect(new Rect(currentRect.x+offRect.x,currentRect.y+offRect.y,currentRect.width+offRect.width,currentRect.height+offRect.height),transTime);
	}
	public static Rect addRects(Rect a,Rect b){
		return new Rect(a.x+b.x,a.y+b.y,a.width+b.width,a.height+b.height);
	}
	//transition functions
	public void enterFade(float transTime){
		setAlpha(0f);
		changeAlpha(1f,transTime);
	}
	
	public void exitFade(float transTime){changeAlpha(0f,transTime);}
	
	//exit
	public void exitLeft(float transTime){offsetRect(new Rect(-currentRect.width,0,0,0),transTime);}
	public void exitRight(float transTime){offsetRect(new Rect(Screen.width,0,0,0),transTime);}
	public void exitTop(float transTime){offsetRect(new Rect(0,-currentRect.height,0,0),transTime);}
	public void exitBottom(float transTime){offsetRect(new Rect(0,-Screen.height,0,0),transTime);}
	//enter
	public void enterLeft(float transTime){
		wantRect = originalRect;
		currentRect.x = -myRect.width;
		myRect = currentRect;
		changeRect(wantRect,transTime);
	}
	
	public void enterRight(float transTime){
		wantRect = originalRect;
		currentRect.x = Screen.width;
		myRect = currentRect;
		changeRect(wantRect,transTime);
	}
	
	public void enterTop(float transTime){
		wantRect = originalRect;
		currentRect.y = -myRect.height;
		myRect = currentRect;
		changeRect(wantRect,transTime);
	}
	
	public void enterBottom(float transTime){
		wantRect = originalRect;
		currentRect.y = Screen.height;
		myRect = currentRect;
		changeRect(wantRect,transTime);
	}
	
	//transitions using the area's GUI matrix
	public bool exitLeftMat(float transTime){return matrixTransition(Matrix4x4.TRS(new Vector3(-Screen.width,0,0),Quaternion.identity,Vector3.one),transTime);	}
	public bool exitRightMat(float transTime){return matrixTransition(Matrix4x4.TRS(new Vector3(Screen.width,0,0),Quaternion.identity,Vector3.one),transTime);}
	public bool exitTopMat(float transTime){return matrixTransition(Matrix4x4.TRS(new Vector3(0,-Screen.height,0),Quaternion.identity,Vector3.one),transTime);}
	public bool exitBottomMat(float transTime){return matrixTransition(Matrix4x4.TRS(new Vector3(0,Screen.height,0),Quaternion.identity,Vector3.one),transTime);}
	
	public bool returnFromExit(float transTime){return matrixTransition(Matrix4x4.identity,transTime);	}
	
	public bool returnScaleRot(float transTime){
		scale(Vector2.one,transTime);
		spin(0f,transTime);
		return true;
	}
	//change the matrix over time
	protected bool matrixTransition(Matrix4x4 newMat,float transTime){
		if(inTrans){
			Debug.LogWarning("Already Performing Transition");
			return false;
		}
		rat = 0f;
		enable = true;
		transitionTime = transTime;
		startTransitionTime = Time.time;
		inTrans = true;
		matTrans = true;
		wantMat = newMat;
		return true;
	}
	//spin the area around the center over time
	public bool spin(float deg,float transTime){
		transitionTime = transTime;
		rat = 0f;
		startTransitionTime = Time.time;
		inTrans = true;
		wantAngle = deg;
		rotTrans = true;
		return true;
	}
	//scale the area from center over time
	public bool scale(Vector2 scl,float transTime){
		transitionTime = transTime;
		rat = 0f;
		startTransitionTime = Time.time;
		inTrans = true;
		wantScale = scl;
		scaleTrans = true;
		return true;
	}
	
	//transitions
	public bool enterExpand(float transTime){
		currentScale = new Vector2(0.001f,0.001f);
		myScale = currentScale;
		return scale(Vector2.one,transTime);
	}
	
	public bool exitShrink(float transTime){
		return scale(new Vector2(0.001f,0.001f),transTime);
	}

	public bool enterExpandFade(float transTime){
		enterFade(transTime);
		return enterExpand(transTime);
	}
	
	public bool exitShrinkFade(float transTime){
		exitFade(transTime);
		return exitShrink(transTime);
	}
	
	public bool exitSpinScale(float transTime){
		scale(new Vector2(0.001f,0.001f),transTime);
		spin(360f*3,transTime);
		return true;
	}
	
	public bool enterSpinScale(float transTime){
		currentScale = new Vector2(0.001f,0.001f);
		myScale = currentScale;
		currentAngle = 360f*3;
		myAngle=currentAngle;
		returnScaleRot(transTime);
		return true;
	}
	//transition from one GUIArea to Another
	public static void fadeOutIn(float transTime,ref GUIArea exit,ref GUIArea enter){
		exit.exitFade(transTime);
		enter.enterFade(transTime);
	}
	
	public static void fadeOutIn(float transTime,GUIArea[] exit,GUIArea enter){
		foreach(GUIArea e in exit){
			e.exitFade(transTime);
		}
		enter.enterFade(transTime);
	}
	
	public static void slideUpOutIn(float transTime,ref GUIArea exit,ref GUIArea enter){
		exit.exitTop(transTime);
		enter.enterTop(transTime);
	}
	public static void slideDownOutIn(float transTime,ref GUIArea exit,ref GUIArea enter){
		exit.exitBottom(transTime);
		enter.enterBottom(transTime);
	}
	public static void slideRightOutIn(float transTime,ref GUIArea exit,ref GUIArea enter){
		exit.exitRight(transTime);
		enter.enterRight(transTime);
	}
	public static void slideLeftOutIn(float transTime,ref GUIArea exit,ref GUIArea enter){
		exit.exitLeft(transTime);
		enter.enterLeft(transTime);
	}
	public static void slideLeftRight(float transTime,ref GUIArea exit,ref GUIArea enter){
		exit.exitLeft(transTime);
		enter.enterRight(transTime);
	}
	public static void slideRightLeft(float transTime,ref GUIArea exit,ref GUIArea enter){
		exit.exitRight(transTime);
		enter.enterLeft(transTime);
	}
	public static void slideTopBottom(float transTime,ref GUIArea exit,ref GUIArea enter){
		exit.exitTop(transTime);
		enter.enterBottom(transTime);
	}
	public static void slideBottomTop(float transTime,ref GUIArea exit,ref GUIArea enter){
		exit.exitBottom(transTime);
		enter.enterTop(transTime);
	}
	// helper functions
	public static Rect recLerp(Rect from,Rect to,float t){
		Rect outRect = new Rect();
		outRect.x = Mathf.Lerp(from.x,to.x,t);
		outRect.y = Mathf.Lerp(from.y,to.y,t);
		outRect.width = Mathf.Lerp(from.width,to.width,t);
		outRect.height = Mathf.Lerp(from.height,to.height,t);
		return outRect;
	}
	
	public static Matrix4x4 matLerp(Matrix4x4 from,Matrix4x4 to,float t){
		Matrix4x4 outMat = new Matrix4x4();
		for (int i = 0; i < 16; i++){outMat[i] = Mathf.Lerp(from[i],to[i],t);}
		return outMat;
	}
}
