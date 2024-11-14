using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardMove : MonoBehaviour
{
    public float speed;
    public float accelerationSpeed;

    /// <summary>
    /// Smoothly moves the card to the final position with adjustable acceleration and deceleration.
    /// </summary>
    /// <param name="finalPosition">The target position to move the card to.</param>
    public void move_card(Transform finalPosition)
    {
        StartCoroutine(MoveCardCoroutine(finalPosition, speed, accelerationSpeed));
    }

    private IEnumerator MoveCardCoroutine(Transform finalPosition, float speed, float accelerationSpeed)
    {
        Debug.Log("Moving card");
        Vector3 startPosition = transform.position;
        float journeyLength = Vector3.Distance(startPosition, finalPosition.position);
        float startTime = Time.time;

        while (Vector3.Distance(transform.position, finalPosition.position) > 0.01f)
        {
            float timeElapsed = (Time.time - startTime) * speed;
            float fractionOfJourney = timeElapsed / journeyLength;
            float easedFraction = EaseInOutQuad(fractionOfJourney, accelerationSpeed);
            transform.position = Vector3.Lerp(startPosition, finalPosition.position, easedFraction);
            yield return null;
        }

        transform.position = finalPosition.position;
    }

    private float EaseInOutQuad(float t, float accelerationSpeed)
    {
        t = Mathf.Clamp01(t * accelerationSpeed);
        return t < 0.5f ? 2 * t * t : -1 + (4 - 2 * t) * t;
    }
}