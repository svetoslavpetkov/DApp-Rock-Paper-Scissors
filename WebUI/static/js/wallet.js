import Lightwallet from 'eth-lightwallet'
import store from 'store'

var wallet = (function(wallet){
    var walletNamePrefix = 'wallet:';
    var current = null;
    var isLoaded = function(){
        return current != null;
    };

    wallet.getMessage = function(){
        return 'I am wallet';
    }

    wallet.load = function(name,password,callbackFn){
        var keyStore = Lightwallet.keystore.deserialize(store.get(walletNamePrefix+name));

        keyStore.keyFromPassword(password, function (err, pwDerivedKey) {
            if (err) throw err;

            // generate five new address/private key pairs
            // the corresponding private keys are also encrypted
            keyStore.generateNewAddress(pwDerivedKey, 5);
            var addr = keyStore.getAddresses()[0];              

            keyStore.passwordProvider = function (callback) {
              var pw = prompt("Please enter password", "Password");
              callback(null, pw);
            };
            
            current = {
                keyStore : keyStore,
                address : addr
            };
            
            callbackFn(current);
          });
    }

    wallet.getList = function(){
        var result = new Array();
        store.each(function(value, key) {
            if(key.startsWith(walletNamePrefix))
                result.push(key.substring(walletNamePrefix.length));
        });
        return result;
    }
    
    wallet.getAddress = function(){

    }

    wallet.create = function(name, password, seed, callbackFn){
            var randomSeed = Lightwallet.keystore.generateRandomSeed(seed);

            Lightwallet.keystore.createVault({
            password: password,
            seedPhrase: randomSeed, // Optionally provide a 12-word seed phrase
            // salt: fixture.salt,     // Optionally provide a salt.
                                      // A unique salt will be generated otherwise.
             hdPathString: "m/44'/60'/0'/0"    // Optional custom HD Path String
          }, function (err, ks) {
            
            // Some methods will require providing the `pwDerivedKey`,
            // Allowing you to only decrypt private keys on an as-needed basis.
            // You can generate that value with this convenient method:
            ks.keyFromPassword(password, function (err, pwDerivedKey) {
              if (err) throw err;

              // generate five new address/private key pairs
              // the corresponding private keys are also encrypted
              ks.generateNewAddress(pwDerivedKey, 5);
              var addr = ks.getAddresses()[0];              

              ks.passwordProvider = function (callback) {
                var pw = prompt("Please enter password", "Password");
                callback(null, pw);
              };
              
              current = {
                  keyStore : ks,
                  address : addr
              };
              
              var serializedKeyStore = ks.serialize();
              //console.log();
              // Now set ks as transaction_signer in the hooked web3 provider
              // and you can start using web3 using the keys/addresses in ks!
              store.set('wallet:'+ name, serializedKeyStore);
              callbackFn(randomSeed);
            });
          });
    }

    return wallet;
}(wallet || {}))

export default wallet;