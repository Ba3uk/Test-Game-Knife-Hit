using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogRotate : MonoBehaviour {

    [SerializeField] private float speed;
    public GameObject destrouParticle;
    private int maxKnife = 5;

    private int currentCountKnife = 0;
	void Update () {
        transform.Rotate(Vector3.forward, speed);
	}

    // определяет скороть вращения 
    public void SetSpeedRotate()
    {
         speed = Random.Range(2,speed); 
    }

    // считаем колличество ножей воткнутых в дерево
    public void CountKnife()
    {
        currentCountKnife++;
        if (currentCountKnife == maxKnife)
        {
            DestroyLog();
        }
        
    }

    // удаление бревна , и создание частиц
    public void DestroyLog()
    {
        GameObject particle = Instantiate(destrouParticle, transform.position, Quaternion.identity);
        Destroy(particle, 1f);
        GameConroller.logIsDestroy = true;
        Destroy(gameObject);
    }
}
