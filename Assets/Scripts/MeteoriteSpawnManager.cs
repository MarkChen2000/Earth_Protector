using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteSpawnManager : MonoBehaviour
{

    [SerializeField] float SpawnCircleRadius = 10f, MeteoriteSpeed = 0.5f;

    [SerializeField] float InitialInterval = 5f, MinInterval = 1f, IntensityIncreaseRate = 0.01f;
    float currentInterval;

    [SerializeField] Transform MeteoriteSpawnTrans;
    [SerializeField] GameObject MeteoritePrefab;

    void Awake()
    {
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) {
            RandomSpawnFromCircleEdge();
        }
    }
    
    public IEnumerator StartSpawningMeteorites()
    {
        currentInterval = InitialInterval;

        while ( true )
        {
            RandomSpawnFromCircleEdge();

            currentInterval = InitialInterval - Mathf.Exp(IntensityIncreaseRate * Time.time);
            currentInterval = Mathf.Max(currentInterval, MinInterval);

            yield return new WaitForSeconds(currentInterval);
            Debug.Log("Current interval is " + currentInterval+", and time is "+Time.time);

        }
    }

    void RandomSpawnFromCircleEdge()
    {
        float randonAngle = Random.Range(0f, 360f);
        Vector2 randonSpawnPoint = new Vector2();
        randonSpawnPoint.x = Mathf.Cos(randonAngle) * SpawnCircleRadius; //sin@ = 斜邊/底邊 底邊為半徑 sin@*半徑即成為斜邊，即是x座標
        randonSpawnPoint.y = Mathf.Sin(randonAngle) * SpawnCircleRadius; //cos@ = 鄰邊/底邊
        
        Vector2 targetVector = -randonSpawnPoint.normalized;
        GameObject prefab = Instantiate(MeteoritePrefab, randonSpawnPoint, Quaternion.identity, MeteoriteSpawnTrans);
        float rotateAngle = Mathf.Atan2(targetVector.y,targetVector.x) * Mathf.Rad2Deg; //計算出(1,0)前方向量到目標向量的弧度再乘Rad2Deg成為角度
        prefab.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f,rotateAngle-90f)); //再以該角度以z為軸旋轉 -90是因為是想以y軸(0,1)面相目標 所以少轉90度

        prefab.GetComponent<MeteoriteController>().MoveSpeed = MeteoriteSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Vector3.zero, SpawnCircleRadius);
    }

}
