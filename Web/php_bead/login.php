<?php
require_once 'includes/json.php';
require_once 'includes/auth.php';

$error = '';

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $username = trim($_POST['username'] ?? '');
    $password = $_POST['password'] ?? '';

    $users = readJson('data/users.json');

    foreach ($users as $user) {
        if ($user['username'] === $username &&
            password_verify($password, $user['password'])) {

            $_SESSION['user'] = $user;
            header('Location: index.php');
            exit;
        }
    }

    $error = 'Hibás felhasználónév vagy jelszó.';
}
?>
<!DOCTYPE html>
<html lang="hu">
<head>
    <meta charset="UTF-8">
    <title>Bejelentkezés</title>
    <link rel="stylesheet" href="assets/style.css">
</head>
<body>
<div class="container">

<h1>Bejelentkezés</h1>

<?php if ($error): ?>
    <p class="closed"><?= htmlspecialchars($error) ?></p>
<?php endif; ?>

<form method="post">
    <label>Felhasználónév</label><br>
    <input type="text" name="username" required><br><br>

    <label>Jelszó</label><br>
    <input type="password" name="password" required><br><br>

    <button type="submit">Bejelentkezés</button>
</form>

<p>
    Nincs még fiókod?
    <a href="register.php">Regisztráció</a>
</p>

</div>
</body>
</html>
