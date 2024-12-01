const express = require('express');
const { registerPlayer, getPlayer, updateTokens } = require('../controllers/playerController');
const router = express.Router();

router.post('/', registerPlayer);
router.get('/:id', getPlayer);
router.put('/:id/tokens', updateTokens);

module.exports = router;
