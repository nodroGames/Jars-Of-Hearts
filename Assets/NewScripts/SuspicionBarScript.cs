using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;

namespace Assets.NewScripts
{
    public class SuspicionBarScript : MonoBehaviour
    {
        public CinemachineVirtualCamera storeFrontCam;
        public CinemachineVirtualCamera kitchenCam;

        bool gameHasEnded = false;

        public float waitToRestart = 1f;

        Image suspicionBar;
        float maxSuspicion = 100f;
        public static float suspicion;
        private const float coef = 0.2f;
        private const float pos = 5f;
        // Start is called before the first frame update
        void Start()
        {
            suspicionBar = GetComponent<Image>();
            suspicion = 0f;
        }

        // Update is called once per frame
        private void Update()
        {
            suspicionBar.fillAmount = suspicion / maxSuspicion;

            if (CinemachineCore.Instance.IsLive(storeFrontCam) && suspicion > 0)
            {
                suspicion -= pos * Time.deltaTime;
            }
            else
            {
                suspicion += coef * Time.deltaTime;
            }

            if (suspicion >= 100)
            {
                EndGame();
            }
        }

        public void EndGame()
        {
            if (gameHasEnded == false)
            {
                gameHasEnded = true;
                Debug.Log("YOU GET NOTHING, YOU LOSE, GOOD DAY SIR!");
                Invoke("Restart", waitToRestart);
            }
        }

        void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        /*public void BarDown()
        {
            while (CinemachineCore.Instance.IsLive(storeFrontCam) && suspicion > 0)
            {
                suspicion -= pos;
            }
        }

        public void BarUp()
        {
            while (CinemachineCore.Instance.IsLive(kitchenCam))
            {
                suspicion += coef;
            }
        }*/
    }
}
