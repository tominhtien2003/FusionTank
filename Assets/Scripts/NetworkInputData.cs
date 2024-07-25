using Fusion;
using UnityEngine;

public struct NetworkInputData : INetworkInput
{
    public Vector3 direction;
    public const byte MOUSELEFT = 1;
    public NetworkButtons buttons;
}
