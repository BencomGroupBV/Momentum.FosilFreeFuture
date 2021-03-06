// SPDX-License-Identifier: MIT
pragma solidity >=0.5.4 <0.9.0;

contract ProjectFunding {
    struct ProjectDetails {
        string name;
        string description;
        string country;
    }

    // List of existing projects
    Project[] private projects;

    event ProjectCreated(
        address contractAddress
    );

    function createProject(
        ProjectDetails memory details,
        uint256 fundsNeeded,
        string calldata image,
        string calldata logo,
        string calldata initiated,
        string calldata status  // Enum?
    ) external {
        Project newProject = new Project(
            details.name,
            details.country,
            details.description,
            fundsNeeded,
            image,
            logo,
            initiated,
            status
        );
        projects.push(newProject);
        emit ProjectCreated(
            address(newProject)
        );
    }

    function returnAllProjects() external view returns(Project[] memory){
        return projects;
    }

}

contract Project {
    event ProjectApproved(
        address indexed partnerAddress,
        address projectAddress
    );

    event ProjectCompleted(
        address projectAddress
    );

    address public intiator;
    string public name;
    string public country;
    string public description;
    uint256 public fundsNeeded;
    uint256 public fundsReceived;
    string public image;
    string public logo;
    string public initiated;
    string public status;

    constructor
    (
        string memory _name,
        string memory _country,
        string memory _description,
        uint256 _fundsNeeded,
        string memory _image,
        string memory _logo,
        string memory _initiated,
        string memory _status
    ) {
        name = _name;
        country = _country;
        description = _description;
        fundsNeeded = _fundsNeeded;
        fundsReceived = 0;
        image = _image;
        logo = _logo;
        initiated = _initiated;
        status = _status;
    }

    function GetProjectDetails() public view returns 
    (
        address projectAddress,
        string memory projectName,
        string memory projectCountry,
        string memory projectDescription,
        uint256 projectFundsNeeded,
        string memory projectImage,
        string memory projectLogo,
        string memory projectInitiated,
        string memory projectStatus
    ) {
        projectAddress = address(this);
        projectName = name;
        projectCountry = country;
        projectDescription = description;
        projectFundsNeeded = fundsNeeded;
        projectImage = image;
        projectLogo = logo;
        projectInitiated = initiated;
        projectStatus = status;
    }

    function approveProject() public {
        emit ProjectApproved(msg.sender, address(this));
    }

    receive() external payable {
        fundsReceived += msg.value;

        if (fundsReceived >= fundsNeeded) {
            emit ProjectCompleted(address(this));
        }
    }
}