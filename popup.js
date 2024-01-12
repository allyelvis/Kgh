document.addEventListener('DOMContentLoaded', () => {
  const tablesContainer = document.getElementById('tables-container');

  // Load saved tables
  chrome.storage.local.get('tables', (data) => {
    const tables = data.tables;

    tables.forEach((table) => {
      const tableItem = document.createElement('div');
      tableItem.classList.add('table-item');

      const tableNumber = document.createElement('span');
      tableNumber.textContent = `Table ${table.number}`;
      tableNumber.classList.add('table-number');
      tableItem.appendChild(tableNumber);

      const tableStatus = document.createElement('span');
      tableStatus.textContent = table.status;
      tableStatus.classList.add('table-status');
      tableItem.appendChild(tableStatus);

      tablesContainer.appendChild(tableItem);
    });
  });
});
