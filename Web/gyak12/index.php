<?php
session_start();

include("auth.php");
include("utils.php");
include('userstorage.php');

include_once("contactstorage.php");

$user_storage = new UserStorage();

$auth = new Auth($user_storage);

if (!$auth->is_authenticated()) {
  redirect("login.php");
}

$cs = new ContactStorage();

$contacts = $cs->findMany(function ($contact) {
  return $contact["creator"] === $_SESSION["user"]["id"];
});

//findAll() visszadja az összes kontaktot tömbként
if (!isset($contacts)) {
  $contacts = [];
}
//print_r($contacts);
?>
<!DOCTYPE html>
<html lang="en">

<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Document</title>
</head>

<body>
  <h1>Névjegyek</h1>
  <p>Hello <?= $_SESSION["user"]["fullname"] ?>!
    <a href="logout.php">Kijelentkezés</a>
  </p>
  <h2>Lista</h2>
  <a href="add.php">Új névjegy...</a>
  <table>
    <tr>
      <th>Név</th>
      <th>Email címek</th>
    </tr>
    <?php foreach ($contacts as $c) : ?>
      <tr>
        <td>
          <a href="detail.php?id=<?= $c["id"] ?>">
            <?= $c["name"] ?>
          </a>
        </td>
        <!-- Az implode függvény egy tömb elemeit egyesíti egy stringgé, a megadott elválasztóval, így jeleníti meg a táblázatban, ugyanaz, mint joint js-ben -->
        <td><?= implode(", ", $c["emails"]) ?></td>
      </tr>
    <?php endforeach ?>
  </table>
</body>

</html>