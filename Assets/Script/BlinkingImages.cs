using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingImages : MonoBehaviour
{
    public float BlinkSpeed = 1f; // Скорость моргания
    public bool IsBlinking = false;
    private float targetAlpha = 0.0f; // Цель альфа-значения
    public IEnumerator BlinkImage(Image image)
    {
        // Установка флага моргания
        IsBlinking = true;
        // Переключение цели альфа-значения между 0 и 1
        while (true)
        {
            // Линейная интерполяция альфа-значения текущего цвета изображения к целевому альфа-значению
            while (Mathf.Abs(targetAlpha - image.color.a) > 0.01f)
            {
                Color color = image.color;
                color.a = Mathf.Lerp(color.a, targetAlpha, BlinkSpeed * Time.deltaTime);
                image.color = color;
                yield return null;
            }
            // Обновление целевого альфа-значения на противоположное (0 или 1)
            targetAlpha = targetAlpha == 1.0f ? 0.0f : 1.0f;
            // Ожидание небольшой задержки перед началом следующего моргания
            yield return new WaitForSeconds(0.1f);
        }
    }
}
