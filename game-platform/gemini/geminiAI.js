const { GeminiClient } = require('gemini-sdk');

const client = new GeminiClient({
  apiKey: process.env.GEMINI_API_KEY,
});

exports.generateChallenges = async (playerData) => {
  try {
    const response = await client.analyze({
      input: playerData,
      type: 'recommendation',
    });
    return response;
  } catch (error) {
    throw new Error('AI Error: ' + error.message);
  }
};
