using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToyGame
{
    [CreateAssetMenu(fileName = "Connection", menuName = "Scriptable Objects/Level Connection")]
    public class LevelConnection : ScriptableObject
    {
        public List<SceneConnection> connections;   
    }

    [Serializable]
    public struct SceneConnection
    {
        public SceneField scene;
        public Vector2 playerSpawnPoint;
    }
}
