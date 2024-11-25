const Player = require('../models/player');

exports.registerPlayer = async (req, res) => {
  try {
    const player = new Player(req.body);
    await player.save();
    res.status(201).send(player);
  } catch (error) {
    res.status(400).send({ error: error.message });
  }
};

exports.getPlayer = async (req, res) => {
  try {
    const player = await Player.findById(req.params.id);
    if (!player) return res.status(404).send({ error: 'Player not found' });
    res.send(player);
  } catch (error) {
    res.status(500).send({ error: error.message });
  }
};

exports.updateTokens = async (req, res) => {
  try {
    const player = await Player.findById(req.params.id);
    if (!player) return res.status(404).send({ error: 'Player not found' });

    player.tokens += req.body.tokens;
    await player.save();
    res.send(player);
  } catch (error) {
    res.status(400).send({ error: error.message });
  }
};
