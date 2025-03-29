// wwwroot/js/search.js

document.addEventListener('DOMContentLoaded', function () {
    const searchQueryInput = document.getElementById('searchQuery');
    const usersTable = document.getElementById('usersTable');

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
});
