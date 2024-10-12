using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость перемещения
    public float jumpForce = 5f; // Сила прыжка
    private Rigidbody rb;
    private bool isGrounded; // Проверка, на земле ли персонаж
    public LayerMask whatIsGrounded;
    private float checkRadius;

    public Transform feetPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Получаем компонент Rigidbody
    }

    void Update()
    {
        MovePlayer(); // Вызываем метод перемещения
        Jump(); // Вызываем метод прыжка
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // Получаем ввод по оси X
        float moveVertical = Input.GetAxis("Vertical"); // Получаем ввод по оси Z

        // Создаем вектор перемещения
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Перемещаем персонажа
        rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);

        // Если есть движение, поворачиваем персонажа
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement); // Определяем целевое вращение
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // Плавно поворачиваем
        }
    }

    void Jump()
    {
        // Проверяем, нажата ли клавиша пробела и находится ли персонаж на земле
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Применяем силу для прыжка
            isGrounded = false; // Персонаж теперь в воздухе
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, касается ли персонаж земли
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Персонаж на земле
        }
    }
}