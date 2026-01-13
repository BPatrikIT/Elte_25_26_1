<?php
require_once 'includes/json.php';
require_once 'includes/auth.php';

$projects   = readJson('data/projects.json');
$categories = readJson('data/categories.json');
$votes      = readJson('data/votes.json');

$selectedCategory = $_GET['category'] ?? 'all';

/* Szavazatok száma projektenként */
function voteCount($projectId, $votes) {
    return count(array_filter($votes, function ($v) use ($projectId) {
        return $v['project'] == $projectId;
    }));
}

/* 2 hetes szavazási idő ellenőrzése */
function canVote($project) {
    if (empty($project['approved'])) {
        return false;
    }
    $approvedTime = strtotime($project['approved']);
    return time() <= $approvedTime + 14*24*3600; // 2 hét
}

/* Segítség: felhasználó szavazatai kategóriánként */
function userVotesInCategory($userId, $categoryId, $projects, $votes) {
    return array_filter($votes, function($v) use ($userId, $categoryId, $projects) {
        if ($v['user'] !== $userId) return false;
        foreach ($projects as $p) {
            if ($p['id'] === $v['project'] && $p['category'] === $categoryId) return true;
        }
        return false;
    });
}
?>
<!DOCTYPE html>
<html lang="hu">
<head>
    <meta charset="UTF-8">
    <title>Budapesti Közösségi Költségvetés</title>
    <link rel="stylesheet" href="assets/style.css">
</head>
<body>

<div class="container">

    <header>
        <h1>Budapesti Közösségi Költségvetés</h1>
        <div>
            <?php if (isLoggedIn()): ?>
                Bejelentkezve: <strong><?= htmlspecialchars(currentUser()['username']) ?></strong>
                |
                <a href="logout.php">Kijelentkezés</a>
                |
                <a href="submit-project.php" class="button">Új projekt leadása</a>
                |
                <a href="projects-own.php" class="button">Saját projektek</a>
            <?php else: ?>
                <a href="login.php">Bejelentkezés</a>
            <?php endif; ?>
            <?php if (isLoggedIn() && currentUser()['is_admin']): ?>
                <a href="projects-admin.php" class="button">Admin felület</a>
            <?php endif; ?>
        </div>
    </header>

    <!-- Kategória szűrő -->
    <div class="filter">
        <form method="get">
            <label for="category">Kategória:</label>
            <select name="category" id="category" onchange="this.form.submit()">
                <option value="all">Összes</option>
                <?php foreach ($categories as $cat): ?>
                    <option value="<?= $cat['id'] ?>" <?= $selectedCategory == $cat['id'] ? 'selected' : '' ?>>
                        <?= htmlspecialchars($cat['text']) ?>
                    </option>
                <?php endforeach; ?>
            </select>
        </form>
    </div>

    <!-- Projektek listázása kategóriánként -->
    <?php foreach ($categories as $cat): ?>
        <?php
        if ($selectedCategory !== 'all' && $selectedCategory != $cat['id']) {
            continue;
        }
        ?>

        <section class="category">
            <h2><?= htmlspecialchars($cat['text']) ?></h2>

            <?php
            $hasProject = false;
            foreach ($projects as $p):
                if ($p['status'] !== 1 || $p['category'] != $cat['id']) continue;
                $hasProject = true;

                // Felhasználó szavazatainak ellenőrzése
                $userVotes = isLoggedIn() ? array_filter($votes, function($v) use ($projects) {
                    return $v['user'] === currentUser()['id'];
                }) : [];

                $userVotesInCat = isLoggedIn() ? userVotesInCategory(currentUser()['id'], $p['category'], $projects, $votes) : [];
                $remainingVotes = 3 - count($userVotesInCat);
                $hasVoted = isLoggedIn() && in_array($p['id'], array_column($userVotes,'project'));
            ?>
                <div class="project">
                    <a href="project.php?id=<?= $p['id'] ?>">
                        <?= htmlspecialchars($p['title']) ?>
                    </a>
                    <span class="votes"><?= voteCount($p['id'], $votes) ?> szavazat</span>

                    <?php if (isLoggedIn()): ?>
                        <?php if (canVote($p)): ?>
                            <?php if ($hasVoted): ?>
                                <form method="post" action="vote.php" style="display:inline;">
                                    <input type="hidden" name="project" value="<?= $p['id'] ?>">
                                    <button type="submit" name="action" value="unvote">Szavazat visszavonása</button>
                                </form>
                            <?php elseif ($remainingVotes > 0): ?>
                                <form method="post" action="vote.php" style="display:inline;">
                                    <input type="hidden" name="project" value="<?= $p['id'] ?>">
                                    <button type="submit" name="action" value="vote">Szavazok</button>
                                </form>
                                (<?= $remainingVotes ?> szavazat maradt ebben a kategóriában)
                            <?php else: ?>
                                <span style="color:gray;">Már leadtad a 3 szavazatod ebben a kategóriában</span>
                            <?php endif; ?>
                        <?php else: ?>
                            <span class="closed">(szavazás lezárva)</span>
                        <?php endif; ?>
                    <?php endif; ?>
                </div>

            <?php endforeach; ?>

            <?php if (!$hasProject): ?>
                <div class="empty">Ebben a kategóriában még nincs közzétett projekt.</div>
            <?php endif; ?>
        </section>

    <?php endforeach; ?>

</div>

</body>
</html>
