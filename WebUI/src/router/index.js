import Vue from 'vue'
import Router from 'vue-router'
import HelloWorld from '@/components/HelloWorld'
import NewWallet from '@/components/NewWallet'
import NewGame from '@/components/NewGame'
import PlayGame from '@/components/PlayGame'
import TopScores from '@/components/TopScores'
import Player from '@/components/Player'
import Game from '@/components/Game'


Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'HelloWorld',
      component: HelloWorld
    },
    {
      path: '/new-game',
      name: 'new-game',
      component: NewGame
    },
    {
      path: '/play-game/:gameid',
      name: 'play-game',
      component: PlayGame
    },
    { path: '/top-scores', name: 'top-scores', component: TopScores },
    { path: '/player/:playerAddress', name: 'player', component: Player },
    { path: '/game/:gameid', name: 'game', component: Game }
  ]
})
