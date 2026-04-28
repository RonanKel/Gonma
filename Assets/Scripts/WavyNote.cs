using UnityEngine;

public class WavyNote : NoteScript
{
    [SerializeField] float amplitude = 5f;
    [SerializeField] float frequency = 2f;

    private Vector3 direction;

    override protected void CalculateMovement()
    {
        if (!mmScript.waiting)
        {
            direction = Vector3.Normalize(beatLinePos.position - spawnPos.position);
            float ratio = (float)(mmScript.GetCurrentSongTime() - spawnTime) / (spb * beatsToTravel);
            Vector3 basePos = Vector3.LerpUnclamped(spawnPos.position, beatLinePos.position, ratio);
            
            float offset = amplitude * Mathf.Sin(ratio * Mathf.PI * frequency);
            //Debug.Log(offset.ToString());
            Vector3 cross = Vector3.Cross(direction.normalized, Vector3.forward);
            Vector3 vecOffset = cross * offset;

            transform.position = basePos + (vecOffset);
            

            
            /*
            FROM OTHER BRANCH
            amplitude * Mathf.Sin(frequency * (Time.time - startTime));
            transform.Translate(Vector3.left * (speed/2) * Time.deltaTime, Space.World);
            transform.position = new Vector3(transform.position.x, initialPosition.y + yOffset, transform.position.z);*/


           
        }
    }
}
