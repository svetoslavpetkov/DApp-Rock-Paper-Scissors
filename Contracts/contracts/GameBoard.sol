pragma solidity ^0.4.19;
contract GameBoard {
    uint constant bidValue = 1000 finney;
    
    enum GameMove{
        Rock,
        Paper,
        Scissors
    }
    
    enum Winner{
        Equal,
        Player1,
        Player2
    }
    
    struct GameCreatedData{
        uint gameID;
        address player1;
        uint8[3] player1Moves;
        uint createdDate;
    }
    
    struct GameCompletedData{
        uint gameID;
        
        address player1;
        uint8[3] player1Moves;
        
        address player2;
        uint8[3] player2Moves;
        
        uint8 winner;
        
        uint createdDate;
        uint completedDate;
    }
    
    modifier isOwner{
        require(msg.sender == owner);
        _;
    }
    
    GameCreatedData[] private createdGamesLog;
    GameCompletedData[] private completedGamesLog;
	mapping(uint => GameCompletedData) private completedGames;
    address owner;
    uint private collectedFees;
    
    function getCollectedFees() public view isOwner returns(uint){
        return(collectedFees);
    }
    
    function withdraw(uint value) public isOwner{
        require(collectedFees >= value);
        owner.transfer(collectedFees);
        collectedFees -= value;
    }
    
    function GameBoard() public{
        owner = msg.sender;
        collectedFees = 0;
    }
    
    function getCompletedGameData(uint gameID) public view 
        returns(address player1, uint8 player1Move0, uint8 player1Move1, uint8 player1Move2, 
                address player2, uint8 player2Move0, uint8 player2Move1, uint8 player2Move2,  
                uint8 winner, uint returnGameID,uint createdDate, uint completedDate){
            GameCompletedData memory gameData = completedGames[gameID];
            require(gameData.player1 != 0x0);
        
        
        return ( player1 = gameData.player1,  player1Move0 = gameData.player1Moves[0],  player1Move1 = gameData.player1Moves[1],  player1Move2 = gameData.player1Moves[2], 
                 player2 = gameData.player2,  player2Move0 = gameData.player2Moves[0],  player2Move1 = gameData.player2Moves[1],  player2Move2 = gameData.player2Moves[2],  
                 winner = gameData.winner, returnGameID = gameData.gameID, createdDate = gameData.createdDate, completedDate = gameData.completedDate);
    }
    
    function getCompletedGamesCount() public view returns(uint){
        return completedGamesLog.length;
    }
    
    function getCompletedGameLog(uint index) public view 
        returns(address player1, uint8 player1Move0, uint8 player1Move1, uint8 player1Move2, 
                address player2, uint8 player2Move0, uint8 player2Move1, uint8 player2Move2,  
                uint8 winner, uint returnGameID,uint createdDate, uint completedDate){
        require(completedGamesLog.length > index);
        
        GameCompletedData memory gameData = completedGamesLog[index];
        return ( player1 = gameData.player1,  player1Move0 = gameData.player1Moves[0],  player1Move1 = gameData.player1Moves[1],  player1Move2 = gameData.player1Moves[2], 
                 player2 = gameData.player2,  player2Move0 = gameData.player2Moves[0],  player2Move1 = gameData.player2Moves[1],  player2Move2 = gameData.player2Moves[2],  
                 winner = gameData.winner, returnGameID = gameData.gameID, createdDate = gameData.createdDate, completedDate = gameData.completedDate);
    }
    
    function placeGameRequest(uint8 move0, uint8 move1, uint8 move2) public payable returns(uint){
        require(msg.value == bidValue);
        require(isValidMove(move0));
        require(isValidMove(move1));
        require(isValidMove(move2));
        
        GameCreatedData memory gameData;
        gameData.gameID = createdGamesLog.length;
        gameData.player1 = tx.origin;
        gameData.player1Moves[0] = move0;
        gameData.player1Moves[1] = move1;
        gameData.player1Moves[2] = move2;
        gameData.createdDate = now;
        
        createdGamesLog.push(gameData);
        
        return gameData.gameID;
    }
    
    function getCreatedGamesCount() public view returns(uint){
        return createdGamesLog.length;
    }
    
    function getCreatedGameData(uint index) public view returns(address player1,uint value,uint createdDate){
        require(createdGamesLog.length > index);
        return (player1 = createdGamesLog[index].player1, value = bidValue, createdDate =createdGamesLog[index].createdDate );
    }
    
    function isValidMove(uint8 move) public pure returns(bool){ return (move <= 2);}
    
    function acceptGameRequest(uint gameID,uint8 move0, uint8 move1, uint8 move2) public payable returns(uint8){
        require(msg.value == bidValue);
        require(createdGamesLog.length > gameID);
        require(isValidMove(move0));
        require(isValidMove(move1));
        require(isValidMove(move2));
        
        GameCreatedData memory createGame = createdGamesLog[gameID];
        
        GameCompletedData memory gameCompletedData;
        gameCompletedData.gameID = gameID;
        gameCompletedData.player1 =  createGame.player1;
        gameCompletedData.player1Moves = createGame.player1Moves;
        gameCompletedData.player2 = tx.origin;
        gameCompletedData.player2Moves[0] = move0;
        gameCompletedData.player2Moves[1] = move1;
        gameCompletedData.player2Moves[2] = move2;
        gameCompletedData.createdDate = createGame.createdDate;
        gameCompletedData.completedDate = now;
        
		uint winner = calculateWinner(gameCompletedData.player1Moves, gameCompletedData.player2Moves);

        if(winner == 0){
            gameCompletedData.winner = 0;
			gameCompletedData.player1.transfer(900 finney);
			gameCompletedData.player2.transfer(900 finney);
			collectedFees += 200 finney;
		}
        else if(winner == 1){
            gameCompletedData.winner = 1;
			gameCompletedData.player1.transfer(1900 finney);
			collectedFees += 100 finney;
		}
        else{
            gameCompletedData.winner = 2;
			gameCompletedData.player2.transfer(1900 finney);
			collectedFees += 100 finney;
		}
        
        completedGamesLog.push(gameCompletedData);
		completedGames[gameID] = gameCompletedData;
		
		return gameCompletedData.winner;
    }
	
	function getWinner(uint gameID) public view returns(uint8){
		require(completedGames[gameID].player1 != 0x0);
		return completedGames[gameID].winner;
	}
    
    function areValidMoves(uint8[5] moves) public pure returns(bool){
        for(uint i=0; i<5;i++){
            if(moves[i] > 2){
                return false;
            }
        }
        return true;
    }
	
	function calculateWinnerWithSixParams(uint8 p1m0, uint8 p1m1, uint8 p1m2, uint8 p2m0, uint8 p2m1, uint8 p2m2) public pure returns(uint8){
		uint8[3] memory player1Moves;
		player1Moves[0] = p1m0;
		player1Moves[1] = p1m1;
		player1Moves[2] = p1m2;
		
		uint8[3] memory player2Moves;
		player2Moves[0] = p2m0;
		player2Moves[1] = p2m1;
		player2Moves[2] = p2m2;
		
		return calculateWinner(player1Moves, player2Moves);
	}
	
	function calculateWinner(uint8[3] player1Moves,uint8[3] player2Moves) public pure returns(uint8){
		uint8 player1Score;
        uint8 player2Score;
        
        for(uint i=0; i<3;i++){
            uint8 roundWin = getWiningMove(player1Moves[i], player2Moves[i]);
            if(roundWin == 1){
                player1Score++;
            }
            if(roundWin == 2){
                player2Score++;
            }
        }
		
		 if(player1Score == player2Score){
            return 0;
		}
        else if(player1Score > player2Score){
            return 1;
		}
        else{
            return 2;
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
}