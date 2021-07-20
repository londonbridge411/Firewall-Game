using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BHPattern))]
public class PatternEditor : Editor
{
    // The function that makes the custom editor work
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("bullet"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("numberOfBullets"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("speed"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cooldownTime"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rotation"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("canTrack"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("hasNextPattern"));


        EditorGUILayout.PropertyField(serializedObject.FindProperty("type"));
        // Create a space to separate this enum popup from the other variables 
        EditorGUILayout.Space();

        // Check the value of the enum and display variables based on it

        // Save all changes made on the Inspector

        switch (serializedObject.FindProperty("type").enumValueIndex)
        {
            case 0:
                DisplayFull();
                break;

            case 1:
                DisplayHalf();
                break;
            case 2:
                DisplaySpiral();
                break;
            case 3:
                DisplayBurst();
                break;
        }

        if (serializedObject.FindProperty("hasNextPattern").boolValue)
            EditorGUILayout.PropertyField(serializedObject.FindProperty("nextPattern"));

        serializedObject.ApplyModifiedProperties();
    }



    private void DisplayFull()
    {

    }

    private void DisplayHalf()
    {
        /*// Store the hasMagic bool as a serializedProperty so we can access it
        SerializedProperty hasMagicProperty = serializedObject.FindProperty("hasMagic");

        // Draw a property for the hasMagic bool
        EditorGUILayout.PropertyField(hasMagicProperty);

        // Check if hasMagic is true
        if (hasMagicProperty.boolValue)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("mana"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("magicType"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("magicDamage"));
        }*/
    }

    private void DisplaySpiral()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("strength"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("inverse"));
    }

    private void DisplayBurst()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("strength"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("inverse"));
    }
}
