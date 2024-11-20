using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardMove : MonoBehaviour
{
    public float accelerationDuration;
    public float duration;
    private Coroutine moveCoroutine;

    /// <summary>
    /// Smoothly moves the card to the final position within a specified duration with adjustable acceleration.
    /// </summary>
    /// <param name="finalPosition">The target position to move the card to.</param>
    public void move_card(Vector3 finalPosition)
    {
        if (accelerationDuration * 2 > duration)
        {
            Debug.LogError("Acceleration duration must be at least half of the total duration.");
            return;
        }

        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        moveCoroutine = StartCoroutine(MoveCardCoroutine(finalPosition));
    }

    private IEnumerator MoveCardCoroutine(Vector3 finalPosition)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        // Acceleration phase
        while (elapsedTime < accelerationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / accelerationDuration;
            transform.position = Vector3.Lerp(startPosition, finalPosition, Mathf.SmoothStep(0, 1, t));
            yield return null;
        }

        // Constant speed phase
        float constantSpeedDuration = duration - 2 * accelerationDuration;
        elapsedTime = 0f;
        Vector3 midPosition = transform.position;
        while (elapsedTime < constantSpeedDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / constantSpeedDuration;
            transform.position = Vector3.Lerp(midPosition, finalPosition, t);
            yield return null;
        }

        // Deceleration phase
        elapsedTime = 0f;
        Vector3 decelerationStartPosition = transform.position;
        while (elapsedTime < accelerationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / accelerationDuration;
            transform.position = Vector3.Lerp(decelerationStartPosition, finalPosition, Mathf.SmoothStep(0, 1, t));
            yield return null;
        }

        // Ensure the final position is exactly the target position
        transform.position = finalPosition;
        moveCoroutine = null;
    }
}