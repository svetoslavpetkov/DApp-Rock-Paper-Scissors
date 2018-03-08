var GameBoard = artifacts.require("./GameBoard.sol");

module.exports = function(deployer) {
  deployer.deploy(GameBoard);
};