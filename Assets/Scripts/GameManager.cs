using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // ���Ͱ� ������ ��ġ�� ������ �迭 (Array)
    // public Transform[] points;

    // ���Ͱ� ������ ��ġ�� ������ List Ÿ�� ����
    public List<Transform> points = new List<Transform>();

    // ���͸� �̸� ������ ������ ����Ʈ �ڷ��� (������Ʈ Ǯ��)
    public List<GameObject> monsterPool = new List<GameObject>();

    // ������Ʈ Ǯ(Object Pool)�� ������ ������ �ִ� ����
    public int maxMonsters = 10;

    // ���� �������� ������ ����
    public GameObject monster;

    // ������ ���� ����
    public float createTime = 3.0f;

    // ������ ���� ���θ� ������ ��� ����
    private bool isGameOver;

    // ������ ���� ���θ� ������ ������Ƽ
    public bool IsGameOver
    {
        get { return isGameOver; }
        set
        {
            isGameOver = value;
            if (isGameOver)
            {
                CancelInvoke("CreateMonster");
            }
        }
    }

    // �̱��� �ν��Ͻ� ����
    public static GameManager instance = null;

    // ���ھ� �ؽ�Ʈ�� ������ ����
    public TMP_Text scoreText;
    // ���� ������ ����ϱ� ���� ����
    private int totScore = 0;

    // ��ũ��Ʈ�� ����Ǹ� ���� ���� ȣ��Ǵ� ����Ƽ �̺�Ʈ �Լ�
    private void Awake()
    {
        // instance�� �Ҵ���� �ʾ��� ���
        if (instance == null)
        {
            instance = this;
        }
        // instance�� �Ҵ�� Ŭ������ �ν��Ͻ��� �ٸ� ��� ���� ������ Ŭ������ �ǹ���
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        // �ٸ� ������ �Ѿ���� �����ϱ� �ʰ� ������
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        // ���� ������Ʈ Ǯ ����
        CreateMonsterPool();

        // SpawnPointGroup ������Ʈ�� Transform ������Ʈ ����
        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;

        // SpawnPointGroup ������ �ִ� ��� ���ϵ� ���ӿ�����Ʈ�� Transform ������Ʈ ���� (Array)
        // points = spawnPointGroup?.GetComponentsInChildren<Transform>();

        // SpawnPointGroup ������ �ִ� ��� ���ϵ� ���ӿ�����Ʈ�� Transform ������Ʈ ���� (List)
        // spawnPointGroup?.GetComponentsInChildren<Transform>(points);
        // => parent Object�� List[0]�� ��

        foreach(Transform point in spawnPointGroup) // spawnPointGroup�� ���� ������Ʈ���� ������ ��� Transform�� ���� ����
        {
            points.Add(point);
        }

        // ������ �ð� �������� �Լ��� ȣ��
        InvokeRepeating("CreateMonster", 2.0f, createTime);

        // ���ھ� ���� ���
        totScore = PlayerPrefs.GetInt("TOT_SCORE", 0);
        DisplayScore(0);
    }

    void CreateMonster()
    {
        // ������ �ұ�Ģ�� ���� ��ġ ����
        int idx = Random.Range(0, points.Count);

        // ���� ������ ����
        // Instantiate(monster, points[idx].position, points[idx].rotation); => ������Ʈ Ǯ ���

        // ������Ʈ Ǯ���� ���� ����
        GameObject _monster = GetMonsterInPool();
        
        // ������ ������ ��ġ�� ȸ���� ����
        _monster?.transform.SetPositionAndRotation(points[idx].position, points[idx].rotation);

        // ������ ���͸� Ȱ��ȭ
        _monster?.SetActive(true);
    }

    // ������Ʈ Ǯ�� ���� ����
    void CreateMonsterPool()
    {
        for (int i = 0; i < maxMonsters; i++)
        {
            // ���� ����
            var _monster = Instantiate<GameObject>(monster);
            // ������ �̸��� ����
            _monster.name = $"Monster_{i:00}"; // _monster.name = "Monster_" + i.ToString("00");
            // ���� ��Ȱ��ȭ
            _monster.SetActive(false);

            // ������ ���͸� ������Ʈ Ǯ�� �߰�
            monsterPool.Add(_monster);
        }
    }

    // ������Ʈ Ǯ���� ��� ������ ���͸� ������ ��ȯ�ϴ� �Լ�
    public GameObject GetMonsterInPool()
    {
        // ������Ʈ Ǯ�� ó������ ������ ��ȸ
        foreach (var _monster in monsterPool)
        {
            // ��Ȱ��ȭ ���η� ��� ������ ���͸� �Ǵ�
            if (_monster.activeSelf == false)
            {
                // ���� ��ȯ
                return _monster;
            }
        }

        return null;
    }

    // ������ �����ϰ� ����ϴ� �Լ�
    public void DisplayScore(int score)
    {
        totScore += score;
        scoreText.text = $"<color=#00ff00>SCORE :</color> <color=#ff0000>{totScore:#,##0}</color>";
        // ���ھ� ����
        PlayerPrefs.SetInt("TOT_SCORE", totScore);
    }
}