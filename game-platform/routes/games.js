const express = require('express');
const { addGame, getGames, getLeaderboard } = require('../controllers/gameController');
const router = express.Router();

router.post('/', addGame);
router.get('/', getGames);
router.get('/:id/leaderboard', getLeaderboard);

module.exports = router;
