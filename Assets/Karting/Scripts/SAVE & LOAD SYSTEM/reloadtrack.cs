using UnityEngine;
using UnityEngine.SceneManagement;

namespace KartGame.UI
{
    public class reloadtrack : MonoBehaviour
    {
        [Tooltip("What is the name of the scene we want to load when clicking the button?")]
        public string SceneName;

        public void LoadTargetScene() 
        {
            SceneManager.LoadSceneAsync(SceneName);
        }
        void Update()
        {
            SceneName = CarController.instance.mapName;
            player.instance.loadPlayer();
            player.instance.mapName = SceneName;
            player.instance.savePlayer();
        }
    }
}