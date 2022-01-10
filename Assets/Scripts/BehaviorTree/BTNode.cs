using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bowen.BehaviorTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public class BTNode
    {
        public NodeState nodeState { get; private set; }
        public BTNode parent;
        protected BTNode[] children;

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public BTNode()
        {
            parent = null;
        }

        public BTNode(BehaviorTree tree)
        {
            parent = tree.root;
        }
    }

    public class Decorator : BTNode
    {
        private BTNode child;

        public Decorator(BehaviorTree tree, BTNode child) : base(tree)
        {
            this.child = child;
        }
    }

    public class Composite : BTNode
    {
        public List<BTNode> Children;

        public Composite(BehaviorTree tree, BTNode[] children) : base(tree)
        {
            Children = new List<BTNode>(children);
        }
    }

    public class Sequence : Composite
    {
        private int currentNode = 0; 

        public Sequence(BehaviorTree tree, BTNode[] children) : base(tree, children)
        {

        }

        public override NodeState Evaluate()
        {
            if (currentNode < Children.Count)
            {
                NodeState state = children[currentNode].Evaluate();

                if (state == NodeState.RUNNING)
                {
                    currentNode = 0;
                    return NodeState.RUNNING;
                }
                else
                {
                    currentNode++;
                    if (currentNode < Children.Count)
                        return NodeState.RUNNING;
                    else
                    {
                        currentNode = 0;
                        return NodeState.SUCCESS;
                    }
                }
            }

            return NodeState.SUCCESS;
        }
    }

    public class Selector : Composite
    {
        private int currentNode = 0;

        public Selector(BehaviorTree tree, BTNode[] children) : base(tree, children)
        {

        }

        public override NodeState Evaluate()
        {
            if (currentNode < Children.Count)
            {
                NodeState state = children[currentNode].Evaluate();

                if (state == NodeState.RUNNING)
                {
                    currentNode = 0;
                    return NodeState.RUNNING;
                }
                else
                {
                    currentNode++;
                    if (currentNode < Children.Count)
                        return NodeState.RUNNING;
                    else
                    {
                        currentNode = 0;
                        return NodeState.SUCCESS;
                    }
                }
            }

            return NodeState.SUCCESS;
        }
    }

    public class Repeater : Decorator
    {
        public Repeater(BehaviorTree tree, BTNode child) : base(tree, child)
        {

        }

        public override NodeState Evaluate()
        {
            return NodeState.RUNNING;
        }
    }

    public class Node1 : BTNode
    {
        public Node1(BehaviorTree tree)
        {
            parent = tree.root;
        }

        public override NodeState Evaluate()
        {
            Debug.Log(1);
            return NodeState.RUNNING;
        }


    }
}