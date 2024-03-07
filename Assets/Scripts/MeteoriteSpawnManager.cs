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

    [SerializeField] GravityEffectType GravityType = GravityEffectType.Physics;
    [SerializeField] float SimplifiedEarthPullForce = 0.001f;
    [SerializeField] Transform EarthTrans;
    [SerializeField] float EarthMass = 150f, initialForceMultiplier = 1f;

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
                // �N���u�@���H���Ȫ��[�v�A�i�H�N���v��ı��
                int amount = Mathf.RoundToInt( Mathf.Lerp(MinSpawnAmount, MaxSpawnAmount, RandomOfSpaweAmountCurve.Evaluate(Random.value)));
                
                currentSpawnInterval = Mathf.Lerp(MinSpawnInterval, currentMaxSpawnInterval, RandomOfIntervalCurve.Evaluate(Random.value) ); //�H�̤j�ȻP�̤p�Ȱ��������ȡA�����u0~1�����G�e�{
                
                // Debug.Log("interval: " + currentSpawnInterval+", timer: "+ (Time.time-startTime)+" amount: "+amount);

                // �p�G�C���ͦ��W�L�@�� �K���������b�����j�ɶ����o�g
                if ( amount > 1 ) {
                    for (int i = 0; i < amount ; i++) {
                        RandomSpawnFromCircleEdge();
                        yield return new WaitForSeconds(currentSpawnInterval/2);
                    }
                }
                else RandomSpawnFromCircleEdge();
            }

            // decrease maxinterval 
            if ( Time.time >= increaseIntensityTimer + TimeToDecreaseInterval + decreaseCounter*IncreaseTimepPerTurn && currentMaxSpawnInterval-IntervalDecreaseRate > MinSpawnInterval ) {
                increaseIntensityTimer = Time.time;
                decreaseCounter += 1;

                currentMaxSpawnInterval -= IntervalDecreaseRate;

                //Debug.Log("Update MaxInterval:" + currentMaxSpawnInterval+" Increase count:"+ decreaseCounter);
            }

            yield return null; //wait one frame.
        }
    }

    void RandomSpawnFromCircleEdge()
    {
        float randonAngle = Random.Range(0f, 360f);
        Vector2 randonSpawnPoint = new Vector2();
        randonSpawnPoint.x = Mathf.Cos(randonAngle) * SpawnCircleRadius; //sin@ = ����/���� ���䬰�b�| sin@*�b�|�Y��������A�Y�Ox�y��
        randonSpawnPoint.y = Mathf.Sin(randonAngle) * SpawnCircleRadius; //cos@ = �F��/����

        Vector2 targetVector = -randonSpawnPoint.normalized; //�¦V���ߪ���V

        GameObject prefab = Instantiate(MeteoritePrefabs[Random.Range(0,4)], randonSpawnPoint, Quaternion.identity, MeteoriteSpawnTrans); // �H���ͦ����P���k�� //Range����ƽd��]�t�̤p�Ȧ����]�t�̤j��

        float minOffset = -RandomOffsetAngleRange / 2;
        float maxOffset = RandomOffsetAngleRange / 2;
        float randomAngleOffset = Mathf.Lerp(minOffset, maxOffset, RandomAngleOffset.Evaluate(Random.value) ); //�����@�ǰ���

        float rotateAngle = Mathf.Atan2(targetVector.y,targetVector.x) * Mathf.Rad2Deg; //�p��X(1,0)�e��V�q��ؼЦV�q�����צA��Rad2Deg��������
        prefab.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f,rotateAngle-90f + randomAngleOffset )); //�A�H�Ө��ץHz���b���� -90�O�]���O�Q�Hy�b(0,1)���ۥؼ� �ҥH����90��

        //prefab.GetComponent<MeteoriteController>().SetDefaultStats();
        prefab.GetComponent<MeteoriteController>().SetStats(GravityType, SimplifiedEarthPullForce, EarthTrans, EarthMass, initialForceMultiplier);

        //Debug.Log(" SpawnPoint: " + randonSpawnPoint + " AngleOffset: " + randomAngleOffset);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Vector3.zero, SpawnCircleRadius);
    }

}
