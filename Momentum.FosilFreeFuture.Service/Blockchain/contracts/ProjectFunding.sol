// SPDX-License-Identifier: MIT
pragma solidity >=0.4.22 <0.9.0;

contract Project {
    
    // enum State {
    //     Fundraising,
    //     Successful
    // }

    // address payable public creator;
    // uint public amountGoal; 
    // uint public completeAt;
    // uint256 public currentBalance;
    // uint public raiseBy;
    // string public title;
    // string public description;
    
    string public name;
    // string public country;
    // string public description;
    // uint256 public fundsNeeded;
    // string public image;
    // string public logo;
    // string public initiated;
    // string public status;

    // State public state = State.Fundraising; 
    // mapping (address => uint) public contributions;

    constructor
    (
        string memory _name
        // string memory _country,
        // string memory _description,
        // uint256 _fundsNeeded,
        // string memory _image,
        // string memory _logo,
        // string memory _initiated,
        // string memory _status
        // string memory projectTitle,
        // string memory projectDesc,
        // uint goalAmount
    ) {
        name = _name;
        // country = _country;
        // description = _description;
        // fundsNeeded = _fundsNeeded;
        // image = _image;
        // logo = _logo;
        // initiated = _initiated;
        // status = _status;
    }
}