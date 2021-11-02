using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LigaGame.UI.Save
{
    public class SaveSystemView : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;

        private void Start()
        {
            Refresh();
        }

        private void SaveSystemUpdated()
        {
            _playerData = SaveSystem.PlayerData;
        }

        public void Refresh()
        {
            _playerData = SaveSystem.PlayerData;
        }

        public void Save()
        {
            SaveSystem.SavePlayer(_playerData);
        }
    }


    [CustomEditor(typeof(SaveSystemView))]
    public class ObjectBuilderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            SaveSystemView saveSystemView = (SaveSystemView)target;
            
            if(GUILayout.Button("Refresh"))
            {
                saveSystemView.Refresh();
            }

            if(GUILayout.Button("Save"))
            {
                saveSystemView.Save();
            }
        }
    }
}
