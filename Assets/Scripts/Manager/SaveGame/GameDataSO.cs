using UnityEngine;

namespace Manager.SaveGame
{
    [CreateAssetMenu(fileName = "GameDataSO", menuName = "Data Object/GameDataSO")]
    public class GameDataSO : ScriptableObject
    {
        public GameData gameData;
    }
}