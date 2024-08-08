using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public enum WeaponState { SearchTarget = 0, AttackToTarget }

public class TowerWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab; // �߻�ü ������
    [SerializeField]
    private Transform spawnPoint; // �߻�ü ���� ��ġ
    [SerializeField]
    private float attackRate = 0.5f; // ���� �ӵ�
    [SerializeField]
    private float attackRange = 2.0f; // ���� ����
    private WeaponState weaponState = WeaponState.SearchTarget; // Ÿ�� ������ ����
    private Transform attackTarget = null; // ���� ���
    private EnemySpawner enemySpawner; // ���ӿ� �����ϴ� �� ���� ȹ���

    public void Setup(EnemySpawner enemySpawner)
    {
        this.enemySpawner = enemySpawner;

        // ���� ���¸� WeaponState.SearchTarget���� ����
        ChangeState(WeaponState.SearchTarget);
    }

    public void ChangeState(WeaponState newState)
    {
        // ������ ������̴� ���� ����
        StopCoroutine(weaponState.ToString());
        // ���� ����
        weaponState = newState;
        // ���ο� ���� ���
        StartCoroutine(weaponState.ToString());
    }

    private void Update()
    {
        if(attackTarget != null)
        {
            RotateToTarget();
        }
    }

    private void RotateToTarget()
    {
        /* �������κ����� �Ÿ��� ���������κ����� ������ �̿��� ��ġ�� ���ϴ� �� ��ǥ�� �̿�
        ���� = arctan(y/x)
        x, y ������ ���ϱ�*/
        float dx = attackTarget.position.x-transform.position.x;
        float dy = attackTarget.position.y-transform.position.y;
        /*x, y �������� �������� ���� ���ϱ�
         ������ radian �����̱� ������ Mathf.Rad2Deg�� ���� �� ������ ����*/
        float degree = Mathf.Atan2(dx, dy) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, degree);
    }

    private IEnumerator SearchTarget()
    {
        // ���� ������ �ִ� ���� ã�� ���� ���� �Ÿ��� �ִ��� ũ�� ����
        float closestDistSqr = Mathf.Infinity;
        // EnemySpawner�� EnemyLIst�� �ִ� ���� �ʿ� �����ϴ� ��� �� �˻�
        for (int i = 0; i < enemySpawner.EnemyLIst.Count; i++)
        {
            float distance = Vector3.Distance(enemySpawner.EnemyLIst[i].transform.position, transform.position);
            // ���� �˻����� ������ �Ÿ��� ���ݹ��� ���� �ְ�, ������� �˻��� ������ �Ÿ��� ������
            if(distance <= attackRange && distance <= closestDistSqr)
            {
                closestDistSqr = distance;
                attackTarget = enemySpawner.EnemyLIst[i].transform;
            }

            if(attackTarget != null)
            {
                ChangeState(WeaponState.AttackToTarget);
            }

            yield return null;
        }
    }

    private IEnumerator AttackToTarget()
    {
        while (true)
        {
            // 1. target�� �ִ��� �˻� (�ٸ� �߻�ü�� ���� ����, Goal �������� �̵��� ���� ��)
            if (attackTarget == null)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            // 2. target�� ���� ���� �ȿ� �ִ��� �˻� (���� ������ ����� ���ο� �� Ž��)
            float distange = Vector3.Distance(attackTarget.position, transform.position);
            if (distange > attackRange)
            {
                attackTarget = null;
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            // 3. attackRate �ð���ŭ ���
            yield return new WaitForSeconds(attackRate);

            // 4. ���� (�߻�ü ����)
            SpawnProjectile();
        }
    }

    private void SpawnProjectile()
    {
        GameObject clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        // ������ �߻�ü���� ���ݴ��(attackTarget) ���� ����
        clone.GetComponent<Projectile>().Setup(attackTarget);
    }
}
