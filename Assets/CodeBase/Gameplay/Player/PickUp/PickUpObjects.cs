using Gameplay.View.HUD;
using UnityEngine;
using UniRx;
using Zenject;

namespace Gameplay.Player.PickUp
{
    public class PickUpObjects : MonoBehaviour
    {
        [Inject] private readonly UIGrabItDownHUDButtonPresenter _downPresenter;
        [Inject] private readonly UIGrabItUpHUDButtonPresenter _upPresenter;

        [Header("Grab Properties")]
        [SerializeField, Range(4, 50)] private float _grabSpeed = 7;
        [SerializeField, Range(0.1f, 5)] private float _grabMinDistance = 1;
        [SerializeField, Range(4, 25)] private float _grabMaxDistance = 10;
        [SerializeField, Range(1, 10)] private float _scrollSpeed = 2;
        [SerializeField] LayerMask _collisionMask;

        private Rigidbody _targetRB;
        private Transform _transform;
        private Vector3 _targetPos;
        private float _targetDistance;
        private bool _grabbing = false;

        //Debug
        LineRenderer _lineRenderer;



        void Awake()
        {
            _transform = transform;

            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Start()
        {
            _upPresenter.OnUpClicked
                .Subscribe(_ => TryGrabObject())
                .AddTo(this);

            _downPresenter.OnDownClicked
                .Subscribe(_ => ReleaseObject())
                .AddTo(this);
        }

        private void TryGrabObject()
        {
            if (_grabbing) return;

            if (Physics.Raycast(_transform.position, _transform.forward, out RaycastHit hit, _grabMaxDistance, _collisionMask))
            {
                Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    GrabObject(rb, hit.distance);
                }
            }
        }

        private void GrabObject(Rigidbody target, float distance)
        {
            _targetRB = target;
            _targetDistance = Mathf.Clamp(distance, _grabMinDistance, _grabMaxDistance);
            _grabbing = true;
        }

        private void ReleaseObject()
        {
            if (!_grabbing) return;

            _targetRB = null;
            _grabbing = false;
        }

        private void Update()
        {
            if (!_grabbing) return;

            _targetPos = _transform.position + _transform.forward * _targetDistance;
        }

        private void FixedUpdate()
        {
            if (!_grabbing || _targetRB == null) return;

            Vector3 force = (_targetPos - _targetRB.position) * _grabSpeed;
            _targetRB.velocity = force;
        }
    }
}
