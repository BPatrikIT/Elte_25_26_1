<?php
require_once 'includes/json.php';
require_once 'includes/auth.php';

$error = '';
$success = '';

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $username  = trim($_POST['username'] ?? '');
    $email     = trim($_POST['email'] ?? '');
    $password  = $_POST['password'] ?? '';
    $password2 = $_POST['password2'] ?? '';

    // Felhasználónév: nem lehet üres és nem tartalmazhat szóközt
    if (empty($username) || strpos($username, ' ') !== false) {
        $error = 'A felhasználónév nem lehet üres és nem tartalmazhat szóközt.';
    }
    // E-mail ellenőrzése
    elseif (!filter_var($email, FILTER_VALIDATE_EMAIL)) {
        $error = 'Érvénytelen e-mail cím.';
    }
    // Jelszó ellenőrzése: legalább 8 karakter, kisbetű, nagybetű, szám
    elseif (strlen($password) < 8 
        || !preg_match('/[a-z]/', $password) 
        || !preg_match('/[A-Z]/', $password) 
        || !preg_match('/[0-9]/', $password)) {
        $error = 'A jelszó legalább 8 karakter, tartalmaz kisbetűt, nagybetűt és számot.';
    }
    // Jelszavak egyezése
    elseif ($password !== $password2) {
        $error = 'A két jelszó mező nem egyezik.';
    } else {
        $users = readJson('data/users.json');

        // Egyedi felhasználónév ellenőrzése
        foreach ($users as $u) {
            if ($u['username'] === $username) {
                $error = 'Ez a felhasználónév már foglalt.';
                break;
            }
        }

        if (!$error) {
            $newId = $users ? max(array_column($users,'id')) + 1 : 1;
            $users[] = [
                'id' => $newId,
                'username' => $username,
                'email' => $email,
                'password' => password_hash($password, PASSWORD_DEFAULT),
                'is_admin' => false
            ];

            file_put_contents(
                'data/users.json',
                json_encode($users, JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE)
            );

            $success = 'Sikeres regisztráció! Most már bejelentkezhetsz.';
            // Adatok törlése a formból sikeres regisztráció után
            $username = $email = '';
        }
    }
}
?>
<!DOCTYPE html>
<html lang="hu">
<head>
    <meta charset="UTF-8">
    <title>Regisztráció</title>
    <link rel="stylesheet" href="assets/style.css">
</head>
<body>
<div class="container">

<h1>Regisztráció</h1>

<?php if ($error): ?>
    <p class="closed"><?= htmlspecialchars($error) ?></p>
<?php endif; ?>

<?php if ($success): ?>
    <p style="color:green; font-weight:bold; margin-bottom:15px;"><?= htmlspecialchars($success) ?></p>
<?php endif; ?>

<form method="post">
    <label>Felhasználónév</label><br>
    <input type="text" name="username" required value="<?= htmlspecialchars($username ?? '') ?>"><br><br>

    <label>E-mail</label><br>
    <input type="email" name="email" required value="<?= htmlspecialchars($email ?? '') ?>"><br><br>

    <label>Jelszó</label><br>
    <input type="password" name="password" required><br><br>

    <label>Jelszó újra</label><br>
    <input type="password" name="password2" required><br><br>

    <button type="submit">Regisztráció</button>
</form>

<div style="margin-top:10px;">
    <a href="login.php">← Már van fiókod? Bejelentkezés</a>
</div>

</div>
</body>
</html>