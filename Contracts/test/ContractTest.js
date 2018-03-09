const GameBoard = artifacts.require('./GameBoard.sol');

contract('GameBoard', function (accounts) {
	let instance; 
	
	const _owner = accounts[0];
	const _campaignAcc = accounts[1]; 
	const _player1 = accounts[2]; 
	const _player2 = accounts[3]; 
	
	const oneFinney = 10 ** 15;
	const oneEther = 10 ** 18;
	const bidValue = 10 ** 17;
	
	console.log(_owner);
	
	 beforeEach(async function (){
	  instance = await GameBoard.new({ from: _owner});
	 });
		  
	 
	 
	 it("there should be no games", async function(){
	  let gamesCount = await instance.getCreatedGamesCount();
	  assert.strictEqual(gamesCount.toNumber(), 0, "gamesCount should be 0");  
	 })	 

	 it("place one game", async function () { 
		  
		 await instance.placeGameRequest(0, 0,0, {from: _player1, value: bidValue});	

		await instance.placeGameRequest(0, 0,0, {from: _player2, value: bidValue});		 
		 
		 let gamesCount = await instance.getCreatedGamesCount();
		 assert.strictEqual(gamesCount.toNumber(),2, "gamesCount should be 2");
		
		 [address, gameValue] = await instance.getCreatedGameData(0);
		 assert.strictEqual(gameValue.toNumber(), bidValue, "bid value should be ");
		 assert.strictEqual(address, _player1, "bid value should be ");		 
		 
		 [address, gameValue] = await instance.getCreatedGameData(1);
		 assert.strictEqual(address, _player2, "bid value should be ");	
	 });
	 
	 
	 it("play one out of many games", async function () { 		 
		 await instance.placeGameRequest(0,0,0, {from: _player1, value: bidValue});		
		 await instance.placeGameRequest(0,0,0, {from: _player1, value: bidValue});		
		 await instance.placeGameRequest(0,0,0, {from: _player1, value: bidValue});		
		
		 await instance.acceptGameRequest(2,1,1,1, {from: _player2, value: bidValue});
		 
		 let winner = await instance.getWinner(2);
		 assert.strictEqual(winner.toNumber(), 2, "winner should be player 2 ");
		 
		 let completedLogCount = await instance.getCompletedGamesCount();
		 assert.strictEqual(completedLogCount.toNumber(), 1, "winner should be player 2 ");
		 
		 /*[totalNeeded, collectedUntilNow, giversCount]  = await  instance.campaignDetails(0);
		  
		 assert.strictEqual(collectedUntilNow.toNumber(), 4 * oneEther, " should be 4 ethers collected");
		 assert.strictEqual(giversCount.toNumber(), 2, " should be 2 donators");*/
	 });
	 
	 it("test compare", async function () { 		 
		//Rock
		let winner = await instance.getWiningMove(0,0);	
		assert.strictEqual(winner.toNumber(), 0, "winner should be equal");
		
		winner = await instance.getWiningMove(0,1);	
		assert.strictEqual(winner.toNumber(), 2, "winner should be equal");
		
		winner = await instance.getWiningMove(0,2);	
		assert.strictEqual(winner.toNumber(), 1, "winner should be equal");
		
		//Paper
		winner = await instance.getWiningMove(1,0);	
		assert.strictEqual(winner.toNumber(), 1, "winner should be equal");
		
		winner = await instance.getWiningMove(1,1);	
		assert.strictEqual(winner.toNumber(), 0, "winner should be equal");
		
		winner = await instance.getWiningMove(1,2);	
		assert.strictEqual(winner.toNumber(), 2, "winner should be equal");
		
		//Scissors
		winner = await instance.getWiningMove(2,0);	
		assert.strictEqual(winner.toNumber(), 2, "winner should be equal");
		
		winner = await instance.getWiningMove(2,1);	
		assert.strictEqual(winner.toNumber(), 1, "winner should be equal");
		
		winner = await instance.getWiningMove(2,2);	
		assert.strictEqual(winner.toNumber(), 0, "winner should be equal");
	 });
	 
	 
	 //calculateWinner
	 
	 it("test calculateWinner", async function () { 
		
		let winner = await instance.calculateWinnerWithSixParams(0,0,0,1,1,1);			
		assert.strictEqual(winner.toNumber(), 2, "winner should be 2");
		
		winner = await instance.calculateWinnerWithSixParams(0,1,2,2,0,1);			
		assert.strictEqual(winner.toNumber(), 1, "winner should be 1");
		
		winner = await instance.calculateWinnerWithSixParams(0,1,0,2,2,0);			
		assert.strictEqual(winner.toNumber(), 0, "winner should be equal");						
	 });
	 
	 it("get game data", async function () { 		 
		 await instance.placeGameRequest(0,0,0, {from: _player1, value: bidValue});		
		 await instance.placeGameRequest(0,0,0, {from: _player1, value: bidValue});		
		 await instance.placeGameRequest(0,1,2, {from: _player1, value: bidValue});		
		
		 await instance.acceptGameRequest(2,2,0,1, {from: _player2, value: bidValue});		 
		 
		 let completedLogCount = await instance.getCompletedGamesCount();
		 assert.strictEqual(completedLogCount.toNumber(), 1, "winner should be player 2 ");
		 
		 let fees = await instance.getCollectedFees();
		
		 
		 [player1, player1Move0, player1Move1,  player1Move2,  player2,  player2Move0,  player2Move1,  player2Move2, winner] =  await instance.getCompletedGameLog(0);
		 
		assert.strictEqual(player1, _player1, " should be equal players");
		assert.strictEqual(player1Move0.toNumber(), 0, " should be equal players");
		assert.strictEqual(player1Move1.toNumber(), 1, " should be equal players");
		assert.strictEqual(player1Move2.toNumber(), 2, " should be equal players");
		
		assert.strictEqual(player2, _player2, " should be equal players");
		assert.strictEqual(player2Move0.toNumber(), 2, " should be equal players");
		assert.strictEqual(player2Move1.toNumber(), 0, " should be equal players");
		assert.strictEqual(player2Move2.toNumber(), 1, " should be equal players");
		
		assert.strictEqual(winner.toNumber(), 1, "winner should be equal");
		 
		 /*[totalNeeded, collectedUntilNow, giversCount]  = await  instance.campaignDetails(0);
		  
		 assert.strictEqual(collectedUntilNow.toNumber(), 4 * oneEther, " should be 4 ethers collected");
		 assert.strictEqual(giversCount.toNumber(), 2, " should be 2 donators");*/
	 });	 
	 
	it("fees", async function () { 		 
		 await instance.placeGameRequest(0,0,0, {from: _player1, value: bidValue});		
		 await instance.placeGameRequest(0,0,0, {from: _player1, value: bidValue});		
		 await instance.placeGameRequest(0,1,2, {from: _player1, value: bidValue});		
		
		 await instance.acceptGameRequest(2,2,0,1, {from: _player2, value: bidValue});		 
		 
		 let completedLogCount = await instance.getCompletedGamesCount();
		 assert.strictEqual(completedLogCount.toNumber(), 1, "winner should be player 2 ");
		 
		 let fees = await instance.getCollectedFees();
		 assert.strictEqual(fees.toNumber(), oneFinney*10, "fee should be 10 finney");
		 
		 await instance.acceptGameRequest(0,0,0,0, {from: _player2, value: bidValue});
		 
		 fees = await instance.getCollectedFees();
		 assert.strictEqual(fees.toNumber(), oneFinney*30, "fee should be 30 finney");
	 });
}); 