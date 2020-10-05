using Sirenix.Utilities.Editor;
using System;
using System.Collections.Generic;
using UnityEditor;
using UE = UnityEngine;

public class DependencyObject
{
  protected static Dictionary<string, Dictionary<string, DependencyObject>> dependencyDict = new Dictionary<string, Dictionary<string, DependencyObject>>();
  protected static Dictionary<string, DependencyObject> currentDict;

  public static void PurgeDependencies()
  {
    if (currentDict != null)
      currentDict.Clear();
    
    dependencyDict.Clear();
  }

  public static void CreateDependencyInstance(string id, bool setAsCurrent)
  {
    if (dependencyDict.ContainsKey("id"))
      throw new ArgumentException(id + " already exists as a dependency instance!");

    Dictionary<string, DependencyObject> temp = new Dictionary<string, DependencyObject>();

    dependencyDict.Add(id, temp);

    if (setAsCurrent)
      currentDict = temp;
  }

  public static void EnforceDependencyInstance(string id, bool setAsCurrent)
  {
    if (dependencyDict.ContainsKey(id))
    {
      currentDict = dependencyDict[id];
      return;
    }

    Dictionary<string, DependencyObject> temp = new Dictionary<string, DependencyObject>();

    dependencyDict.Add(id, temp);

    if (setAsCurrent)
      currentDict = temp;
  }

  public static void SetDependencyInstance(string id)
  {
    if (!dependencyDict.ContainsKey(id))
      throw new ArgumentException(id + " does not exist as a dependency instance!");

    currentDict = dependencyDict[id];
  }

  public static void RemoveCurrentDependencyInstance(bool clearInstance)
  {
    if (clearInstance)
      currentDict.Clear();

    currentDict = null;
  }
}

public class DependencyObject<T> : DependencyObject
{
  public enum DependencyLinkType
  {
    SendOnly,
    ReceiveOnly,
    SendReceive
  }

  public T Value { get; private set; }
  UE.GUIContent name;
  DependencyLinkType linkType;

  private DependencyObject() { }

  public static DependencyObject<T> EnforceDependencyObject(string id, UE.GUIContent displayName, T defaultValue, DependencyLinkType dependencyType = DependencyLinkType.SendReceive)
  {
    if (currentDict == null)
      throw new NullReferenceException("There is no current dictionary set!");

    if (currentDict.ContainsKey(id))
      return (DependencyObject<T>)currentDict[id];

    DependencyObject<T> temp = new DependencyObject<T>
    {
      Value = defaultValue,
      name = displayName,
      linkType = dependencyType
    };

    currentDict.Add(id, temp);

    return temp;
  }

  public void SetValue(T newValue)
  {
    if (linkType != DependencyLinkType.ReceiveOnly)
      Value = newValue; 
  }

  public void Draw()
  {
    if (linkType == DependencyLinkType.SendOnly)
      throw new AccessViolationException("Cannot draw DependencyObject! Link Type is SendOnly!");

    bool cache = UE.GUI.enabled; 

    UE.Object obj = Value as UE.Object;

    if (linkType == DependencyLinkType.ReceiveOnly)
      UE.GUI.enabled = false;

    EditorGUI.BeginChangeCheck();
    T temp;

    if (!(bool)obj)
      temp = SirenixEditorGUI.DynamicPrimitiveField(name, Value);

    else
      temp = (T)SirenixEditorFields.PolymorphicObjectField(Value, typeof(T), false);

    if (EditorGUI.EndChangeCheck() && linkType != DependencyLinkType.ReceiveOnly)
      Value = temp;

    UE.GUI.enabled = cache;
  }
}