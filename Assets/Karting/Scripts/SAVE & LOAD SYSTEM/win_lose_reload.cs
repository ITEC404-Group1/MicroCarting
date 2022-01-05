using UnityEngine;
using UnityEngine.SceneManagement;

namespace KartGame.UI
{
    public class win_lose_reload : MonoBehaviour
    {
        [Tooltip("What is the name of the scene we want to load when clicking the button?")]
        public string SceneName;

        public void LoadTargetScene() 
        {
            SceneManager.LoadSceneAsync(SceneName);
        }
        void Update()
        {
            player.instance.loadPlayer();
            SceneName = player.instance.mapName;
            player.instance.savePlayer();
        }
    }
}

