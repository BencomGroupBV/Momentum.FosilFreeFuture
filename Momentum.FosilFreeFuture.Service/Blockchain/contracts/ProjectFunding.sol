pragma solidity >=0.4.22 <0.9.0;

import '@openzeppelin/contracts/utils/math/SafeMath.sol';

contract ProjectFunding {
using SafeMath for uint256;

 Project[] public projects;

    event ProjectStarted(
        address contractAddress,
        address projectStarter,
        string projectTitle,
        string projectDesc,
        uint256 goalAmount
    );

    function createProject(
    string calldata title,
    string calldata description,
    uint amountToRaise

    ) external {
        Project newProject = new Project (payable(msg.sender), title, description, amountToRaise);
        projects.push(newProject);
        emit ProjectStarted(
            address(newProject),
            payable(msg.sender),
            title,
            description,
            amountToRaise
        );
    }   

}

contract Project {
    using SafeMath for uint256;
    
    enum State {
        Fundraising,
        Successful
    }

    address payable public creator;
    uint public amountGoal; 
    uint public completeAt;
    uint256 public currentBalance;
    uint public raiseBy;
    string public title;
    string public description;
    State public state = State.Fundraising; 
    mapping (address => uint) public contributions;

    constructor
    (
        address payable projectStarter,
        string memory projectTitle,
        string memory projectDesc,
        uint goalAmount
    ) public {
        creator = projectStarter;
        title = projectTitle;
        description = projectDesc;
        amountGoal = goalAmount;
        currentBalance = 0;
    }

    function getDetails() public view return 
    (
        address payable projectStarter,
        string memory projectTitle,
        string memory projectDesc,
        State currentState,
        uint256 currentAmount,
        uint256 goalAmount
    ) {
        projectStarter = creator;
        projectTitle = title;
        projectDesc = description;
        currentState = state;
        currentAmount = currentBalance;
        goalAmount = amountGoal;
    }
}