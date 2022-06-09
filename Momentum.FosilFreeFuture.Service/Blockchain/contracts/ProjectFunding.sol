pragma solidity >=0.4.22 <0.9.0;

// import '@openzeppelin/contracts/utils/math/SafeMath.sol';

contract ProjectFunding {
// using SafeMath for uint256;

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
    // using SafeMath for uint256;
    
    enum State {
        Fundraising,
        Successful
    }

    // address payable public creator;
    // uint public amountGoal; 
    // uint public completeAt;
    // uint256 public currentBalance;
    // uint public raiseBy;
    // string public title;
    // string public description;
    
    string public name;
    string public country;
    string public description;
    uint256 public fundsNeeded;
    string public image;
    string public logo;
    string public initiated;
    string public status;

    // State public state = State.Fundraising; 
    // mapping (address => uint) public contributions;

    constructor
    (
        string storage _name,
        string storage _country,
        string storage _description,
        string storage _fundsNeeded,
        string storage _image,
        string storage _logo,
        string storage _initiated,
        string storage _status
        // string memory projectTitle,
        // string memory projectDesc,
        // uint goalAmount
    ) public {
        name = _name;
        country = _country;
        description = _description;
        fundsNeeded = _fundsNeeded;
        image = _image;
        logo = _logo;
        initiated = _initiated;
        status = _status;
    }
}