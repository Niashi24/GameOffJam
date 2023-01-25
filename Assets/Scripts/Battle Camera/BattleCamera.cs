using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SaturnRPG.Battle.Camera
{
    public class BattleCamera : MonoBehaviour
    {
        [Header("Restrictions")]

        [SerializeField]
        Vector2 _minMaxX;
        [SerializeField]
        Vector2 _minMaxY;
        [SerializeField]
        Vector2 _minMaxZ;

        [SerializeField]
        [Tooltip("Offset from Target. Y-Value is Ignored")]
        Vector3 _targetOffset;

        [SerializeField]
        float _smoothTime = 0.3f;

        [ShowInInspector, ReadOnly]
        private Vector3 vel = Vector2.zero;

        [ShowInInspector, ReadOnly]
        private Vector3 targetPos;

        void Start()
        {
            targetPos = transform.localPosition;
        }

        void Update()
        {
            transform.localPosition = Vector3.SmoothDamp(
                transform.localPosition,
                targetPos,
                ref vel,
                _smoothTime
            );
        }

        [Button]
        [DisableInEditorMode]
        public void SetTargetLocalPosition(Vector3 position)
        {
            position += _targetOffset;
            targetPos = new Vector3(
                Mathf.Clamp(position.x, _minMaxX.x, _minMaxX.y),
                Mathf.Clamp(position.y, _minMaxY.x, _minMaxY.y),
                Mathf.Clamp(position.z, _minMaxZ.x, _minMaxZ.y)
            );
        }

        [Button]
        [DisableInEditorMode]
        public void SetTargetWorldPosition(Vector3 position)
        {
            SetTargetLocalPosition(position - transform.parent.position);
        }

        [Button]
        [DisableInEditorMode]
        public void SetTargetTransform(Transform target)
        {
            if (target == null) return;

            // Debug.Log($"Set Target to \"{target.name}\"", this);

            SetTargetLocalPosition(target.position - transform.parent.position);
        }

        public void SetTargetTransforms(IEnumerable<Transform> targets)
        {
            Vector3 positionSum = Vector3.zero;
            int num = 0;

            foreach (var target in targets)
            {
                positionSum += target.position;
                num++;
            }

            Vector3 averagePosition = positionSum / num;

            SetTargetWorldPosition(averagePosition);
        }
    }
}
