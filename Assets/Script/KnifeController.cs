using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : MonoBehaviour {

    public GameObject shavings;

    private float speed = 0.5f;
    private KnifeController kc;
    private bool shot = false;
	void Start () {
        kc = GetComponent<KnifeController>();
    }

	void Update () {
        if (shot)
        {
            transform.position += transform.up * speed;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        // при срабатывание тригера проверяется тег , если нож попал в бревно 
        // - нож становится дочерним объектом бревна
        // - создается система частиц
        // - уничтожается компонент у ножа ,что бы не выполнять лишний  update

        if (collision.tag == "Log")
        {
            transform.SetParent(collision.transform);

            GameObject particle =  Instantiate(shavings, transform.position + new Vector3(0, 1.2f), Quaternion.identity);
            Destroy(particle, 5f);
            
            speed = 0;
            collision.GetComponent<LogRotate>().CountKnife();
            Destroy(kc);
        }
        else if (collision.tag == "Knife")
        {
            // Удаление ножа и загрузка нового уровня

            Destroy(gameObject);
            LevelLoader levelLoader = new LevelLoader();
            levelLoader.LoadLevel(1);
        }
    }

    public void Shot()
    {
        shot = true;
    }


}
