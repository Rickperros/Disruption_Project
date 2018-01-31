using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor//OBSOLETE!!! PROTO Version
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameManager l_gameManager = (GameManager)target;

        if(GUILayout.Button("Set Player Bullet Pool"))
        {
            l_gameManager.SetPlayerBulletsPool();
            EditorUtility.SetDirty(l_gameManager);
        }
    }
}
