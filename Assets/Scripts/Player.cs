using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckMove()
    {
        var deltaX = Input.GetAxis("Horizontal");
        var newXPosition = transform.position.x + deltaX;
        transform.position = new Vector2(newXPosition, transform.position.y); 
    }
}
