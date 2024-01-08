using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeteoriteSpawnManager : MonoBehaviour
{

    [SerializeField] float SpawnCircleRadius = 10f;

    [SerializeField] float MaxSpawnInterval = 5f, MinSpawnInterval = 0.5f, IntervalDecreaseRate = 0.5f, TimeToDecreaseInterval = 5f, IncreaseTimepPerTurn = 2f;
    float currentSpawnInterval, currentMaxSpawnInterval;
    [SerializeField] AnimationCurve RandomOfIntervalCurve;
    [SerializeField] int MinSpawnAmount = 1, MaxSpawnAmount = 3;
    [SerializeField] AnimationCurve RandomOfSpaweAmountCurve;
    [SerializeField] float RandomOffsetAngleRange = 180f;
    [SerializeField] AnimationCurve RandomAngleOffset;

    [SerializeField] Transform MeteoriteSpawnTrans;

    [SerializeField] Transform EarthTrans;
    [SerializeField] float EarthMass = 150f, MeteorMass = 0.0001f, MeteorInitForce = 0.1f;
    [SerializeField] int MeteorHitPoint = 1;
    [SerializeField] GravityEffectType GravityType = GravityEffectType.Physics;

    [SerializeField] List<GameObject> MeteoritePrefabs = new List<GameObject>();

    void Awake()
    {
        
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) {
            RandomSpawnFromCircleEdge();
        }
    }

    float startTime, timer, increaseIntensityTimer, decreaseCounter;

    void IntializeSpawnStats()
    {
        startTime = Time.time;
        timer = Time.time;
        increaseIntensityTimer = Time.time;

        currentMaxSpawnInterval = MaxSpawnInterval;

        decreaseCounter = 0;
    }

    public IEnumerator StartSpawningMeteorites()
    {
        IntializeSpawnStats();
        Debug.Log("Start! starttime:" + startTime);

        while ( true ) {

            if ( Time.time >= timer + currentSpawnInterval ) {
                timer = Time.time;

                // Spawn in random amount
                // 將曲線作為隨機值的加權，可以將機率視覺化
                int amount = Mathf.RoundToInt( Mathf.Lerp(MinSpawnAmount, MaxSpawnAmount, RandomOfSpaweAmountCurve.Evaluate(Random.value)));
                Debug.Log("interval: " + currentSpawnInterval+", timer: "+ (Time.time-startTime)+" amount: "+amount);
                for (int i = 0; i < amount ; i++) {
                    RandomSpawnFromCircleEdge();
                }

                currentSpawnInterval = Mathf.Lerp(MinSpawnInterval, currentMaxSpawnInterval, RandomOfIntervalCurve.Evaluate(Random.value) ); //以最大值與最小值做中間插值，讓曲線0~1的結果呈現
            }

            // decrease maxinterval 
            if ( Time.time >= increaseIntensityTimer + TimeToDecreaseInterval + decreaseCounter*IncreaseTimepPerTurn && currentMaxSpawnInterval-IntervalDecreaseRate > MinSpawnInterval ) {
                increaseIntensityTimer = Time.time;
                decreaseCounter += 1;

                currentMaxSpawnInterval -= IntervalDecreaseRate;

                Debug.Log("Update MaxInterval:" + currentMaxSpawnInterval+" Increase count:"+ decreaseCounter);
            }

            yield return new WaitForEndOfFrame();
        }
    }

    void RandomSpawnFromCircleEdge()
    {
        float randonAngle = Random.Range(0f, 360f);
        Vector2 randonSpawnPoint = new Vector2();
        randonSpawnPoint.x = Mathf.Cos(randonAngle) * SpawnCircleRadius; //sin@ = 斜邊/底邊 底邊為半徑 sin@*半徑即成為斜邊，即是x座標
        randonSpawnPoint.y = Mathf.Sin(randonAngle) * SpawnCircleRadius; //cos@ = 鄰邊/底邊

        

        Vector2 targetVector = -randonSpawnPoint.normalized; //朝向中心的方向
        GameObject prefab = Instantiate(MeteoritePrefabs[Mathf.RoundToInt(Random.Range(0,3))], randonSpawnPoint, Quaternion.identity, MeteoriteSpawnTrans);

        float minOffset = -RandomOffsetAngleRange / 2;
        float maxOffset = RandomOffsetAngleRange / 2;
        float randomAngleOffset = Mathf.Lerp(minOffset, maxOffset, RandomAngleOffset.Evaluate(Random.value) ); //給予一些偏移

        float rotateAngle = Mathf.Atan2(targetVector.y,targetVector.x) * Mathf.Rad2Deg; //計算出(1,0)前方向量到目標向量的弧度再乘Rad2Deg成為角度
        prefab.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f,rotateAngle-90f + randomAngleOffset )); //再以該角度以z為軸旋轉 -90是因為是想以y軸(0,1)面相目標 所以少轉90度

        //prefab.GetComponent<MeteoriteController>().SetDefaultStats();
        prefab.GetComponent<MeteoriteController>().SetStats(GravityType, EarthTrans, EarthMass, MeteorInitForce, MeteorMass, MeteorHitPoint);

        Debug.Log(" SpawnPoint: " + randonSpawnPoint + " AngleOffset: " + randomAngleOffset);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Vector3.zero, SpawnCircleRadius);
    }

}
