pragma solidity ^0.4.19;
contract Simple{
    
    struct EventData{
        address incrementer;
        uint value;
    }
    
    uint public value;
    EventData[] events;
    
    function Simple() public{
        value = 10;
    }
    
    /*event incrementer(address incrementer, uint value);*/
    event incrementers(address incrementer, uint value);
    
    function increment(uint a) public{
        value += a;
		incrementers(msg.sender,a);
		
		EventData memory data;
		data.incrementer = msg.sender;
		data.value = a;
		
		events.push(data);
    }
    
    function getValue() public view returns(uint){
        return value;
    }
    
    function getEventLenght() public view returns(uint){
        return events.length;
    }
    
    function getEvent(uint index) public view returns(address,uint){
        EventData data = events[index];
        
        return (data.incrementer,data.value);
    }
    
}