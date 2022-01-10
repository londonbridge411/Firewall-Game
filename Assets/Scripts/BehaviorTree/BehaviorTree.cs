using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bowen.BehaviorTree
{
    public class BehaviorTree : MonoBehaviour
    {
        public BTNode root { get; private set; }

        public Dictionary<string, object> Blackboard;
        private bool startedBehavior;
        private Coroutine behavior;

        // Start is called before the first frame update
        void Start()
        {
            Blackboard = new Dictionary<string, object>();
            Blackboard.Add("WorldBounds", new Rect(0, 0, 5, 5));
            root.Evaluate();

            startedBehavior = false;

            root = new BTNode(this);
            BTNode abc = new Sequence(this, new BTNode[] { new Node1(this) });
        }

        // Update is called once per frame
        void Update()
        {
            if (!startedBehavior)
            {
                behavior = StartCoroutine(RunBehavior());
            }
        }

        IEnumerator RunBehavior()
        {
            NodeState state = root.Evaluate();

            while (state == NodeState.RUNNING)
            {
                print("Root State: " + state);
                yield return null;
                root.Evaluate();
            }

            print("Behavior has finished with " + state);
        }
    }
}
