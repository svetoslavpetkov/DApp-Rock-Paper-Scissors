pragma solidity ^0.4.19;
contract Simple{
    
    uint public value;
    
    function Simple() public{
        value = 10;
    }
    
    /*event incrementer(address incrementer, uint value);*/
    event incrementers(address incrementer, uint value);
    
    function increment(uint a) public{
        value += a;
		incrementers(msg.sender,a);
    }
    
    function getValue() public view returns(uint){
        return value;
    }
    
}