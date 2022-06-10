// SPDX-License-Identifier: MIT
pragma solidity >=0.4.22 <0.9.0;

contract PartnerParticipantContractLedger {

    event PartnerParticipantContractCreated(
        address partnerAddress,
        address participantAddress,
        address contractAddress
    );

    PartnerParticipantContract[] private contracts;

    function createContract(
        address participantAddress,
        uint256 initialAmount
    ) external
    {
        PartnerParticipantContract ppContract = new PartnerParticipantContract(msg.sender, participantAddress, initialAmount);
        contracts.push(ppContract);

        emit PartnerParticipantContractCreated(
            msg.sender,
            participantAddress,
            address(ppContract)
        );
    }
}

contract PartnerParticipantContract {
    
    event ParticipantDonated(
        address indexed participantAddress,
        address payable partnerAddress,
        address projectAddress,
        uint256 amount
    );

    address public partnerAddress;
    address public participantAddress;
    uint256 public amountOfCoins;

    constructor(
        address _partnerAddress,
        address _participantAddress,
        uint256 _initialAmount
    )
    {
        partnerAddress = _partnerAddress;
        participantAddress = _participantAddress;
        amountOfCoins = _initialAmount;
    }

    function addCoins() external payable
    {
        require(msg.sender == partnerAddress);

        amountOfCoins += msg.value;
    }

    function donate(address payable projectAddress) external payable returns (bool)
    {
        require(amountOfCoins >= msg.value);

        amountOfCoins -= msg.value;

        projectAddress.transfer(msg.value);

        emit ParticipantDonated(participantAddress, projectAddress, projectAddress, msg.value);

        return true;
    }

    function getContractDetails() public view returns (
        address _partnerAddress,
        address _participantAddress,
        uint256 _amountOfCoins
    ) {
        _partnerAddress = partnerAddress;
        _participantAddress = participantAddress;
        _amountOfCoins = amountOfCoins;
    }
}