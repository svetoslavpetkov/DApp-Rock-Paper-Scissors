const GameBoard = artifacts.require('./GameBoard.sol');

contract('GameBoard', function (accounts) {
	let instance; 
	
	const _owner = accounts[0];
	const _campaignAcc = accounts[1]; 
	const _player1 = accounts[2]; 
	const _player2 = accounts[3]; 
	
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
		 
		 let gamesCount = await instance.getCreatedGamesCount();
		 assert.strictEqual(gamesCount.toNumber(), 1, "gamesCount should be 1");
		
		 [address, gameValue] = await instance.getCreatedGameData(0);
		 assert.strictEqual(gameValue.toNumber(), bidValue, "bid value should be ");
		 assert.strictEqual(address, _player1, "bid value should be ");		 
	 });
	 
	 
	 it("play one game", async function () { 		 
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
	 	 

}); 