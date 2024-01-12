chrome.runtime.onInstalled.addListener(() => {
  // Initialize storage for tables and sales
  chrome.storage.local.set({ tables: [], sales: [] });
});
