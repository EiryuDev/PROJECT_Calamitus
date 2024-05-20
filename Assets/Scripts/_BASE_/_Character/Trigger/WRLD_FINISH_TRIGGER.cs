using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nutbusterz.Calamitus
{
    public class WRLD_FINISH_TRIGGER : MonoBehaviour
    {
        public bool playerReached = false;
        public float timer;
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                playerReached = true;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(playerReached)
            {
                timer += Time.deltaTime;

                if(timer >= 10)
                {
                    SceneManager.LoadSceneAsync(0);
                    playerReached = false;
                }
            }
        }
    }
}
