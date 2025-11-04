const filter = document.querySelector('#filter');
const list = document.querySelector('#list');
const details = list.querySelectorAll('#details');

filter.addEventListener('input', onFilter);

function onFilter(event) {
  const filterText = filter.value.toLowerCase().trim();
  const filteredMovies = filterText == "" ? [] : movies.filter(m=> m.title.toLowerCase().includes(filterText)).slice(0, 10);
  renderList(filteredMovies);
}

function renderList(movies) {
    list.innerHTML = "";
    for (const movie of movies) {
        const li = document.createElement('li');
        li.textContent = movie.title;
        li.addEventListener('click', () => onMovieClick(movie));
        list.appendChild(li);
    }
    if (movies.length === 0) {
        list.innerHTML = "<li>No movies found</li>";
    }
    details.forEach(d => d.style.display = 'none');
}

function onMovieClick(movie) {
    details.forEach(d => d.style.display = 'none');
    const detail = document.querySelector('#details');
    if (detail) {
        detail.style.display = 'block';
        detail.innerHTML = `
            <p>${movie.title}</p>
            <p>${movie.director} (${movie.year})</p>
        `;
    }
    filter.value = movie.title;
    renderList([movie]);
}