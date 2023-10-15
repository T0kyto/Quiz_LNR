using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldController : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField => GetComponent<TMP_InputField>();
    private Process oskProcess;

    private void Start()
    {
        // Добавляем слушатель события получения фокуса
        inputField.onSelect.AddListener((string text) => OpenKeyboard());
        inputField.onDeselect.AddListener((string text) => CloseKeyboard());
    }
    public void OpenKeyboard()
    {
        oskProcess = Process.Start("osk.exe"); // Запуск экранной клавиатуры
    }

    public void CloseKeyboard()
    {
        if (oskProcess != null && !oskProcess.HasExited)
        {
            oskProcess.CloseMainWindow(); // Закрытие экранной клавиатуры
        }
    }
}
