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

namespace Blockchain.Contracts.PartnerParticipantContractLedger.ContractDefinition
{


    public partial class PartnerParticipantContractLedgerDeployment : PartnerParticipantContractLedgerDeploymentBase
    {
        public PartnerParticipantContractLedgerDeployment() : base(BYTECODE) { }
        public PartnerParticipantContractLedgerDeployment(string byteCode) : base(byteCode) { }
    }

    public class PartnerParticipantContractLedgerDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "0x";
        public PartnerParticipantContractLedgerDeploymentBase() : base(BYTECODE) { }
        public PartnerParticipantContractLedgerDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class CreateContractFunction : CreateContractFunctionBase { }

    [Function("createContract")]
    public class CreateContractFunctionBase : FunctionMessage
    {
        [Parameter("address", "participantAddress", 1)]
        public virtual string ParticipantAddress { get; set; }
        [Parameter("uint256", "initialAmount", 2)]
        public virtual BigInteger InitialAmount { get; set; }
    }

    public partial class PartnerParticipantContractCreatedEventDTO : PartnerParticipantContractCreatedEventDTOBase { }

    [Event("PartnerParticipantContractCreated")]
    public class PartnerParticipantContractCreatedEventDTOBase : IEventDTO
    {
        [Parameter("address", "partnerAddress", 1, false )]
        public virtual string PartnerAddress { get; set; }
        [Parameter("address", "participantAddress", 2, false )]
        public virtual string ParticipantAddress { get; set; }
        [Parameter("address", "contractAddress", 3, false )]
        public virtual string ContractAddress { get; set; }
    }


}
