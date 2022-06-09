pragma solidity >=0.4.22 <0.9.0;

contract ProjectFunding_dev {
 Project_dev[] public projects;

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
        Project_dev newProject = new Project_dev (payable(msg.sender), title, description, amountToRaise);
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

contract Project_dev {
    
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
}