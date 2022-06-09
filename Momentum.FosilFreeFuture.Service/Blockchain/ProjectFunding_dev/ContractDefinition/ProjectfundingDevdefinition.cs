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

namespace Blockchain.Contracts.ProjectFunding_dev.ContractDefinition
{


    public partial class ProjectfundingDevDeployment : ProjectfundingDevDeploymentBase
    {
        public ProjectfundingDevDeployment() : base(BYTECODE) { }
        public ProjectfundingDevDeployment(string byteCode) : base(byteCode) { }
    }

    public class ProjectfundingDevDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "0x";
        public ProjectfundingDevDeploymentBase() : base(BYTECODE) { }
        public ProjectfundingDevDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class CreateProjectFunction : CreateProjectFunctionBase { }

    [Function("createProject")]
    public class CreateProjectFunctionBase : FunctionMessage
    {
        [Parameter("string", "title", 1)]
        public virtual string Title { get; set; }
        [Parameter("string", "description", 2)]
        public virtual string Description { get; set; }
        [Parameter("uint256", "amountToRaise", 3)]
        public virtual BigInteger AmountToRaise { get; set; }
    }

    public partial class ProjectsFunction : ProjectsFunctionBase { }

    [Function("projects", "address")]
    public class ProjectsFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class ProjectStartedEventDTO : ProjectStartedEventDTOBase { }

    [Event("ProjectStarted")]
    public class ProjectStartedEventDTOBase : IEventDTO
    {
        [Parameter("address", "contractAddress", 1, false )]
        public virtual string ContractAddress { get; set; }
        [Parameter("address", "projectStarter", 2, false )]
        public virtual string ProjectStarter { get; set; }
        [Parameter("string", "projectTitle", 3, false )]
        public virtual string ProjectTitle { get; set; }
        [Parameter("string", "projectDesc", 4, false )]
        public virtual string ProjectDesc { get; set; }
        [Parameter("uint256", "goalAmount", 5, false )]
        public virtual BigInteger GoalAmount { get; set; }
    }



    public partial class ProjectsOutputDTO : ProjectsOutputDTOBase { }

    [FunctionOutput]
    public class ProjectsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }
}
