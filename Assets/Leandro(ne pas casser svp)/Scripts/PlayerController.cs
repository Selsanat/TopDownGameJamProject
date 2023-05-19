using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] int MSpeed;
    [SerializeField] float DashSpeed;
    [SerializeField] float DashLength;
    [SerializeField] int RotationSpeed;
    private float dashlengthPrivate;
    private float dashTimePrivate;
    private CharacterController controller;
    private Vector2 movementInput = Vector2.zero;
    private bool Dash;
    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context){
        movementInput = context.ReadValue<Vector2>();
    }
    public void OnDash(InputAction.CallbackContext context){
        context.action.started += _ => Dashing();
    }

    void Dashing(){
        dashlengthPrivate = DashSpeed;
        dashTimePrivate = DashLength/100;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(movementInput.x,movementInput.y,0);
        if (dashTimePrivate>0){
            move+= transform.up*dashlengthPrivate;
        }
        controller.Move(move*Time.deltaTime * MSpeed);
        dashTimePrivate-=Time.deltaTime;
        if (move != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward,move);
            transform.rotation = Quaternion.RotateTowards(transform.rotation,toRotation,100*RotationSpeed*Time.deltaTime);
        }
    }
}
