// SPDX-License-Identifier: MIT
pragma solidity >=0.4.22 <0.9.0;

contract Project {
    
    // enum State {
    //     Fundraising,
    //     Successful
    // }

    // address payable public creator;
    // uint public amountGoal; 
    // uint256 public currentBalance;
    
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
        image = _image;
        logo = _logo;
        initiated = _initiated;
        status = _status;
    }

    function GetProjectDetails() public view returns 
    (
        string memory name_,
        string memory country_,
        string memory description_,
        uint256 fundsNeeded_,
        string memory image_,
        string memory logo_,
        string memory initiated_,
        string memory status_
    ) {
        name_ = name;
        country_ = country;
        description_ = description;
        fundsNeeded_ = fundsNeeded;
        image_ = image;
        logo_ = logo;
        initiated_ = initiated;
        status_ = status;
    }
}