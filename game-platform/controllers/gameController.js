const Game = require('../models/game');

exports.addGame = async (req, res) => {
  try {
    const game = new Game(req.body);
    await game.save();
    res.status(201).send(game);
  } catch (error) {
    res.status(400).send({ error: error.message });
  }
};

exports.getGames = async (req, res) => {
  try {
    const games = await Game.find();
    res.status(200).send(games);
  } catch (error) {
    res.status(500).send({ error: error.message });
  }
};

exports.getLeaderboard = async (req, res) => {
  try {
    const game = await Game.findById(req.params.id);
    if (!game) return res.status(404).send({ error: 'Game not found' });
    res.send(game.leaderboard);
  } catch (error) {
    res.status(500).send({ error: error.message });
  }
};
