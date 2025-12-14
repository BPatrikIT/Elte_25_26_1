<?php
include_once("storage.php");

class UserStorage extends Storage {
  public function __construct() {
    //UserStorage a felhasználókat a users.json fájlban tárolja, JSON formátumban.”
    parent::__construct(new JsonIO('users.json'));
  }
}