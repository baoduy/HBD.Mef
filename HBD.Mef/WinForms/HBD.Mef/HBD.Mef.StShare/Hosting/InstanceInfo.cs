using HBD.Framework.Attributes;
using HBD.Framework.Core;
using System;

namespace HBD.Mef.Hosting
{
    internal sealed class InstanceInfo<TInterface> : IEquatable<IInstanceInfo>, IInstanceInfo
    {
        public InstanceInfo([NotNull]Type contractType, [NotNull]Func<TInterface> instanceSelector)
            : this(contractType.FullName, contractType, instanceSelector)
        {

        }

        public InstanceInfo([NotNull]string contractName, [NotNull]Type contractType, [NotNull]Func<TInterface> instanceSelector)
        {
            Guard.ArgumentIsNotNull(contractType, nameof(contractName));
            Guard.ArgumentIsNotNull(contractType, nameof(contractType));
            Guard.ArgumentIsNotNull(instanceSelector, nameof(instanceSelector));

            ContractName = contractName;
            ContractType = contractType;
            InstanceSelector = instanceSelector;
        }

        public string ContractName { get; }
        public Type ContractType { get; }
        public Func<TInterface> InstanceSelector { get; }

        public object Instance => InstanceSelector.Invoke();

        public override int GetHashCode()
            => this.ContractName.GetHashCode() * 379 ^ this.ContractType.GetHashCode() * 379;

        public override bool Equals(object obj)
        {
            if (obj is IInstanceInfo a)
                return this.Equals(a);
            return false;
        }

        public bool Equals(IInstanceInfo other)
            => this.ContractName == other.ContractName
                && this.ContractType == other.ContractType;
    }
}
