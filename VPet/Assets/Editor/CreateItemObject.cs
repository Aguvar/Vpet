using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateItemObject
{

    
    private static void CreateObject(ItemObject asset)
    {
        AssetDatabase.CreateAsset(asset, "Assets/ScriptableObjects/NewItemObject.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
    }

    [MenuItem("Assets/Create/ConsumableObject")]
    public static void CreateConsumableObject()
    {
        ItemObject asset = ScriptableObject.CreateInstance<Consumable>();
        CreateObject(asset);
    }

}
