<?php

session_start();

include_once("userstorage.php");
include_once("auth.php");
include("utils.php");

// functions
function validate($post, &$data, &$errors): bool
{
    $data = [
        "username" => trim($post["username"] ?? ""),
        "password" => trim($post["password"] ?? ""),
    ];

    // username
    if ($data["username"] === "") {
        $errors["username"] = "A felhasználónév megadása kötelező!";
    }

    // password
    if ($data["password"] === "") {
        $errors["password"] = "A jelszó megadása kötelező!";
    }
  
  return count($errors) === 0;
}


// main
$user_storage = new UserStorage();
$auth = new Auth($user_storage);

$data = [];
$errors = [];
if (count($_POST) > 0) {
  if (validate($_POST, $data, $errors)) {
    //próbáljunk meg bejelentkezni
    $user = $auth->authenticate($data["username"], $data["password"]);
    if (!$user) {
        $errors["global"] = "Hibás felhasználónév vagy jelszó!";
    } else {
        //sikeres bejelentkezés
        $auth->login($user);

        if(isset($_GET["redirect"])) {
            redirect($_GET["redirect"]);
        } else {
            redirect("index.php");
        }
    }
  }
}
?>
<!DOCTYPE html>
<html lang="en">

<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Document</title>
  <style>
    input~span {
      color: red;
      font-size: smaller;
    }
    .error {
      color: red;
    }
  </style>
</head>

<body>
  <h1>Névjegyek</h1>
  <h2>Bejelentkezés</h2>
    <?php if (isset($errors["global"])) : ?>
        <p><span class="error"><?= $errors["global"] ?></span></p>
    <?php endif ?>
  <form action="" method="post" novalidate>
    Username: <br>
    <input type="text" name="username" value="<?= $_POST['username'] ?? '' ?>" required> <br>
    <?php if (isset($errors["username"])) : ?>
      <span><?= $errors["username"] ?></span>
    <?php endif ?>
    <br>
    Password: <br>
    <input type="password" name="password" value="<?= $_POST['password'] ?? '' ?>" required> <br>
    <?php if (isset($errors["password"])) : ?>
      <span><?= $errors["password"] ?></span>
    <?php endif ?>
    <br>
    <button>Bejelentkezés</button> <br> <br> <br>
    <button>Regisztráció</button>
  </form>
</body>

</html>