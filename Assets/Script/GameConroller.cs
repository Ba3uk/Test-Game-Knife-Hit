using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConroller : MonoBehaviour {

    public static bool logIsDestroy = false;
    public Vector2 knifeSpawn;
    public Vector2 logSpawn;

    [SerializeField] private GameObject knifePrefab;
    [SerializeField] private GameObject logPrefab;

    private KnifeController knife;
    private LogRotate log;
    private void Start()
    {
        CreateNewLog();
        CreateNewKnife();
    }

    void Update() {
        //  при нажатии на левую кнопку мыши и на сцене есть нож и бревно
        // - кидаем нож
        if (Input.GetKeyDown(KeyCode.Mouse0) && knife != null && log != null)
        {
            KnifeShot();
        }

        // если бревно уничтожено - создаем новое
        if(logIsDestroy)
        {
            StartCoroutine(Creator());
            logIsDestroy = false;
        }
	}

    // Создает новое бревное, если бревно уже есть на сцене , выходим из метода
    // если нет - создаем новое бревно и задаем скорость вращения
   public void CreateNewLog()
    {
        Debug.Log(2);
        if (log != null)
            return;
        else
        {
            Debug.Log(1);
            log = Instantiate(logPrefab, logSpawn, Quaternion.identity).GetComponent<LogRotate>();
            log.SetSpeedRotate();
        }
    }

    // Создает новый нож, если нож уже есть на сцене , выходим из метода

    private void CreateNewKnife()
    {
        if (knife != null)
            return;
        else
        {
            knife = Instantiate(knifePrefab, knifeSpawn, Quaternion.identity).GetComponent<KnifeController>();
        }
    }

    // Обращается к ножу , который готов к выстрелу,
    // придает ему силу броска
    private void KnifeShot()
    {
        knife.Shot();
        knife = null;
        StartCoroutine(Reloader());
    }


    public IEnumerator Reloader()
    {
        yield return new WaitForSeconds(0.15f);
        CreateNewKnife();
    }

    public IEnumerator Creator()
    {
        yield return new WaitForSeconds(0.7f);
        CreateNewLog();
    }

}
