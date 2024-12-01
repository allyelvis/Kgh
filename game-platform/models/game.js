const mongoose = require('mongoose');

const gameSchema = new mongoose.Schema({
  name: String,
  description: String,
  maxPlayers: Number,
  createdAt: { type: Date, default: Date.now },
  leaderboard: [
    {
      playerId: mongoose.Schema.Types.ObjectId,
      playerName: String,
      score: Number,
    },
  ],
});

module.exports = mongoose.model('Game', gameSchema);
