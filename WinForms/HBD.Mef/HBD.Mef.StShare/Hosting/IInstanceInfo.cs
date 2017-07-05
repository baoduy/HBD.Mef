using System;

namespace HBD.Mef.Hosting
{
    internal interface IInstanceInfo
    {
        string ContractName { get; }
        Type ContractType { get; }
        object Instance { get; }
    }
}