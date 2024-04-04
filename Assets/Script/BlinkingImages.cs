using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingImages : MonoBehaviour
{
    public float BlinkSpeed = 1f; // �������� ��������
    public bool IsBlinking = false;
    private float targetAlpha = 0.0f; // ���� �����-��������
    public IEnumerator BlinkImage(Image image)
    {
        // ��������� ����� ��������
        IsBlinking = true;
        // ������������ ���� �����-�������� ����� 0 � 1
        while (true)
        {
            // �������� ������������ �����-�������� �������� ����� ����������� � �������� �����-��������
            while (Mathf.Abs(targetAlpha - image.color.a) > 0.01f)
            {
                Color color = image.color;
                color.a = Mathf.Lerp(color.a, targetAlpha, BlinkSpeed * Time.deltaTime);
                image.color = color;
                yield return null;
            }
            // ���������� �������� �����-�������� �� ��������������� (0 ��� 1)
            targetAlpha = targetAlpha == 1.0f ? 0.0f : 1.0f;
            // �������� ��������� �������� ����� ������� ���������� ��������
            yield return new WaitForSeconds(0.1f);
        }
    }
}
