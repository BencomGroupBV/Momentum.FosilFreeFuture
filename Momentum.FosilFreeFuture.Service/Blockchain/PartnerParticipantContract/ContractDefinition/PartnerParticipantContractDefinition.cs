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

namespace Blockchain.Contracts.PartnerParticipantContract.ContractDefinition
{


    public partial class PartnerParticipantContractDeployment : PartnerParticipantContractDeploymentBase
    {
        public PartnerParticipantContractDeployment() : base(BYTECODE) { }
        public PartnerParticipantContractDeployment(string byteCode) : base(byteCode) { }
    }

    public class PartnerParticipantContractDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "0x";
        public PartnerParticipantContractDeploymentBase() : base(BYTECODE) { }
        public PartnerParticipantContractDeploymentBase(string byteCode) : base(byteCode) { }
        [Parameter("address", "_partnerAddress", 1)]
        public virtual string PartnerAddress { get; set; }
        [Parameter("address", "_participantAddress", 2)]
        public virtual string ParticipantAddress { get; set; }
        [Parameter("uint256", "_initialAmount", 3)]
        public virtual BigInteger InitialAmount { get; set; }
    }

    public partial class AddCoinsFunction : AddCoinsFunctionBase { }

    [Function("addCoins")]
    public class AddCoinsFunctionBase : FunctionMessage
    {

    }

    public partial class AmountOfCoinsFunction : AmountOfCoinsFunctionBase { }

    [Function("amountOfCoins", "uint256")]
    public class AmountOfCoinsFunctionBase : FunctionMessage
    {

    }

    public partial class DonateFunction : DonateFunctionBase { }

    [Function("donate", "bool")]
    public class DonateFunctionBase : FunctionMessage
    {
        [Parameter("address", "projectAddress", 1)]
        public virtual string ProjectAddress { get; set; }
    }

    public partial class GetContractDetailsFunction : GetContractDetailsFunctionBase { }

    [Function("getContractDetails", typeof(GetContractDetailsOutputDTO))]
    public class GetContractDetailsFunctionBase : FunctionMessage
    {

    }

    public partial class ParticipantAddressFunction : ParticipantAddressFunctionBase { }

    [Function("participantAddress", "address")]
    public class ParticipantAddressFunctionBase : FunctionMessage
    {

    }

    public partial class PartnerAddressFunction : PartnerAddressFunctionBase { }

    [Function("partnerAddress", "address")]
    public class PartnerAddressFunctionBase : FunctionMessage
    {

    }

    public partial class ParticipantDonatedEventDTO : ParticipantDonatedEventDTOBase { }

    [Event("ParticipantDonated")]
    public class ParticipantDonatedEventDTOBase : IEventDTO
    {
        [Parameter("address", "participantAddress", 1, true )]
        public virtual string ParticipantAddress { get; set; }
        [Parameter("address", "partnerAddress", 2, false )]
        public virtual string PartnerAddress { get; set; }
        [Parameter("address", "projectAddress", 3, false )]
        public virtual string ProjectAddress { get; set; }
        [Parameter("uint256", "amount", 4, false )]
        public virtual BigInteger Amount { get; set; }
    }



    public partial class AmountOfCoinsOutputDTO : AmountOfCoinsOutputDTOBase { }

    [FunctionOutput]
    public class AmountOfCoinsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }



    public partial class GetContractDetailsOutputDTO : GetContractDetailsOutputDTOBase { }

    [FunctionOutput]
    public class GetContractDetailsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "_partnerAddress", 1)]
        public virtual string PartnerAddress { get; set; }
        [Parameter("address", "_participantAddress", 2)]
        public virtual string ParticipantAddress { get; set; }
        [Parameter("uint256", "_amountOfCoins", 3)]
        public virtual BigInteger AmountOfCoins { get; set; }
    }

    public partial class ParticipantAddressOutputDTO : ParticipantAddressOutputDTOBase { }

    [FunctionOutput]
    public class ParticipantAddressOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class PartnerAddressOutputDTO : PartnerAddressOutputDTOBase { }

    [FunctionOutput]
    public class PartnerAddressOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }
}
