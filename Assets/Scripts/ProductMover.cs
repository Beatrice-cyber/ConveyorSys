using UnityEngine;

public class ProductMover : MonoBehaviour
{
	[SerializeField] private float speed = 2.0f;
    private Conveyor currentConveyor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
	// the obj itself need to know what convyor its on to go to next...
	public void SetConveyor(Conveyor conveyor)
{
    currentConveyor = conveyor;

    if (currentConveyor != null)
    {
        transform.position = currentConveyor.StartPoint.position;
    }
}
    // Update is called once per frame
    void Update()
    {
        if (currentConveyor == null)
        {
            return;
        }

        Vector3 target = currentConveyor.EndPoint.position;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );

        // Have we reached this conveyor's end?
        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            currentConveyor = currentConveyor.Next;
        }
    }
}
