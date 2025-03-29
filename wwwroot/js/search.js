// wwwroot/js/search.js

document.addEventListener('DOMContentLoaded', function () {
    const searchQueryInput = document.getElementById('searchQuery');
    const usersTable = document.getElementById('usersTable');
    const gamesTable = document.getElementById('gamesTable');

    if (searchQueryInput && usersTable) {
        searchQueryInput.addEventListener('input', function () {
            const searchQuery = this.value;  // Use 'const' because searchQuery won't be reassigned
            fetch(`/User/Index?searchQuery=${encodeURIComponent(searchQuery)}`)
                .then(response => response.text())
                .then(data => {
                    // Update the table with the new filtered results
                    usersTable.innerHTML = new DOMParser().parseFromString(data, 'text/html').querySelector('#usersTable').innerHTML;
                });
        });
    }

    if (searchQueryInput && gamesTable) {
        searchQueryInput.addEventListener('input', function () {
            const searchQuery = this.value;
            fetch(`/Game/Index?searchQuery=${encodeURIComponent(searchQuery)}`)
                .then(response => response.text())
                .then(data => {
                    gamesTable.innerHTML = new DOMParser().parseFromString(data, 'text/html').querySelector('#gamesTable').innerHTML;
                });
        });
    }
});

