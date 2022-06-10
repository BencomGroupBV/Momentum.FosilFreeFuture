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
        [Parameter("string", "_country", 2)]
        public virtual string Country { get; set; }
        [Parameter("string", "_description", 3)]
        public virtual string Description { get; set; }
        [Parameter("uint256", "_fundsNeeded", 4)]
        public virtual BigInteger FundsNeeded { get; set; }
        [Parameter("string", "_image", 5)]
        public virtual string Image { get; set; }
        [Parameter("string", "_logo", 6)]
        public virtual string Logo { get; set; }
        [Parameter("string", "_initiated", 7)]
        public virtual string Initiated { get; set; }
        [Parameter("string", "_status", 8)]
        public virtual string Status { get; set; }
    }

    public partial class GetProjectDetailsFunction : GetProjectDetailsFunctionBase { }

    [Function("GetProjectDetails", typeof(GetProjectDetailsOutputDTO))]
    public class GetProjectDetailsFunctionBase : FunctionMessage
    {

    }

    public partial class ApproveProjectFunction : ApproveProjectFunctionBase { }

    [Function("approveProject")]
    public class ApproveProjectFunctionBase : FunctionMessage
    {

    }

    public partial class CountryFunction : CountryFunctionBase { }

    [Function("country", "string")]
    public class CountryFunctionBase : FunctionMessage
    {

    }

    public partial class DescriptionFunction : DescriptionFunctionBase { }

    [Function("description", "string")]
    public class DescriptionFunctionBase : FunctionMessage
    {

    }

    public partial class FundsNeededFunction : FundsNeededFunctionBase { }

    [Function("fundsNeeded", "uint256")]
    public class FundsNeededFunctionBase : FunctionMessage
    {

    }

    public partial class FundsReceivedFunction : FundsReceivedFunctionBase { }

    [Function("fundsReceived", "uint256")]
    public class FundsReceivedFunctionBase : FunctionMessage
    {

    }

    public partial class ImageFunction : ImageFunctionBase { }

    [Function("image", "string")]
    public class ImageFunctionBase : FunctionMessage
    {

    }

    public partial class InitiatedFunction : InitiatedFunctionBase { }

    [Function("initiated", "string")]
    public class InitiatedFunctionBase : FunctionMessage
    {

    }

    public partial class LogoFunction : LogoFunctionBase { }

    [Function("logo", "string")]
    public class LogoFunctionBase : FunctionMessage
    {

    }

    public partial class NameFunction : NameFunctionBase { }

    [Function("name", "string")]
    public class NameFunctionBase : FunctionMessage
    {

    }

    public partial class StatusFunction : StatusFunctionBase { }

    [Function("status", "string")]
    public class StatusFunctionBase : FunctionMessage
    {

    }

    public partial class ProjectApprovedEventDTO : ProjectApprovedEventDTOBase { }

    [Event("ProjectApproved")]
    public class ProjectApprovedEventDTOBase : IEventDTO
    {
        [Parameter("address", "projectAddress", 1, false )]
        public virtual string ProjectAddress { get; set; }
    }

    public partial class ProjectCompletedEventDTO : ProjectCompletedEventDTOBase { }

    [Event("ProjectCompleted")]
    public class ProjectCompletedEventDTOBase : IEventDTO
    {
        [Parameter("address", "projectAddress", 1, false )]
        public virtual string ProjectAddress { get; set; }
    }

    public partial class GetProjectDetailsOutputDTO : GetProjectDetailsOutputDTOBase { }

    [FunctionOutput]
    public class GetProjectDetailsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "projectName", 1)]
        public virtual string ProjectName { get; set; }
        [Parameter("string", "projectCountry", 2)]
        public virtual string ProjectCountry { get; set; }
        [Parameter("string", "projectDescription", 3)]
        public virtual string ProjectDescription { get; set; }
        [Parameter("uint256", "projectFundsNeeded", 4)]
        public virtual BigInteger ProjectFundsNeeded { get; set; }
        [Parameter("string", "projectImage", 5)]
        public virtual string ProjectImage { get; set; }
        [Parameter("string", "projectLogo", 6)]
        public virtual string ProjectLogo { get; set; }
        [Parameter("string", "projectInitiated", 7)]
        public virtual string ProjectInitiated { get; set; }
        [Parameter("string", "projectStatus", 8)]
        public virtual string ProjectStatus { get; set; }
    }



    public partial class CountryOutputDTO : CountryOutputDTOBase { }

    [FunctionOutput]
    public class CountryOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class DescriptionOutputDTO : DescriptionOutputDTOBase { }

    [FunctionOutput]
    public class DescriptionOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class FundsNeededOutputDTO : FundsNeededOutputDTOBase { }

    [FunctionOutput]
    public class FundsNeededOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class FundsReceivedOutputDTO : FundsReceivedOutputDTOBase { }

    [FunctionOutput]
    public class FundsReceivedOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class ImageOutputDTO : ImageOutputDTOBase { }

    [FunctionOutput]
    public class ImageOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class InitiatedOutputDTO : InitiatedOutputDTOBase { }

    [FunctionOutput]
    public class InitiatedOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class LogoOutputDTO : LogoOutputDTOBase { }

    [FunctionOutput]
    public class LogoOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class NameOutputDTO : NameOutputDTOBase { }

    [FunctionOutput]
    public class NameOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class StatusOutputDTO : StatusOutputDTOBase { }

    [FunctionOutput]
    public class StatusOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }
}
