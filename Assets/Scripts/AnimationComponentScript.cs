using UnityEngine;

public class AnimationComponentScript : MonoBehaviour
{
    Vector3 starting_pos;
    float start_time;
    [SerializeField] float jump_height = 5;
    [SerializeField] float jump_speed = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        starting_pos = gameObject.transform.position;
    }

    void Awake()
    {
        start_time = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float y_offset = jump_height * Mathf.Cos((Time.time - start_time) * jump_speed);
        gameObject.transform.position = new Vector3(starting_pos.x, starting_pos.y + y_offset, starting_pos.z);
    }
}
