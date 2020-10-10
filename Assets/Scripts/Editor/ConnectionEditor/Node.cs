//using System;
//using UnityEditor;
//using UnityEngine;

//public class Node
//{
//  public const float NODE_MIN_HEIGHT = 60f;
//  public const float NODE_MIN_WIDTH = 60f;

//  public const float NODE_MARGIN = 7.5f;
//  public const float CLOSE_BUTTON_SIZE = 25f;

//  GUIStyle nodeStyle;

//  Rect nodeRect;
//  int nodeRotation;
//  Action<Node> nodeCloseAction;

//  public Node(Rect rect, int rotation, Action<Node> closeFunc)
//  {
//    SetupNode(rect, rotation, closeFunc);
//  }

//  public Node(Rect rect, Action<Node> closeFunc)
//  {
//    SetupNode(rect, 0, closeFunc);
//  }
  
//  public Node(Rect rect)
//  {
//    SetupNode(rect, 0, null);
//  }

//  void SetupNode(Rect rect, int rotation, Action<Node> closeFunc)
//  {
//    if (nodeStyle == null)
//    {
//      nodeStyle = new GUIStyle();
//      nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node0.png") as Texture2D;
//      nodeStyle.border = new RectOffset((int)CLOSE_BUTTON_SIZE, (int)CLOSE_BUTTON_SIZE, (int)CLOSE_BUTTON_SIZE, (int)CLOSE_BUTTON_SIZE);
//    }

//    rect.width = Mathf.Max(rect.width, NODE_MIN_WIDTH);
//    rect.height = Mathf.Max(rect.height, NODE_MIN_HEIGHT);

//    nodeRect = rect;

//    nodeCloseAction = closeFunc;
//    nodeRotation = rotation;
//  }

//  public void Draw()
//  {
//    if (nodeRotation != 0)
//      GUIUtility.RotateAroundPivot(nodeRotation, nodeRect.center);
    
//    GUI.Box(nodeRect, "", nodeStyle);

//    GUILayout.BeginArea(nodeRect);
//    GUILayout.BeginVertical();
//    DrawContents();
//    GUILayout.EndVertical();
//    GUILayout.EndArea();

//    if (nodeCloseAction != null)
//    {
//      GUI.backgroundColor = Color.red;

//      if (GUI.Button(new Rect(nodeRect.x + (nodeRect.width - CLOSE_BUTTON_SIZE), nodeRect.y, CLOSE_BUTTON_SIZE, CLOSE_BUTTON_SIZE), "X"))
//        nodeCloseAction.Invoke(this);

//      GUI.backgroundColor = Color.white;
//    }

//    if (nodeRotation != 0)
//      GUIUtility.RotateAroundPivot(-nodeRotation, nodeRect.center);
//  }

//  protected virtual void DrawContents() { }
//}
