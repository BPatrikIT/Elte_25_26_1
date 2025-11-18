// components/game.js
import { loadStationsJson, loadLinesJson, createStationMap, attachLinesToStations } from "../js/stations.js";
import { renderGame, showRoundResultModal, showGameResultModal } from "../js/renderer.js";
import { initializeEngine, startRoundForLine, getState, commitSegment, validateSegment, findStationById, stationByPos, getLineEndpoints, getCompletedSegments, calculateRoundScore, calculateGameScore } from "../js/line-drawing.js";
import { renderLineSegment, initializeLineLayer, clearLineLayer } from "../js/line-renderer.js";
import { loadPage } from "../main.js";

let gameState = {
    stationMap: null,
    lines: null,
    shuffledLines: [],
    currentLineIndex: 0,
    deck: [],
    currentCard: null,
    segmentsDrawn: 0
};

export function GameComponent() {
    const container = document.createElement("div");
    container.classList.add("game-container");
    return container;
}

function createDeck() {
    const letters = ["A", "B", "C", "D"];
    const deck = [];
    // 6 cards of each letter = 24
    letters.forEach(l => { for(let i=0;i<6;i++) deck.push({type:"letter", value:l}); });
    // 2 Jokers
    deck.push({ type: "joker" }, { type: "joker" });
    
    // Fisher-Yates Shuffle
    for (let i = deck.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random()*(i+1));
        [deck[i], deck[j]] = [deck[j], deck[i]];
    }
    return deck;
}

function drawCard() {
    if (!gameState.deck || gameState.deck.length === 0) {
        gameState.deck = createDeck(); 
    }
    return gameState.deck.pop();
}

function startNextLineRound(autoStartFrom=null) {
    // Check if all lines are done
    if (gameState.currentLineIndex >= 4) {
        finishGame();
        return;
    }

    if (gameState.shuffledLines.length === 0) return;

    const line = gameState.shuffledLines[gameState.currentLineIndex];
    startRoundForLine(line);
    localStorage.setItem("currentLine", JSON.stringify(line));
    
    gameState.segmentsDrawn = 0;
    gameState.deck = createDeck(); // New deck per round
    
    if (autoStartFrom !== null) {
        getState().startStationOverride = autoStartFrom;
    }

    // Draw first card automatically
    gameState.currentCard = drawCard();
    
    refreshGameUI();
}

function refreshGameUI() {
    const container = document.querySelector(".game-container");
    if (!container || !gameState.stationMap) return;

    renderGame(container, gameState.stationMap);
    
    // Re-init SVG layer
    const wrapper = container.querySelector(".game-grid-wrapper");
    initializeLineLayer(wrapper);

    // Re-draw completed segments from previous rounds
    const prevSegs = getCompletedSegments();
    prevSegs.forEach(seg => {
         const fromSt = stationByPos(seg.fromPos);
         const toSt = stationByPos(seg.toPos);
         renderLineSegment(fromSt, toSt, seg.color, `seg-${seg.id}`);
    });

    updateCardUI();
    // Ensure selection state is reset for the new round
    partialFromPos = null;
    clearSelectionVisual();
}

function finishGame() {
    const finalScore = calculateGameScore();
    
    // Stop timer
    if(window.__gameTimer) clearInterval(window.__gameTimer);
    const endTime = Date.now();
    const startTime = Number(localStorage.getItem("currentStartTime"));
    const duration = Math.floor((endTime - startTime) / 1000);
    
    // Save score
    const scores = JSON.parse(localStorage.getItem("scores") || "[]");
    scores.push({
        name: localStorage.getItem("currentPlayer") || "Anon",
        points: finalScore.total,
        time: duration,
        date: new Date().toISOString()
    });
    scores.sort((a,b) => b.points - a.points);
    localStorage.setItem("scores", JSON.stringify(scores));

    showGameResultModal(finalScore, () => {
        loadPage("menu");
    });
}

function updateCardUI() {
    const cardEl = document.querySelector(".card-display");
    if (!cardEl) return;
    if (!gameState.currentCard) { cardEl.textContent = "---"; return; }
    cardEl.textContent = gameState.currentCard.type === "joker" ? "★ JOKER" : gameState.currentCard.value;
    cardEl.style.color = gameState.currentCard.type === "joker" ? "purple" : "black";
}

let partialFromPos = null;

export async function initGame(container) {
    const stations = await loadStationsJson();
    const lines = await loadLinesJson();
    
    let stationMap = createStationMap(stations);
    stationMap = attachLinesToStations(stationMap, lines);
    
    initializeEngine(stationMap, lines);
    gameState.stationMap = stationMap;
    gameState.lines = lines;
    
    gameState.shuffledLines = [...lines].sort(() => Math.random() - 0.5);
    gameState.currentLineIndex = 0;
    gameState.deck = createDeck();
    
    // Ensure initial render before starting round
    renderGame(container, stationMap);
    const gridWrapper = container.querySelector(".game-grid-wrapper");
    initializeLineLayer(gridWrapper);

    startNextLineRound();

    container.addEventListener("draw-card", () => {
        gameState.currentCard = drawCard();
        updateCardUI();
    });
}

export function onStationClicked(pos) {
    const station = stationByPos(pos);
    if (!station) return; 
    
    // If no card, cannot play
    if (!gameState.currentCard) return;

    // 1. Selection phase (Start of segment)
    if (partialFromPos === null) {
        const endpoints = getLineEndpoints();
        
        // Allow selection if station is an endpoint
        const isEndpoint = endpoints.some(e => e.pos === pos);
        
        if (isEndpoint) {
            partialFromPos = pos;
            highlightSelected(station);
        } else {
            flashInvalid(station);
        }
        return;
    }

    // 2. Target phase (End of segment)
    const fromPos = partialFromPos;
    const toPos = pos;

    if (fromPos === toPos) {
        partialFromPos = null;
        clearSelectionVisual();
        return;
    }

    const validation = validateSegment(fromPos, toPos);
    if (!validation.ok) {
        flashInvalid(station, validation.reason);
        return; 
    }

    // Check Card Match
    const targetStation = stationByPos(toPos);
    const card = gameState.currentCard;
    
    let match = false;
    if (card.type === "joker") match = true;
    else if (targetStation.type === "?") match = true;
    else if (targetStation.type === card.value) match = true;

    if (!match) {
        flashInvalid(station, "Card mismatch");
        return;
    }

    // Success
    const seg = commitSegment(fromPos, toPos);
    const fromSt = stationByPos(seg.fromPos);
    const toSt = stationByPos(seg.toPos);
    
    renderLineSegment(fromSt, toSt, seg.color, `seg-${seg.id}`);

    gameState.segmentsDrawn++;

    // Check Round End (8 segments)
    if (gameState.segmentsDrawn >= 8) {
        const roundScore = calculateRoundScore();
        partialFromPos = null;
        clearSelectionVisual();
        
        // Show result and then proceed
        showRoundResultModal(gameState.currentLineIndex + 1, roundScore, () => {
             gameState.currentLineIndex++;
             startNextLineRound();
        });

    } else {
        // Next Card
        gameState.currentCard = drawCard();
        updateCardUI();
        
        // Auto-select destination as new start
        partialFromPos = toPos;
        clearSelectionVisual();
        highlightSelected(toSt);
    }
}

function highlightSelected(station) {
    const cell = document.querySelector(`.grid-cell[data-pos="${station.pos}"]`);
    if (cell) cell.classList.add("selected-station");
}

function clearSelectionVisual() {
    document.querySelectorAll(".grid-cell.selected-station").forEach(el=>el.classList.remove("selected-station"));
}

function flashInvalid(station, reason) {
    const cell = document.querySelector(`.grid-cell[data-pos="${station.pos}"]`);
    if (cell) {
        cell.classList.add("invalid-selection");
        setTimeout(()=>cell.classList.remove("invalid-selection"), 600);
    }
    if(reason) console.log("Invalid:", reason);
}