import { loadPage } from "../main.js"; // import SPA navigation

// Game Description Component
export function createGameDescription() {
    const container = document.createElement("div");
    container.classList.add("description-container");

    // HTML structure of the game description
    container.innerHTML = `
        <h1>Game Description</h1>

        <section>
            <h2>Overview</h2>
            <p>This JavaScript project is a single-player metro-building game where the goal is to collect as many points as possible. The game consists of four rounds, each with multiple turns to build a metro line (M1/yellow, M2/red, M3/blue, M4/green) in a random order.</p>

            <p>To build sections of the metro lines, station cards are revealed, showing letters A, B, C, D or a Joker symbol. The letters indicate which station the current metro line can extend to.</p>

            <p>The map is a 10×10 grid, with stations defined in stations.json. Four starting stations are highlighted based on lines.json. Station ID 30 (Deák tér) acts as a Joker station.</p>
        </section>

        <section>
            <h2>Key Concepts</h2>
            <ul>
                <li><strong>Station:</strong> A point on the map defined in stations.json.</li>
                <li><strong>Station Card:</strong> A card showing a letter, Joker, or switch symbol.</li>
                <li><strong>Section:</strong> A colored line connecting two stations.</li>
                <li><strong>Metro Line:</strong> A collection of same-colored sections.</li>
                <li><strong>Turn:</strong> Reveal a card and optionally build a section.</li>
                <li><strong>Round:</strong> All turns for a specific metro line.</li>
            </ul>
        </section>

        <section>
            <h2>Gameplay</h2>
            <p>All four metro lines are randomized at the start. For each round, station cards are shuffled and revealed turn by turn. The player can decide whether to build a section for the revealed card.</p>

            <p>Rules for section building:</p>
            <ul>
                <li>Optional: Building a section is not mandatory.</li>
                <li>Sections can be horizontal, vertical, or (advanced) 45° diagonal.</li>
                <li>The first section starts from the starting station.</li>
                <li>New sections can only start from the current end of the line.</li>
                <li>Sections cannot pass through other stations.</li>
                <li>Sections cannot cross other sections on the open map.</li>
                <li>Only one section can exist between two stations.</li>
                <li>Deák tér acts as a Joker station.</li>
            </ul>
        </section>

        <section>
            <h2>End of Round</h2>
            <p>Two rule sets exist:</p>

            <h3>Simple Mode</h3>
            <p>The round ends after the 8th revealed card.</p>

            <h3>Advanced Mode</h3>
            <p>Cards have two types (middle platform and side platform). If 5 of one type are revealed, the round ends.</p>
        </section>

        <section>
            <h2>Scoring</h2>
            <p>Round points:</p>
            <ul>
                <li><strong>PK:</strong> Number of districts the line passes through.</li>
                <li><strong>PM:</strong> Max stations in one district.</li>
                <li><strong>PD:</strong> Number of river crossings.</li>
                <li><strong>FP = (PK × PM) + PD</strong></li>
            </ul>

            <p>Station hubs give extra points on the slider at the end of the game (0–25).</p>

            <p>Final score:</p>
            <ul>
                <li>Sum(FP)</li>
                <li>+ station hub points</li>
                <li>+ (2 × 2-line junctions)</li>
                <li>+ (5 × 3-line junctions)</li>
                <li>+ (9 × 4-line junctions)</li>
            </ul>
        </section>

        <button class="back-button">Back to Menu</button>
    `;

    // Back button returns to menu
    const backButton = container.querySelector(".back-button");
    backButton.addEventListener("click", () => {
        loadPage("menu");
    });

    return container;
}
