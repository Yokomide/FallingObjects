mergeInto(LibraryManager.library, {

SaveExtern: function(date){
  console.log('Сохранение');
  var dateString = UTF8ToString(date);
  var myobj = JSON.parse(dateString);
  player.setData(myobj);
},

LoadExtern: function()
{
console.log('Загрузка');
player.getData().then(_date => {
const myJSON = JSON.stringify(_date);
myGameInstance.SendMessage('GameManager', 'SetPlayerInfo', myJSON);
})
},

SetToLeaderboard : function(value)
{
  ysdk.getLeaderboards()
  .then(lb => {
    lb.setLeaderboardScore('BestScore', value);
  });

},

ShowAdv : function(){
ysdk.adv.showFullscreenAdv({
    callbacks: {
        onClose: function(wasShown) {
          // some action after close
        },
        onError: function(error) {
          // some action on error
        }
    }
})
},
ContinueGameExtern: function(){
ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
        },
        onRewarded: () => {
          console.log('Rewarded!');
          myGameInstance.SendMessage('GameManager', 'Continue');
        },
        onClose: () => {
          myGameInstance.SendMessage('GameManager', 'UnPause');
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
    }
})

},
UnlockSkinExtern: function(){
ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
        },
        onRewarded: () => {
          console.log('Rewarded!');
          myGameInstance.SendMessage('GameManager', 'SetSkinUnlock');
        },
        onClose: () => {
          console.log('Video ad closed.');
          myGameInstance.SendMessage('GameManager', 'UnPause');
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
    }
})

},
UpgradeHealthExtern: function(){
ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
        },
        onRewarded: () => {
          console.log('Rewarded!');
          myGameInstance.SendMessage('GameManager', 'UpgradeHealth');
        },
        onClose: () => {
          console.log('Video ad closed.');
          myGameInstance.SendMessage('GameManager', 'UnPause');
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
    }
})

},
GetCoinsExtern: function(){
ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
        },
        onRewarded: () => {
          console.log('Rewarded!');
          myGameInstance.SendMessage('GameManager', 'AddRewardCoins');
        },
        onClose: () => {
          console.log('Video ad closed.');
         myGameInstance.SendMessage('GameManager', 'UnPause');
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
    }
})


}
});
