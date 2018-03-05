pragma solidity ^0.4.20;
import "Destructible.sol";

library Model{
    struct GameData{
        address player1;
        uint8[5] player1Moves;
        
        address player2;
        uint8[5] player2Moves;
    }
    
    function getWinner(GameData storage self) public view returns(uint8){
        uint8 player1Wins;
        uint8 player2Wins;
        
        for(uint i=0; i<5;i++){
            uint8 roundWin = getWiningMove(self.player1Moves[i], self.player2Moves[i]);
            if(roundWin == 1){
                player1Wins++;
            }
            if(roundWin == 2){
                player2Wins++;
            }
        }
    }
    
    function getWiningMove(uint8 player1Move, uint8 player2Move) public pure returns(uint8){
        if(player1Move == player2Move)
            return uint8(0);
        if(
            (player1Move == uint8(GameMove.Rock) && player2Move == uint8(GameMove.Scissors))
            || (player1Move == uint8(GameMove.Paper) && player2Move == uint8(GameMove.Rock))
            || (player1Move == uint8(GameMove.Scissors) && player2Move == uint8(GameMove.Paper))
            ){
            return uint8(1);
        }
        
        return uint8(2);
    } 
    
    enum GameMove{
        Rock,
        Paper,
        Scissors
    }
    
    struct GameRequest{
        uint8[5] playerMoves;
        uint value;
        bool hasValue;
    }
}

contract GameBoard is Destructible{
    using Model for Model.GameData;
    using Model for Model.GameRequest;
    
    uint constant minBidValue = 1 finney;
    
    mapping(address => Model.GameRequest) gameRequests;
    mapping(uint => address) gameRequestsIndex;
    uint gameRequestCount;
    
    event gameRequestEvent(address requester, uint256 createdTime);
    Model.GameData[] public GameArchive;
    
    function placeGameRequest(uint8[5] moves) public payable{
        require(gameRequests[msg.sender].hasValue == false);
        require(msg.value >= minBidValue);
        require(areValidMoves(moves));
        
        Model.GameRequest memory gameRequest;
        gameRequest.playerMoves = moves;
        gameRequest.value = msg.value;
        gameRequest.hasValue = true;
        
        gameRequests[msg.sender] = gameRequest;
        gameRequestsIndex[gameRequestCount] = msg.sender;
        gameRequestCount++;
        
    }
    
    function acceptGameRequest(address otherPlayer, ) public payable{
        
    }
    
    
    function areValidMoves(uint8[5] moves) public pure returns(bool){
        for(uint i=0; i<5;i++){
            if(moves[i] > 2){
                return false;
            }
        }
        return true;
    }
}