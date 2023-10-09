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
        randonSpawnPoint.x = Mathf.Cos(randonAngle) * SpawnCircleRadius; //sin@ = ����/���� ���䬰�b�| sin@*�b�|�Y��������A�Y�Ox�y��
        randonSpawnPoint.y = Mathf.Sin(randonAngle) * SpawnCircleRadius; //cos@ = �F��/����
        
        Vector2 targetVector = -randonSpawnPoint.normalized;
        GameObject prefab = Instantiate(MeteoritePrefab, randonSpawnPoint, Quaternion.identity, MeteoriteSpawnTrans);
        float rotateAngle = Mathf.Atan2(targetVector.y,targetVector.x) * Mathf.Rad2Deg; //�p��X(1,0)�e��V�q��ؼЦV�q�����צA��Rad2Deg��������
        prefab.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f,rotateAngle-90f)); //�A�H�Ө��ץHz���b���� -90�O�]���O�Q�Hy�b(0,1)���ۥؼ� �ҥH����90��

        prefab.GetComponent<MeteoriteController>().MoveSpeed = MeteoriteSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Vector3.zero, SpawnCircleRadius);
    }

}
