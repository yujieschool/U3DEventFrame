using UnityEngine;
using System.Collections;

using System.Collections.Generic;


using UnityEditor;



public class MyWindow : EditorWindow
{

    [SerializeField]
    static  Object[] tagGameObject ;



    //   static List<string>  ObjName;

    // Add menu item named "My Window" to the Window menu

    [MenuItem("ITools/Tags/AddTags")]
    public static void ShowTag()
    {
        TagTools.AddTags();
    }


    [MenuItem("ITools/Tags/TagManager")]
    public static void ShowWindow()
    {


     

            tagGameObject = new Object[TagTools.tags.Length];


 


        for (int i = 0; i < TagTools.tags.Length; i++)
        {
            string  tmpName = PlayerPrefs.GetString(TagTools.tags[i]);

            if (tmpName != null)
            {

                Debug.Log("tmpName =="+ tmpName);
                  GameObject  tmpObj=   GameObject.Find(tmpName);

                tagGameObject[i] = tmpObj;
            }
        }
        
        //  tagGameObject = new Object[TagTools.tags.Length];
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(MyWindow));
    }


   // public Object source;


    void OnGUI()
    {


       EditorGUILayout.BeginVertical();


       
        //source = EditorGUILayout.ObjectField(source, typeof(Object), true);


        //if (GUILayout.Button( "AddObject"))
        //{


        //    if (source != null)
        //    {
        //        tagGameObject.Add(source);

               
        //    }
                
        //}

        for (int i = 0; i < TagTools.tags.Length; i++)
        {
            GUILayout.Label(TagTools.tags[i]);
            tagGameObject[i] = EditorGUILayout.ObjectField(tagGameObject[i], typeof(Object), true);

            if (GUILayout.Button("Save!"))
                if (tagGameObject[i] != null)
                {
                    PlayerPrefs.SetString(TagTools.tags[i], tagGameObject[i].name);

                    TagTools.AddTags();
                    ((GameObject)tagGameObject[i]).tag = TagTools.tags[i];
                }
                   
                else
                    if (Help.HasHelpForObject(tagGameObject[i]))
                    Help.ShowHelpForObject(tagGameObject[i]);
                else
                    Help.BrowseURL("http://forum.unity3d.com/search.php");


        }
        EditorGUILayout.EndVertical();

    }
}


public class TagTools  {

   public static string[] tags = { "TestOne","PlayerOne" };


   


   
    public static void AddTags()
    {

        for (int i = 0; i < tags.Length; i++)
        {
            Debug.Log("add tag =="+tags[i]);
            AddTag(tags[i]);
        }

        AssetDatabase.Refresh();

     
    }


    static bool isHasTag(string tag)
    {
        for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.tags.Length; i++)
        {

            if (UnityEditorInternal.InternalEditorUtility.tags[i].Equals(tag))
            {

                Debug.Log("is has  tags ==" + UnityEditorInternal.InternalEditorUtility.tags[i]);

                return true;
            }
               
        }

        Debug.Log("is has not tags ==" + tag);
        return false;
    }

    static bool isHasLayer(string layer)
    {
        for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.layers.Length; i++)
        {
            if (UnityEditorInternal.InternalEditorUtility.layers[i].Contains(layer))
                return true;
        }
        return false;
    }



    static void AddTag(string tag)
    {
        if (!isHasTag(tag))
        {
            Debug.Log("will  add tag name =="+tag);
            SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty it = tagManager.GetIterator();
            while (it.NextVisible(true))
            {
                if (it.name == "tags")
                {

                    int tmpInt = it.arraySize;
                    it.InsertArrayElementAtIndex(tmpInt);


                    SerializedProperty dataPoint = it.GetArrayElementAtIndex(tmpInt);

                    dataPoint.stringValue = tag;
                     tagManager.ApplyModifiedProperties();
                    //for (int i = 0; i < it.arraySize; i++)
                    //{
                    //    SerializedProperty dataPoint = it.GetArrayElementAtIndex(i);

                    //    Debug.Log("dataPoint.stringValue =="+ dataPoint.stringValue);
                    //    if (string.IsNullOrEmpty(dataPoint.stringValue))
                    //    {  
                    //        dataPoint.stringValue = tag;
                    //        tagManager.ApplyModifiedProperties();
                    //        return;
                    //    }
                    //}
                }
            }
        }
    }

    static void AddLayer(string layer)
    {
        if (!isHasLayer(layer))
        {
            SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty it = tagManager.GetIterator();
            while (it.NextVisible(true))
            {
                if (it.name.StartsWith("User Layer"))
                {
                    if (it.type == "string")
                    {
                        if (string.IsNullOrEmpty(it.stringValue))
                        {
                            it.stringValue = layer;
                            tagManager.ApplyModifiedProperties();
                            return;
                        }
                    }
                }
            }
        }
    }



}
