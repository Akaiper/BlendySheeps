using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OvelhaAI : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    public Animator OvelhaAnimator;
    public GameObject OvelhaPrefab;
    public Transform getChildOvelha;



    public bool IsBeingTouched;
    bool walk;


    private float _attackRange = 3f;
    private float _rayDistance = 5.0f;
    private float _stoppingDistance = 1.5f;

    private Vector3 _destination;
    private Quaternion _desiredRotation;
    private Vector3 _direction;
    private OvelhaAI _target;
    private OvelhaState _currentState;

    private void Start()
    {
        getChildOvelha = OvelhaPrefab.transform.GetChild(0);
        OvelhaAnimator = getChildOvelha.GetComponent<Animator>();

        walk = OvelhaAnimator.GetBool("IsWalking");

    }

    private void Update()
    {
        switch (_currentState)
        {
            case OvelhaState.Wander:
                {
                    if (IsBeingTouched == true)
                    {
                        _currentState= OvelhaState.Idle;
                    }

                    OvelhaAnimator.SetBool("IsWalking", true);

                    if (NeedsDestination())
                    {
                        GetDestination();
                    }

                   

                    transform.rotation = _desiredRotation;

                    transform.Translate(Vector3.forward * Time.deltaTime * 5f);
                  

                    var rayColor = IsPathBlocked() ? Color.red : Color.green;
                    Debug.DrawRay(transform.position, _direction * _rayDistance, rayColor);

                    if (IsPathBlocked())
                        GetDestination();
                    

                    //var targetresources = CheckForAggro();
                    //if (targetresources != null)
                    //{
                    //    //_target = targetresources.GetComponent<Drone>();
                    //    _currentState = OvelhaState.Chase;
                    //}

                    break;
                }
            case OvelhaState.Chase:
                {
                    if (_target == null)
                    {
                        _currentState = OvelhaState.Wander;
                        return;
                    }

                    transform.LookAt(_target.transform);
                    transform.Translate(Vector3.forward * Time.deltaTime * 5f);

                    if (Vector3.Distance(transform.position, _target.transform.position) < _attackRange)
                    {
                        _currentState = OvelhaState.Attack;
                    }
                    break;
                }
            case OvelhaState.Attack:
                {
                    if (_target != null)
                    {
                        Destroy(_target.gameObject);
                    }

                    // play laser beam

                    _currentState = OvelhaState.Wander;
                    break;
                }
            case OvelhaState.Idle:
                {
                   
                    OvelhaAnimator.SetBool("IsWalking", false);
                    
                  
                    if (IsBeingTouched == false)
                    {
                        _currentState = OvelhaState.Wander;
                    }

                   
                    break;
                }
        }
    }

    private bool IsPathBlocked()
    {
        Ray ray = new Ray(transform.position, _direction);
        var hitSomething = Physics.RaycastAll(ray, _rayDistance, _layerMask);
        return hitSomething.Any();
    }

    private void GetDestination()
    {
        Vector3 testPosition = (transform.position + (transform.forward * 4f)) + new Vector3(UnityEngine.Random.Range(-4.5f, 4.5f), 0f, UnityEngine.Random.Range(-4.5f, 4.5f));

        _destination = new Vector3(testPosition.x, 1f, testPosition.z);
        _direction = Vector3.Normalize(_destination - transform.position);
        _direction = new Vector3(_direction.x, 0f, _direction.z);      
        _desiredRotation = Quaternion.LookRotation(_direction);
    }

    private bool NeedsDestination()
    {
        if (_destination == Vector3.zero)
            return true;

        var distance = Vector3.Distance(transform.position, _destination);
        if (distance <= _stoppingDistance)
        {
            return true;
        }

        return false;
    }



    Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
    Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);

    //private Transform CheckForAggro()
    //{
    //    float aggroRadius = 5f;

    //    RaycastHit hit;
    //    var angle = transform.rotation * startingAngle;
    //    var direction = angle * Vector3.forward;
    //    var pos = transform.position;
    //    for (var i = 0; i < 24; i++)
    //    {
    //        if (Physics.Raycast(pos, direction, out hit, aggroRadius))
    //        {
    //            var drone = hit.collider.GetComponent<Drone>();
    //            if (drone != null && drone.Team != gameObject.GetComponent<Drone>().Team)
    //            {
    //                Debug.DrawRay(pos, direction * hit.distance, Color.red);
    //                return drone.transform;
    //            }
    //            else
    //            {
    //                Debug.DrawRay(pos, direction * hit.distance, Color.yellow);
    //            }
    //        }
    //        else
    //        {
    //            Debug.DrawRay(pos, direction * aggroRadius, Color.white);
    //        }
    //        direction = stepAngle * direction;
    //    }

    //    return null;
    //}
}


public enum OvelhaState
{
    Wander,
    Chase,
    Attack,
    Idle
}

