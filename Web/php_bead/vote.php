<?php
require_once 'includes/json.php';
require_once 'includes/auth.php';

if (!isLoggedIn() || !isset($_POST['project'])) {
    header('Location: index.php');
    exit;
}

$user = currentUser();
$projectId = (int)$_POST['project'];
$action = $_POST['action'] ?? 'vote';

$projects = readJson('data/projects.json');
$votes = readJson('data/votes.json');

// Projekt keresése
$project = null;
foreach ($projects as $p) {
    if ($p['id'] === $projectId) {
        $project = $p;
        break;
    }
}

if (!$project || $project['status'] !== 1) {
    header('Location: index.php');
    exit;
}

// Ellenőrzés: 2 hét lejárta
$approved = strtotime($project['approved']);
if (time() > $approved + 14*24*3600) {
    header('Location: index.php');
    exit;
}

// Szavazatok számolása kategóriánként
$userVotes = array_filter($votes, fn($v) => $v['user'] === $user['id']);
$userVotesInCategory = array_filter($userVotes, function($v) use ($projects, $project) {
    foreach ($projects as $p) {
        if ($p['id'] === $v['project'] && $p['category'] === $project['category']) {
            return true;
        }
    }
    return false;
});


// Ellenőrzés: max 3 szavazat / kategória
$alreadyVotedThisProject = false;
foreach ($userVotesInCategory as $v) {
    if ($v['project'] === $projectId) $alreadyVotedThisProject = true;
}

if ($action === 'vote') {
    if ($alreadyVotedThisProject || count($userVotesInCategory) >= 3) {
        header('Location: index.php');
        exit;
    }
    $votes[] = ['user' => $user['id'], 'project' => $projectId];
} elseif ($action === 'unvote') {
    $votes = array_filter($votes, fn($v) => !($v['user']==$user['id'] && $v['project']==$projectId));
}

file_put_contents('data/votes.json', json_encode(array_values($votes), JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE));
header('Location: index.php');
exit;
