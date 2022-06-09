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

namespace Blockchain.Contracts.Project_dev.ContractDefinition
{


    public partial class ProjectDevDeployment : ProjectDevDeploymentBase
    {
        public ProjectDevDeployment() : base(BYTECODE) { }
        public ProjectDevDeployment(string byteCode) : base(byteCode) { }
    }

    public class ProjectDevDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "0x";
        public ProjectDevDeploymentBase() : base(BYTECODE) { }
        public ProjectDevDeploymentBase(string byteCode) : base(byteCode) { }
        [Parameter("address", "projectStarter", 1)]
        public virtual string ProjectStarter { get; set; }
        [Parameter("string", "projectTitle", 2)]
        public virtual string ProjectTitle { get; set; }
        [Parameter("string", "projectDesc", 3)]
        public virtual string ProjectDesc { get; set; }
        [Parameter("uint256", "goalAmount", 4)]
        public virtual BigInteger GoalAmount { get; set; }
    }

    public partial class AmountGoalFunction : AmountGoalFunctionBase { }

    [Function("amountGoal", "uint256")]
    public class AmountGoalFunctionBase : FunctionMessage
    {

    }

    public partial class CompleteAtFunction : CompleteAtFunctionBase { }

    [Function("completeAt", "uint256")]
    public class CompleteAtFunctionBase : FunctionMessage
    {

    }

    public partial class ContributionsFunction : ContributionsFunctionBase { }

    [Function("contributions", "uint256")]
    public class ContributionsFunctionBase : FunctionMessage
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class CreatorFunction : CreatorFunctionBase { }

    [Function("creator", "address")]
    public class CreatorFunctionBase : FunctionMessage
    {

    }

    public partial class CurrentBalanceFunction : CurrentBalanceFunctionBase { }

    [Function("currentBalance", "uint256")]
    public class CurrentBalanceFunctionBase : FunctionMessage
    {

    }

    public partial class DescriptionFunction : DescriptionFunctionBase { }

    [Function("description", "string")]
    public class DescriptionFunctionBase : FunctionMessage
    {

    }

    public partial class RaiseByFunction : RaiseByFunctionBase { }

    [Function("raiseBy", "uint256")]
    public class RaiseByFunctionBase : FunctionMessage
    {

    }

    public partial class StateFunction : StateFunctionBase { }

    [Function("state", "uint8")]
    public class StateFunctionBase : FunctionMessage
    {

    }

    public partial class TitleFunction : TitleFunctionBase { }

    [Function("title", "string")]
    public class TitleFunctionBase : FunctionMessage
    {

    }

    public partial class AmountGoalOutputDTO : AmountGoalOutputDTOBase { }

    [FunctionOutput]
    public class AmountGoalOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class CompleteAtOutputDTO : CompleteAtOutputDTOBase { }

    [FunctionOutput]
    public class CompleteAtOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class ContributionsOutputDTO : ContributionsOutputDTOBase { }

    [FunctionOutput]
    public class ContributionsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class CreatorOutputDTO : CreatorOutputDTOBase { }

    [FunctionOutput]
    public class CreatorOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class CurrentBalanceOutputDTO : CurrentBalanceOutputDTOBase { }

    [FunctionOutput]
    public class CurrentBalanceOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class DescriptionOutputDTO : DescriptionOutputDTOBase { }

    [FunctionOutput]
    public class DescriptionOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class RaiseByOutputDTO : RaiseByOutputDTOBase { }

    [FunctionOutput]
    public class RaiseByOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class StateOutputDTO : StateOutputDTOBase { }

    [FunctionOutput]
    public class StateOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint8", "", 1)]
        public virtual byte ReturnValue1 { get; set; }
    }

    public partial class TitleOutputDTO : TitleOutputDTOBase { }

    [FunctionOutput]
    public class TitleOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }
}
