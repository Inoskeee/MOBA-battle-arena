<?php

	$connect = mysqli_connect("localhost", "root", "root", "unityaccess");

	if(mysqli_connect_errno())
	{
		echo "1"; //: Connection failed
		exit();
	}

	$username = $_POST["name"];


	$namecheckquery = "SELECT id, username, hash, salt, email, players.character_id, score, head, body, legs, sword, bow FROM players INNER JOIN characters ON players.character_id = characters.character_id WHERE username = '".$username."';";

	$namecheck = mysqli_query($connect, $namecheckquery) or die("2"); //: Name check query failed

	if(mysqli_num_rows($namecheck) != 1)
	{
		echo "6"; //: No one users with this name
		exit();
	}

	$existinginfo = mysqli_fetch_assoc($namecheck);

	$namecheckquery = "SELECT * FROM player_clans WHERE player_clans.player_id = ".$existinginfo["id"].";";

	$namecheck = mysqli_query($connect, $namecheckquery) or die("2"); //: Name check query failed

	if(mysqli_num_rows($namecheck) != 0)
	{
		echo "8"; //: No one users with this name
		exit();
	}

	echo "0\t".$existinginfo["email"];
	//Если игрок в клане - ошибка №8


?>