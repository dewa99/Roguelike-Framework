using UnityEngine;
using UnityEngine.SceneManagement;

namespace RogueLikeCardSystem
{
    public class SceneManager : MonoBehaviour
    {
        [SerializeField] private AdditiveSceneSO additiveScene;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            foreach (var scene in additiveScene.AdditiveScene)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(scene, LoadSceneMode.Additive);
            }
        }

    }
}
