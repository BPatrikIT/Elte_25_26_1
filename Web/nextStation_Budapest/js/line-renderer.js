// js/line-renderer.js

let svgLayer = null;

/**
 * Initialize an SVG overlay layer inside the grid wrapper
 */
export function initializeLineLayer(gridWrapper) {
    if (!gridWrapper) return;

    // Remove existing SVG if any
    const existing = gridWrapper.querySelector("svg");
    if (existing) existing.remove();

    // The SVG should be attached to .station-grid (which is relative) 
    // so lines align with the grid content (cells).
    const stationGrid = gridWrapper.querySelector(".station-grid");
    if (!stationGrid) return;
    
    svgLayer = document.createElementNS("http://www.w3.org/2000/svg", "svg");
    svgLayer.style.width = "100%";
    svgLayer.style.height = "100%";
    svgLayer.style.position = "absolute";
    svgLayer.style.top = "0";
    svgLayer.style.left = "0";
    svgLayer.style.pointerEvents = "none"; // clicks go to grid cells
    svgLayer.style.zIndex = "10";
    
    stationGrid.appendChild(svgLayer);
}

/**
 * Clear all drawn segments
 */
export function clearLineLayer() {
    if (!svgLayer) return;
    while (svgLayer.firstChild) svgLayer.firstChild.remove();
}

/**
 * Render a line segment between two stations
 */
export function renderLineSegment(fromStation, toStation, color, id) {
    if (!svgLayer || !fromStation || !toStation) return;

    const padding = 10; // Matches padding of .station-grid in CSS
    const cellSize = 48; // Matches CSS
    const offset = cellSize / 2; // Center of cell

    const x1 = fromStation.x * cellSize + offset + padding;
    const y1 = fromStation.y * cellSize + offset + padding;
    const x2 = toStation.x * cellSize + offset + padding;
    const y2 = toStation.y * cellSize + offset + padding;

    const line = document.createElementNS("http://www.w3.org/2000/svg", "line");
    line.setAttribute("x1", x1);
    line.setAttribute("y1", y1);
    line.setAttribute("x2", x2);
    line.setAttribute("y2", y2);
    line.setAttribute("stroke", color || "#000");
    line.setAttribute("stroke-width", 8);
    line.setAttribute("stroke-opacity", 0.8);
    line.setAttribute("stroke-linecap", "round");
    if (id) line.setAttribute("id", id);

    svgLayer.appendChild(line);
}