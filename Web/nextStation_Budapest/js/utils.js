// js/utils.js
// Utility helpers for grid, geometry and segment validation.

export const XMAX = 10;
export const YMAX = 10;

/**
 * Convert grid coordinates to linear position index (y * XMAX + x).
 */
export function posFromXY(x, y, xmax = XMAX) {
    return y * xmax + x;
}

/**
 * Return center coordinates (in pixels) for a cell.
 */
export function cellCenter(station) {
    return { cx: station.x + 0.5, cy: station.y + 0.5 };
}

/**
 * Check if two grid points are aligned horizontally, vertically or (optionally) 45deg diagonal.
 */
export function isStraightLine(a, b, diagAllowed = false) {
    if (a.x === b.x) return true;
    if (a.y === b.y) return true;
    if (diagAllowed && Math.abs(a.x - b.x) === Math.abs(a.y - b.y)) return true;
    return false;
}

/**
 * Return an array of integer grid points (inclusive endpoints) along a straight segment.
 */
export function integerPointsBetween(a, b) {
    const points = [];
    
    const dx = Math.sign(b.x - a.x);
    const dy = Math.sign(b.y - a.y);
    
    let currX = a.x;
    let currY = a.y;
    
    // Distance logic assumes straight or 45-deg diagonal
    const dist = Math.max(Math.abs(b.x - a.x), Math.abs(b.y - a.y));
    
    for(let i=0; i<=dist; i++) {
        points.push({x: currX, y: currY});
        currX += dx;
        currY += dy;
    }

    return points;
}

/**
 * Compare two points equality.
 */
export function equalPoint(a, b) {
    return a.x === b.x && a.y === b.y;
}

/**
 * Check if two segments intersect strictly internally (excluding endpoints).
 */
export function segmentsIntersect(a1, a2, b1, b2) {
    const cross = (v, w) => v.x * w.y - v.y * w.x;
    
    const p = a1;
    const r = { x: a2.x - a1.x, y: a2.y - a1.y };
    const q = b1;
    const s = { x: b2.x - b1.x, y: b2.y - b1.y };
    
    const rxs = cross(r, s);
    const qmp = { x: q.x - p.x, y: q.y - p.y };
    const qmpxr = cross(qmp, r);
    
    // Parallel or collinear
    if (rxs === 0) {
        return false; 
    }
    
    const t = cross(qmp, s) / rxs;
    const u = qmpxr / rxs;
    
    // 0 < t < 1 and 0 < u < 1 means strict interior intersection
    return (t > 0 && t < 1) && (u > 0 && u < 1);
}