import { loadPage } from "../main.js"; // import SPA navigation

// About Us Component
export function createAboutUs() {
    const container = document.createElement("div");
    container.classList.add("about-container");

    container.innerHTML = `
        <h1>About This Project</h1>

        <p>This project was created as part of a university JavaScript assignment, implementing a complex metro-building game frontend.</p>

        <p>Our goal was to develop a modern, clean, well-structured, component-based application following the given technical requirements and best practices.</p>

        <p>The game and interface are built entirely with HTML, CSS, and JavaScript, without external libraries, using dynamically loadable modules.</p>

        <p>Thank you for trying out the game!</p>

        <hr>

        <div class="info-panel">
            <p>&copy; 2025 Bartók Patrik</p>
            <p>Neptun Code: <strong>MNDJ3P</strong></p>
            <p>Website: <a href="https://patrikit.hu" target="_blank">patrikit.hu</a></p>
            <p>Contact: <a href="mailto:mndj3p@inf.elte.hu">mndj3p@inf.elte.hu</a> - <a href="mailto:bpatrik@patrikit.hu">bpatrik@patrikit.hu</a></p>
        </div>

        <button class="back-button">Back to Menu</button>
    `;

    // Back button navigates to menu
    const backButton = container.querySelector(".back-button");
    backButton.addEventListener("click", () => {
        loadPage("menu");
    });

    return container;
}