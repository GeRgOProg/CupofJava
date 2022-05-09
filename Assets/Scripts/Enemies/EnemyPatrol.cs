using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [Header ("Enemy")]
    [SerializeField] private Transform enemy;
    [Header("Parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
   
    private bool movingLeft;
    [Header("Idle")]
    [SerializeField] private float idleDuration;
    private float idleTimer;
    [Header ("Animator")]
    [SerializeField] private Animator anim;
      

    private void Awake()
    {
        initScale = enemy.localScale; //15 1.3 56
        if (enemy.position.x >= leftEdge.position.x)
            movingLeft = true;
        else
            movingLeft = false;
        
        
    }
    private void Update()
    {
        //Debug.Log(leftEdge.position.x + " " + enemy.position.x + " " + rightEdge.position.x);
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x) //15 >= 6
                MoveInDirection(-1); // go left
            else
                DirectionChange(); // change dir
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x) //15 >= 21
                MoveInDirection(1); //go right
            else
                DirectionChange(); //change dir
        }
    

}
    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("moving", true);
        //face dir
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
            initScale.y, initScale.z);
        ///Move
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z);
        
         

    }

    private void DirectionChange()
    {
        anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;
        if(idleTimer > idleDuration)
        movingLeft = !movingLeft;
       
    }
}
