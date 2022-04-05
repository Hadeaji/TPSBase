using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    private StarterAssetsInputs starterAssetsInputs;
     
    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }
    private void Update()
    {
        if (starterAssetsInputs.jump)
        {
            StartGame();
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(0);
    }
}
