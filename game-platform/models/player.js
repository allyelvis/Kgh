const mongoose = require('mongoose');

const playerSchema = new mongoose.Schema({
  name: String,
  email: String,
  tokens: { type: Number, default: 100 },
  gamesPlayed: [{ type: mongoose.Schema.Types.ObjectId, ref: 'Game' }],
  createdAt: { type: Date, default: Date.now },
});

module.exports = mongoose.model('Player', playerSchema);
