using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //Ÿ�Ͽ� Ÿ���� �Ǽ��Ǿ� �ִ��� �˻��ϴ� ����
    public bool IsBuildTower { set; get; }
    /*�ڵ� ���� ������Ƽ
     ������Ƽ�� set, get�� �߰����� ������ ���� �⺻���� �� �ڵ� ���̸� ���̱� ���� ���
    ������ ���� ������ �ʿ� ������, set, get�� �⺻���� �ڵ����� �ۼ���*/

    private void Awake()
    {
        IsBuildTower = false;
    }
}
