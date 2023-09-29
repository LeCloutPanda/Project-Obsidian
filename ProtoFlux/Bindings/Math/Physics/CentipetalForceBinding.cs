﻿using Elements.Core;
using FrooxEngine;
using FrooxEngine.ProtoFlux;
using FrooxEngine.ProtoFlux.Runtimes.Execution;
using ProtoFlux.Core;
using ProtoFlux.Runtimes.Execution;
using ProtoFlux.Runtimes.Execution.Nodes.Obsidian.Math.Physics;
using System;

[Category(new string[] { "ProtoFlux/Runtimes/Execution/Nodes/Obsidian/Math/Physics" })]
public class CentripetalForceCalculation : FrooxEngine.ProtoFlux.Runtimes.Execution.ValueFunctionNode<ExecutionContext, floatQ>
{
    public readonly SyncRef<INodeValueOutput<float>> Mass;
    public readonly SyncRef<INodeValueOutput<float>> Velocity;
    public readonly SyncRef<INodeValueOutput<float>> Radius;

    // Use the correct NodeType and NodeInstance properties
    public override Type NodeType => typeof(CentripetalForceCalculationNode);
    public CentripetalForceCalculationNode TypedNodeInstance { get; private set; }

    public override INode NodeInstance => TypedNodeInstance;

    public override int NodeInputCount => base.NodeInputCount + 3;

    public override N Instantiate<N>()
    {
        if (TypedNodeInstance != null)
        {
            throw new InvalidOperationException("Node has already been instantiated");
        }
        CentripetalForceCalculationNode instance = (TypedNodeInstance = new CentripetalForceCalculationNode());
        return instance as N;
    }

    protected override void AssociateInstanceInternal(INode node)
    {
        if (node is CentripetalForceCalculationNode typedNodeInstance)
        {
            TypedNodeInstance = typedNodeInstance;
            return;
        }
        throw new ArgumentException("Node instance is not of type " + typeof(CentripetalForceCalculationNode));
    }

    public override void ClearInstance()
    {
        TypedNodeInstance = null;
    }

    protected override ISyncRef GetInputInternal(ref int index)
    {
        ISyncRef inputInternal = base.GetInputInternal(ref index);
        if (inputInternal != null)
        {
            return inputInternal;
        }
        switch (index)
        {
            case 0:
                return Mass;
            case 1:
                return Velocity;
            case 2:
                return Radius;
            default:
                index -= 3;
                return null;
        }
    }
}
