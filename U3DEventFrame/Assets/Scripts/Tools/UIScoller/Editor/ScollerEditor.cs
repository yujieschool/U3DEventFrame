using UnityEngine;
using System.Collections;

using UnityEditor;


[CustomEditor(typeof(ScollerBuild))]
public class ScollerEditor : Editor
{
    ScollerBuild builder;
    // Use this for initialization
    void Start () {

        builder = (ScollerBuild)target;

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void OnInspectorGUI()
    {
        if (builder != null && !Application.isPlaying)
        {

            if (builder.ScollerDirect != null)
            builder.ScollerDirect = (ScollerDirect)EditorGUILayout.EnumPopup("Direct", builder.ScollerDirect);


            if (builder.GenMoveIterm != null)
            builder.GenMoveIterm = EditorGUILayout.Toggle("GenMoveIterm", builder.GenMoveIterm);



            if (GUI.changed)
            {
                EditorUtility.SetDirty(builder);
            }
        }
        else
        {
            builder = (ScollerBuild)target;
        }

    }
}
