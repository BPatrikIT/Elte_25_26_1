import { loadPage } from "../main.js";

export function MenuComponent() {
    const container = document.createElement("div");
    container.className = "menu-container";

    // Load previous scores from localStorage
    const scores = JSON.parse(localStorage.getItem("scores") || "[]");

    const scoresHtml = scores.length === 0
        ? `<p>No previous scores yet.</p>`
        : `
            <ul class="score-list">
                ${scores.map(s => `
                    <li>
                        <strong>${s.name}</strong> – ${s.points} points – ${s.time} sec
                    </li>
                `).join("")}
            </ul>
        `;

    container.innerHTML = `
        <h1>Welcome to the game!</h1>

        <div class="name-input-section">
            <label for="playerName">Player Name:</label>
            <input id="playerName" type="text" placeholder="Enter your name..." />
        </div>

        <button class="start-button">Start</button>

        <h2 id="previousScores">Previous Scores</h2>
        <div class="scores-section">
            ${scoresHtml}
        </div>

        <button class="description-button">Game Description</button>
        <button class="about-button">About Us</button>
    `;

    // Start button functionality
    container.querySelector(".start-button")
        .addEventListener("click", () => {
            const nameInput = container.querySelector("#playerName");
            const playerName = nameInput.value.trim();

            if (playerName.length === 0) {
                nameInput.classList.add("input-error");
                return;
            }

            nameInput.classList.remove("input-error");

            // Store active player and start time
            localStorage.setItem("currentPlayer", playerName);
            localStorage.setItem("currentStartTime", String(Date.now()));

            loadPage("game");
        });

    // Game Description button functionality
    container.querySelector(".description-button")
        .addEventListener("click", () => {
            loadPage("description");
        });

    // About Us button functionality
    container.querySelector(".about-button")
        .addEventListener("click", () => {
            loadPage("about");
        });

    return container;
}
