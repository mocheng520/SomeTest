using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public float timebetweenWaves = 2f;


    private float countdown = 2f;
    private int wavenumber = 1 ;

    void  Update(){
        
        if(countdown <= 0f)
        {
            StartCoroutine(Spawnwaves());

            countdown = timebetweenWaves;
        }
        countdown -= Time.deltaTime;
    

    }
    IEnumerator Spawnwaves()
    {  
        wavenumber++;
        for(int i=0;i<wavenumber;i++)
        {
             Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

            yield return new WaitForSeconds(0.5f);
        }     
   }


}
