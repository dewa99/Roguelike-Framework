using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RogueLikeCardSystem
{
    [CreateAssetMenu(fileName = "AdditiveSceneSO", menuName = "Scriptable Objects/AdditiveSceneSO")]
    public class AdditiveSceneSO : ScriptableObject
    {
        [Scene]
        public List<string> AdditiveScene;
    }
}
