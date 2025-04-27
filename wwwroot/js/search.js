document.addEventListener('DOMContentLoaded', function () {
    const searchQueryInput = document.getElementById('searchQuery');
    const usersTable = document.getElementById('usersTable');
    const gamesTable = document.getElementById('gamesTable');
    const librariesTable = document.getElementById('librariesTable');

    if (searchQueryInput && usersTable) {
        searchQueryInput.addEventListener('input', function () {
            const searchQuery = this.value;
            fetch(`/User/Index?searchQuery=${encodeURIComponent(searchQuery)}`)
                .then(response => response.text())
                .then(data => {
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

    if (searchQueryInput && librariesTable) {
        searchQueryInput.addEventListener('input', function () {
            const searchQuery = this.value;
            fetch(`/Library/Index?searchQuery=${encodeURIComponent(searchQuery)}`)
                .then(response => response.text())
                .then(data => {
                    librariesTable.innerHTML = new DOMParser().parseFromString(data, 'text/html').querySelector('#librariesTable').innerHTML;
                });
        });
    }
});