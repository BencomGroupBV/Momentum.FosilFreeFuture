using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace Blockchain.Contracts.Project.ContractDefinition
{


    public partial class ProjectDeployment : ProjectDeploymentBase
    {
        public ProjectDeployment() : base(BYTECODE) { }
        public ProjectDeployment(string byteCode) : base(byteCode) { }
    }

    public class ProjectDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "0x";
        public ProjectDeploymentBase() : base(BYTECODE) { }
        public ProjectDeploymentBase(string byteCode) : base(byteCode) { }
        [Parameter("string", "_name", 1)]
        public virtual string Name { get; set; }
    }

    public partial class NameFunction : NameFunctionBase { }

    [Function("name", "string")]
    public class NameFunctionBase : FunctionMessage
    {

    }

    public partial class NameOutputDTO : NameOutputDTOBase { }

    [FunctionOutput]
    public class NameOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }
}
