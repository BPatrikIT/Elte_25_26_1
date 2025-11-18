// js/line-drawing.js
import { integerPointsBetween, segmentsIntersect, posFromXY } from "./utils.js";

let engineState = {
    stations: [], // Full array of station objects (including nulls if map based) or list
    lines: [],
    currentLine: null,
    segments: [],
    previousSegments: [], // Stores segments from completed rounds
    roundScores: [], // Stores score objects for each round
    startStationOverride: null
};

/**
 * Initialize engine with stations and lines
 */
export function initializeEngine(stationMap, lines) {
    // stationMap is indexed by pos
    engineState.stations = stationMap.filter(s => s !== null); 
    engineState.stationMap = stationMap; 
    engineState.lines = lines;
    engineState.segments = [];
    engineState.previousSegments = [];
    engineState.roundScores = [];
    engineState.startStationOverride = null;
}

/**
 * Start a round for a line
 */
export function startRoundForLine(line) {
    // Archive segments from the previous round
    if (engineState.segments.length > 0) {
        engineState.previousSegments.push(...engineState.segments);
    }

    engineState.currentLine = line;
    engineState.segments = [];
    engineState.startStationOverride = null;
}

/**
 * Return current engine state
 */
export function getState() {
    return engineState;
}

/**
 * Get all completed segments from previous rounds
 */
export function getCompletedSegments() {
    return engineState.previousSegments;
}

/**
 * Find station by ID
 */
export function findStationById(id) {
    return engineState.stations.find(st => st.id === id);
}

/**
 * Find station by position in the grid array
 */
export function stationByPos(pos) {
    return engineState.stationMap[pos];
}

/**
 * Return endpoints of current line
 */
export function getLineEndpoints() {
    // If no segments yet, the only endpoint is the start station (or override)
    if (engineState.segments.length === 0) {
        let startId = engineState.currentLine.start;
        let startSt = findStationById(startId);
        
        if (engineState.startStationOverride !== null) {
            startSt = stationByPos(engineState.startStationOverride);
        }
        return startSt ? [startSt] : [];
    }

    // Return all stations currently connected to the line. 
    const degreeCount = {};
    engineState.segments.forEach(seg => {
        degreeCount[seg.fromPos] = (degreeCount[seg.fromPos] || 0) + 1;
        degreeCount[seg.toPos] = (degreeCount[seg.toPos] || 0) + 1;
    });

    const endpoints = [];
    for (const [pos, count] of Object.entries(degreeCount)) {
        if (count === 1) {
            endpoints.push(stationByPos(Number(pos)));
        }
    }
    
    return endpoints;
}

/**
 * Validate proposed segment from `fromPos` to `toPos`
 */
export function validateSegment(fromPos, toPos) {
    const from = stationByPos(fromPos);
    const to = stationByPos(toPos);
    
    if (!from || !to) return { ok: false, reason: "Invalid stations" };
    if (fromPos === toPos) return { ok: false, reason: "Same station" };

    const dx = Math.abs(to.x - from.x);
    const dy = Math.abs(to.y - from.y);

    // 1. Geometric validity: Horizontal, Vertical, or Diagonal
    if (!(dx === 0 || dy === 0 || dx === dy)) {
        return { ok: false, reason: "Must be straight or diagonal (45°)" };
    }

    // 2. Check intermediate stations
    const points = integerPointsBetween(from, to);
    for (let i = 1; i < points.length - 1; i++) {
        const pt = points[i];
        const pos = posFromXY(pt.x, pt.y);
        const st = engineState.stationMap[pos];
        if (st) return { ok: false, reason: "Cannot pass through existing station" };
    }

    // 3. Check if segment already exists (no double lines)
    // Check against BOTH current segments and previous segments
    const allSegments = [...engineState.previousSegments, ...engineState.segments];
    const exists = allSegments.some(s => 
        (s.fromPos === fromPos && s.toPos === toPos) || 
        (s.fromPos === toPos && s.toPos === fromPos)
    );
    if (exists) return { ok: false, reason: "Segment already exists" };

    // 4. Check loop/cycle (Target station already in line?)
    // Only check against current line to prevent self-loops. 
    const targetInLine = engineState.segments.some(s => s.fromPos === toPos || s.toPos === toPos);
    if (engineState.segments.length > 0 && targetInLine) {
         return { ok: false, reason: "Cannot create loop" };
    }

    // 5. Check Crossing (Lines cannot cross each other)
    for (const seg of allSegments) {
        const segFrom = stationByPos(seg.fromPos);
        const segTo = stationByPos(seg.toPos);
        
        if (segmentsIntersect(from, to, segFrom, segTo)) {
            return { ok: false, reason: "Cannot cross other lines" };
        }
    }

    return { ok: true };
}

/**
 * Commit segment after validation
 */
let nextSegmentId = 0;
export function commitSegment(fromPos, toPos) {
    const seg = {
        id: nextSegmentId++,
        fromPos,
        toPos,
        color: engineState.currentLine.color
    };
    engineState.segments.push(seg);
    return seg;
}

/* -----------------------------------------------------------
   SCORING LOGIC
----------------------------------------------------------- */

export const TRAIN_POINTS_TABLE = [0, 1, 2, 4, 6, 8, 11, 14, 17, 21, 25];

/**
 * Get count of unique train stations connected so far (across all rounds)
 */
export function getConnectedTrainStationCount() {
    const allSegments = [...engineState.previousSegments, ...engineState.segments];
    const connectedStations = new Set();
    
    allSegments.forEach(seg => {
        connectedStations.add(seg.fromPos);
        connectedStations.add(seg.toPos);
    });
    
    let count = 0;
    connectedStations.forEach(pos => {
        if (engineState.stationMap[pos] && engineState.stationMap[pos].train) {
            count++;
        }
    });
    return count;
}

/**
 * Calculate score for the CURRENT round segments
 */
export function calculateRoundScore() {
    const segments = engineState.segments;
    if (!segments || segments.length === 0) return { PK: 0, PM: 0, PD: 0, total: 0 };

    const stationsTouched = new Set();
    let riverCrossings = 0;

    segments.forEach(seg => {
        const from = stationByPos(seg.fromPos);
        const to = stationByPos(seg.toPos);
        
        stationsTouched.add(from);
        stationsTouched.add(to);

        if (from.side !== to.side) {
            riverCrossings++;
        }
    });

    const districts = {};
    stationsTouched.forEach(st => {
        const d = st.district;
        districts[d] = (districts[d] || 0) + 1;
    });

    const PK = Object.keys(districts).length;
    const PM = Math.max(0, ...Object.values(districts));
    const PD = riverCrossings;
    const total = (PK * PM) + PD;

    const result = { PK, PM, PD, total };
    
    // Save this score to state
    engineState.roundScores.push(result);
    
    return result;
}

/**
 * Calculate Final Game Score
 */
export function calculateGameScore() {
    // 1. Sum of round scores
    const sumFP = engineState.roundScores.reduce((acc, r) => acc + r.total, 0);

    // 2. Train Points (PP)
    const trainCount = getConnectedTrainStationCount();
    const maxIndex = TRAIN_POINTS_TABLE.length - 1;
    const PP = TRAIN_POINTS_TABLE[Math.min(trainCount, maxIndex)];

    // 3. Interchange Points (P2, P3, P4)
    // We need to count how many unique lines touch each station.
    // We use segment colors to distinguish lines (or line rounds).
    const stationLines = new Map(); // pos -> Set<color>
    
    const allSegments = [...engineState.previousSegments]; // segments from last round are already pushed to previous in startRoundForLine? 
    // Wait, calculateGameScore is called AFTER the last round ends but BEFORE startRoundForLine pushes segments?
    // Actually, usually called after round calculation.
    // Let's assume `calculateRoundScore` handles the current round logic, but `engineState.segments` still holds the data
    // until `startNextLineRound` calls `startRoundForLine` which moves them.
    // So we should look at `previousSegments` AND `segments` to be safe, or ensure we call this at the right time.
    // Best check: look at all segments in memory.
    
    const everything = [...engineState.previousSegments, ...engineState.segments];

    everything.forEach(seg => {
        [seg.fromPos, seg.toPos].forEach(pos => {
            if (!stationLines.has(pos)) stationLines.set(pos, new Set());
            stationLines.get(pos).add(seg.color);
        });
    });

    let P2 = 0, P3 = 0, P4 = 0;
    stationLines.forEach((linesSet) => {
        const c = linesSet.size;
        if (c === 2) P2++;
        if (c === 3) P3++;
        if (c === 4) P4++;
    });

    const total = sumFP + PP + (2 * P2) + (5 * P3) + (9 * P4);

    return {
        sumFP,
        PP,
        P2, P3, P4,
        total
    };
}