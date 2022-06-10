using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Blockchain.Contracts.ProjectFunding.ContractDefinition
{
    public partial class ProjectDetails : ProjectDetailsBase { }

    public class ProjectDetailsBase 
    {
        [Parameter("string", "name", 1)]
        public virtual string Name { get; set; }
        [Parameter("string", "description", 2)]
        public virtual string Description { get; set; }
        [Parameter("string", "country", 3)]
        public virtual string Country { get; set; }
    }
}
