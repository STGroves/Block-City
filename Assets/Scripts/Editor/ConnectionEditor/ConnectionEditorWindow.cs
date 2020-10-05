using UnityEditor;
using UnityEngine;

public class ConnectionEditorWindow : EditorWindow
{
  bool top;
  bool left;
  bool right;
  bool bottom;

  Node topNode;
  Node leftNode;
  Node rightNode;
  Node bottomNode;

  const int CENTER_SIZE = 200;
  const float CENTER_BUTTON_PADDING = 25f;
  const float CENTER_NODE_PADDING = 5f;

  PropertyWindow propWin;

  static string dependencyGUID;

  [MenuItem("Window/Project/Connection Editor Window")]
  static void Init()
  {
    // Get existing open window or if none, make a new one
    ConnectionEditorWindow window = (ConnectionEditorWindow)GetWindow(typeof(ConnectionEditorWindow));
    window.Show();
  }

  [MenuItem("Window/Project/Clear Dependencies")]
  static void ClearDependencies()
  {
    DependencyObject.PurgeDependencies();
    dependencyGUID = null;
  }

  protected void OnDestroy()
  {
    DependencyObject.RemoveCurrentDependencyInstance(false);
  }

  protected void OnDisable()
  {
    DependencyObject.RemoveCurrentDependencyInstance(false);
  }

  protected void OnEnable()
  {
    minSize = Vector2.one * 50;

    if (string.IsNullOrEmpty(dependencyGUID))
      dependencyGUID = GUID.Generate().ToString();

    DependencyObject.EnforceDependencyInstance(dependencyGUID, true);

    propWin = new ConnectionEditorPropertyWindow(new Rect(0, 0, 300, 500), "Connections Properties");
  }

  private void RemoveNode(Node node)
  {
    if (node == topNode)
      top = !top;
    else if (node == leftNode)
      left = !left;
    else if (node == rightNode)
      right = !right;
    else if (node == bottomNode)
      bottom = !bottom;

    node = null;
  }

  private void OnGUI()
  {
    if (propWin == null)
      OnEnable();

    Vector2 center = (position.size / 2);
    Vector2 size = Vector2.one * CENTER_SIZE;

    propWin.Draw();

    EditorGUI.DrawRect(new Rect(center - (size / 2), size), Color.grey);

    if (!top)
    {
      if (GUI.Button(
                      new Rect(
                                center - (Vector2.up * ((CENTER_SIZE * 0.5f) + CENTER_BUTTON_PADDING + Node.CLOSE_BUTTON_SIZE)) +
                                (Vector2.left * Node.CLOSE_BUTTON_SIZE * 0.5f), Vector2.one * Node.CLOSE_BUTTON_SIZE
                              ),
                      "+"
                    ))
        top = !top;
    }
    else
    {
      if (topNode == null)
        topNode = new ConnectionNode(
                            new Rect(
                                      center - (Vector2.up * ((CENTER_SIZE * 0.5f) + Node.NODE_MIN_HEIGHT + CENTER_NODE_PADDING)) +
                                      (Vector2.left * ((CENTER_SIZE * 0.5f) + Node.NODE_MARGIN)),
                                      new Vector2(CENTER_SIZE + (Node.NODE_MARGIN * 2), Node.NODE_MIN_HEIGHT)
                                    ),
                            0,
                            RemoveNode
                          );

      topNode.Draw();
    }

    if (!bottom)
    {
      if (GUI.Button(
                      new Rect(
                                center - (Vector2.down * ((CENTER_SIZE * 0.5f) + CENTER_BUTTON_PADDING)) +
                                (Vector2.left * (Node.CLOSE_BUTTON_SIZE * 0.5f)), Vector2.one * Node.CLOSE_BUTTON_SIZE
                              ),
                      "+"
                    ))
        bottom = !bottom;
    }

    else
    {
      if (bottomNode == null)
        bottomNode = new Node(
                               new Rect(
                                         center - (Vector2.down * ((CENTER_SIZE * 0.5f) + CENTER_NODE_PADDING)) +
                                         (Vector2.left * ((CENTER_SIZE * 0.5f) + Node.NODE_MARGIN)),
                                         new Vector2(CENTER_SIZE + (Node.NODE_MARGIN * 2), Node.NODE_MIN_HEIGHT)
                                       ),
                               180,
                               RemoveNode
                             );

      bottomNode.Draw();
    }

    if (!left)
    {
      if (GUI.Button(
                      new Rect(
                                center + (Vector2.left * ((CENTER_SIZE * 0.5f) + CENTER_BUTTON_PADDING + Node.CLOSE_BUTTON_SIZE)) -
                                (Vector2.up * (Node.CLOSE_BUTTON_SIZE * 0.5f)), Vector2.one * Node.CLOSE_BUTTON_SIZE
                              ),
                      "+"
                    ))
        left = !left;
    }
    else
    {
      if (leftNode == null)
        leftNode = new Node(
                             new Rect(
                                       center + (Vector2.left * (CENTER_SIZE + Node.NODE_MARGIN + (Node.NODE_MIN_HEIGHT * 0.5f) + CENTER_NODE_PADDING)) -
                                       (Vector2.up * (Node.NODE_MIN_HEIGHT * 0.5f)),
                                       new Vector2(CENTER_SIZE + (Node.NODE_MARGIN * 2), Node.NODE_MIN_HEIGHT)
                                     ),
                             270,
                             RemoveNode
                           );

      leftNode.Draw();
    }

    if (!right)
    {
      if (GUI.Button(
                      new Rect(
                                center + (Vector2.right * ((CENTER_SIZE * 0.5f) + CENTER_BUTTON_PADDING)) -
                                (Vector2.up * (Node.CLOSE_BUTTON_SIZE * 0.5f)), Vector2.one * Node.CLOSE_BUTTON_SIZE
                              ),
                      "+"
                    ))
        right = !right;
    }
    else
    {
      if (rightNode == null)
        rightNode = new Node(
                             new Rect(
                                       center + (Vector2.right * (Node.NODE_MIN_HEIGHT * 0.5f)) -
                                       (Vector2.up * (Node.NODE_MIN_HEIGHT * 0.5f)),
                                       new Vector2(CENTER_SIZE + (Node.NODE_MARGIN * 2), Node.NODE_MIN_HEIGHT)
                                     ),
                             90,
                             RemoveNode
                           );

      rightNode.Draw();
    }
  }
}