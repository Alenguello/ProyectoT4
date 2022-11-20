using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{
    private void Update()
    {
        AllCoinCollected();
    }
    public void AllCoinCollected()
    {
        if (transform.childCount == 0)
        {
            Debug.Log("No quedan monedas");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
