using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActiveRagdollBone : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer _targetSkeleton;
    [SerializeField] float _linearStiffness = 1200;
    [SerializeField] float _linearDamping = 40;

    [SerializeField] float _angularStiffness = 4000;
    [SerializeField] float _angularDamping = 80;

    [SerializeField]
    List<Rigidbody> _bones = new();
    [SerializeField]
    List<Transform> _targetBones = new();

    void Start()
    {
        foreach (var joint in transform.GetComponentsInChildren<CharacterJoint>())
        {
            _bones.Add(joint.GetComponent<Rigidbody>());

            if (_targetSkeleton.bones.Any(b => b.name.Equals(joint.name)))
                _targetBones.Add(_targetSkeleton.bones.Where((b => b.name.Equals(joint.name))).FirstOrDefault());
        }
    }

    void FixedUpdate()
    {
        for (int i = 0; i < _bones.Count; i++)
        {
            var targBone = _targetBones[i];
            var bone = _bones[i];

            var posDiff = targBone.position - bone.position;
            var force = HookesLaw(posDiff, bone.velocity, _linearStiffness, _linearDamping);
            bone.AddForce(force);

            var rotDiff = targBone.localRotation * bone.transform.localRotation;
            var torque = HookesLaw(rotDiff.eulerAngles, bone.angularVelocity, _angularStiffness, _angularDamping);
            bone.AddTorque(torque);
        }
    }

    Vector3 HookesLaw(Vector3 displacement, Vector3 currentVelocity, float stiffness, float damping) => (stiffness * displacement) - (damping * currentVelocity);
}
