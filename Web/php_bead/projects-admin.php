<?php
require_once 'includes/json.php';
require_once 'includes/auth.php';

if (!isLoggedIn() || !currentUser()['is_admin']) {
    header('Location: index.php');
    exit;
}

$projects   = readJson('data/projects.json');
$categories = readJson('data/categories.json');
$votes      = readJson('data/votes.json');

/* Szavazatok száma projektenként */
function voteCount($projectId, $votes) {
    return count(array_filter($votes, fn($v) => $v['project'] == $projectId));
}

/* Csak approved státuszú projektek a statisztikákhoz */
$approvedProjects = array_filter($projects, fn($p) => $p['status'] === 1);

/* Legtöbb szavazatot kapó projekt */
$topProject = null;
$maxVotes = -1;
foreach ($approvedProjects as $p) {
    $vCount = voteCount($p['id'], $votes);
    if ($vCount > $maxVotes) {
        $maxVotes = $vCount;
        $topProject = $p;
    }
}
?>

<!DOCTYPE html>
<html lang="hu">
<head>
    <meta charset="UTF-8">
    <title>Admin - Projektek</title>
    <link rel="stylesheet" href="assets/style.css">
</head>
<body>

<div class="container">

<header>
    <h1>Admin - Projektek kezelése</h1>
    <div>
        Bejelentkezve: <strong><?= htmlspecialchars(currentUser()['username']) ?></strong> |
        <a href="index.php">← Főoldal</a> |
        <a href="logout.php">Kijelentkezés</a>
    </div>
</header>

<!-- Legtöbb szavazatot kapó projekt -->
<section class="category">
    <h2>Legtöbb szavazatot kapó projekt</h2>
    <?php if ($topProject): ?>
        <div class="project">
            <a href="project.php?id=<?= $topProject['id'] ?>">
                <?= htmlspecialchars($topProject['title']) ?>
            </a>
            <span class="votes">(<?= voteCount($topProject['id'], $votes) ?> szavazat)</span>
        </div>
    <?php else: ?>
        <p>Nincs projekt.</p>
    <?php endif; ?>
</section>

<!-- Top 3 projekt kategóriánként -->
<section class="category">
    <h2>Kategóriánként a 3 legtöbb szavazatot kapó projekt</h2>
    <?php foreach ($categories as $cat): ?>
        <h3><?= htmlspecialchars($cat['text']) ?></h3>
        <?php
        $catProjects = array_filter($approvedProjects, fn($p) => $p['category'] == $cat['id']);
        usort($catProjects, fn($a,$b) => voteCount($b['id'],$votes) <=> voteCount($a['id'],$votes));
        $top3 = array_slice($catProjects, 0, 3);
        if (!$top3) {
            ?>
            <div class="project">
                <p>Ebben a kategóriában nincs projekt.</p>
            </div>
            <?php
        } else {
            foreach ($top3 as $p): ?>
                <div class="project">
                    <a href="project.php?id=<?= $p['id'] ?>">
                        <?= htmlspecialchars($p['title']) ?>
                    </a>
                    <span class="votes">(<?= voteCount($p['id'], $votes) ?> szavazat)</span>
                </div>
            <?php endforeach;
        }
        ?>
    <?php endforeach; ?>
</section>

<!-- Pending projektek admin műveletekkel -->
<section class="category">
    <h2>Függőben lévő projektek (pending)</h2>
    <?php
    $pendingProjects = array_filter($projects, fn($p) => $p['status'] === 0);
    if (!$pendingProjects):
        echo '<p>Jelenleg nincs függőben lévő projekt.</p>';
    else:
        foreach ($pendingProjects as $p): ?>
            <div class="project">
                <a href="project.php?id=<?= $p['id'] ?>">
                    <?= htmlspecialchars($p['title']) ?>
                </a>
                <form method="post" action="project-action.php" style="display:inline;">
                    <input type="hidden" name="id" value="<?= $p['id'] ?>">
                    <button name="action" value="approve">✔ Jóváhagyás</button>
                    <button name="action" value="reject">✖ Elutasítás</button>
                </form>
            </div>
        <?php endforeach;
    endif;
    ?>
</section>

</div>
</body>
</html>
