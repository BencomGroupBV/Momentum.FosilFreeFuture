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

namespace Blockchain.Contracts.ProjectFunding.ContractDefinition
{


    public partial class ProjectFundingDeployment : ProjectFundingDeploymentBase
    {
        public ProjectFundingDeployment() : base(BYTECODE) { }
        public ProjectFundingDeployment(string byteCode) : base(byteCode) { }
    }

    public class ProjectFundingDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "0x";
        public ProjectFundingDeploymentBase() : base(BYTECODE) { }
        public ProjectFundingDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class CreateProjectFunction : CreateProjectFunctionBase { }

    [Function("createProject")]
    public class CreateProjectFunctionBase : FunctionMessage
    {
        [Parameter("tuple", "details", 1)]
        public virtual ProjectDetails Details { get; set; }
        [Parameter("uint256", "fundsNeeded", 2)]
        public virtual BigInteger FundsNeeded { get; set; }
        [Parameter("string", "image", 3)]
        public virtual string Image { get; set; }
        [Parameter("string", "logo", 4)]
        public virtual string Logo { get; set; }
        [Parameter("string", "initiated", 5)]
        public virtual string Initiated { get; set; }
        [Parameter("string", "status", 6)]
        public virtual string Status { get; set; }
    }

    public partial class ReturnAllProjectsFunction : ReturnAllProjectsFunctionBase { }

    [Function("returnAllProjects", "address[]")]
    public class ReturnAllProjectsFunctionBase : FunctionMessage
    {

    }

    public partial class ProjectCreatedEventDTO : ProjectCreatedEventDTOBase { }

    [Event("ProjectCreated")]
    public class ProjectCreatedEventDTOBase : IEventDTO
    {
        [Parameter("address", "contractAddress", 1, false )]
        public virtual string ContractAddress { get; set; }
    }



    public partial class ReturnAllProjectsOutputDTO : ReturnAllProjectsOutputDTOBase { }

    [FunctionOutput]
    public class ReturnAllProjectsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address[]", "", 1)]
        public virtual List<string> ReturnValue1 { get; set; }
    }
}
